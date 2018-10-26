using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PsHelloAzureApp.Models;
using PsHelloAzureApp.Services;

namespace PsHelloAzureApp.Controllers
{
    public class CoursesController : Controller
    {
        CourseStore courseStore;

        public CoursesController(CourseStore _courseStore)
        {
            this.courseStore = _courseStore;
        }

        public IActionResult Index()
        {
            var model = courseStore.GetAllCourses();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Insert()
        {
            var data = new SampleData().GetCourses();
            await courseStore.InsertCourses(data);
            return RedirectToAction(nameof(Index));
        }
    }
}