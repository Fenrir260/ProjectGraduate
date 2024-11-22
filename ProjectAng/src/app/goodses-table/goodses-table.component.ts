import { Component, OnInit } from '@angular/core';
import { Goods } from '../../models/goods';
import { GoodsService } from '../goods.service';
import { Router, ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { SignService } from '../sign.service';

@Component({
  selector: 'app-goodses-table',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './goodses-table.component.html',
  styleUrl: './goodses-table.component.css'
})
export class GoodsesTableComponent implements OnInit {

  goodses: Goods[] = [];

  constructor(private signService: SignService, private goodsService: GoodsService, private router: Router){
    
  }

  private _authenticationCheck: boolean = false;
  private _statusCheck: boolean = false;

  ngOnInit(): void {
    this.goodsService.getAllGoods().subscribe((data: Goods[]) => {
      this.goodses = data;
      this._authenticationCheck = JSON.parse(localStorage.getItem('authenticationCheck') || 'false');
      this._statusCheck = JSON.parse(localStorage.getItem('statusCheck') || 'false');
      // console.log(JSON.stringify(this.goodses));
    },
    (error) => {
      console.log(error);
    }
    );
  }

  get authenticationCheck(): boolean {
    return this._authenticationCheck;
  }

  get statusCheck(): boolean {
    return this._statusCheck;
  }

  deleteGoods(id: number): void {
    this.goodsService.deleteGoodsById(id).subscribe({
      next: () =>{
        this.goodses = this.goodses.filter(g=>g.id !== id);
      },
      error: (err) =>{
        console.log(`deleting goods error - ${err}`);
      }
    })
  }

  editGoods(id: number): void {
    this.router.navigate(['/editGoods', id]); 
  }

}
