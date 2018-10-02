import { GameStartRoutingModule } from './game-start-routing.module';

describe('GameStartRoutingModule', () => {
  let gameStartRoutingModule: GameStartRoutingModule;

  beforeEach(() => {
    gameStartRoutingModule = new GameStartRoutingModule();
  });

  it('should create an instance', () => {
    expect(gameStartRoutingModule).toBeTruthy();
  });
});
