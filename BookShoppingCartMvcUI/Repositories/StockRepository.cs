using Microsoft.EntityFrameworkCore;

namespace jerseyShoppingCartMvcUI.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock?> GetStockByjerseyId(int jerseyId) => await _context.Stocks.FirstOrDefaultAsync(s => s.jerseyId == jerseyId);

        public async Task ManageStock(StockDTO stockToManage)
        {
            // if there is no stock for given jersey id, then add new record
            // if there is already stock for given jersey id, update stock's quantity
            var existingStock = await GetStockByjerseyId(stockToManage.jerseyId);
            if (existingStock is null)
            {
                var stock = new Stock { jerseyId = stockToManage.jerseyId, Quantity = stockToManage.Quantity };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockToManage.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "")
        {
            var stocks = await (from jersey in _context.jerseys
                                join stock in _context.Stocks
                                on jersey.Id equals stock.jerseyId
                                into jersey_stock
                                from jerseyStock in jersey_stock.DefaultIfEmpty()
                                where string.IsNullOrWhiteSpace(sTerm) || jersey.jerseyName.ToLower().Contains(sTerm.ToLower())
                                select new StockDisplayModel
                                {
                                    jerseyId = jersey.Id,
                                    jerseyName = jersey.jerseyName,
                                    Quantity = jerseyStock == null ? 0 : jerseyStock.Quantity
                                }
                                ).ToListAsync();
            return stocks;
        }

    }

    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sTerm = "");
        Task<Stock?> GetStockByjerseyId(int jerseyId);
        Task ManageStock(StockDTO stockToManage);
    }
}
