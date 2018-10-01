import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { HttpModule } from '@angular/http';
import {environment} from "../environments/environment";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  ngOnInit(): Observable<any> {
    return null;
  }

  title = 'ClientApp';
  public history: HistoryGameView;
  constructor(private http: HttpClient) {

  }

  public getHistory() {
    this.http.get<HistoryGameView>({ environment }.environment.baseUrl + "/history")
      .subscribe(result => {
        console.log(result);
        this.history = result;
      });
  }
}

export interface HistoryGameView {
  playerList: PlayerGameViewItem[];
}


export interface PlayerGameViewItem {
  id: number;
  name: string;
  layerType: string;
  gameNumber: number;
  score: number;
  round: number;
  cardList: CardViewItem[];
}


export interface CardViewItem {
  id: number;
  value: number;
}
