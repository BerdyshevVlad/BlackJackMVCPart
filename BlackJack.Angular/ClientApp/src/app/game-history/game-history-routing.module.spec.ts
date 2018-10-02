import { GameHistoryRoutingModule } from './game-history-routing.module';

describe('GameHistoryRoutingModule', () => {
  let gameHistoryRoutingModule: GameHistoryRoutingModule;

  beforeEach(() => {
    gameHistoryRoutingModule = new GameHistoryRoutingModule();
  });

  it('should create an instance', () => {
    expect(gameHistoryRoutingModule).toBeTruthy();
  });
});
