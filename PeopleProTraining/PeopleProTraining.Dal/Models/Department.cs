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
        [Display(Name = "Building")]
        public int BuildingId;
        public int Id;
        [Display(Name = "Deparment")]
        [Required]
        public string Name;
        [Display(Name = "Room #")]
        public int RoomNumber;
    }
}
