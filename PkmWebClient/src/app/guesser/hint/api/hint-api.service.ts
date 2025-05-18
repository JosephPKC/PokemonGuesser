import { HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { ApiEndpointService, ApiService } from "@core/api";
import { HintInput, RevealHintResult } from "@guesser/hint/models";

@Injectable({
  providedIn: "root"
})
export class HintApiService {
  // #region Services
  private _api: ApiService = inject(ApiService);
  private _endpoint: ApiEndpointService = inject(ApiEndpointService);
  // #endregion

  public revealHint(pUserId: string, pHint: HintInput): Observable<HttpResponse<RevealHintResult>> {
    return this._api.put(this._endpoint.getHintEndpoint(), pHint, undefined, pUserId);
  }
}
