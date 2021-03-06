﻿using System;
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
    public class EmployeesController : Controller
    {
        private IPeopleProRepo p_repo;

        public EmployeesController() : this (new PeopleProRepo()) { } 

        public EmployeesController(IPeopleProRepo newRepo)
        {
            p_repo = newRepo;
        }

        // GET: Employees
        public ActionResult Index()
        {
            var employees = p_repo.GetEmployees().ToList();
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = p_repo.GetEmployee(id.Value);

            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(p_repo.GetDepartments(), "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FName,LName,DepartmentId")] Employee employee)
        {
            if(employee == null)
            {
                return RedirectToAction("Create");
            }
            if (ModelState.IsValid)
            {
                p_repo.SaveEmployee(employee);
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(p_repo.GetDepartments(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = p_repo.GetEmployee(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(p_repo.GetDepartments(), "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FName,LName,DepartmentId")] Employee employee)
        {
            if(employee == null)
            {
                return RedirectToAction("Edit");
            }
            if (ModelState.IsValid)
            {
                p_repo.SaveEmployee(employee);
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(p_repo.GetDepartments(), "Id", "Name", employee.DepartmentId);
            return View(employee);
            /*
            if (ModelState.IsValid)
            {
                p_repo.Entry(employee).State = EntityState.Modified;
                p_repo.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(p_repo.GetDepartments(), "Id", "Name", employee.DepartmentId);
            return View(employee);*/
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = p_repo.GetEmployee(id.Value);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = p_repo.GetEmployee(id);
            p_repo.DeleteEmployee(employee);
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
