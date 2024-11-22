using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Repositories
{
    public class Repository : IRepository
    {
        private readonly ContextDb _contextDb;

        public Repository(ContextDb contextDb)
        {
            _contextDb = contextDb;
        }

        public async Task<IEnumerable<Goods>> GetAllGoodsAsync()
        {
            return await _contextDb.GoodsTable.ToListAsync();
        }

        public async Task AddGoodsAsync(Goods goods)
        {
            //goods.Id = GenerateId(_contextDb.GoodsTable.ToList());
            
            await _contextDb.GoodsTable.AddAsync(goods);
            await _contextDb.SaveChangesAsync();
        }

        public int GenerateId(List<Goods> _goods)
        {
            var existingIds = new HashSet<int>(_goods.Select(g => g.Id));
            int newId = 1;
            while (existingIds.Contains(newId))
            {
                newId++;
            }
            return newId;
        }

        public async Task UpdateGoodsAsync(Goods goods)
        {
            _contextDb.GoodsTable.Update(goods);
            await _contextDb.SaveChangesAsync();
        }
        
        public async Task<Goods?> GetGoodsByIdAsync(int id)
        {
            return await _contextDb.GoodsTable.FindAsync(id);
        }

        public async Task<Goods?> GetGoodsByProductCodeAsync(string productCode)
        {
            return await _contextDb.GoodsTable.FirstOrDefaultAsync(g => g.ProductCode == productCode);
        }

        public async Task UpdateGoodsByIdAsync(Goods goods, int id)
        {
            goods.Id = id;

            _contextDb.GoodsTable.Update(goods);
            await _contextDb.SaveChangesAsync();
        }

        public async Task<bool> UniqueProductCode(string productCode)
        {
            var goods = await _contextDb.GoodsTable.FirstOrDefaultAsync(g => g.ProductCode == productCode);
            return goods == null;
        }

        public async Task DeleteGoodsByIdAsync(int id)
        {
            var goods = await _contextDb.GoodsTable.FirstOrDefaultAsync(g => id == g.Id);
            if (goods == null)
            {
                throw new Exception("There is no goods with this id: " + id);
            }

            _contextDb.Remove(goods);
            await _contextDb.SaveChangesAsync();
        }

        public async Task DeleteGoodsByProductCodeAsync(string productCode)
        {
            var goods = await _contextDb.GoodsTable.FirstOrDefaultAsync(g => g.ProductCode == productCode);
            
            if (goods == null)
            {
                throw new Exception("There is no goods with this ProductCode: " + productCode);
            }

            _contextDb.Remove(goods);
            await _contextDb.SaveChangesAsync();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>


        public async Task<int> GetCountOfProductsAsync()
        {
            return await _contextDb.GoodsTable.CountAsync();
        }

        public async Task<int> GetCountOfSellsAsync()
        {
            var goodses = await _contextDb.GoodsTable.ToListAsync();
            var totalSells = 0;
            foreach (var goods in goodses)
            {
                totalSells += Convert.ToInt32(goods.OrderPrice);
            }
            return totalSells;
        }

        public async Task<Goods?> GetBestSellingProductAsync()
        {
            return await _contextDb.GoodsTable.OrderByDescending(g => g.NumberOf).FirstOrDefaultAsync(); ;
        }

        public async Task<Goods[]> GetMostPopularProducerAsync()
        {
            var goodsList = await _contextDb.GoodsTable.ToListAsync();

            var groupedByProducer = goodsList
                .GroupBy(g => g.Producer) 
                .OrderByDescending(g => g.Count()) 
                .FirstOrDefault(); 

            if (groupedByProducer != null)
            {
                var mostPopularProducerGoods = goodsList
                    .Where(g => g.Producer == groupedByProducer.Key)
                    .ToArray();

                return mostPopularProducerGoods;
            }
            return Array.Empty<Goods>();
        }

        public async Task<Goods?> GetMostOrderedProductAsync()
        {
            return await _contextDb.GoodsTable.OrderByDescending(g => g.NumberOf).FirstOrDefaultAsync(); 
        }

        public async Task<Goods?> GetMostExpensiveProductAsync()
        {
            return await _contextDb.GoodsTable.OrderByDescending(g => g.Price).FirstOrDefaultAsync();
        }

        public async Task<Goods?> GetLeastExpensiveProductAsync()
        {
            return await _contextDb.GoodsTable.OrderBy(g => g.Price).FirstOrDefaultAsync(); 
        }



        public async Task<double> GetAveragePriceAsync()
        {
            return await _contextDb.GoodsTable.AverageAsync(g => g.Price);
        }

        public async Task<double> GetAverageSalesPerProductAsync()
        {
            return await _contextDb.GoodsTable.AverageAsync(g => g.NumberOf);
        }

        public async Task<Dictionary<string, int>> GetSalesVolumeByCategoryAsync()
        {
            return await _contextDb.GoodsTable
                .GroupBy(g => g.Category)
                .Select(group => new { Category = group.Key, TotalSales = group.Sum(g => g.NumberOf) })
                .ToDictionaryAsync(g => g.Category, g => g.TotalSales);
        }

        public async Task<List<Goods>> GetTopSellingProductsAsync(int topCount = 3)
        {
            return await _contextDb.GoodsTable
                .OrderByDescending(g => g.NumberOf)
                .Take(topCount)
                .ToListAsync();
        }

        public async Task<int> GetUniqueProducersCountAsync()
        {
            return await _contextDb.GoodsTable
                .Select(g => g.Producer)
                .Distinct()
                .CountAsync();
        }


    }
}
