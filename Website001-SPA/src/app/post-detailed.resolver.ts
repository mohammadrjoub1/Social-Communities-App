import { Injectable } from "@angular/core";
import { ActivatedRoute, ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { error } from "selenium-webdriver";
import { Post } from "./Models/Post.interface";
import { PostService } from "./services/post.service";

@Injectable()
export class postDetailedResolver implements Resolve<Post>{
    constructor(private postService:PostService){}
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Post> {
        return this.postService.getPost(route.params['id']).pipe(
            catchError(error=>{
                console.log(error);
                 return of(null);
            })
        );
    }
}
