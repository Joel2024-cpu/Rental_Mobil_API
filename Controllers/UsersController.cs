using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _config;

    public UsersController(IConfiguration config)
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
        using var conn = GetConnection();
        var data = conn.Query("SELECT * FROM users");
        return Ok(new { status = "success", data });
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        using var conn = GetConnection();
        var data = conn.QueryFirstOrDefault("SELECT * FROM users WHERE id=@id", new { id });

        if (data == null)
        {
            return NotFound(new { status = "error", message = "Data user tidak ditemukan" });
        }

        return Ok(new { status = "success", data });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var conn = GetConnection();

        var cek = conn.QueryFirstOrDefault("SELECT * FROM users WHERE id=@id", new { id });

        if (cek == null)
        {
            return NotFound(new { status = "error", message = "Data user tidak ditemukan" });
        }

        conn.Execute("DELETE FROM users WHERE id=@id", new { id });

        return Ok(new { status = "success", message = "User berhasil dihapus" });
    }
}