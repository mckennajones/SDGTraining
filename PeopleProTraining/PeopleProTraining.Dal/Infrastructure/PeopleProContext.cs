using PeopleProTraining.Dal.Interfaces;
using PeopleProTraining.Dal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleProTraining.Dal.Infrastructure
{
    public class PeopleProContext : DbContext, IPeopleProContext
    {
        public PeopleProContext() : base("PeopleProTrainingDB") {
            // There's probably a better way to do this...
            var ensureDLLIsCopied =
                System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public IDbSet<Employee> Employees { get; set; }

        public IDbSet<Department> Departments { get; set; }

        public IDbSet<Building> Buildings { get; set; }

    }
}
