import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {RouterModule, Routes} from '@angular/router';

import { AppComponent } from './app.component';
import { ErrorInterceptorProvider } from './services/error.interceptor';
import {HttpClientModule} from '@angular/common/http/';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './services/auth.service';
import { PostListComponent } from './posts/post-list/post-list.component';
import { PostService } from './services/post.service';
import { PostDetailedComponent } from './posts/post-detailed/post-detailed.component';
import { HomeComponent } from './home/home.component';
import { routs } from './Routs';
import { postDetailedResolver } from './post-detailed.resolver';
import { AlertifyService } from './services/alertify.service';
import { UserService } from './services/user.service';
import { JwtModule } from '@auth0/angular-jwt';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatSelectModule} from '@angular/material/select';
import { UserComponent } from './users/user/user.component';
import { PostCardComponent } from './posts/post-card/post-card.component'; 
import { CategorieService } from './services/categorie.service';
import { CategorieComponent } from './Categories/categorie/categorie.component';
import { CategoriesListComponent } from './Categories/categories-list/categories-list.component';
import { CategorieCardComponent } from './Categories/categorie-card/categorie-card.component';
import { PostAddComponent } from './posts/post-add/post-add.component';
import { ReactionComponent } from './reaction/reaction.component';
import { TimeagoModule } from 'ngx-timeago';


import {MatMenuModule} from '@angular/material/menu'; 

import {MatProgressSpinnerModule} from '@angular/material/progress-spinner'; 
import { ImageUploadModule } from "angular2-image-upload";
import {MatButtonToggleModule} from '@angular/material/button-toggle'; 
import {MatTabsModule} from '@angular/material/tabs'; 
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { FileUploadModule } from 'ng2-file-upload';
import { AddCommentCardComponent } from './comments/add-comment-card/add-comment-card.component';
import { CommentCardComponent } from './comments/comment-card/comment-card.component';
import { CategorieCreationPanelComponent } from './Categories/categorie-creation-panel/categorie-creation-panel.component';
import { CategorieCreateComponent } from './Categories/categorie-create/categorie-create.component';
import { RegisterUserComponent } from './users/register-user/register-user.component';
import { AllComponent } from './posts/all/all.component';
import { UserInfoCardComponent } from './users/user-info-card/user-info-card.component';


import {MatExpansionModule} from '@angular/material/expansion';
import { CategoriesTopListComponent } from './Categories/categories-top-list/categories-top-list.component'; 

export function tokenGetter() {
  return localStorage.getItem("token");
}

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    PostListComponent,
    PostDetailedComponent,
    HomeComponent,
    CategorieComponent,
    UserComponent,
    PostCardComponent,
    CategoriesListComponent,
    CategorieCardComponent,
    PostAddComponent,
    ReactionComponent,
    AddCommentCardComponent,
    CommentCardComponent,
    CategorieCreationPanelComponent,
    CategorieCreateComponent,
    RegisterUserComponent,
    AllComponent,
    UserInfoCardComponent,
    CategoriesTopListComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatTabsModule,
    FileUploadModule,
    MatProgressSpinnerModule,
    TimeagoModule.forRoot(),
    MatSelectModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5000"],
        disallowedRoutes: ["http://adasda.com"],
      },
    }),
    RouterModule.forRoot(routs),
    BrowserAnimationsModule,
    MatExpansionModule,
    InfiniteScrollModule,
    MatButtonToggleModule,
    MatMenuModule,
    ImageUploadModule.forRoot(),
  ],
  providers: [
    ErrorInterceptorProvider,
    UserService,
    AuthService,
    PostService,
    AlertifyService,
    postDetailedResolver,
    CategorieService],
  bootstrap: [AppComponent]
})
export class AppModule { }
