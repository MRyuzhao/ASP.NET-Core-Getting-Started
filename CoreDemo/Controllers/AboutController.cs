using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
    [Route("[controller]/[action]")]
    public class AboutController
    {
        [Route("")]
        public string Me()
        {
            return "Me";
        }

        public string Dave()
        {
            return "Dave";
        }
    }
}