namespace Projeto.Services
{
    public class LeadService
    {
        private readonly ApplicationDbContext _context;

        public LeadService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Lead>> GetLeadsByFilter(string nome, string email, string cursoInteresse)
        {
            var query = _context.Leads.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(l => l.Nome.Contains(nome));

            if (!string.IsNullOrEmpty(email))
                query = query.Where(l => l.Email.Contains(email));

            if (!string.IsNullOrEmpty(cursoInteresse))
                query = query.Where(l => l.CursoInteresse.Contains(cursoInteresse));

            return await query.ToListAsync();
        }
    }
}
