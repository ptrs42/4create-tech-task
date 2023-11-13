using CompanyEmployees.Core;
using CompanyEmployees.Persistence.Interceptors;

namespace CompanyEmployees.UnitTests
{
    [TestFixture]
    public class CompanyTests
    {
        private Repository<Company> _companyRepo;
        private Repository<Employee> _employeeRepo;
        private ICompanyService _service;

        [Test]
        public void AddCompanyAndEmployeeWithBothEmailandIdTest()
        {
            var companyToCreate = new CreateCompanyDto() { Name = "Company1", Employees = new EmployeeInCreateCompanyDto[] { new() { Title = EmployeeTitle.Developer, Email = "dev1@email.com", Id = 1 } } };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is CannotCreateEmployeeWithIdError);
        }

        [Test]
        public void AddCompanyAndEmployeeWithNoEmailTest()
        {
            var companyToCreate = new CreateCompanyDto() { Name = "Company1", Employees = new EmployeeInCreateCompanyDto[] { new() { Title = EmployeeTitle.Developer, Email = string.Empty } } };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is UniqueIdentifierCannotBeNullOrEmptyError);
        }

        [Test]
        public void AddCompanyAndEmployeeWithSameEmailTest()
        {
            _ = _employeeRepo.AddAsync(new Employee(EmployeeTitle.Developer, "dev1@email.com")).Result;

            var companyToCreate = new CreateCompanyDto() { Name = "Company1", Employees = new EmployeeInCreateCompanyDto[] { new() { Title = EmployeeTitle.Developer, Email = "dev1@email.com" } } };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is EmployeeWithSameEmailAlreadyExistsError);
        }

        [Test]
        public void AddCompanyHappyFlowExistingEmployeeTest()
        {
            var employee = _employeeRepo.AddAsync(new Employee(EmployeeTitle.Developer, "dev1@email.com")).Result;

            var companyToCreate = new CreateCompanyDto() { Name = "Company1", Employees = new EmployeeInCreateCompanyDto[] { new() { Id = employee.Id } } };
            var expectedCompany = new CompanyDto() { Id = 1, Name = "Company1" };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsFalse(result.HasError());

            var createdCompany = result.GetResult();

            Assert.AreEqual(expectedCompany.Id, createdCompany.Id);
            Assert.AreEqual(expectedCompany.Name, createdCompany.Name);

            Assert.AreEqual(1, createdCompany.Employees?.Length);
            Assert.AreEqual(employee.Id, createdCompany.Employees?[0].Id);
            Assert.AreEqual(employee.Email, createdCompany.Employees?[0].Email);
            Assert.AreEqual(employee.Title, createdCompany.Employees?[0].Title);
        }

        [Test]
        public void AddCompanyHappyFlowMultipleEmployeesOneExistsTest()
        {
            var employee = _employeeRepo.AddAsync(new Employee(EmployeeTitle.Developer, "dev1@email.com")).Result;

            var companyToCreate = new CreateCompanyDto() { Name = "Company1", Employees = new EmployeeInCreateCompanyDto[] { new() { Id = employee.Id }, new() { Title = EmployeeTitle.Manager, Email = "mng1@email.com" } } };
            var expectedCompany = new CompanyDto() { Id = 1, Name = "Company1" };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsFalse(result.HasError());

            var createdCompany = result.GetResult();

            Assert.AreEqual(expectedCompany.Id, createdCompany.Id);
            Assert.AreEqual(expectedCompany.Name, createdCompany.Name);

            Assert.AreEqual(2, createdCompany.Employees?.Length);
        }

        [Test]
        public void AddCompanyHappyFlowNewEmployeeTest()
        {
            var companyToCreate = new CreateCompanyDto() { Name = "Company1", Employees = new EmployeeInCreateCompanyDto[] { new() { Title = EmployeeTitle.Developer, Email = "dev1@email.com" } } };
            var expectedCompany = new CompanyDto() { Id = 1, Name = "Company1" };
            var expectedEmployee = new EmployeeDto() { Id = 1, Title = EmployeeTitle.Developer, Email = "dev1@email.com" };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsFalse(result.HasError());

            var createdCompany = result.GetResult();

            Assert.AreEqual(expectedCompany.Id, createdCompany.Id);
            Assert.AreEqual(expectedCompany.Name, createdCompany.Name);

            Assert.AreEqual(1, createdCompany.Employees?.Length);
            Assert.AreEqual(expectedEmployee.Id, createdCompany.Employees?[0].Id);
            Assert.AreEqual(expectedEmployee.Email, createdCompany.Employees?[0].Email);
            Assert.AreEqual(expectedEmployee.Title, createdCompany.Employees?[0].Title);
        }

        [Test]
        public void AddCompanyHappyFlowNoEmployeesTest()
        {
            var companyToCreate = new CreateCompanyDto() { Name = "Company1" };
            var expectedCompany = new CompanyDto() { Id = 1, Name = "Company1" };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsFalse(result.HasError());

            var createdCompany = result.GetResult();

            Assert.AreEqual(expectedCompany.Id, createdCompany.Id);
            Assert.AreEqual(expectedCompany.Name, createdCompany.Name);
        }

        [Test]
        public void AddCompanyMultipleEmployeesWithSameEmailTest()
        {
            var companyToCreate = new CreateCompanyDto() { Name = "Company1", Employees = new EmployeeInCreateCompanyDto[] { new() { Title = EmployeeTitle.Developer, Email = "dev1@email.com" }, new() { Title = EmployeeTitle.Manager, Email = "dev1@email.com" } } };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is CannotCreateEmployeesWithSameEmailError);
        }

        [Test]
        public void AddCompanyMultipleEmployeesWithSameTitleTest()
        {
            var companyToCreate = new CreateCompanyDto() { Name = "Company1", Employees = new EmployeeInCreateCompanyDto[] { new() { Title = EmployeeTitle.Manager, Email = "mng1@email.com" }, new() { Title = EmployeeTitle.Manager, Email = "mng2@email.com" } } };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is CannotCreateEmployeesWithSameTitleInCompanyError);
        }

        [Test]
        public void AddCompanyWithNoNameTest()
        {
            var companyToCreate = new CreateCompanyDto() { Name = string.Empty };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is UniqueIdentifierCannotBeNullOrEmptyError);
        }

        [Test]
        public void AddCompanyWithNonExistantEmployeeTest()
        {
            var companyToCreate = new CreateCompanyDto() { Name = "Company1", Employees = new EmployeeInCreateCompanyDto[] { new() { Id = 0 } } };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is EmployeeDoesNotExistError);
        }

        [Test]
        public void AddCompanyWithSameNameTest()
        {
            _ = _companyRepo.AddAsync(new Company("Company1"));

            var companyToCreate = new CreateCompanyDto() { Name = "Company1" };

            var result = _service.CreateCompany(companyToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is CompanyWithSameNameAlreadyExistsError);
        }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"inMemoryDb_{DateTime.Now.ToFileTimeUtc()}")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var auditingOptions = new DbContextOptionsBuilder<AuditDbContext>()
                .UseInMemoryDatabase($"inMemoryDb_{DateTime.Now.ToFileTimeUtc()}")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var context = new ApplicationDbContext(options, new AuditingInterceptor(new AuditDbContext(auditingOptions)));

            _companyRepo = new Repository<Company>(context);
            _employeeRepo = new Repository<Employee>(context);

            _service = new CompanyService(_companyRepo, _employeeRepo, Mock.Of<ILogger<CompanyService>>());
        }
    }
}