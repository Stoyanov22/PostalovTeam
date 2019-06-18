using PostalovTeam.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
            //return View();
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

            SendEmail();
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

        public void SendEmail()
        {
            var dbContext = new PostalovTeamDbContext();
            List<String> userEmails = dbContext.Users.Select(x => x.UserName).ToList();
            foreach(String email in userEmails)
            {
                SmtpClient smtpClient = new SmtpClient("neko_gig@abv.bg", 25);

                smtpClient.Credentials = new System.Net.NetworkCredential("neko_gig@abv.bg", "password");
                smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;
                MailMessage mail = new MailMessage();

                //Setting From , To and CC
                mail.From = new MailAddress("neko_gig@abv.bg", "Postalov Team");
                mail.To.Add(new MailAddress(email));
                mail.Body = "Има промяна в графика. Моля проверете!";

                //smtpClient.Send(mail);
            }
        }
    }
}