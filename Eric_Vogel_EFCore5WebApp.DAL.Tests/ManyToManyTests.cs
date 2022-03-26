using Eric_Vogel_EFCore5WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eric_Vogel_EFCore5WebApp.DAL.Tests
{
    [TestFixture]
    public class ManyToManyTests
    {
        private AppDbContext _context;
        private List<Person> _family;

        [SetUp]
        public void SetUp()
        {
            _context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=one.deepanjannag.net;Database=Eric_Vogel_EFCore5WebAppDB;User Id=sa;Password=TmaORZ5!;MultipleActiveResultSets=true").Options);

            _family = new List<Person>();

            var parent1 = new Person()
            {
                FirstName = "Clarke",
                LastName = "Kent",
                EmailAddress = "clark@daileybugel.com",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "1234 Fake Street",
                        AddressLine2 = "Suite 1",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60652",
                        Country = "United States"
                    },
                }
            };
            var parent2 = new Person()
            {
                FirstName = "Lois",
                LastName = "Lane",
                EmailAddress = "Lois@daileybugel.com",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "1234 Fake Street",
                        AddressLine2 = "Suite 1",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60652",
                        Country = "United States"
                    },
                }
            };
            var child1 = new Person()
            {
                FirstName = "David",
                LastName = "Kent",
                EmailAddress = "Lois@daileybugel.com",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "1234 Fake Street",
                        AddressLine2 = "Suite 1",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60652",
                        Country = "United States"
                    },
                }
            };
            var child2 = new Person()
            {
                FirstName = "Anna",
                LastName = "Kent",
                EmailAddress = "Lois@daileybugel.com",
                Addresses = new List<Address>
                {
                    new Address
                    {
                        AddressLine1 = "1234 Fake Street",
                        AddressLine2 = "Suite 1",
                        City = "Chicago",
                        State = "IL",
                        ZipCode = "60652",
                        Country = "United States"
                    },
                }
            };

            _context.Persons.AddRange(parent1, parent2, child1, child2);
            _family.AddRange(new List<Person> { parent1, parent2, child1, child2 });

            parent1.Children.AddRange(new List<Person> { child1, child2 });
            parent2.Children.AddRange(new List<Person> { child1, child2 });

            child1.Parents.AddRange(new List<Person> { parent1, parent2 });
            child2.Parents.AddRange(new List<Person> { parent1, parent2 });

            _context.SaveChanges();
        }

        [Test]
        public void GetParentsFromChildren()
        {
            var daughter = _family.Single(x => x.FirstName == "Anna");
            var son = _family.Single(x => x.FirstName == "David");

            Assert.AreEqual(2, daughter.Parents.Count);
            Assert.AreEqual(2, son.Parents.Count);
        }

        [Test]
        public void GetChildrenFromParents()
        {
            var mother = _family.Single(x => x.FirstName == "Lois");
            var father = _family.Single(x => x.FirstName == "Clarke");
            Assert.AreEqual(2, mother.Children.Count);
            Assert.AreEqual(2, father.Children.Count);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Persons.RemoveRange(_family);
            _context.SaveChanges();
        }
    }
}
