using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PostalovTeam.Controllers
{
    [AllowAnonymous]
    public class FatFightController : Controller
    {
        // GET: FatFight
        public ActionResult Index()
        {
            return View();
        }
    }
}