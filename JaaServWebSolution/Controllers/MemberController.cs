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
    public class MemberController : Controller
    {



        public ActionResult Index()
        {

            return View();

        }

    }
}