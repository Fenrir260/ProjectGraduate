import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Environment } from '../environment/environment';
import { Goods } from '../models/goods';


@Injectable({
  providedIn: 'root'
})
export class GoodsService {

  private apiUrl = `${Environment.apiUrl}/main`

  constructor(private http: HttpClient) {

   }

   getAllGoods(): Observable<Goods[]>{
    return this.http.get<Goods[]>(this.apiUrl);
   }

  createGoods(goods: Goods): Observable<Goods>{
    return this.http.post<Goods>(`${this.apiUrl}/createGoods`, goods);
  }

  deleteGoodsById(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}/deleteGoodsById`);
  }

  deleteGoodsByProductCode(productCode: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${productCode}/deleteGoodsByProductCode`);
  }

  updateGoodsById(id: number, goods: Goods): Observable<Goods>{
    return this.http.put<Goods>(`${this.apiUrl}/${id}/updateGoodsById`, goods);
  }

  updateGoodsByProductCode(productCode: string, goods: Goods): Observable<Goods>{
    return this.http.put<Goods>(`${this.apiUrl}/${productCode}/updateGoodsByProductCode`, goods);
  }
   
  updateGoods(goods: Goods): Observable<Goods>{
    return this.http.put<Goods>(`${this.apiUrl}/updateGoods`, goods);
  }

  getGoodsById(id: number): Observable<Goods>{
    return this.http.get<Goods>(`${this.apiUrl}/${id}/getGoodsById`);
  }

  getGoodsByProductCode(productCode: string): Observable<Goods>{
    return this.http.get<Goods>(`${this.apiUrl}/${productCode}/getGoodsByProductCode`);
  }



  getCountOfProducts(): Observable<number>{
    return this.http.get<number>(`${this.apiUrl}/getCountOfProducts`);
  }

  getCountOfTotalSells(): Observable<number>{
    return this.http.get<number>(`${this.apiUrl}/getCountOfTotalSells`);
  }

  getBestSellingProduct(): Observable<Goods>{
    return this.http.get<Goods>(`${this.apiUrl}/getBestSellingProduct`);
  }

  getPopularProducer(): Observable<Goods[]>{
    return this.http.get<Goods[]>(`${this.apiUrl}/getPopularProducer`);
  }

  getMostOrderedProduct(): Observable<Goods>{
    return this.http.get<Goods>(`${this.apiUrl}/getMostOrderedProductAsync`);
  }

  getMostExpensiveProduct(): Observable<Goods>{
    return this.http.get<Goods>(`${this.apiUrl}/getMostExpensiveProductAsync`);
  }

  getLeastExpensiveProduct(): Observable<Goods>{
    return this.http.get<Goods>(`${this.apiUrl}/getLeastExpensiveProductAsync`);
  }

  getAveragePrice(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/getAveragePrice`);
}

getAverageSalesPerProduct(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/getAverageSalesPerProduct`);
}

getSalesVolumeByCategory(): Observable<{ [category: string]: number }> {
    return this.http.get<{ [category: string]: number }>(`${this.apiUrl}/getSalesVolumeByCategory`);
}

getTopSellingProducts(topCount: number = 3): Observable<Goods[]> {
    return this.http.get<Goods[]>(`${this.apiUrl}/getTopSellingProducts?topCount=${topCount}`);
}

getUniqueProducersCount(): Observable<number> {
    return this.http.get<number>(`${this.apiUrl}/getUniqueProducersCount`);
}

}
