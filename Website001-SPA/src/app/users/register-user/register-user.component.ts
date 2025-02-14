import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  registerForm:FormGroup;
  constructor(private route:Router,private userService:UserService,private alertify:AlertifyService) { }

  userDto:any={};
  ngOnInit() {
    this.registerForm = new FormGroup(
      {
        username:new FormControl('',[Validators.required,Validators.maxLength(120),Validators.minLength(5)]),
        email:new FormControl('',[Validators.required,Validators.maxLength(120),Validators.minLength(5),Validators.email]),
        password:new FormControl('',[Validators.required,Validators.maxLength(200),Validators.minLength(8)]),
        confirmPassword:new FormControl('',[Validators.required])
       
      },this.passwordIdenticalValidation
    );
   
  }
  passwordIdenticalValidation(g:FormGroup){
    return g.get('confirmPassword').value===g.get('password').value ? null : {"mismatch":true};
  }
  register(){
    this.userDto.username=this.registerForm.get('username').value;
    this.userDto.email=this.registerForm.get('email').value;
    this.userDto.password=this.registerForm.get('password').value;
     this.userService.register(this.userDto).subscribe(
      next=>{
        this.alertify.success("Registerd Successfully");
        this.route.navigate(['/']);

      },
      error=>{
        this.alertify.error(error);
      }
    );
  }
 
}
