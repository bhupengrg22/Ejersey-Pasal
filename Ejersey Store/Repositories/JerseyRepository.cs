using Microsoft.EntityFrameworkCore;

namespace jerseyShoppingCartMvcUI.Repositories
{
    public interface IjerseyRepository
    {
        Task Addjersey(jersey jersey);
        Task Deletejersey(jersey jersey);
        Task<jersey?> GetjerseyById(int id);
        Task<IEnumerable<jersey>> Getjerseys();
        Task Updatejersey(jersey jersey);
    }

    public class jerseyRepository : IjerseyRepository
    {
        private readonly ApplicationDbContext _context;
        public jerseyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Addjersey(jersey jersey)
        {
            _context.jerseys.Add(jersey);
            await _context.SaveChangesAsync();
        }

        public async Task Updatejersey(jersey jersey)
        {
            _context.jerseys.Update(jersey);
            await _context.SaveChangesAsync();
        }

        public async Task Deletejersey(jersey jersey)
        {
            _context.jerseys.Remove(jersey);
            await _context.SaveChangesAsync();
        }

        public async Task<jersey?> GetjerseyById(int id) => await _context.jerseys.FindAsync(id);

        public async Task<IEnumerable<jersey>> Getjerseys() => await _context.jerseys.Include(a=>a.category).ToListAsync();
    }
}
