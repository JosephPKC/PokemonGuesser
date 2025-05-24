import { HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { ReplaySubject, Observable } from "rxjs";

import { LogService } from "@core/logger";
import { CreateNewGameResult, GameState, GetActiveGameResult } from "@guesser/game/models";
import { GameApiService, GameStateService } from "@guesser/game/services";

@Injectable({
  providedIn: "root"
})
export class GameProcService {
  // #region Services
  private _logger: LogService = inject(LogService);
  private _gameApi: GameApiService = inject(GameApiService);
  private _gameState: GameStateService = inject(GameStateService);
  // #endregion

  private _gameReadySrc: ReplaySubject<GameState> = new ReplaySubject<GameState>(1);
  public gameReady$: Observable<GameState> = this._gameReadySrc.asObservable();

  public createNewGame(pUserId: string): void {
    this._logger.log(`Creating game for ${pUserId}.`);
    this._gameApi.createNewGame(pUserId).subscribe({
      next: (resp: HttpResponse<CreateNewGameResult>): void => this._onCreateNewGameOk(resp, pUserId)
    });
  }

  public loadOrCreateGame(pUserId: string): void {
    this._logger.log(`Getting game state for ${pUserId}.`);
    this._gameApi.getGame(pUserId).subscribe({
      next: (resp: HttpResponse<GetActiveGameResult>): void => this._onLoadOrCreateGameOk(resp, pUserId),
      error: (resp: HttpResponse<GetActiveGameResult>): void => this._onLoadOrCreateGameErr(resp, pUserId)
    });
  }

  private _onCreateNewGameOk(pResp: HttpResponse<CreateNewGameResult>, pUserId: string): void {
    if (!pResp.body) {
      return;
    }

    if (pResp.body) {
      this._gameState.gameState = pResp.body.game;
      this._gameReadySrc.next(pResp.body.game);
    }
  }

  private _onLoadOrCreateGameOk(pResp: HttpResponse<GetActiveGameResult>, pUserId: string): void {
    if (!pResp.body) {
      return;
    }

    if (pResp.body) {
      this._gameState.gameState = pResp.body.game;
      this._gameReadySrc.next(pResp.body.game);
    }
  }

  private _onLoadOrCreateGameErr(pResp: HttpResponse<GetActiveGameResult>, pUserId: string): void {
    this._logger.log(`${pResp.status}: Failed to find game state for ${pUserId}.`);
    if (pResp.status === 404) {
      this.createNewGame(pUserId);
    }
  }
}
