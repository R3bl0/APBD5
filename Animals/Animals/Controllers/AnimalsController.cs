using Animals.DTOs;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new List<GetAnimalsResponse>();
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = new SqlCommand("SELECT * FROM animals", sqlConnection);
                sqlCommand.Connection.Open();
                var reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    response.Add(new GetAnimalsResponse(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                    ));
                }
            }

            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetAnimal(int id)
        {
            var response = new List<GetAnimalResponse>();
            var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default"));
            var sqlCommand = new SqlCommand("SELECT * FROM animals WHERE Id_Animal = @1", sqlConnection);
            sqlCommand.Parameters.AddWithValue("@1", id);
            sqlCommand.Connection.Open();
            var reader = sqlCommand.ExecuteReader();
            if (!reader.Read()) return NotFound();
            return Ok(new GetAnimalResponse(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4)
                )
            );
        }
    }
}