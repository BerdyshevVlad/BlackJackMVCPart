import { NgModule } from '@angular/core';
import { StartComponent } from './start/start.component';
import { GameStartRoutingModule } from "./game-start-routing.module";
import {SharedModule} from "../shared/modules/shared.module";

@NgModule({
  imports: [
    SharedModule.forRoot(),
    GameStartRoutingModule
  ],
  declarations: [StartComponent]
})
export class GameStartModule { }
