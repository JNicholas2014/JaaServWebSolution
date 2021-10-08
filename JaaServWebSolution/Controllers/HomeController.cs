using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JaaServWebSolution.Utilities;
using Microsoft.AspNet.Identity;
using JaaServWebSolution.Models;

namespace JaaServWebSolution.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        BlobUtility utility;
        ApplicationDbContext db;
        string accountName = "jaaservstorage";
        string accountKey = "S43oI27z4IZNo3kSC01AU490Z2UDioIRgQRKHuK7kKegEacidsLcl2Z2Ma12wUVOasqw/e4Y/A+sxbrehtW0Vw==";
        public HomeController()
        {
            utility = new BlobUtility(accountName, accountKey);
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            //string loggedInUserId = User.Identity.GetUserId();
            //List<UserContent> userUploads = (from r in db.UserContents where r.UserId == loggedInUserId select r).ToList();
            //ViewBag.PhotoCount = userUploads.Count;
            //return View(userUploads);

            return View();

        }

        public ActionResult UploadContent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadContent(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string ContainerName = "jaaserv"; //hardcoded container name. 
                file = file ?? Request.Files["content to upload"];
                string fileName = Path.GetFileName(file.FileName);
                Stream contentStream = file.InputStream;
                var result = utility.UploadBlob(fileName, ContainerName, contentStream);
                if (result != null)
                {
                    string loggedInUserId = User.Identity.GetUserId();
                    UserContent usercontent = new UserContent();
                    usercontent.Id = new Random().Next().ToString();
                    usercontent.UserId = loggedInUserId;
                    usercontent.ContentUrl = result.Uri.ToString();
                    db.UserContents.Add(usercontent);
                    db.SaveChanges();
                    return RedirectToAction("Member");
                }
                else
                {
                    return RedirectToAction("Member");
                }
            }
            else
            {
                return RedirectToAction("Member");
            }


        }

        public ActionResult DeleteContent(string id)
        {
            UserContent userUpload = db.UserContents.Find(id);
            db.UserContents.Remove(userUpload);
            db.SaveChanges();
            string BlobNameToDelete = userUpload.ContentUrl.Split('/').Last();
            utility.DeleteBlob(BlobNameToDelete, "jaaserv");
            return RedirectToAction("Member");
        }
        public ActionResult Member()
        {

            ViewBag.Message = "Members page";
            string loggedInUserId = User.Identity.GetUserId();
            List<UserContent> userUploads = (from r in db.UserContents where r.UserId == loggedInUserId select r).ToList();
            ViewBag.PhotoCount = userUploads.Count;
            return View(userUploads);
            //return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "What We Are Doing";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us using the links below";

            return View();
        }
    }
}