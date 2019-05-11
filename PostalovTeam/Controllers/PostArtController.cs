using PostalovTeam.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PostalovTeam.Controllers
{
    [AllowAnonymous]
    public class PostArtController : Controller
    {
        PostalovTeamEntities pte = new PostalovTeamEntities();
        // GET: PostArt
        public ActionResult Index()
        {
            Models.File schedulePhoto = pte.Files.SingleOrDefault(x => x.isScedulePhoto == true);
            return View(schedulePhoto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddPhoto(Models.File file)
        {
            if (ModelState.IsValid)
            {
                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(Request.Files["photo"].InputStream))
                {
                    fileData = binaryReader.ReadBytes(Request.Files["photo"].ContentLength);
                }
                var photo = new PostalovTeam.Models.File
                {
                    Content = fileData,
                    isScedulePhoto = true,
                };
                pte.Files.Add(photo);
                pte.SaveChanges();
            }

            Models.File oldPhoto = pte.Files.OrderBy(x => x.FileId).FirstOrDefault(x => x.isScedulePhoto == true);
            pte.Files.Remove(oldPhoto);
            pte.SaveChanges();

            return RedirectToAction("Index", "PostArt");
        }

        public ActionResult DanceTheater()
        {
            return View();
        }

        public ActionResult TheaterSchool()
        {
            return View();
        }

        public ActionResult KidsDanceSchool()
        {
            return View();
        }

        public ActionResult BulgarianDanceClub()
        {
            return View();
        }

        public ActionResult KidsVocalSchool()
        {
            return View();
        }

        public ActionResult Handmade()
        {
            return View();
        }
    }
}