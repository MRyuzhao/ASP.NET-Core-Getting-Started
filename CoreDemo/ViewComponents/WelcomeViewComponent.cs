using System.Linq;
using System.Threading.Tasks;
using CoreDemo.Services;
using EfCore.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents
{
    public class WelcomeViewComponent : ViewComponent
    {
        private readonly IRepository<Student> _repository;

        public WelcomeViewComponent(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = _repository.GetAll().Count();
            return View("Default", result);
        }
    }
}
