using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Animals.Controllers
{
    [ApiController]
    [Route("controller-animals")]
    public class AnimalsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AnimalsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
    }
}