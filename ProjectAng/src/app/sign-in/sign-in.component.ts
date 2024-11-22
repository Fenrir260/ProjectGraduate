import { Component, OnInit } from '@angular/core';
import { Users } from '../../models/users';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { SignService } from '../sign.service';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.css'
})
export class SignInComponent implements OnInit {
  
  user: Users = {
    id: 0,
    login: '',
    password: '',
    status: false
  }

  constructor(
    private signService: SignService,
    private router: Router,
    private activeRouter: ActivatedRoute,
  ){
   
  }

  errorMessage: string = "";

  ngOnInit(): void {
    
  }

  onSubmit(): void {
  console.log(navigator.cookieEnabled);
  this.signService.verify(this.user).subscribe({
    next: (isVerified) => {
      if (isVerified) {
        this.signService.authenticationCheck = true;
        this.signService.getUserByLogin(this.user.login).subscribe({
          next: (userData) => {           
            this.signService.loginedUser = userData; 

            if (userData.status) {
              this.signService.statusCheck = true;
            }
            this.router.navigate(['/goodses']).then (() => { window.location.reload() });
            /*this.router.navigateByUrl('/goodses', { skipLocationChange: true }).then(() => {
              this.router.navigate([this.router.url]); // Повернення до поточного маршруту
            });*/
            
          },
          error: (error) => {
            console.error("Помилка при отриманні даних користувача:", error);
            this.errorMessage = "Не вдалося отримати дані користувача.";
          }
        });
      } else {
        this.errorMessage = "Невірний логін або пароль";
      }
    },
    error: (error) => {
      console.error("Помилка під час перевірки:", error);
      this.errorMessage = "Сталася помилка під час перевірки. Спробуйте ще раз.";
    }
  });
}

}