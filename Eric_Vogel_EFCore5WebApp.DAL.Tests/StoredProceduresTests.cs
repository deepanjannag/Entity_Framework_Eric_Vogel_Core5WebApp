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
    public class StoredProceduresTests
    {
        private AppDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=one.deepanjannag.net;Database=Eric_Vogel_EFCore5WebAppDB;User Id=sa;Password=TmaORZ5!;MultipleActiveResultSets=true").Options);
        }

        [Test]
        public void GetPersonsByStateInterpolated()
        {
            //FromSqlInterpolated used for querying SP
            string state = "IL";
            var persons = _context.Persons.FromSqlInterpolated($"GetPersonsByState {state}").ToList();
            Assert.AreEqual(2, persons.Count);
        }

        [Test]
        public void GetPersonsByStateRaw()
        {
            //FromSqlRaw used for querying SP
            string state = "IL";
            var persons = _context.Persons.FromSqlRaw($"GetPersonsByState @p0", new[] { state }).ToList();
            Assert.AreEqual(2, persons.Count);
        }

        [Test]
        public void AddLookUpItemInterpolated()
        {
            //ExecuteSqlInterpolated used for executing non-querying SP
            string code = "CAN";
            string description = "Canada";
            LookUpType lookUpType = LookUpType.Country;
            _context.Database.ExecuteSqlInterpolated($"AddLookUpItem {code},{ description}, { lookUpType}");
            var addedItem = _context.LookUps.Single(x => x.Code == "CAN");
            Assert.IsNotNull(addedItem);
            _context.LookUps.Remove(addedItem);
            _context.SaveChanges();
        }

        [Test]
        public void AddLookUpItemRaw()
        {
            //ExecuteSqlRaw used for executing non-querying SP
            string code = "MEX";
            string description = "Mexico";
            LookUpType lookUpType = LookUpType.Country;
            _context.Database.ExecuteSqlRaw("AddLookUpItem @p0,@p1,@p2",
            new object[] { code, description, lookUpType });
            var addedItem = _context.LookUps.Single(x => x.Code == "MEX");
            Assert.IsNotNull(addedItem);
            _context.LookUps.Remove(addedItem);
            _context.SaveChanges();
        }
    }
}
