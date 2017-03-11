using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DexterityAPI.Data.IntegrationTests
{
    using Domain;
    using Interfaces;

    // This class is intended to hold integration tests for the DisconnnectedUserRepository which create and target a test instance of a
    // real SQL Server Dexterity database. They will run much slower than unit tests but they will test actual database constraints work as intended.
    [TestClass]
    public class DisconnectedUserRepositoryTests
    {
        private DexterityContext _dexterityContext;
        private IUserRepository _disconnectedUserRepository;

        [TestInitialize]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DexterityTestDB"].ConnectionString);

            _dexterityContext = new DexterityContext(optionsBuilder.Options);
            _disconnectedUserRepository = new DisconnectedUserRepository(_dexterityContext);

            _dexterityContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Teardown()
        {
            _dexterityContext.Database.EnsureDeleted();
        }

        //[TestMethod]
        //public void AddUser_Should()
        //{
        //    var user = new User { Forename = "Monty", Surname = "Mole", EmailAddress = "monty.mole@darkestcaverns.de", IsEnabled = true };
        //    var actual = _disconnectedUserRepository.AddUser(user);
        //}
    }
}
