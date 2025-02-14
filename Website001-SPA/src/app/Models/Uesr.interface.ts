import { Post } from "./Post.interface";

export interface User{
    id:number;
    username:string;
    email:string;
    password:string;
    imageUrl:string;

    posts:Post[];
}