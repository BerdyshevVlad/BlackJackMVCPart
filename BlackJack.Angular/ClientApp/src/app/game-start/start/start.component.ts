import { Component } from '@angular/core';
import { HttpService } from "../../shared/services/http.service";
import { StartGameView } from "../../shared/models/start-game-view";
import {SetNameAndBotCount} from "../../shared/models/start-game-view";
import {MoreGameView} from "../../shared/models/more-game-view";
import {EnoughGameView} from "../../shared/models/enough-game-view";
import {PlayerGameViewItem} from "../../shared/models/start-game-view";
import { Observable } from 'rxjs';
//import * as $ from 'jquery';
declare var $: any;

@Component({
  selector: 'app-start',
  templateUrl: './start.component.html',
  styleUrls: ['./start.component.css']
})
export class StartComponent {

  userNameAndBotCount: SetNameAndBotCount;
  startView: StartGameView;
  moreView: MoreGameView;
  enoughView: EnoughGameView;
  winnerList: PlayerGameViewItem[];
  playerList: PlayerGameViewItem[];
  winMaxValue:number=21;
  botCount: number;
  userName: string;
  gameIsRunning: boolean;
  endOfGame: boolean;

  constructor(private http: HttpService) {
    var header = document.getElementById("header");
    header.innerHTML = "GAME IS STARTED";
    this.endOfGame = false;
  }

  start() {

    if (this.userName == null  || this.botCount > 5 || this.botCount<1 ) {

      var wrongData = document.getElementById('wrongData');
      wrongData.innerHTML = "Enter a correct data";
      wrongData.style.color = "red";
      return;
    }


    this.userNameAndBotCount = {
      UserName:this.userName,
      BotCount:this.botCount
    }

    this.http.postStart(this.userNameAndBotCount).subscribe(result => {
      this.startView = result;
      this.playerList = this.startView.Players;
      this.gameIsRunning = true;
    });
    var startInputGroup = document.getElementById("startInputGroup");
    startInputGroup.hidden = true;
  }

  more() {
    this.http.getMore().subscribe(result => {
      this.moreView = result;
      this.playerList = this.moreView.Players;

      this.winnerList = this.playerList.filter(x => x.Win == true);
      if (this.winnerList.length != 0) {
        console.log(this.winnerList);
        $('#exampleModal').modal('show');
      }
    });
  }


  enough() {
    this.http.getEnough().subscribe(result => {
      this.enoughView = result;
      this.playerList = this.enoughView.Players;

      this.winnerList = this.playerList.filter(x => x.Win == true);
      if (this.winnerList.length !=0) {
        console.log(this.winnerList);
        $('#exampleModal').modal('show');
      }
    });
  }


}
