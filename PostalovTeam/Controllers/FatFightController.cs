using PostalovTeam.Models;
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
        PostalovTeamEntities pte = new PostalovTeamEntities();
        // GET: FatFight
        public ActionResult Index()
        {
            File schedulePhoto = pte.Files.SingleOrDefault(x => x.isScedulePhoto==true);
            return View(schedulePhoto);
        }
    }
}