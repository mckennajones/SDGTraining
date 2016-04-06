using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleProTraining.Dal.Models
{
    [MetadataType(typeof(DepartmentMetaData))]
    public partial class Department
    {
    }
    public class DepartmentMetaData
    {
        public int Id;

        [Required]
        public string Name;
        public int RoomNumber;
    }
}
