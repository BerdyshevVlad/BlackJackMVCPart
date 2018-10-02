import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from "../../../environments/environment";
import {StartGameView} from "../models/start-game-view";
import {MoreGameView} from "../models/more-game-view";
import {EnoughGameView} from "../models/enough-game-view";
import {SetNameAndBotCount} from "../models/start-game-view";
import {HistoryGameView} from "../models/history-game-view";

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private http: HttpClient) { }

  public postStart(start: SetNameAndBotCount): Observable<StartGameView> {
    return this.http.post<StartGameView>(environment.baseUrl + "/start", start);
  }

  public getMore(): Observable<MoreGameView> {
    return this.http.get<MoreGameView>(environment.baseUrl + "/more");
  }

  public getEnough(): Observable<EnoughGameView> {
    return this.http.get<EnoughGameView>(environment.baseUrl + "/enough");
  }

  public getHistory(): Observable<HistoryGameView> {
    return this.http.get<HistoryGameView>({environment}.environment.baseUrl + "/history");
  }
}
