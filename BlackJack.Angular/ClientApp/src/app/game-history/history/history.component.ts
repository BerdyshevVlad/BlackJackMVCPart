import { Component } from '@angular/core';
import {HistoryGameView} from "../../shared/models/history-game-view";
import {HttpService} from "../../shared/services/http.service";

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent {

  public historyView: HistoryGameView;


  constructor(private http: HttpService) {
    this.http.getHistory().subscribe(result => {
      this.historyView = result;

      console.log(this.historyView);
    });

    var header = document.getElementById("header");
    header.innerHTML = "GAME HISTORY";
  }

}
