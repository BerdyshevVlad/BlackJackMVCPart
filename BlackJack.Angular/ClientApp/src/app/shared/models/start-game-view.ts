export interface StartGameView {
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

export interface SetNameAndBotCount {
  name: string,
  botCount: number;
}
