import { HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { ApiEndpointService, ApiService } from "@core/api";
import { CreateNewGameResult, GetActiveGameResult } from "@guesser/game/models";

@Injectable({
  providedIn: "root"
})
export class GameApiService {
  // #region Services
  private _api: ApiService = inject(ApiService);
  private _endpoint: ApiEndpointService = inject(ApiEndpointService);
  // #endregion

  public createNewGame(pUserId: string): Observable<HttpResponse<CreateNewGameResult>> {
    return this._api.post(this._endpoint.getGameEndpoint(), undefined, pUserId);
  }

  public getGame(pUserId: string): Observable<HttpResponse<GetActiveGameResult>> {
    return this._api.get(this._endpoint.getGameEndpoint(), undefined, pUserId);
  }
}
