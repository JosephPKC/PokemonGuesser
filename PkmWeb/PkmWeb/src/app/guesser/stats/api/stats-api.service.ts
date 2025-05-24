import { HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { ApiEndpointService, ApiService } from "@core/api";
import { GetStatsResult } from "@guesser/stats/models";

@Injectable({
  providedIn: "root"
})
export class StatsApiService {
  // #region Services
  private _api: ApiService = inject(ApiService);
  private _endpoint: ApiEndpointService = inject(ApiEndpointService);
  // #endregion

  public getStats(pUserId: string): Observable<HttpResponse<GetStatsResult>> {
    return this._api.get(this._endpoint.getStatsEndpoint(), undefined, pUserId);
  }
}
