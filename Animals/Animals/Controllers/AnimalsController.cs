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

        [HttpGet("{orderBy}")]
        public IActionResult GetAll(string orderBy = "name")
        {
            var response = new List<GetAnimalsResponse>();
            var list = new List<string>{"name", "description", "category", "area"};
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                if (!list.Contains(orderBy))
                {
                    Console.WriteLine("blad");
                    return BadRequest();
                }
                var sqlCommand = new SqlCommand($"SELECT * FROM animals ORDER BY {orderBy} ASC", sqlConnection);
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

        [HttpPost]
        public IActionResult CreateAnimal(CreateAnimalRequest request)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = new SqlCommand("INSERT INTO animals (Name, Description, Category, Area) values (@1,@2,@3,@4);SELECT CAST(SCOPE_IDENTITY() as int)", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@1", request.Name);
                sqlCommand.Parameters.AddWithValue("@2", request.Description);
                sqlCommand.Parameters.AddWithValue("@3", request.Category);
                sqlCommand.Parameters.AddWithValue("@4", request.Area);
                sqlCommand.Connection.Open();

                var id = sqlCommand.ExecuteScalar();

                return Created($"animal/{id}", new CreateAnimalResponse((int)id, request));
            }
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceAnimal(int id, ReplaceAnimalRequest request)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = new SqlCommand("UPDATE animals SET Name = @1, Description = @2, Category = @3, Area = @4 WHERE id_animal = @5", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@1", request.Name);
                sqlCommand.Parameters.AddWithValue("@2", request.Description);
                sqlCommand.Parameters.AddWithValue("@3", request.Category);
                sqlCommand.Parameters.AddWithValue("@4", request.Area);
                sqlCommand.Parameters.AddWithValue("@5", id);
                sqlConnection.Open();

                var affectedRows = sqlCommand.ExecuteNonQuery();
                if (affectedRows==0)
                {
                    return NotFound();
                }
                return NoContent();
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                var sqlCommand = new SqlCommand("DELETE FROM animals WHERE id_animal=@1", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@1", id);
                sqlConnection.Open();

                var affectedRows = sqlCommand.ExecuteNonQuery();

                if (affectedRows==0)
                {
                    return NotFound();
                }

                return NoContent();
            }
        }
    }
}