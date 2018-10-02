import { Component } from '@angular/core';
import { HttpService } from "../../shared/services/http.service";
import { StartGameView } from "../../shared/models/start-game-view";
import {SetNameAndBotCount} from "../../shared/models/start-game-view";

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.css']
})
export class StartComponent {

  userNameAndBotCount: SetNameAndBotCount;
  startView:StartGameView;
  botCount: number;
  userName:string;

  constructor(private http: HttpService) {
    var header = document.getElementById("header");
    header.innerHTML = "GAME IS STARTED";
  }

  start() {
    //this.userNameAndBotCount.botCount = this.botCount;
    //this.userNameAndBotCount.name = this.userName;
    this.userNameAndBotCount = {
      name:this.userName,
      botCount:this.botCount
    }
    this.http.postStart(this.userNameAndBotCount).subscribe(result => {
      this.startView = result;
      console.log(this.start);
    });
  }
}
