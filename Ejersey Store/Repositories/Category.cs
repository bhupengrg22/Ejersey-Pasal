using Microsoft.EntityFrameworkCore;

namespace jerseyShoppingCartMvcUI.Repositories;

public interface IcategoryRepository
{
    Task Addcategory(category category);
    Task Updatecategory(category category);
    Task<category?> GetcategoryById(int id);
    Task Deletecategory(category category);
    Task<IEnumerable<category>> Getcategorys();
}
public class categoryRepository : IcategoryRepository
{
    private readonly ApplicationDbContext _context;
    public categoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Addcategory(category category)
    {
        _context.categorys.Add(category);
        await _context.SaveChangesAsync();
    }
    public async Task Updatecategory(category category)
    {
        _context.categorys.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task Deletecategory(category category)
    {
        _context.categorys.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task<category?> GetcategoryById(int id)
    {
        return await _context.categorys.FindAsync(id);
    }

    public async Task<IEnumerable<category>> Getcategorys()
    {
        return await _context.categorys.ToListAsync();
    }

    
}
