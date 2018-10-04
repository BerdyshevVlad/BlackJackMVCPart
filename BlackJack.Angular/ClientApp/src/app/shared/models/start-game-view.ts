export interface StartGameView {
  Players: PlayerGameViewItem[];
}


export interface PlayerGameViewItem {
  Id: number;
  Name: string;
  PlayerType: string;
  GameNumber: number;
  Score: number;
  Round: number;
  Win: boolean;
  CardList: CardViewItem[];
}


export interface CardViewItem {
  Id: number;
  Value: number;
}

export interface SetNameAndBotCount {
  UserName: string,
  BotCount: number;
}
