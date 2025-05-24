import { HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { ReplaySubject, Observable } from "rxjs";

import { LogService } from "@core/logger";
import { GuessInput, ProcessGuessResult } from "@guesser/guess/models";
import { GuessApiService } from "@guesser/guess/services";

@Injectable({
  providedIn: "root"
})
export class GuessProcService {
  // #region Services
  private _logger: LogService = inject(LogService);
  private _guessApi: GuessApiService = inject(GuessApiService);
  // #endregion

  private _processGuessReadySrc: ReplaySubject<ProcessGuessResult> = new ReplaySubject<ProcessGuessResult>(1);
  public processGuessReady$: Observable<ProcessGuessResult> = this._processGuessReadySrc.asObservable();

  public processGuess(pUserId: string, pGuess: GuessInput): void {
    this._logger.log(`Processing guess ${pGuess.guess} for ${pUserId}.`);

    this._guessApi.processGuess(pUserId, pGuess).subscribe({
      next: (resp: HttpResponse<ProcessGuessResult>): void => this._onLoadOrCreateGameOk(resp, pUserId, pGuess)
    });
  }

  private _onLoadOrCreateGameOk(pResp: HttpResponse<ProcessGuessResult>, pUserId: string, pGuess: GuessInput): void {
    if (!pResp.body) {
      return;
    }

    this._logger.log(`Processed guess ${pGuess.guess} for ${pUserId}: ${pResp.body.result}.`);
    this._processGuessReadySrc.next(pResp.body);
  }
}
