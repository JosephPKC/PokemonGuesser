import { HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { ApiEndpointService, ApiService } from "@core/api";
import { GuessInput, ProcessGuessResult } from "@guesser/guess/models";

@Injectable({
  providedIn: "root"
})
export class GuessApiService {
  // #region Services
  private _api: ApiService = inject(ApiService);
  private _endpoint: ApiEndpointService = inject(ApiEndpointService);
  // #endregion

  public processGuess(pUserId: string, pGuess: GuessInput): Observable<HttpResponse<ProcessGuessResult>> {
    return this._api.put(this._endpoint.getGuessEndpoint(), pGuess, undefined, pUserId);
  }
}
