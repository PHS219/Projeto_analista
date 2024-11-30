using MySql.Data.MySqlClient;
using System;

class Program
{
    static void Main()
    {
        string connectionString = "Server=DESKTOP-R2PM8H1;Database=MeuBancoDeDados;IntegratedSecurity=True;";
        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                Console.WriteLine("Conexão bem-sucedida!");

                string query = "SELECT Nome, Email FROM Alunos";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Nome"]} - {reader["Email"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }
    }
}

}
