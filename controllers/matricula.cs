using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MeuProjeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaController : ControllerBase
    {
        private readonly string _connectionString = "Server=localhost;Database=MeuBancoDeDados;User ID=root;Password=;";

        [HttpPost("matricular")]
        public IActionResult MatricularAluno([FromBody] Aluno aluno)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    int codigoMatricula = GerarCodigoMatricula();

                    string query = "INSERT INTO Alunos (CodigoMatricula, Nome, Telefone, Email, CursoId, TurmaId, DataCadastro) VALUES (@CodigoMatricula, @Nome, @Telefone, @Email, @CursoId, @TurmaId, @DataCadastro)";
                    
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CodigoMatricula", codigoMatricula);
                        cmd.Parameters.AddWithValue("@Nome", aluno.Nome);
                        cmd.Parameters.AddWithValue("@Telefone", aluno.Telefone);
                        cmd.Parameters.AddWithValue("@Email", aluno.Email);
                        cmd.Parameters.AddWithValue("@CursoId", aluno.CursoId);
                        cmd.Parameters.AddWithValue("@TurmaId", aluno.TurmaId);
                        cmd.Parameters.AddWithValue("@DataCadastro", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }

                    return Ok("Aluno matriculado com sucesso.");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao matricular aluno: {ex.Message}");
                }
            }
        }

        private int GerarCodigoMatricula()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT MAX(CodigoMatricula) FROM Alunos";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    var result = cmd.ExecuteScalar();
                    int codigoMatricula = result != DBNull.Value ? Convert.ToInt32(result) + 1 : 1;
                    return codigoMatricula;
                }
            }
        }

        [HttpGet("turmas/{cursoId}")]
        public IActionResult ListarTurmas(int cursoId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Id, Descricao FROM Turmas WHERE CursoId = @CursoId";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CursoId", cursoId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var turmas = new List<Turma>();
                            while (reader.Read())
                            {
                                turmas.Add(new Turma
                                {
                                    Id = reader.GetInt32("Id"),
                                    Descricao = reader.GetString("Descricao")
                                });
                            }
                            return Ok(turmas);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Erro ao listar turmas: {ex.Message}");
                }
            }
        }
    }

    public class Turma
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }
}
