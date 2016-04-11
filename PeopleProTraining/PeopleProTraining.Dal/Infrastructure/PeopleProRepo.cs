using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeopleProTraining.Dal.Extensions;
using PeopleProTraining.Dal.Interfaces;
using PeopleProTraining.Dal.Models;

namespace PeopleProTraining.Dal.Infrastructure
{
    public sealed class PeopleProRepo : IPeopleProRepo
    {
        private IPeopleProContext p_context;
        private bool p_disposed;

        public PeopleProRepo() : this(new PeopleProContext()) { }

        public PeopleProRepo(IPeopleProContext context)
        {
            p_context = context;
        }

        #region access

        #region employees
        public IQueryable<Employee> GetEmployees()
        {
            return p_context.Employees;
        }
        public IEnumerable<Employee> GetEmployees(Func<Employee, bool> predicate)
        {
            return p_context.Employees.Where(predicate);
        }

        public Employee GetEmployee(Func<Employee, bool> predicate)
        {
            return GetEmployees().SingleOrDefault(predicate);
        }
        public Employee GetEmployee(int id)
        {
            return GetEmployee(t => t.Id == id);
        }

        public void SaveEmployee(Employee employee)
        {
            DoSave(p_context.Employees, employee, employee.Id, e => e.Id == employee.Id);
        }

        public void DeleteEmployee(Employee employee)
        {
            if (employee == null || employee.Id <= 0)
            {
                return;
            }

            p_context.Employees.Remove(employee);
            p_context.SaveChanges();
        }
        #endregion

        #region departments
        public IQueryable<Department> GetDepartments()
        {
            return p_context.Departments;
        }

        public IEnumerable<Department> GetDepartments(Func<Department, bool> predicate)
        {
            return p_context.Departments.Where(predicate);
        }

        public Department GetDepartment(Func<Department, bool> predicate)
        {
            return GetDepartments().SingleOrDefault(predicate);
        }
        public Department GetDepartment(int id)
        {
            return GetDepartment(t => t.Id == id);
        }

        public void SaveDepartment(Department department)
        {
            DoSave(p_context.Departments, department, department.Id, d => d.Id == department.Id);
        }

        public void DeleteDepartment(Department department)
        {
            if (department == null || department.Id <= 0)
            {
                return;
            }

            p_context.Departments.Remove(department);
            p_context.SaveChanges();
        }
        #endregion

        #region buildings
        public IQueryable<Building> GetBuildings()
        {
            return p_context.Buildings;
        }

        public IEnumerable<Building> GetBuildings(Func<Building, bool> predicate)
        {
            return p_context.Buildings.Where(predicate);
        }

        public Building GetBuilding(Func<Building, bool> predicate)
        {
            return GetBuildings().SingleOrDefault(predicate);
        }
        public Building GetBuilding(int id)
        {
            return GetBuilding(t => t.Id == id);
        }

        public void SaveBuilding(Building building)
        {
            DoSave(p_context.Buildings, building, building.Id, b => b.Id == building.Id);
        }

        public void DeleteBuilding(Building building)
        {
            if (building == null || building.Id <= 0)
            {
                return;
            }

            p_context.Buildings.Remove(building);
            p_context.SaveChanges();
        }

        #endregion

        #endregion



        /// <summary>
        /// Abstracts the saving process for an item in the Db Context.
        /// </summary>
        private void DoSave<T>(IDbSet<T> dbSet, T entity, int entityId, Func<T, bool> predicate) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException(entity.GetType().Name);
            }

            if (entityId <= 0)
            {
                dbSet.Add(entity);
            }
            else
            {
                var old = dbSet.SingleOrDefault(predicate);
                entity.CopyTo(old);
            }

            p_context.SaveChanges();
        }

        #region Disposal
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="isDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031",
            Justification = "Swallows general exceptions, to prevent the service from being disabled.")]
        private void Dispose(bool isDisposing)
        {
            if (!p_disposed)
            {
                try
                {
                    // Called from the IDisposable Method
                    // Nothing happens when isDisposing is false, since we don't have unmanaged resources
                    if (isDisposing)
                    {
                        try
                        {
                            if (p_context != null)
                            {
                                p_context.Dispose();
                            }
                        }
                        catch (Exception)
                        {
                            // This is intended to swallow up any exceptions to prevent the service from being crashing.
                        }
                    }
                }
                finally
                {
                    p_disposed = true;
                }
            }
        }
        #endregion

    }
}
