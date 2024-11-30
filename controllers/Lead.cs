using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MeuProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        private readonly string _connectionString = "Server=DESKTOP-R2PM8H1;Database=MeuBancoDeDados;IntegratedSecurity=True;";
        [HttpPost]
        public IActionResult CreateLead([FromBody] Lead lead)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Leads (Nome, Telefone, Email, CursoInteresse) VALUES (@Nome, @Telefone, @Email, @CursoInteresse)";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Nome", lead.Nome);
                        cmd.Parameters.AddWithValue("@Telefone", lead.Telefone);
                        cmd.Parameters.AddWithValue("@Email", lead.Email);
                        cmd.Parameters.AddWithValue("@CursoInteresse", lead.CursoInteresse);
                        
                        cmd.ExecuteNonQuery();
                    }

                    return Ok("Lead cadastrado com sucesso.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao cadastrar lead: {ex.Message}");
                }
            }
        }
    }

    public class Lead
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int CursoInteresse { get; set; }
    }
}
