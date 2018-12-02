using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        
        public short SignUpFee { get; set; }

        public byte DurationInMonths { get; set; }

        public byte DiscountRate { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // removing magic numbers for comparations if user is able to select membership type
        public static readonly byte Unknown = 0; 
        public static readonly byte PayAsYouGo = 1;
    }
}