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
    public class BuildingsController : Controller
    {
        private IPeopleProRepo p_repo;

        public BuildingsController() : this (new PeopleProRepo()) { }

        public BuildingsController(IPeopleProRepo newRepo)
        {
            p_repo = newRepo;
        }
        // GET: Buildings
        public ActionResult Index()
        {
            IEnumerable<Building> buildings = p_repo.GetBuildings();

            if (buildings == null)
            {
                return RedirectToAction("Create");
            }

            // Paginate buildings here, what if there are a bunch? IPagedList<T>
            //var buildings = p_repo.GetBuildings().ToList();
            return View(buildings);
        }

        // GET: Buildings/Details/5
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = p_repo.GetBuilding(id.Value);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // GET: Buildings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address")] Building building)
        {
            if(building == null)
            {
                return RedirectToAction("Create");
            }
            if (ModelState.IsValid)
            {
                p_repo.SaveBuilding(building);
                return RedirectToAction("Index");
            }

            //view bag?

            return View(building);
        }

        [HttpPost]
        public ActionResult CreateAjax(Building building)
        {
            if (building == null)
            {
                return null;
            }
            if (ModelState.IsValid)
            {
                p_repo.SaveBuilding(building);
                return PartialView("_BuildingRow", building);
            }
           
            return PartialView("_BuildingRow", building);

        }
        // GET: Buildings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = p_repo.GetBuilding(id.Value);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address")] Building building)
        {
            if(building == null)
            {
                return RedirectToAction("Edit");
            }
            if (ModelState.IsValid)
            {
                p_repo.SaveBuilding(building);
                return RedirectToAction("Index");
            }
            return View(building);
        }

        // GET: Buildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = p_repo.GetBuilding(id.Value);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Building building = p_repo.GetBuilding(id);
            p_repo.DeleteBuilding(building);
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
