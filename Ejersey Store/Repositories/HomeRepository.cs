

using Microsoft.EntityFrameworkCore;

namespace jerseyShoppingCartMvcUI.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<category>> categorys()
        {
            return await _db.categorys.ToListAsync();
        }
        public async Task<IEnumerable<jersey>> Getjerseys(string sTerm = "", int categoryId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<jersey> jerseys = await (from jersey in _db.jerseys
                         join category in _db.categorys
                         on jersey.categoryId equals category.Id
                         join stock in _db.Stocks
                         on jersey.Id equals stock.jerseyId
                         into jersey_stocks
                         from jerseyWithStock in jersey_stocks.DefaultIfEmpty()
                         where string.IsNullOrWhiteSpace(sTerm) || (jersey != null && jersey.jerseyName.ToLower().StartsWith(sTerm))
                         select new jersey
                         {
                             Id = jersey.Id,
                             Image = jersey.Image,
                            
                             jerseyName = jersey.jerseyName,
                             categoryId = jersey.categoryId,
                             Price = jersey.Price,
                             categoryName = category.categoryName,
                             Quantity=jerseyWithStock==null? 0:jerseyWithStock.Quantity
                         }
                         ).ToListAsync();
            if (categoryId > 0)
            {

                jerseys = jerseys.Where(a => a.categoryId == categoryId).ToList();
            }
            return jerseys;

        }
    }
}
