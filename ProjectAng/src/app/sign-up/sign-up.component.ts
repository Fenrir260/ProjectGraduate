import { Component } from '@angular/core';
import { Users } from '../../models/users';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SignService } from '../sign.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.css'
})
export class SignUpComponent {

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
  

  onSubmit(): void {
    this.signService.createUser(this.user).subscribe({
      next:() => {
        this.router.navigate(['/signup']);
        console.log(this.user);    
      },
      error:(err) =>{
        console.log(err);
      }
    })
  }
}
