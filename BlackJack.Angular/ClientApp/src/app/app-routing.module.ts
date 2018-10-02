import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {Routes,RouterModule} from '@angular/router'

const routes: Routes = [
  {
    path: 'history',
    loadChildren: './game-history/game-history.module#GameHistoryModule'
  },
  {
    path: 'start',
    loadChildren: './game-start/game-start.module#GameStartModule'
  },
  {
    path: '',
    redirectTo: '',
    pathMatch: 'full'
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }
