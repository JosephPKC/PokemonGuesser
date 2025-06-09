import { HttpResponse } from "@angular/common/http";
import { inject, Injectable, OnDestroy } from "@angular/core";
import { ReplaySubject, Observable } from "rxjs";

import { ApiService } from "@core/api";
import { LogLevel, LogService } from "@core/logger";
import { GuessInput, ProcessGuessResult } from "@guesser/guess";

@Injectable({
  providedIn: "root"
})
export class GuessService implements OnDestroy {
  // #region Services
  private _api : ApiService = inject(ApiService);
  private _logger: LogService = inject(LogService);
  // #endregion

  private _endpoint: string = "api/guess";

  private _processGuessReadySrc: ReplaySubject<ProcessGuessResult> = new ReplaySubject<ProcessGuessResult>(1);
  public processGuessReady$: Observable<ProcessGuessResult> = this._processGuessReadySrc.asObservable();

  public ngOnDestroy(): void {
    this._processGuessReadySrc.complete();
  }

  public processGuess(pUserId: string, pGuess: GuessInput): void {
    this._logger.log(`Processing guess ${pGuess.guess} for ${pUserId}.`, LogLevel.DEBUG);

    this._api.put<GuessInput, ProcessGuessResult>(this._endpoint, pGuess, undefined, { "userId": pUserId }).subscribe({
      next: (resp: HttpResponse<ProcessGuessResult>): void => this._onProcessGuessOk(resp, pUserId, pGuess)
    });
  }

  // #region ProcsesGuess
  private _onProcessGuessOk(pResp: HttpResponse<ProcessGuessResult>, pUserId: string, pGuess: GuessInput): void {
    if (!pResp.body) {
      return;
    }

    this._logger.log(`Processed guess ${pGuess.guess} for ${pUserId}: ${pResp.body.result}.`, LogLevel.DEBUG);
    this._processGuessReadySrc.next(pResp.body);
  }
  // #endregion
}
