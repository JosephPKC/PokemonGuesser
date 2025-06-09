import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { LogService } from "@core/logger";
import { environment } from "@env/environment";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  // #region Services
  private _http: HttpClient = inject(HttpClient);
  private _logger: LogService = inject(LogService);
  // #endregion

  // #region GET
  public get<TOutput>(pContext: string, pId?: string, pParams?: Record<string, string>): Observable<HttpResponse<TOutput>> {
    const url: string = this._getUrl(pContext, pId);
    const params: HttpParams = this._getParams(pParams);

    this._logger.log(`GET ${url}.`);

    return this._http.get<TOutput>(url,
      {
        params: params,
        observe: "response"
      }
    );
  }
  // #endregion

  // #region POST
  public post<TOutput>(pContext: string, pId?: string, pParams?: Record<string, string>): Observable<HttpResponse<TOutput>> {
    const url: string = this._getUrl(pContext, pId);
    const params: HttpParams = this._getParams(pParams);

    this._logger.log(`POST ${url}.`);

    return this._http.post<TOutput>(url, null,
      {
        params: params,
        observe: "response"
      }
    );
  }
  // #endregion

  // #region PUT
  public put<TInput, TOutput>(pContext: string, pItem: TInput, pId?: string, pParams?: Record<string, string>): Observable<HttpResponse<TOutput>> {
    const url: string = this._getUrl(pContext, pId);
    const params: HttpParams = this._getParams(pParams);

    this._logger.log(`PUT ${url}.`);

    return this._http.put<TOutput>(url, pItem,
      {
        params: params,
        observe: "response"
      }
    );
  }
  // #endregion

  private _getUrl(pContext: string, pId?: string): string {
    let url: string = `${environment.baseApiUrl}/${pContext}`;

    if (pId) {
      url += `/${pId}`;
    }

    return url;
  }

  private _getParams(pParams?: Record<string, string>): HttpParams {
    let params: HttpParams = new HttpParams();
    
    if (pParams === undefined) {
      return params;
    }

    for (let key in pParams) {
      params = params.set(key, pParams[key]);
    }

    return params;
  }
}
