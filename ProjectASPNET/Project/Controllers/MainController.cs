using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.Models;
using Project.Repositories;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IRepository _goodsRepository;

        public MainController(IRepository goodsRepository)
        {
            _goodsRepository = goodsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goods>>> GetAllGoodsAsync()
        {
            var allGoods = await _goodsRepository.GetAllGoodsAsync();
            return Ok(allGoods);
        }

        [HttpGet("{id}/getGoodsById")]
        public async Task<ActionResult<Goods>> GetGoodsByIdAsync(int id)
        {
            var goods = await _goodsRepository.GetGoodsByIdAsync(id);
            if (goods == null)
            {
                return NotFound();
            }
            return Ok(goods);
        }

        [HttpGet("{productCode}/getGoodsByProductCode")]
        public async Task<ActionResult<Goods>> GetGoodsByProductCodeAsync(string productCode)
        {
            var goods = await _goodsRepository.GetGoodsByProductCodeAsync(productCode);
            if (goods == null)
            {
                return NotFound();
            }
            return Ok(goods);
        }

        [HttpPost("createGoods")]
        public async Task<ActionResult<Goods>> CreateGoodsAsync(Goods goods)
        {
            var existingGoods = await _goodsRepository.GetGoodsByProductCodeAsync(goods.ProductCode);
            if (existingGoods != null)
            {
                return BadRequest(new { error = "ProductCode must be unique." });
            }
            if ((!ModelState.IsValid))
            {
                return BadRequest();
            }

            await _goodsRepository.AddGoodsAsync(goods);
            return Ok(goods);//CreatedAtAction(nameof(GetAllGoodsAsync), goods);
        }

        [HttpPut("{productCode}/updateGoodsByProductCode")]
        public async Task<ActionResult<Goods>> UpdateGoodsByProductCodeAsync(string productCode, Goods goods)
        {
            await _goodsRepository.UpdateGoodsAsync(goods);
            return Ok(goods);
        }

        [HttpPut("{id}/updateGoodsById")]
        public async Task<ActionResult<Goods>> UpdateGoodsByIdAsync(int id, Goods goods)
        {
            if (! await _goodsRepository.UniqueProductCode(goods.ProductCode))
            {
                return BadRequest();
            }
            await _goodsRepository.UpdateGoodsByIdAsync(goods, id);
            return Ok(goods);
        }

        [HttpPut("updateGoods")]
        public async Task<ActionResult<Goods>> UpdateGoodsAsync(Goods goods)
        {
            await _goodsRepository.UpdateGoodsAsync(goods);
            return Ok(goods);
        }
        
        [HttpDelete("{id}/deleteGoodsById")]
        public async Task<ActionResult> DeleteGoodsById(int id)
        {
            await _goodsRepository.DeleteGoodsByIdAsync(id);
            return Ok();
        }

        [HttpDelete("{productCode}/deleteGoodsByProductCode")]
        public async Task<ActionResult> DeleteGoodsByProductCode(string productCode)
        {
            await _goodsRepository.DeleteGoodsByProductCodeAsync(productCode);
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        [HttpGet("getCountOfProducts")]
        public async Task<ActionResult<int>> GetCountOfProductsAsync()
        {
            var count = await _goodsRepository.GetCountOfProductsAsync();
            return Ok(count);
        }

        [HttpGet("getCountOfTotalSells")]
        public async Task<ActionResult<int>> GetCountOfTotalSellsAsync()
        {
            var count = await _goodsRepository.GetCountOfSellsAsync();
            return Ok(count);
        }

        [HttpGet("getBestSellingProduct")]
        public async Task<ActionResult<Goods?>> GetBestSellingProductAsync()
        {
            var goods = await _goodsRepository.GetBestSellingProductAsync();
            return Ok(goods);
        }

        [HttpGet("getPopularProducer")]
        public async Task<ActionResult<Goods[]>> GetPopularProducerAsync()
        {
            var producer = await _goodsRepository.GetMostPopularProducerAsync();
            return Ok(producer);
        }

        [HttpGet("getMostOrderedProductAsync")]
        public async Task<ActionResult<Goods?>> GetMostOrderedProductAsync()
        {
            var goods = await _goodsRepository.GetMostOrderedProductAsync();
            return Ok(goods);
        }

        [HttpGet("getMostExpensiveProductAsync")]
        public async Task<ActionResult<Goods?>> GetMostExpensiveProductAsync()
        {
            var goods = await _goodsRepository.GetMostExpensiveProductAsync();
            return Ok(goods);
        }

        [HttpGet("getLeastExpensiveProductAsync")]
        public async Task<ActionResult<Goods?>> GetLeastExpensiveProductAsync()
        {
            var goods = await _goodsRepository.GetLeastExpensiveProductAsync();
            return Ok(goods);
        }

        [HttpGet("getAveragePrice")]
        public async Task<ActionResult<double>> GetAveragePriceAsync()
        {
            var averagePrice = await _goodsRepository.GetAveragePriceAsync();
            return Ok(averagePrice);
        }

        [HttpGet("getAverageSalesPerProduct")]
        public async Task<ActionResult<double>> GetAverageSalesPerProductAsync()
        {
            var averageSales = await _goodsRepository.GetAverageSalesPerProductAsync();
            return Ok(averageSales);
        }

        [HttpGet("getSalesVolumeByCategory")]
        public async Task<ActionResult<Dictionary<string, int>>> GetSalesVolumeByCategoryAsync()
        {
            var salesVolumeByCategory = await _goodsRepository.GetSalesVolumeByCategoryAsync();
            return Ok(salesVolumeByCategory);
        }

        [HttpGet("getTopSellingProducts")]
        public async Task<ActionResult<List<Goods>>> GetTopSellingProductsAsync([FromQuery] int topCount = 3)
        {
            var topSellingProducts = await _goodsRepository.GetTopSellingProductsAsync(topCount);
            return Ok(topSellingProducts);
        }

        [HttpGet("getUniqueProducersCount")]
        public async Task<ActionResult<int>> GetUniqueProducersCountAsync()
        {
            var uniqueProducersCount = await _goodsRepository.GetUniqueProducersCountAsync();
            return Ok(uniqueProducersCount);
        }

    }
}
