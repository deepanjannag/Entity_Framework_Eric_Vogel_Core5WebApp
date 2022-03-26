using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eric_Vogel_EFCore5WebApp.Core.Entities
{
    public class Address
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] Use this to disable database autogeneration of identity field values
        public int Id { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        //Navigational
        public int PersonId { get; set; }
        public Person Person { get; set; }      //This is needed if you want to explicitly set cascade delete in OnModelCreating() within DbContext
    }
}
