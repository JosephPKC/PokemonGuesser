import { HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { ReplaySubject, Observable } from "rxjs";

import { LogService } from "@core/logger";
import { GameState } from "@guesser/game/models";
import { HintInput, HintResultTypes, HintTypes, RevealHintResult } from "@guesser/hint/models";
import { HintApiService } from "@guesser/hint/services";

@Injectable({
  providedIn: "root"
})
export class HintProcService {
  // #region Services
  private _logger: LogService = inject(LogService);
  private _hintApi: HintApiService = inject(HintApiService);
  // #endregion

  private _revealHintReadySrc: ReplaySubject<GameState> = new ReplaySubject<GameState>(1);
  public revealHintReady$: Observable<GameState> = this._revealHintReadySrc.asObservable();

  public revealHint(pUserId: string, pHint: HintInput): void {
    this._logger.log(`Revealing hint ${pHint.moveId} / ${pHint.hintType}, for ${pUserId}.`);


    this._hintApi.revealHint(pUserId, pHint).subscribe({
      next: (resp: HttpResponse<RevealHintResult>): void => this.onRevealHintOk(resp, pUserId, pHint),
      error: (resp: HttpResponse<RevealHintResult>): void => this.onRevealHintErr(resp, pUserId, pHint)
    });
  }

  private onRevealHintOk(pResp: HttpResponse<RevealHintResult>, pUserId: string, pHint: HintInput): void {
    if (!pResp.body) {
      return;
    }

    if (pResp.body.result !== HintResultTypes.REVEALED) {
      this._logger.log(`Invalid usage: Hint ${pHint.moveId} / ${pHint.hintType} is already answered or revealed for user ${pUserId}.`);
    }
    else {
      this._revealHintReadySrc.next(pResp.body.newState);
    }
  }

  private onRevealHintErr(pResp: HttpResponse<RevealHintResult>, pUserId: string, pHint: HintInput): void {
    if (pResp.status === 400) {
      this._logger.log(`400 Bad Request: Move ${pHint.moveId} has no hint of type ${pHint.hintType}.`);
    }

    if (pResp.status === 404) {
      this._logger.log(`404 Not Found: Move ${pHint.moveId} or user ${pUserId} not found.`);
    }
  }
}
