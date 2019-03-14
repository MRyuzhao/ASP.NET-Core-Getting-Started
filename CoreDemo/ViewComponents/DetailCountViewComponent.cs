using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Models;
using CoreDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents
{
    public class DetailCountViewComponent:ViewComponent
    {
        private readonly IRepository<Student> _inMomoryRepository;

        public DetailCountViewComponent(IRepository<Student> inMomoryRepository)
        {
            _inMomoryRepository = inMomoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var details =  _inMomoryRepository.GetById(id);

            return View(details.Id);
        }
    }
}
