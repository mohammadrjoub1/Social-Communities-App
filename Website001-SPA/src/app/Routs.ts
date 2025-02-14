import {RouterModule, Routes} from '@angular/router';
import { CategorieCreateComponent } from './Categories/categorie-create/categorie-create.component';
import { CategorieComponent } from './Categories/categorie/categorie.component';
import { CategoriesListComponent } from './Categories/categories-list/categories-list.component';
import { HomeComponent } from './home/home.component';
import { postDetailedResolver } from './post-detailed.resolver';
import { AllComponent } from './posts/all/all.component';
import { PostAddComponent } from './posts/post-add/post-add.component';
 import { PostDetailedComponent } from './posts/post-detailed/post-detailed.component';
import { PostListComponent } from './posts/post-list/post-list.component';
import { RegisterUserComponent } from './users/register-user/register-user.component';
import { UserComponent } from './users/user/user.component';

export const routs:Routes=[
    
    {path:'',component:HomeComponent},
    {path:'all',component:AllComponent},
    
    {path:'categories',children:[
        {path:'create',component:CategorieCreateComponent},
        {path:'',component:CategoriesListComponent},
        {path:':title',component:CategorieComponent},
        {path:":title'/post/:id",component:PostDetailedComponent,resolve:{post:postDetailedResolver}}
        
    ]},

    {path:'user',children:[
        {path:'register',component:RegisterUserComponent},
        {path:':username',component:UserComponent}
    ]
    },
    {path:'posts',runGuardsAndResolvers:'always',
    children:[

        {path:'',component:PostListComponent} ,
        {path:'add/:title',component:PostAddComponent},
        {path:':id',component:PostDetailedComponent,resolve:{post:postDetailedResolver}}
    ]}

    ,    {path:'**',redirectTo:'',pathMatch:'full'} 
]