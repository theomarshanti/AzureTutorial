using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using PsHelloAzureApp.Services;
using PsHelloAzureApp.Models;

namespace PsHelloAzureApp.Controllers
{
    [Route("[controller]/[action]")]
    public class ImagesController : Controller
    {
        ImageStore imageStore;

        public ImagesController(ImageStore _imageStore)
        {
            this.imageStore = _imageStore;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile image)
        {
            if(image!=null)
            {
                using (var stream = image.OpenReadStream())
                {
                    var imageId = await imageStore.SaveImage(stream);
                    return RedirectToAction("Show", new { imageId });
                }
            }
            return View();
        }

        [HttpGet("{imageId}")]
        public ActionResult Show(string imageId)
        {
            var model = new ShowModel { Uri = imageStore.UriFor(imageId) };
            return View(model);
        }
    }
}