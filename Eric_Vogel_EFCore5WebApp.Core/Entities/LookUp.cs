using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eric_Vogel_EFCore5WebApp.Core.Entities
{

    public class LookUp
    {
        [Key]
        public string Code { get; set; }
        public string Description { get; set; }
        public LookUpType LookUpType { get; set; }
    }

    public enum LookUpType
    {
        State = 0,
        Country = 1
    }
}
