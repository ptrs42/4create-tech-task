using System.Globalization;
using System.Text.Json;
using CompanyEmployees.Common.Models;
using CompanyEmployees.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CompanyEmployees.Persistence.Interceptors
{
    /// <summary>
    /// </summary>
    public class AuditingInterceptor : ISaveChangesInterceptor
    {
        private readonly List<SystemLogEntry> _auditEntries = new List<SystemLogEntry>();
        private readonly AuditDbContext _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public AuditingInterceptor(AuditDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            _auditEntries.Clear();
        }

        /// <inheritdoc/>
        public Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            _auditEntries.Clear();

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            UpdateIds(eventData.Context, _auditEntries);

            _context.SystemLogs.AddRange(ToSystemLog(_auditEntries));
            _context.SaveChanges();

            return result;
        }

        /// <inheritdoc/>
        public async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            UpdateIds(eventData.Context, _auditEntries);

            await _context.SystemLogs.AddRangeAsync(ToSystemLog(_auditEntries), cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return result;
        }

        /// <inheritdoc/>
        public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            _auditEntries.AddRange(CreateAuditEntry(eventData.Context));

            return result;
        }

        /// <inheritdoc/>
        public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            _auditEntries.AddRange(CreateAuditEntry(eventData.Context));

            return ValueTask.FromResult(result);
        }

        private static IEnumerable<SystemLogEntry> CreateAuditEntry(DbContext? auditedContext)
        {
            if (auditedContext == null)
            {
                throw new ArgumentNullException(nameof(auditedContext), "Audited context cannot be null.");
            }

            auditedContext.ChangeTracker.DetectChanges();

            var entries = new List<SystemLogEntry>();

            foreach (var entry in auditedContext.ChangeTracker.Entries())
            {
                var eventType = GetEventType(entry);

                if (eventType == EventType.Skip)
                {
                    continue;
                }

                var resourceType = GetResourceType(entry);

                if (resourceType == ResourceType.Skip)
                {
                    continue;
                }

                var createdAt = entry.CurrentValues.GetValue<DateTime>(nameof(AuditableEntity.CreatedAt));

                var uniqueIdentifier = GetUniqueIdentifierValue(entry, resourceType);

                var comment = GetComment(eventType, resourceType, uniqueIdentifier);

                var logEntry = new SystemLogEntry(uniqueIdentifier, resourceType, eventType, createdAt, comment);
                logEntry.Changeset.AddRange(GetChangeset(entry));

                entries.Add(logEntry);
            }

            return entries;
        }

        private static IEnumerable<SystemLogEntryChagesetItem> GetChangeset(EntityEntry entry)
        {
            var properties = new List<SystemLogEntryChagesetItem>();

            foreach (var property in entry.Properties)
            {
                var key = property.Metadata.Name ?? string.Empty;
                var newValue = property.CurrentValue?.ToString() ?? string.Empty;
                var oldValue = property.OriginalValue?.ToString() ?? string.Empty;

                properties.Add(new(key, newValue, oldValue));
            }

            return properties;
        }

        private static string GetComment(EventType eventType, ResourceType resourceType, string uniqueIdentifier)
        {
            var comment = string.Empty;

            var resourceTypeString = resourceType.ToString().ToLowerInvariant();
            if (eventType == EventType.Create)
            {
                comment = string.Format(CultureInfo.InvariantCulture, "New {0} {1} was created.", resourceTypeString, uniqueIdentifier);
            }
            else if (eventType == EventType.Update)
            {
                comment = string.Format(CultureInfo.InvariantCulture, "The {0} {1} was updated.", resourceTypeString, uniqueIdentifier);
            }

            return comment;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0072:Add missing cases", Justification = "When deducing the event type this warning occurs even though there is a default case")]
        private static EventType GetEventType(EntityEntry entry)
        {
            return entry.State switch
            {
                EntityState.Added => EventType.Create,
                EntityState.Modified => EventType.Update,
                _ => EventType.Skip
            };
        }

        private static ResourceType GetResourceType(EntityEntry entry)
        {
            return entry.Entity switch
            {
                Employee => ResourceType.Employee,
                Company => ResourceType.Company,
                _ => ResourceType.Skip,
            };
        }

        private static string GetUniqueIdentifierValue(EntityEntry entry, ResourceType resourceType)
        {
            var uniqueIdentifier = string.Empty;

            if (resourceType == ResourceType.Employee)
            {
                uniqueIdentifier = entry.CurrentValues.GetValue<string>(nameof(Employee.Email));
            }
            else if (resourceType == ResourceType.Company)
            {
                uniqueIdentifier = entry.CurrentValues.GetValue<string>(nameof(Company.Name));
            }

            return uniqueIdentifier;
        }

        private static IEnumerable<SystemLog> ToSystemLog(List<SystemLogEntry> auditEntries)
        {
            return auditEntries.Select(e => new SystemLog(e.ResourceType, e.CreatedAt, e.Event, JsonSerializer.Serialize(e.Changeset), e.Comment));
        }

        private static void UpdateIds(DbContext? auditedContext, List<SystemLogEntry> auditEntries)
        {
            if (auditedContext == null)
            {
                throw new ArgumentNullException(nameof(auditedContext), "Audited context cannot be null.");
            }

            auditedContext.ChangeTracker.DetectChanges();

            foreach (var entry in auditedContext.ChangeTracker.Entries())
            {
                var resourceType = GetResourceType(entry);

                if (resourceType == ResourceType.Skip)
                {
                    continue;
                }

                var uniqueIdentifier = GetUniqueIdentifierValue(entry, resourceType);

                foreach (var auditEntry in auditEntries)
                {
                    if (auditEntry.UniqueIdentifierValue != uniqueIdentifier)
                    {
                        continue;
                    }

                    var idEntry = auditEntry.Changeset.FirstOrDefault(i => i.Key == "Id");

                    if (idEntry == null)
                    {
                        throw new ArgumentException("No ID record.");
                    }

                    idEntry.NewValue = entry.Properties
                    .SingleOrDefault(p => p.Metadata.Name == "Id")?
                    .CurrentValue?
                    .ToString() ?? string.Empty;
                }
            }
        }
    }
}