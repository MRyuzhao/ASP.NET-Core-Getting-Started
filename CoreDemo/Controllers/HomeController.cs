using System;
using System.Linq;
using CoreDemo.Models;
using CoreDemo.Services;
using CoreDemo.Settings;
using CoreDemo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CoreDemo.Controllers
{
    //mvc Controller是接受请求，构建model，渲染view
    public class HomeController : Controller
    {
        private readonly IRepository<Student> _repository;
        private readonly IOptions<ConnectionOptions> _options;

        public HomeController(IRepository<Student> repository,IOptions<ConnectionOptions> options)
        {
            _repository = repository;
            _options = options;
        }

        public IActionResult Index()
        {
            var students = _repository.GetAll();
            var studentsViewModel = students.Select(x => new StudentViewModel
            {
                Name = $"{x.FirstName}.{x.LastName}",
                Id = x.Id,
                Age = DateTime.Now.Subtract(x.Birthday).Days / 365
            });
            var vm = new HomeIndexViewModel
            {
                Students = studentsViewModel
            };
            return View(vm);
        }

        public IActionResult Detail(int id)
        {
            var student = _repository.GetById(id);

            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]//验证post token
        public IActionResult Create(StudentAddModel student)
        {
            if (ModelState.IsValid)
            {
                var newStudent = new Student
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Birthday = student.Birthday,
                    Gender = student.Gender
                };

                var result = _repository.Add(newStudent);

                return RedirectToAction(nameof(Detail), new { id = result.Id });
            }

            ModelState.AddModelError(string.Empty,"model empty error");

            return View();
        }
    }
}
