import { Component, OnInit } from '@angular/core';
import { RouterOutlet, RouterModule, Router } from '@angular/router';
import { GoodsesTableComponent } from './goodses-table/goodses-table.component';
import { SignService } from './sign.service';
import { Users } from '../models/users';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, GoodsesTableComponent, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'ProjectAng';
  
  private _loginedUser: Users = {
    id: 0,
    login: '',
    password: '',
    status: false
  };
  
  private _authenticationCheck: boolean = false;
  private _statusCheck: boolean = false;
  showLogout: boolean = false;

  constructor(private router: Router){
    
  }

  ngOnInit() {
    const savedUser = localStorage.getItem('loginedUser');
    this._loginedUser = savedUser ? JSON.parse(savedUser) : { id: 0, login: '', password: '', status: false };
    
    this._authenticationCheck = JSON.parse(localStorage.getItem('authenticationCheck') || 'false');
    this._statusCheck = JSON.parse(localStorage.getItem('statusCheck') || 'false');
  }

  get loginedUser(): Users {
    return this._loginedUser;
  }

  get authenticationCheck(): boolean {
    return this._authenticationCheck;
  }

  get statusCheck(): boolean {
    return this._statusCheck;
  }

  logout() {
    localStorage.removeItem('loginedUser');
    localStorage.removeItem('authenticationCheck');
    localStorage.removeItem('statusCheck');
    //window.location.reload();
    this.router.navigateByUrl('/signin').then(() => {
      window.location.reload(); // Повернення до поточного маршруту
    });
    
  }
}
