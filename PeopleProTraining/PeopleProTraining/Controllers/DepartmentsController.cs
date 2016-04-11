using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PeopleProTraining.Dal.Infrastructure;
using PeopleProTraining.Dal.Models;
using PeopleProTraining.Dal.Interfaces;

namespace PeopleProTraining.Controllers
{
    public class DepartmentsController : Controller
    {
        private IPeopleProRepo p_repo;

        public DepartmentsController() : this (new PeopleProRepo()) { }

        public DepartmentsController(IPeopleProRepo newRepo)
        {
            p_repo = newRepo;
        }

        // GET: Departments
        public ActionResult Index()
        {
            var departments = p_repo.GetDepartments().ToList();
            return View(departments);
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Department department = p_repo.GetDepartment(id.Value);

            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.BuildingId = new SelectList(p_repo.GetBuildings(), "Id", "Name");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,RoomNumber,BuildingId")] Department department)
        {
            if(department == null)
            {
                return RedirectToAction("Create");
            }
            if (ModelState.IsValid)
            {
                p_repo.SaveDepartment(department);
                return RedirectToAction("Index");
            }

            ViewBag.BuildingId = new SelectList(p_repo.GetBuildings(), "Id", "Name", department.BuildingId);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = p_repo.GetDepartment(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingId = new SelectList(p_repo.GetBuildings(), "Id", "Name", department.BuildingId);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,RoomNumber,BuildingId")] Department department)
        {
            if (department == null)
            {
                return RedirectToAction("Edit");
            }
            if (ModelState.IsValid)
            {
                p_repo.SaveDepartment(department);
                return RedirectToAction("Index");
            }

            ViewBag.BuildingId = new SelectList(p_repo.GetBuildings(), "Id", "Name", department.BuildingId);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = p_repo.GetDepartment(id.Value);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Department department = p_repo.GetDepartment(id);
            p_repo.DeleteDepartment(department);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                p_repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
