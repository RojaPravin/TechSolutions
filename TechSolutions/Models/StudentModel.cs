using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TechSolutions.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string course { get; set; }
        [Required]
        public int fees { get; set; }
        [Required]
        [DisplayName("Paid Amount")]
        public string payment { get; set; }
        [Required]
        public string profile { get; set; }

        public HttpPostedFileBase Temp_Profile { get; set; }



    }
}