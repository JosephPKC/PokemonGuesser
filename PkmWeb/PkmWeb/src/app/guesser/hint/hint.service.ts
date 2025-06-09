import { HttpResponse } from "@angular/common/http";
import { inject, Injectable, OnDestroy } from "@angular/core";
import { ReplaySubject, Observable } from "rxjs";

import { ApiService } from "@core/api";
import { LogLevel, LogService } from "@core/logger";
import { HintInput, HintResultTypes, RevealHintResult } from "@guesser/hint";
import { GameState } from "@guesser/models";

@Injectable({
  providedIn: "root"
})
export class HintService implements OnDestroy {
  // #region Services
  private _api: ApiService = inject(ApiService);
  private _logger: LogService = inject(LogService);
  // #endregion

  private _endpoint: string = "api/hint";

  private _revealHintReadySrc: ReplaySubject<GameState> = new ReplaySubject<GameState>(1);
  public revealHintReady$: Observable<GameState> = this._revealHintReadySrc.asObservable();

  public ngOnDestroy(): void {
    this._revealHintReadySrc.complete();
  }

  public revealHint(pUserId: string, pHint: HintInput): void {
    this._logger.log(`Revealing hint ${pHint.moveId} / ${pHint.hintType}, for ${pUserId}.`, LogLevel.DEBUG);

    this._api.put<HintInput, RevealHintResult>(this._endpoint, pHint, undefined, { "userId": pUserId }).subscribe({
      next: (resp: HttpResponse<RevealHintResult>): void => this.onRevealHintOk(resp, pUserId, pHint),
      error: (resp: HttpResponse<RevealHintResult>): void => this.onRevealHintErr(resp, pUserId, pHint)
    });
  }

  // #region RevealHint
  private onRevealHintOk(pResp: HttpResponse<RevealHintResult>, pUserId: string, pHint: HintInput): void {
    if (!pResp.body) {
      return;
    }

    if (pResp.body.result !== HintResultTypes.REVEALED) {
      this._logger.log(`Invalid usage: Hint ${pHint.moveId} / ${pHint.hintType} is already answered or revealed for user ${pUserId}.`, LogLevel.DEBUG);
    }
    else {
      this._revealHintReadySrc.next(pResp.body.newState);
    }
  }

  private onRevealHintErr(pResp: HttpResponse<RevealHintResult>, pUserId: string, pHint: HintInput): void {
    if (pResp.status === 400) {
      this._logger.log(`400 Bad Request: Move ${pHint.moveId} has no hint of type ${pHint.hintType}.`, LogLevel.WARN);
    }

    if (pResp.status === 404) {
      this._logger.log(`404 Not Found: Move ${pHint.moveId} or user ${pUserId} not found.`, LogLevel.WARN);
    }
  }
  // #endregion
}
