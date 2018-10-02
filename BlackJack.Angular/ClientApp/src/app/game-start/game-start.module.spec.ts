import { GameStartModule } from './game-start.module';

describe('GameStartModule', () => {
  let gameStartModule: GameStartModule;

  beforeEach(() => {
    gameStartModule = new GameStartModule();
  });

  it('should create an instance', () => {
    expect(gameStartModule).toBeTruthy();
  });
});
