
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace RentalMobilAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CarsController(IConfiguration config)
        {
            _config = config;
        }

        private NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using var conn = GetConnection();
                var data = conn.Query("SELECT * FROM cars WHERE deleted_at IS NULL");

                return Ok(new
                {
                    status = "success",
                    data
                });
            }
            catch
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "Gagal mengambil data mobil"
                });
            }
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                using var conn = GetConnection();

                var data = conn.QueryFirstOrDefault(
                    "SELECT * FROM cars WHERE id=@id AND deleted_at IS NULL",
                    new { id });

                // VALIDASI ID
                if (data == null)
                {
                    return NotFound(new
                    {
                        status = "error",
                        message = "Data mobil tidak ditemukan"
                    });
                }

                return Ok(new
                {
                    status = "success",
                    data
                });
            }
            catch
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "Terjadi kesalahan server"
                });
            }
        }

        [HttpPost]
        public IActionResult Create(Car car)
        {
            // VALIDASI INPUT
            if (string.IsNullOrEmpty(car.Name))
            {
                return BadRequest(new
                {
                    status = "error",
                    message = "Nama mobil wajib diisi"
                });
            }

            try
            {
                using var conn = GetConnection();

                conn.Execute(
                    "INSERT INTO cars(name, price, status) VALUES(@Name, @Price, @Status)",
                    car
                );

                return StatusCode(201, new
                {
                    status = "success",
                    message = "Data mobil berhasil ditambahkan"
                });
            }
            catch
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "Gagal menambahkan data mobil"
                });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Car car)
        {
            try
            {
                using var conn = GetConnection();

                conn.Execute(
                    "UPDATE cars SET name=@Name, price=@Price, status=@Status WHERE id=@Id",
                    new { car.Name, car.Price, car.Status, Id = id }
                );

                return Ok(new
                {
                    status = "success",
                    message = "Data mobil berhasil diupdate"
                });
            }
            catch
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "Gagal mengupdate data mobil"
                });
            }
        }

        // =========================
        // DELETE (SOFT DELETE)
        // =========================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using var conn = GetConnection();

                conn.Execute(
                    "UPDATE cars SET deleted_at = NOW() WHERE id = @id",
                    new { id }
                );

                return Ok(new
                {
                    status = "success",
                    message = "Data mobil berhasil dihapus"
                });
            }
            catch
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = "Gagal menghapus data mobil"
                });
            }
        }
    }
}