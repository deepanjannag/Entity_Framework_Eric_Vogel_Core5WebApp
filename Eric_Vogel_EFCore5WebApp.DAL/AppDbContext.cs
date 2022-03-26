using Eric_Vogel_EFCore5WebApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eric_Vogel_EFCore5WebApp.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base()  //Generally used by MVC methods as they use Startup.ConfigureServices() 
        {
        }
        public AppDbContext(DbContextOptions options) : base(options)   //Generally used by Test projects 
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<LookUp> LookUps { get; set; }

        //var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DemoDb;Trusted_Connection= True; MultipleActiveResultSets=true").Options);
        //the above line could be used in any class that needs to use the Db via the dbcontext. Since in MVC apps, StartUp.cs already defines it in ConfigureServices...
        //... , methods in MVC apps generally only need new AppDbContext() constructor without a lengthy parameter

        //The OnModelCreating() method is called when DbContext is created.
        //This method can be used to:
        //1. Configure the database schema (instead of decorating the entities directly)
        //2. Seeding via modelBuilder.Entity<T>.HasData()

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region configuring schema

            #region default schema
            //----modelBuilder.HasDefaultSchema("Custom");
            #endregion

            #region default values
            //to hardcode default values (manual or db generated) when a record is inserted, you can use HasDefaultValue or HasDefaultValueSql
            //----modelBuilder.Entity<Person>().Property(p => p.CreatedOn).HasDefaultValueSql("getdate()");
            //----modelBuilder.Entity<Address>().Property(a => a.Country).HasDefaultValue("USA");
            #endregion

            #region primary key constraint
            //----modelBuilder.Entity<LookUp>().HasKey(c => c.Code);
            #endregion

            #region composite key constraint
            //----modelBuilder.Entity<Person>().HasKey(c => new { c.FirstName, c.LastName });
            #endregion

            #region map entity to class
            //to map entities to specific tables in the DB
            //----modelBuilder.Entity<Shape>().ToTable("Shape");
            //----modelBuilder.Entity<Cube>().ToTable("Cube");
            #endregion

            #region delete behavior
            //Define delete action (cascade(default), restrict, client side null, set null)

            //(cascade) this is the default type of deletion. However, it can also be explicity defined here (in OnModelCreating())
            //----modelBuilder.Entity<Person>(entity => { entity.HasMany(x => x.Addresses).WithOne(x => x.Person).OnDelete(DeleteBehavior.Cascade); });

            //(restrict) this does not allow a parent to be deleted as long as it has children
            //----modelBuilder.Entity<Person>(entity => { entity.HasMany(x => x.Addresses).WithOne(x => x.Person).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Restrict); });

            //(client set null) foreign key (of the child table) is set to null (perhaps manually...not sure) instead of deleting the entire row. The foreign key should be made NULLABLE (like int?)
            //----modelBuilder.Entity<Person>(entity => { entity.HasMany(x => x.Addresses).WithOne(x => x.Person).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.ClientSetNull); });

            //(set null) same as client set null except that the foreign key is automatically set to null when parent record is deleted.
            //----modelBuilder.Entity<Person>(entity => { entity.HasMany(x => x.Addresses).WithOne(x => x.Person).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.SetNull); });

            #endregion

            #endregion

            #region seeding
            modelBuilder.Entity<LookUp>().HasData(new List<LookUp>() {
                new LookUp() { Code = "AL", Description = "Alabama", LookUpType = LookUpType.State },
                new LookUp() { Code = "AK", Description = "Alaska",  LookUpType = LookUpType.State},
                new LookUp() { Code = "AZ", Description = "Arizona", LookUpType = LookUpType.State},
                new LookUp() { Code = "AR", Description = "Arkansas", LookUpType = LookUpType.State},
                new LookUp() { Code = "CA", Description = "California", LookUpType = LookUpType.State},
                new LookUp() { Code = "CO", Description = "Colorado", LookUpType = LookUpType.State},
                new LookUp() { Code = "CT", Description = "Connecticut",LookUpType = LookUpType.State},
                new LookUp() { Code = "DE", Description = "Delaware", LookUpType = LookUpType.State},
                new LookUp() { Code = "DC", Description = "District of Columbia", LookUpType = LookUpType.State},
                new LookUp() { Code = "FL", Description = "Florida", LookUpType = LookUpType.State},
                new LookUp() { Code = "GA", Description = "Georgia", LookUpType = LookUpType.State},
                new LookUp() { Code = "ID", Description = "Hawaii", LookUpType = LookUpType.State},
                new LookUp() { Code = "IL", Description = "Idaho", LookUpType = LookUpType.State},
                new LookUp() { Code = "IN", Description = "Illinois", LookUpType = LookUpType.State},
                new LookUp() { Code = "IA", Description = "Indiana", LookUpType = LookUpType.State},
                new LookUp() { Code = "KS", Description = "Iowa", LookUpType = LookUpType.State},
                new LookUp() { Code = "KY", Description = "Kansas", LookUpType = LookUpType.State},
                new LookUp() { Code = "LA", Description = "Kentucky", LookUpType = LookUpType.State},
                new LookUp() { Code = "ME", Description = "Louisiana", LookUpType = LookUpType.State},
                new LookUp() { Code = "MD", Description = "Maine", LookUpType = LookUpType.State},
                new LookUp() { Code = "MA", Description = "Maryland", LookUpType = LookUpType.State},
                new LookUp() { Code = "MI", Description = "Massachusetts", LookUpType = LookUpType.State},
                new LookUp() { Code = "MN", Description = "Michigan",  LookUpType = LookUpType.State},
                new LookUp() { Code = "MS", Description = "Minnesota", LookUpType = LookUpType.State},
                new LookUp() { Code = "MO", Description = "Mississippi", LookUpType = LookUpType.State},
                new LookUp() { Code = "MT", Description = "Missouri",  LookUpType = LookUpType.State},
                new LookUp() { Code = "NE", Description = "Montana",  LookUpType = LookUpType.State},
                new LookUp() { Code = "NV", Description = "Nevada",  LookUpType = LookUpType.State},
                new LookUp() { Code = "NH", Description = "New Hampshire", LookUpType = LookUpType.State},
                new LookUp() { Code = "NJ", Description = "New Jersey", LookUpType = LookUpType.State},
                new LookUp() { Code = "NM", Description = "New Mexico", LookUpType = LookUpType.State},
                new LookUp() { Code = "NY", Description = "New York", LookUpType = LookUpType.State},
                new LookUp() { Code = "NC", Description = "New Carolina", LookUpType = LookUpType.State},
                new LookUp() { Code = "ND", Description = "North Dakota", LookUpType = LookUpType.State},
                new LookUp() { Code = "OH", Description = "Ohio", LookUpType = LookUpType.State},
                new LookUp() { Code = "OK", Description = "Oklahoma", LookUpType = LookUpType.State},
                new LookUp() { Code = "OR", Description = "Oregon", LookUpType = LookUpType.State},
                new LookUp() { Code = "PA", Description = "Pennsylvania", LookUpType = LookUpType.State},
                new LookUp() { Code = "RI", Description = "Rhode Island", LookUpType = LookUpType.State},
                new LookUp() { Code = "SC", Description = "South Carolina", LookUpType = LookUpType.State},
                new LookUp() { Code = "SD", Description = "South Dakota", LookUpType = LookUpType.State},
                new LookUp() { Code = "TN", Description = "Tennessee", LookUpType = LookUpType.State},
                new LookUp() { Code = "TX", Description = "Texas", LookUpType = LookUpType.State},
                new LookUp() { Code = "UT", Description = "Utah", LookUpType = LookUpType.State},
                new LookUp() { Code = "VT", Description = "Vermont", LookUpType = LookUpType.State},
                new LookUp() { Code = "VA", Description = "Virginia", LookUpType = LookUpType.State},
                new LookUp() { Code = "WA", Description = "Washington", LookUpType = LookUpType.State},
                new LookUp() { Code = "WV", Description = "West Virginia", LookUpType = LookUpType.State},
                new LookUp() { Code = "WI", Description = "Wisconsis", LookUpType = LookUpType.State},
                new LookUp() { Code = "WY", Description = "Wyoming", LookUpType = LookUpType.State},
                new LookUp() { Code = "PR", Description = "Puerto Rico", LookUpType = LookUpType.State},
                new LookUp() { Code = "USA", Description = "United States of America", LookUpType = LookUpType.Country}
            });

            modelBuilder.Entity<Person>().HasData(new List<Person>()
            {
                new Person(){ Id = 1, FirstName = "John", LastName = "Smith", EmailAddress = "john@smith.com", Age = 20},
                new Person(){ Id = 2, FirstName = "Susan", LastName = "Jones", EmailAddress = "john@smith.com" , Age = 30}
            });

            modelBuilder.Entity<Address>().HasData(new List<Address>()
            {
                new Address() { Id = 1, AddressLine1 = "123 Test St", AddressLine2 = "", City = "Beverly Hills", State = "CA", ZipCode = "90210", PersonId = 1, Country = "USA"},
                new Address() { Id = 2, AddressLine1 = "123 Michigan Ave", AddressLine2 = "", City = "Chicago", State = "IL", ZipCode = "60612", PersonId = 2, Country = "USA"},
                new Address() { Id = 3, AddressLine1 = "100 1St St", AddressLine2 = "", City = "Chicago", State = "IL", ZipCode = "60612", PersonId = 2, Country = "USA"}
            });

            //do a migration after this to add the record to the DB. (Add-Migration XYZ, Update-Database)
            #endregion
        }
    }
}
