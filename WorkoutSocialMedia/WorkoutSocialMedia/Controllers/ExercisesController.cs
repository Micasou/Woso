using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkoutSocialMedia.Models;

namespace WorkoutSocialMedia.Controllers
{
    public class ExercisesController : Controller
    {
        private ExerciseDBContext db = new ExerciseDBContext();

        // GET: Exercises
        public ActionResult Index(String sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.TargetSortParm = sortOrder == "Target" ? "Target_desc" : "Target";

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var exer = from s in db.Exercises
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                exer = exer.Where(s => s.Name.Contains(searchString)
                                       || s.Target.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    exer = exer.OrderByDescending(s => s.Name);
                    break;
                case "Target":
                    exer = exer.OrderBy(s => s.Target);
                    break;
              
                default:
                    exer = exer.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 40;
            int pageNumber = (page ?? 1);
            return View(exer.ToPagedList(pageNumber, pageSize));
        }

        // GET: Exercises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // GET: Exercises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExerciseId,Name,Description,Target")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                db.Exercises.Add(exercise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExerciseId,Name,Description,Target,Type,Sub,Equipment,Level")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exercise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exercise exercise = db.Exercises.Find(id);
            if (exercise == null)
            {
                return HttpNotFound();
            }
            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exercise exercise = db.Exercises.Find(id);
            db.Exercises.Remove(exercise);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Explorer(string q)
        {
            var exercises = db.Exercises.Include("Name").Where(a => a.Name.Contains(q)).Take(10);
            return View(exercises);
        }
        public ActionResult WorkoutGenerator(String workoutType, String focus)
        {

            String setRange = "";
            var exercises = db.Exercises.Include("Name").Where(a => a.Name.Contains(focus)).Take(10);
            
            if(workoutType == "Full")
            {

              //  exer = db.Exercises.Include("Name").Where(a => a.Name.Contains(focus)).Take(10);
            }
            else if (workoutType == "Upper")
            {

            }
            else if (workoutType == "Lower")
            {

            }
            if(focus == "Strength")
            {
                setRange = "5 sets of 4-6 reps";
            }
            else if (focus == "Endurance")
            {
                setRange = "3 sets of 8-12 reps";
            }
            else if (focus == "Mixed")
            {
                setRange = "4 sets of 6-8 reps";
            }
            return View(exercises);
        }
    }
}
