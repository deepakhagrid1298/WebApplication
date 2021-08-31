using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class Project
    {
            [Display(Name = "Id")]
            public int ProId { get; set; }
            [Required(ErrorMessage = "Project name is required.")]
            public string ProjectName { get; set; }
            [Required(ErrorMessage = "Start Date is required.")]
            public DateTime StartDate { get; set; }
            [Required(ErrorMessage = "End Date is required.")]
            public DateTime EndDate { get; set; }
            public decimal Budget { get; set; }
        

    }
}