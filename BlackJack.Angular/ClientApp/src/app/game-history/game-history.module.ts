import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HistoryComponent } from './history/history.component';
import { GameHistoryRoutingModule } from "./game-history-routing.module";

@NgModule({
  imports: [
    CommonModule,
    GameHistoryRoutingModule
  ],
  declarations: [HistoryComponent]
})
export class GameHistoryModule { }
