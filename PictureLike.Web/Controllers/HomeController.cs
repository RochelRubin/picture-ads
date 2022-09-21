using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PictureLike.Data;
using PictureLike.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PictureLike.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public HomeController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
       

        
        public IActionResult Index()
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new PictureRepository(connectionString);
            var vm = new PictureViewModel
            {
                Pictures = repo.GetAll()
            };
            
            return View(vm);
        }
        public IActionResult Upload()
        {
            return View();
        }
        public IActionResult ViewImage(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new PictureRepository(connectionString);
           
            var vm = new PictureViewModel
            {
                Picture = repo.GetById(id),
                AlreadySaw= HttpContext.Session.Get<List<int>>("Ids")
            };
           
            return View(vm);
        }
        [HttpPost]
        public IActionResult Upload(IFormFile imageFile,string title)
        {

            string fileName = $"{Guid.NewGuid()}-{imageFile.FileName}";
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
            using var fs = new FileStream(filePath, FileMode.CreateNew);
            imageFile.CopyTo(fs);
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new PictureRepository(connectionString);
            var picture = new Picture
            {
                Title= fileName,
                Name= title,
                Date=DateTime.Now,
                Likes=0
            };
            repo.Add(picture);
           
            return Redirect("/");
        }
        public IActionResult GetLikes(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new PictureRepository(connectionString);
            int count = repo.GetLikes(id);
            return Json(count);
        }
        
        public void LikeImage(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new PictureRepository(connectionString);
            repo.LikeIt(id);
            List<int> imagesLiked = HttpContext.Session.Get<List<int>>("Ids");
            if (imagesLiked == null)
            {
                imagesLiked = new List<int>();
            }
            imagesLiked.Add(id);
            HttpContext.Session.Set("Ids", imagesLiked);
        }



    }
}
