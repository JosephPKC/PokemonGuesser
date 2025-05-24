import { HttpClient, HttpParams, HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LogService } from "@core/logger";

export type HttpHeader = Record<string, string | string[]> | undefined;

@Injectable({
  providedIn: "root"
})
export class ApiService {
  private _logger: LogService = inject(LogService);
  private _http: HttpClient = inject(HttpClient);

  public get<TOutput>(pContext: string, pId?: string, pUserId?: string): Observable<HttpResponse<TOutput>> {
    const url: string = this._getUrl(pContext, pId);
    const params: HttpParams = this._getParams(pUserId);

    this._logger.log(`GET ${url}, user-id: ${pUserId}.`);

    return this._http.get<TOutput>(url,
      {
        params: params,
        observe: "response"
      }
    );
  }

  public getUser<TOutput>(pContext: string, pUserId?: string): Observable<HttpResponse<TOutput>> {
    const url: string = this._getUrl(pContext, pUserId);

    this._logger.log(`GET ${url}, user-id: ${pUserId}.`);

    return this._http.get<TOutput>(url,
      {
        observe: "response"
      }
    );
  }

  public post<TOutput>(pContext: string, pId?: string, pUserId?: string): Observable<HttpResponse<TOutput>> {
    const url: string = this._getUrl(pContext, pId);
    const params: HttpParams = this._getParams(pUserId);

    this._logger.log(`POST ${url}, user-id: ${pUserId}.`);

    return this._http.post<TOutput>(url, null,
      {
        params: params,
        observe: "response"
      }
    );
  }

  public put<TInput, TOutput>(pContext: string, pItem: TInput, pId?: string, pUserId?: string): Observable<HttpResponse<TOutput>> {
    const url: string = this._getUrl(pContext, pId);
    const params: HttpParams = this._getParams(pUserId);

    this._logger.log(`PUT ${url}, user-id: ${pUserId}.`);

    return this._http.put<TOutput>(url, pItem,
      {
        params: params,
        observe: "response"
      }
    );
  }

  private _getUrl(pContext: string, pId?: string): string {
    let url: string = `${pContext}/`;

    if (pId) {
      url += `${pId}/`;
    }

    return url;
  }

  private _getParams(pUserId?: string): HttpParams {
    let params: HttpParams = new HttpParams();

    if (pUserId) {
      params = params.set("userId", pUserId);
    }

    return params;
  }
}
