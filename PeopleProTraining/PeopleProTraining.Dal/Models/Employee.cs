using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleProTraining.Dal.Models
{
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    {
    }
    public class EmployeeMetaData
    {
        public int Id;
        [Display(Name = "First Name")]
        [Required]
        public string FName;
        [Display(Name = "Last Name")]
        public string LName;
        [Display(Name = "Department")]
        public int DepartmentId;

    }

}
