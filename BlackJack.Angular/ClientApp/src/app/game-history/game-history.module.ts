import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HistoryComponent } from './history/history.component';
import { GameHistoryRoutingModule } from "./game-history-routing.module";
import { GridModule } from '@progress/kendo-angular-grid/dist/es2015/main';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';

@NgModule({
  imports: [
    CommonModule,
    GameHistoryRoutingModule,
  GridModule,
  DropDownsModule,
  ],
  declarations: [HistoryComponent]
})
export class GameHistoryModule { }
