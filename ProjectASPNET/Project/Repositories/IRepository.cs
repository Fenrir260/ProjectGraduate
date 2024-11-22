using Project.Models;

namespace Project.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<Goods>> GetAllGoodsAsync();
        Task AddGoodsAsync(Goods goods);
        Task UpdateGoodsAsync(Goods goods);
        Task UpdateGoodsByIdAsync(Goods goods, int id);
        Task<Goods?> GetGoodsByProductCodeAsync(string productCode);
        Task<Goods?> GetGoodsByIdAsync(int id);
        Task<bool> UniqueProductCode(string productCode);
        Task DeleteGoodsByIdAsync(int id);
        Task DeleteGoodsByProductCodeAsync(string productCode);

        Task<int> GetCountOfProductsAsync();
        Task<int> GetCountOfSellsAsync();
        Task<Goods?> GetBestSellingProductAsync();
        Task<Goods[]> GetMostPopularProducerAsync();
        Task<Goods?> GetMostOrderedProductAsync();
        Task<Goods?> GetMostExpensiveProductAsync();
        Task<Goods?> GetLeastExpensiveProductAsync();

        Task<double> GetAveragePriceAsync();
        Task<double> GetAverageSalesPerProductAsync();
        Task<Dictionary<string, int>> GetSalesVolumeByCategoryAsync();
        Task<List<Goods>> GetTopSellingProductsAsync(int topCount = 3);
        Task<int> GetUniqueProducersCountAsync();
    }
}
