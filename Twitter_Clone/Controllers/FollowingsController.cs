using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Mvc;
using Twitter_Clone.Data;
using Twitter_Clone.Models;

namespace Twitter_Clone.Controllers
{
    [Authorize]
    public class FollowingsController : Controller
    {
        private Twitter_CloneDBContext db = new Twitter_CloneDBContext();

        // GET: Followings
        public ActionResult Index()
        {
            UserFollowing objP = new UserFollowing();
            objP.FollowingLst = new List<Person>();
            List<Following> obj = new List<Following>();

            //objP.user
            obj = db.Following.Where(x => x.User_Id == User.Identity.Name ).ToList();
            int i = 0;
            //Person p = new Person();
            foreach (var item in obj)
            {                
                objP.FollowingLst.Add((Person)db.Person.Where(f => f.User_Id == item.Following_Id & f.Active).SingleOrDefault());                
                i++;
            }
            return View(objP);
        }

        public ActionResult Followers()
        {
            UserFollowing objP = new UserFollowing();
            objP.FollowingLst = new List<Person>();
            List<Following> obj = new List<Following>();

            //objP.user
            obj = db.Following.Where(x => x.Following_Id == User.Identity.Name).ToList();
            int i = 0;
            //Person p = new Person();
            foreach (var item in obj)
            {
                objP.FollowingLst.Add((Person)db.Person.Where(f => f.User_Id == item.User_Id & f.Active).SingleOrDefault());
                i++;
            }
            return View("Followers",objP);
        }

        // POST: Person/Delete/5    
        public ActionResult UnFollow(string id)
        {
            Following objF = db.Following.Where(f => f.User_Id == User.Identity.Name && f.Following_Id==id).SingleOrDefault();
            db.Following.Remove(objF);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Followings/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Following following = db.Following.Find(id);
            if (following == null)
            {
                return HttpNotFound();
            }
            return View(following);
        }

        // GET: Followings/Create
        public ActionResult Create()
        {
           // var following = db.Following.Include(f => f.Follower).Include(f => f.User);
            return View();

            //ViewBag.User_Id = User.Identity.Name;
           // return View();
        }

        // POST: Followings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string txtName)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Following.Add(following);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.Following_Id = new SelectList(db.Person, "User_Id", "Password", following.Following_Id);
            //ViewBag.User_Id = new SelectList(db.Person, "User_Id", "Password", following.User_Id);
            UserFollowing objP = new UserFollowing();
            if (txtName != User.Identity.Name)
            {
                bool isValid = db.Person.Any(x => x.User_Id == txtName);
               
                if (isValid)
                {
                    bool isFollowingALready = db.Following.Any(f => f.User_Id == User.Identity.Name & f.Following_Id == txtName);
                    if (!isFollowingALready)
                    {
                        objP.FollowingLst = (db.Person.Where(x => x.User_Id == txtName & x.Active).ToList());
                    }
                }
                else if (txtName == string.Empty)
                {
                    objP.FollowingLst = (db.Person.Where(x => x.User_Id != "" & x.User_Id != User.Identity.Name & x.Active).ToList());
                    foreach (var item in objP.FollowingLst.ToList())
                    {
                        bool isFollowingALready = db.Following.Any(f => f.User_Id == User.Identity.Name & f.Following_Id == item.User_Id);
                        if (isFollowingALready)
                        {
                            objP.FollowingLst.Remove(item);
                        }
                    }
                }
            }
            return View(objP);
        }

        // GET: Followings/Edit/5
        public ActionResult Follow(string id)
        {
            try
            {
                Following obj = new Following();
                obj.Following_Id = id;
                obj.User_Id = User.Identity.Name;
                if (ModelState.IsValid)
                {
                    db.Following.Add(obj);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                
            }

            return RedirectToAction("Create");
        }

        // POST: Followings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_Id,Following_Id")] Following following)
        {
            if (ModelState.IsValid)
            {
                db.Entry(following).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Following_Id = new SelectList(db.Person, "User_Id", "Password", following.Following_Id);
            ViewBag.User_Id = new SelectList(db.Person, "User_Id", "Password", following.User_Id);
            return View(following);
        }

      

        // POST: Followings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Following following = db.Following.Find(id);
            db.Following.Remove(following);
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

    [Serializable]
    internal class DuplicateKeyException : Exception
    {
        public DuplicateKeyException()
        {
        }

        public DuplicateKeyException(string message) : base(message)
        {
        }

        public DuplicateKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
