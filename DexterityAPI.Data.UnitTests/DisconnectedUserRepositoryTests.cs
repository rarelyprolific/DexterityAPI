using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DexterityAPI.Data.UnitTests
{
    using Domain;
    using Interfaces;

    [TestClass]
    public class DisconnectedUserRepositoryTests
    {
        private DexterityContext _dexterityContext;
        private IUserRepository _disconnectedUserRepository;

        [TestInitialize]
        public void Setup()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase();

            _dexterityContext = new DexterityContext(optionsBuilder.Options);
            _disconnectedUserRepository = new DisconnectedUserRepository(_dexterityContext);

            _dexterityContext.Database.EnsureDeleted();

            IEnumerable<User> users = new List<User>
            {
                new User { Forename = "Guybrush", Surname = "Threepwood", EmailAddress = "mightypirate@monkeyisland.bs", IsEnabled = false },
                new User { Forename = "Master", Surname = "Chief", EmailAddress = "john-117@haloreach.space", IsEnabled = true },
                new User { Forename = "Gordon", Surname = "Freeman", EmailAddress = "g.freeman@city17.com", IsEnabled = true },
                new User { Forename = "Nathan", Surname = "Drake", EmailAddress = "nathan.drake@unchartedlocation.es", IsEnabled = false },
                new User { Forename = "Lara", Surname = "Croft", EmailAddress = "lara@croftadventures.co.uk", IsEnabled = true }
            };
            _dexterityContext.Users.AddRange(users);

            _dexterityContext.SaveChanges();
        }

        [TestMethod]
        public void GetUsers_ShouldReturnFiveUsers()
        {
            var actual = _disconnectedUserRepository.GetUsers();
            Assert.AreEqual(5, actual.ToList().Count);
        }

        [TestMethod]
        public void AddUser_WhenPassedANewUser_ShouldAddASixthUser()
        {
            var user = new User { Forename = "Monty", Surname = "Mole", EmailAddress = "monty.mole@darkestcaverns.de", IsEnabled = true };

            var actual = _disconnectedUserRepository.AddUser(user);

            Assert.AreEqual(6, _dexterityContext.Users.Count());
        }
    }
}
