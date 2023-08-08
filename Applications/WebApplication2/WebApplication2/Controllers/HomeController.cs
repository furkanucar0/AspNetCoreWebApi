using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("home")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ResponseModel GetMessage()
        {
            return new ResponseModel()
            {
                HttpStatus = 200,
                Message = "Hello Asp.Net Core Web API"
            };

        }
    }
}
