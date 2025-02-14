export interface CommentDto{
      id:number;
      userId:number;
      postId:number;
      categorieId:number;
      comment:string;
      date:any;
      prime:boolean;
      parentCommentId:number;
} 