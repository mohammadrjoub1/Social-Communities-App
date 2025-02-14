import { Author } from "./Author.interface";
import { Categorie } from "./Categorie.interface";

export interface Post{

    id:number;
    title:string;
    content:string;
    authorId:number;
    imageUrl:string;
    postTypeId:number;
    date:Date;
    categorieId:number;
    Author:Author;
    author:Author;
    Categorie:Categorie;
    categorie:Categorie;


}
