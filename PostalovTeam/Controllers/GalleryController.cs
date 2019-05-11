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
    public class GalleryController : Controller
    {
        PostalovTeamEntities pte = new PostalovTeamEntities();
        // GET: Gallery
        public ActionResult Index()
        {
            var galleryPhotos = pte.Files.Where(x => x.isGalleryPhoto == true).ToList();
            return View(galleryPhotos);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddPhoto()
        {
            return View();
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
                    Description = file.Description,
                    isGalleryPhoto = true,
                   
                };
                pte.Files.Add(photo);
                pte.SaveChanges();
            }

            return RedirectToAction("Index", "Gallery");
        }
    }
}