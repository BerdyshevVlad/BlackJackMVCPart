import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StartComponent } from './start/start.component';
import { GameStartRoutingModule } from "./game-start-routing.module";
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    GameStartRoutingModule
  ],
  declarations: [StartComponent]
})
export class GameStartModule { }
