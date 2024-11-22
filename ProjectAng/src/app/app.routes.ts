import { Routes, RouterModule } from '@angular/router';
import { SignInComponent } from './sign-in/sign-in.component';
import { GoodsesTableComponent } from './goodses-table/goodses-table.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { GoodsesTableCreateEditComponent } from './goodses-table-create-edit/goodses-table-create-edit.component';
import { StatisticsComponent } from './statistics/statistics.component';

export const routes: Routes = [
    {path: '', component: GoodsesTableComponent},
    {path: 'goodses', redirectTo: '', pathMatch: 'full'},
    {path: 'signin', component: SignInComponent },
    {path: 'signup', component: SignUpComponent },
    {path: 'create-edit-goodses', component: GoodsesTableCreateEditComponent },
    {path: 'editGoods/:id', component: GoodsesTableCreateEditComponent },
    {path: 'statistics', component: StatisticsComponent},
    
];
