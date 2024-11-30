namespace Projeto.Models
{
    public class Lead
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string CursoInteresse { get; set; }
    }

    public class Curso
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
    }

    public class Turma
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }

    public class Aluno
    {
        public int Id { get; set; }
        public int CodigoMatricula { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int CursoId { get; set; }
        public int TurmaId { get; set; }
        public DateTime DataCadastro { get; set; }
        public Curso Curso { get; set; }
        public Turma Turma { get; set; }
    }
}
