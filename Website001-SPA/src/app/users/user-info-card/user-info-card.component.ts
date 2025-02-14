import { Component, Input, OnInit } from '@angular/core';
import { profilePictureDto } from 'src/app/Dtos/profilePictureDto';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-info-card',
  templateUrl: './user-info-card.component.html',
  styleUrls: ['./user-info-card.component.css']
})
export class UserInfoCardComponent implements OnInit {
   @Input() user:any={};
   profilePictureDto:any={};
   showSpiner=false;
  constructor(private userService:UserService,public authService:AuthService) { }

  ngOnInit() {
  
  }

  onFilesSelected(event){
    this.profilePictureDto.userId=this.user.id;
    this.profilePictureDto.file=event.target.files[0];
      console.log(  this.profilePictureDto.file+"  this.data.file");
      this.showSpiner=true;

    this.userService.addUserProfilePicture(this.profilePictureDto).subscribe(
      next=>{
        this.user.imageUrl=next;
        this.showSpiner=false; }
    )
  }

}
