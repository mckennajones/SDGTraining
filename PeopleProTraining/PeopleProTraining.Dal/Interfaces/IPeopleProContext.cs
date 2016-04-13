using PeopleProTraining.Dal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleProTraining.Dal.Interfaces
{
    public interface IPeopleProContext : IDisposable
    {
        IDbSet<Employee> Employees { get; set; }
        IDbSet<Department> Departments { get; set; }
        IDbSet<Building> Buildings { get; set; }
        //IQueryable<Department> Departments { get; set; }

        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }
}
