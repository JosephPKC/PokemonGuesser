import {
  Injectable, Signal, signal, WritableSignal
} from "@angular/core";

import { GameState } from "@guesser/game/models";

@Injectable({
  providedIn: "root"
})
export class GameStateService {
  // #region GameState
  private _gameState: WritableSignal<GameState | null> = signal<GameState | null>(null);

  public get gameState(): Signal<GameState | null> {
    return this._gameState.asReadonly();
  }

  public set gameState(pGameState: GameState) {
    this._gameState.set(pGameState);
  }
  // #endregion

  public isGameReady(): boolean {
    return this._gameState() !== null;
  }
}
