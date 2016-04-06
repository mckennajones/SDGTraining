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

        [Required]
        public string FName;
        public string LName;
        public int DepartmentId;

    }

}
