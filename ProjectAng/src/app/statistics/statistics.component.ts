import { Component, OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { Goods } from '../../models/goods';
import { GoodsService } from '../goods.service';

@Component({
  selector: 'app-statistics',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './statistics.component.html',
  styleUrl: './statistics.component.css'
})
export class StatisticsComponent implements OnInit{

  countOfProducts: number = 0;
  countOfTotalSells: number = 0;
  bestSellingProduct: Goods = {
    id: 0,
    productCode: '',
    productName: '',
    producer: '',
    numberOf: 0,
    price: 0,
    orderPrice: 0,
    numberOfPrices: 0,
    priceAllOrders: 0,
    category: '',
    subcategory: ''
  };
  popularProducer: Goods[] = [];
  mostOrderedProduct: Goods = this.bestSellingProduct;
  mostExpensiveProduct: Goods = this.bestSellingProduct;
  leastExpensiveProduct: Goods = this.bestSellingProduct;
  averagePrice: number = 0;
  averageSalesPerProduct: number = 0;
  salesVolumeByCategory: { [category: string]: number } = {};
  uniqueProducersCount: number = 0;

  constructor(private goodsService: GoodsService) {}

  ngOnInit(): void {
    this.goodsService.getCountOfProducts().subscribe(count => this.countOfProducts = count);
    this.goodsService.getCountOfTotalSells().subscribe(count => this.countOfTotalSells = count);
    this.goodsService.getBestSellingProduct().subscribe(count => this.bestSellingProduct = count);
    this.goodsService.getPopularProducer().subscribe(count => this.popularProducer = count);
    this.goodsService.getMostOrderedProduct().subscribe(count => this.mostOrderedProduct = count);
    this.goodsService.getMostExpensiveProduct().subscribe(count => this.mostExpensiveProduct = count);
    this.goodsService.getLeastExpensiveProduct().subscribe(count => this.leastExpensiveProduct = count);
    this.goodsService.getAveragePrice().subscribe(avgPrice => this.averagePrice = avgPrice);
    this.goodsService.getAverageSalesPerProduct().subscribe(avgSales => this.averageSalesPerProduct = avgSales);
    this.goodsService.getSalesVolumeByCategory().subscribe(volume => this.salesVolumeByCategory = volume);
    this.goodsService.getUniqueProducersCount().subscribe(count => this.uniqueProducersCount = count);
  }

}
