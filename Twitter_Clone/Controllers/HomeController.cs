using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter_Clone.Data;
using Twitter_Clone.Models;

namespace Twitter_Clone.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private Twitter_CloneDBContext db = new Twitter_CloneDBContext();
        private object viewModel;

        public ActionResult Index()
        {
            Tweet obj = new Tweet();
            obj.TweetList = db.Tweet.ToList();
            foreach(var item in obj.TweetList.ToList())
            {
                if ((item.User_Id == User.Identity.Name) || (db.Following.Any(x => x.User_Id == User.Identity.Name & x.Following_Id == item.User_Id)))
                {
                    continue;
                }
                else
                {
                    obj.TweetList.Remove(item);
                }
            }
            return View(obj);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult AddTweet([Bind(Include = "User_Id,Message,Created")] Tweet obj)
        {
            var user = User.Identity.Name;
            //user = "1";
            obj.User_Id = user;
            obj.Created = DateTime.Now;
            db.Tweet.Add(obj);
            db.SaveChanges();

            //db.tTweetsList = db.Tweet.ToList();

            return RedirectToAction("Index", "Home");
        }


    }
}