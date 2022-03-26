using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eric_Vogel_EFCore5WebApp.Core.Entities
{
    [Table("Persons", Schema = "dbo")]
    public class Person
    {
        [Key]
        [Column("Person_Id")]
        public int Id { get; set; }


        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar(255")]
        public string FirstName { get; set; }


        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar(255")]
        public string LastName { get; set; }


        [Required]
        public string EmailAddress { get; set; }

        public int Age { get; set; }


        public DateTime CreatedOn { get; set; }



        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";


        //Navigational
        public List<Address> Addresses { get; set; } = new List<Address>();

        //Many-to-Many relationship
        public List<Person> Parents { get; set; } = new List<Person>();
        public List<Person> Children { get; set; } = new List<Person>();


    }
}
