using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace New_CRUD_Operation.Models
{
    public class AvengerModel
    {
        public int SrNo { get; set; }
        [Required]
        public string ANAME { get; set; }
        [EmailAddress][Required]
        public string AEMAILID { get; set; }
        [Required][MinLength(6)]
        public string APASSWORD { get; set; }
        public string CODE { get; set; }

    }
}