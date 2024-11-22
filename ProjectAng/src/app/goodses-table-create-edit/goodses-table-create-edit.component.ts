import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Goods } from '../../models/goods';
import { GoodsService } from '../goods.service';
import { Router, ActivatedRoute} from '@angular/router';

@Component({
  selector: 'app-goodses-table-create-edit',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './goodses-table-create-edit.component.html',
  styleUrl: './goodses-table-create-edit.component.css'
})
export class GoodsesTableCreateEditComponent implements OnInit {

goodsToAdd: Goods = {
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
}

errorMessage: string = "";

constructor(private goodsService: GoodsService, private router: Router, private route: ActivatedRoute){

}

isEditing: boolean = false;

ngOnInit(): void {
  this.route.paramMap.subscribe((result) => {
    const id = result.get('id');
    if (id) {
      this.isEditing = true;
      this.goodsService.getGoodsById(Number(id)).subscribe({
        next: (result) => this.goodsToAdd = result,
        error: (err) => this.errorMessage = `Error: ${err.status} - ${err.message}`
      });
    } 
  });
}


onSubmit(): void {
  if(this.isEditing){
    
    if (this.goodsToAdd.productCode && this.goodsToAdd.productName && this.goodsToAdd.price) {
      this.goodsService.updateGoodsById(this.goodsToAdd.id, this.goodsToAdd).subscribe({
        next:() => {  
          console.log('goods was edited' + this.goodsToAdd);
          this.router.navigate(['/goodses']);
        },
        error:(err) => {
          console.error(err);
          this.errorMessage = `Error: ${err.status} - ${err.message} `;
        }
      });
    } else {
      this.errorMessage = "Please fill in all required fields.";
    }
  } else {
    
    if (this.goodsToAdd.productCode && this.goodsToAdd.productName && this.goodsToAdd.price) {
      this.goodsService.createGoods(this.goodsToAdd).subscribe({
        next:() => {
          console.log('goods was added' + this.goodsToAdd);
          this.router.navigate(['/']);
        },
        error:(err) => {
          console.error(err);
          this.errorMessage = `Error: ${err.status} - ${err.message} `;
        }
      });
    } else {
      this.errorMessage = "Please fill in all required fields.";
    }
  }
}

}
