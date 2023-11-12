namespace CompanyEmployees.UnitTests
{
    [TestFixture]
    public class EmployeeTests
    {
        private Repository<Company> _companyRepo;
        private Repository<Employee> _employeeRepo;
        private IEmployeeService _service;

        [Test]
        public void AddEmployeeHappyFlowNoCompanyTest()
        {
            var employeeToCreate = new CreateEmployeeDto() { Email = "dev1@email.com", Title = EmployeeTitle.Developer };
            var expectedEmployee = new EmployeeDto() { Id = 1, Email = "dev1@email.com", Title = EmployeeTitle.Developer };

            var result = _service.CreateEmployee(employeeToCreate).Result;

            Assert.IsFalse(result.HasError());

            var createdEmployee = result.GetResult();

            Assert.AreEqual(expectedEmployee.Id, createdEmployee.Id);
            Assert.AreEqual(expectedEmployee.Email, createdEmployee.Email);
            Assert.AreEqual(expectedEmployee.Title, createdEmployee.Title);
        }

        [Test]
        public void AddEmployeeHappyFlowWithCompanyTest()
        {
            var companyToCreate = new Company() { Name = "Company1" };
            var company = _companyRepo.AddAsync(companyToCreate).Result;

            var employeeToCreate = new CreateEmployeeDto() { Email = "dev1@email.com", Title = EmployeeTitle.Developer, CompanyIds = new int[] { company.Id } };
            var expectedEmployee = new EmployeeDto() { Id = 1, Email = "dev1@email.com", Title = EmployeeTitle.Developer, CompanyIds = new int[] { company.Id } };

            var result = _service.CreateEmployee(employeeToCreate).Result;

            Assert.IsFalse(result.HasError());

            var createdEmployee = result.GetResult();

            Assert.AreEqual(expectedEmployee.Id, createdEmployee.Id);
            Assert.AreEqual(expectedEmployee.Email, createdEmployee.Email);
            Assert.AreEqual(expectedEmployee.Title, createdEmployee.Title);
            Assert.AreEqual(expectedEmployee.CompanyIds[0], createdEmployee.CompanyIds[0]);
        }

        [Test]
        public void AddEmployeeHappyFlowWithMultipleCompaniesTest()
        {
            var company1ToCreate = new Company() { Name = "Company1" };
            var company2ToCreate = new Company() { Name = "Company2" };
            var company1 = _companyRepo.AddAsync(company1ToCreate).Result;
            var company2 = _companyRepo.AddAsync(company2ToCreate).Result;

            var employeeToCreate = new CreateEmployeeDto() { Email = "dev1@email.com", Title = EmployeeTitle.Developer, CompanyIds = new int[] { company1.Id, company2.Id } };
            var expectedEmployee = new EmployeeDto() { Id = 1, Email = "dev1@email.com", Title = EmployeeTitle.Developer, CompanyIds = new int[] { company1.Id, company2.Id } };

            var result = _service.CreateEmployee(employeeToCreate).Result;

            Assert.IsFalse(result.HasError());

            var createdEmployee = result.GetResult();

            Assert.AreEqual(expectedEmployee.Id, createdEmployee.Id);
            Assert.AreEqual(expectedEmployee.Email, createdEmployee.Email);
            Assert.AreEqual(expectedEmployee.Title, createdEmployee.Title);
            Assert.AreEqual(expectedEmployee.CompanyIds.Length, createdEmployee.CompanyIds.Length);
            Assert.AreEqual(expectedEmployee.CompanyIds[0], createdEmployee.CompanyIds[0]);
            Assert.AreEqual(expectedEmployee.CompanyIds[1], createdEmployee.CompanyIds[1]);
        }

        [Test]
        public void AddEmployeeNoEmailTest()
        {
            var employeeToCreate = new CreateEmployeeDto() { Email = string.Empty, Title = EmployeeTitle.Developer };

            var result = _service.CreateEmployee(employeeToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is UniqueIdentifierCannotBeNullOrEmptyError);
        }

        [Test]
        public void AddEmployeeWithNonExitantCompanyTest()
        {
            var employeeToCreate = new CreateEmployeeDto() { Email = "dev1@email.com", Title = EmployeeTitle.Developer, CompanyIds = new int[] { 0 } };

            var result = _service.CreateEmployee(employeeToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is CompanyDoesNotExistError);
        }

        [Test]
        public void AddEmployeeWithSameEmailTest()
        {
            _ = _employeeRepo.AddAsync(new Employee(EmployeeTitle.Developer, "dev1@email.com")).Result;

            var employeeToCreate = new CreateEmployeeDto() { Email = "dev1@email.com", Title = EmployeeTitle.Developer };

            var result = _service.CreateEmployee(employeeToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is EmployeeWithSameEmailAlreadyExistsError);
        }

        [Test]
        public void AddEmployeeWithSameTitleInACompanyTest()
        {
            var companyToCreate = new Company() { Name = "Company1" };
            var company = _companyRepo.AddAsync(companyToCreate).Result;

            var existingEmployee = new Employee(EmployeeTitle.Developer, "dev1@email.com")
            {
                Companies = new List<Company>() { company }
            };
            _ = _employeeRepo.AddAsync(existingEmployee).Result;

            var employeeToCreate = new CreateEmployeeDto() { Email = "dev2@email.com", Title = EmployeeTitle.Developer, CompanyIds = new int[] { company.Id } };
            var expectedEmployee = new EmployeeDto() { Id = 1, Email = "dev2@email.com", Title = EmployeeTitle.Developer, CompanyIds = new int[] { company.Id } };

            var result = _service.CreateEmployee(employeeToCreate).Result;

            Assert.IsTrue(result.HasError());

            var error = result.GetError();

            Assert.IsTrue(error is EmployeeWithSameTitleAlreadyExistsInCompanyError);
        }

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase($"inMemoryDb_{DateTime.Now.ToFileTimeUtc()}")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            var context = new ApplicationDbContext(options);

            _employeeRepo = new Repository<Employee>(context);
            _companyRepo = new Repository<Company>(context);

            _service = new EmployeeService(_employeeRepo, _companyRepo, Mock.Of<ILogger>());
        }
    }
}