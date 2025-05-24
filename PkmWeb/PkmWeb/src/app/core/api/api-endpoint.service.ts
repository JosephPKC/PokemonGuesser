import { Injectable } from "@angular/core";

import { environment } from "@env/environment";

enum EndpointTypes {
  GAME,
  GUESS,
  HINT,
  STATS,
  USER
}

@Injectable({
  providedIn: "root"
})
export class ApiEndpointService {
  private _endpoints: Record<EndpointTypes, string> = {
    [EndpointTypes.GAME]: "/api/game",
    [EndpointTypes.GUESS]: "/api/guess",
    [EndpointTypes.HINT]: "/api/hint",
    [EndpointTypes.STATS]: "/api/stats",
    [EndpointTypes.USER]: "/api/user"
  };

  public getGameEndpoint(): string {
    return this._getFullUrl(this._endpoints[EndpointTypes.GAME]);
  }

  public getGuessEndpoint(): string {
    return this._getFullUrl(this._endpoints[EndpointTypes.GUESS]);
  }

  public getHintEndpoint(): string {
    return this._getFullUrl(this._endpoints[EndpointTypes.HINT]);
  }

  public getStatsEndpoint(): string {
    return this._getFullUrl(this._endpoints[EndpointTypes.STATS]);
  }

  public getUserEndpoint(): string {
    return this._getFullUrl(this._endpoints[EndpointTypes.USER]);
  }

  private _getFullUrl(pEndpoint: string): string {
    return environment.baseApiUrl + pEndpoint;
  }
}
