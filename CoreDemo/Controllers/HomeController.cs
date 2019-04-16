using System;
using System.Collections.Generic;
using System.Linq;
using CoreDemo.Models;
using CoreDemo.Services;
using CoreDemo.Settings;
using CoreDemo.ViewModels;
using EfCore.Data;
using EfCore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CoreDemo.Controllers
{
    //mvc Controller是接受请求，构建model，渲染view
    public class HomeController : Controller
    {
        private readonly IRepository<Student> _repository;
        private readonly IOptions<ConnectionOptions> _options;
        private readonly MyContext _context;
        private readonly MyContext _myContext2;

        public HomeController(IRepository<Student> repository,
            IOptions<ConnectionOptions> options,
            MyContext myContext,
            MyContext myContext2)
        {
            _repository = repository;
            _options = options;
            _context = myContext;
            _myContext2 = myContext2;
        }

        public IActionResult Index()
        {
            var province = new Province
            {
                Name = "北京",
                Population = 2_000_000
            };
            var province1 = new Province
            {
                Name = "上海",
                Population = 1_000_000
            };

            _context.Provinces.Add(province);    // _context is now Tracking province object
                                                 //_context.Add(province);

            _context.Provinces.AddRange(new List<Province>
            {
                province,province1
            });

            _context.SaveChanges();// insert to sqlserver

            var province2 = _context.Provinces.FirstOrDefault();

            //_myContext2.Database.ExecuteSqlCommand("");
            _myContext2.Provinces.Update(province2);
            _myContext2.SaveChanges();

            var queryParams = "北京";
            var provinces = _context.Provinces.Where(x => x.Name == queryParams)
               .ToList();
            var provinces1 = _context.Provinces.Where(x => x.Name == "北京");//forbidden


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

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
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

            ModelState.AddModelError(string.Empty, "model empty error");

            return View();
        }
    }
}
