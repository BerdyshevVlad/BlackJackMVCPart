import { Component } from '@angular/core';
import {HistoryGameView} from "../../shared/models/history-game-view";
import {HttpService} from "../../shared/services/http.service";
import { GridDataResult, PageChangeEvent } from '@progress/kendo-angular-grid';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent {

  public historyView: HistoryGameView;
  public gridView: GridDataResult;
  public pageSize = 10;
  public skip = 0;
  private data: Object[];

  constructor(private http: HttpService) {
    this.http.getHistory().subscribe(result => {
      this.historyView = result;
      this.loadItems();
    });

    var header = document.getElementById("header");
    header.innerHTML = "GAME HISTORY";
  }


  public pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.loadItems();
  }

  private loadItems(): void {
    this.gridView = {
      data: this.historyView.Players.slice(this.skip, this.skip + this.pageSize),
      total: this.historyView.Players.length
    };
  }

}
