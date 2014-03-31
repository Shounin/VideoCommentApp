using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoBlogApplication.Models;
using VideoCommentApp.Models;

namespace VideoCommentApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var model = CommentRepository.Instance.GetComments();
            return View(model);
        }

        [HttpPost]
        //We decided this thing was made of lies, we then tore it appart and used the useful bits.
        public ActionResult Index(FormCollection formData)
        {
            String strComment = formData["CommentText"];
            if (!String.IsNullOrEmpty(strComment))
            {
                Comment c = new Comment();

                c.CommentText = strComment;
                String strUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                if (!String.IsNullOrEmpty(strUser))
                {
                    int slashPos = strUser.IndexOf("\\");
                    if (slashPos != -1)
                    {
                        strUser = strUser.Substring(slashPos + 1);
                    }
                    c.Username = strUser;

                    CommentRepository.Instance.AddComment(c);
                }
                else
                {
                    c.Username = "Unknown user";
                }
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("CommentText", "Comment text cannot be empty!");
                return Index();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [HttpGet]
        //this fellow gets used a lot.  it gets the comment list, modifies some items in it and returns it as a json object
        public ActionResult GetComments()
        {
            String strUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (!String.IsNullOrEmpty(strUser))
            {
                int slashPos = strUser.IndexOf("\\");
                if (slashPos != -1)
                {
                    strUser = strUser.Substring(slashPos + 1);
                }
            }
            else
            {
                strUser = "Unknown user";
            }
            var model = CommentRepository.Instance.GetComments();
            var fiddledmodel = from c in model
                               select new
                               {
                                   CommentDate = c.CommentDate.ToLongDateString() + " at " +  c.CommentDate.ToShortTimeString(),
                                   ID = c.ID,
                                   CommentText = c.CommentText,
                                   Username = c.Username,
                                   Likes = c.Likes,
                                   LStatus = CommentRepository.Instance.HasLiked(strUser, c.ID)
                               };
            return Json(fiddledmodel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(Comment com)
        {
            if (!String.IsNullOrEmpty(com.CommentText))
            {
                String strUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                if (!String.IsNullOrEmpty(strUser))
                {
                    int slashPos = strUser.IndexOf("\\");
                    if (slashPos != -1)
                    {
                        strUser = strUser.Substring(slashPos + 1);
                    }
                    com.Username = strUser;

                    CommentRepository.Instance.AddComment(com);
                }
                else
                {
                    com.Username = "Unknown user";
                }
                return Json(com, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ModelState.AddModelError("CommentText", "Comment text cannot be empty!"); //I don't think this ever gets called but you can't be too safe
                return Index();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        //Calls the likemanip function with the aquired username.
        public ActionResult ChangeLikes(Like li)
        {
            String strUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (!String.IsNullOrEmpty(strUser))
            {
                int slashPos = strUser.IndexOf("\\");
                if (slashPos != -1)
                {
                    strUser = strUser.Substring(slashPos + 1);
                }
                li.Username = strUser;
            }
            else
            {
                li.Username = "Unknown user";
            }
            CommentRepository.Instance.LikeManip(li);
            //Not sure why i have to do this but it was the only way i could figure out for it to work =<
            return Json(li, JsonRequestBehavior.AllowGet);
        }
    }
}