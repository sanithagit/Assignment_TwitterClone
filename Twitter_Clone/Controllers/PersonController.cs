using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Twitter_Clone.Data;
using Twitter_Clone.Models;
//adding changes for GIT assignment
namespace Twitter_Clone.Controllers
{
    public class PersonController : Controller
    {
        private Twitter_CloneDBContext db = new Twitter_CloneDBContext(); 

        [Authorize]
        // GET: Person
        public ActionResult Index()
        {
            return View(db.Person.ToList());
        }


        // GET: Person/Login
        public ActionResult Login()
        {
            return View();
        }

        // GET: Person/Create
        public ActionResult Create()
        {
            Person objPerson = new Person();
            objPerson.Active = true;
            return View(objPerson);           
        }

        
        [HttpPost]
        public ActionResult Login(Person person)
        {           
            bool isValid = db.Person.Any(x => x.User_Id == person.User_Id && x.Password == person.Password);
            if (isValid)
            {
                FormsAuthentication.SetAuthCookie(person.User_Id, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Password", "Invalid User details");
            }

             
            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }



        [Authorize]
        // GET: Person/Details/5
        public ActionResult UserFollowingDetails(string id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Person person = db.Person.Find(id);
            //if (person == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(person);

            return View(db.Person.ToList());
        }

        //// GET: Person/Details/5
        //public ActionResult UserFollowingDetails()
        //{
        //    //if (id == null)
        //    //{
        //    //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    //}
        //    //Person person = db.Person.Find(id);
        //    //if (person == null)
        //    //{
        //    //    return HttpNotFound();
        //    //}
        //    //return View(person);

        //    return View(db.Person.ToList());
        //}



        [Authorize]
        // GET: Person/Details/5
        public ActionResult Details( )
        {
            string id = User.Identity.Name;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }



        // POST: Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_Id,Password,FullName,Email,Joined,Active")] Person person)
        {
            //try
            //{
            if (ModelState.IsValid)
            {
                if (db.Person.Any(x => x.User_Id == person.User_Id))
                {
                    ModelState.AddModelError("User_Id", "User Name already exits!! Please try with a new username.");
                    Person objPerson = new Person();
                    objPerson.Active = true;
                    return View(objPerson);                  
                }
                db.Person.Add(person);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            //}
            //catch (Exception ex)
            //{
            //}

            return View();
        }

        [Authorize]
        // GET: Person/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,Password,FullName,Email,Joined,Active")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(person);
        }

        [Authorize]
        // GET: Person/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Person.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        [Authorize]
        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Person person = db.Person.Find(id);
            db.Person.Remove(person);
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
    }
}
