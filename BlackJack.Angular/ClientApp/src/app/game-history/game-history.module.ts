import { NgModule } from '@angular/core';
import { HistoryComponent } from './history/history.component';
import { GameHistoryRoutingModule } from "./game-history-routing.module";
import {SharedModule}  from"../shared/modules/shared.module";

@NgModule({
  imports: [
    SharedModule.forRoot(),
    GameHistoryRoutingModule,
  ],
  declarations: [HistoryComponent]
})
export class GameHistoryModule { }
