import { HttpResponse } from "@angular/common/http";
import { inject, Injectable, OnDestroy } from "@angular/core";
import { ReplaySubject, Observable } from "rxjs";

import { ApiService } from "@core/api";
import { LogLevel, LogService } from "@core/logger";
import { CreateNewGameResult, GetActiveGameResult } from "@guesser/game";
import { GameState } from "@guesser/models";

@Injectable({
  providedIn: "root"
})
export class GameService implements OnDestroy {
  // #region Services
  private _api: ApiService = inject(ApiService);
  private _logger: LogService = inject(LogService);
  // #endregion

  private _endpoint: string = "api/game";

  private _gameReadySrc: ReplaySubject<GameState> = new ReplaySubject<GameState>(1);
  public gameReady$: Observable<GameState> = this._gameReadySrc.asObservable();

  public ngOnDestroy(): void {
    this._gameReadySrc.complete();
  }

  public createNewGame(pUserId: string): void {
    this._logger.log(`Creating game for ${pUserId}.`, LogLevel.DEBUG);
    this._api.post<CreateNewGameResult>(this._endpoint, undefined, { "userId": pUserId }).subscribe({
      next: (resp: HttpResponse<CreateNewGameResult>): void => this._onCreateNewGameOk(resp, pUserId)
    });
  }

  public loadOrCreateGame(pUserId: string): void {
    this._logger.log(`Getting game state for ${pUserId}.`, LogLevel.DEBUG);
    this._api.get<GetActiveGameResult>(this._endpoint, undefined, { "userId": pUserId }).subscribe({
      next: (resp: HttpResponse<GetActiveGameResult>): void => this._onLoadOrCreateGameOk(resp, pUserId),
      error: (resp: HttpResponse<GetActiveGameResult>): void => this._onLoadOrCreateGameErr(resp, pUserId)
    });
  }

  // #region CreateNewGame
  private _onCreateNewGameOk(pResp: HttpResponse<CreateNewGameResult>, pUserId: string): void {
    if (!pResp.body) {
      return;
    }

    if (pResp.body) {
      this._gameReadySrc.next(pResp.body.game);
    }
  }
  // #endregion

  // #region LoadOrCreateGame
  private _onLoadOrCreateGameOk(pResp: HttpResponse<GetActiveGameResult>, pUserId: string): void {
    if (!pResp.body) {
      return;
    }

    if (pResp.body) {
      this._gameReadySrc.next(pResp.body.game);
    }
  }

  private _onLoadOrCreateGameErr(pResp: HttpResponse<GetActiveGameResult>, pUserId: string): void {
    this._logger.log(`${pResp.status}: Failed to find game state for ${pUserId}.`, LogLevel.WARN);
    if (pResp.status === 400 || pResp.status === 404) {
      this.createNewGame(pUserId);
    }
  }
  // #endregion
}
