import { HttpClient, HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LogService } from "@core/logs";

export type HttpHeader = Record<string, string | string[]> | undefined;

@Injectable({
  providedIn: "root"
})
export class ApiService {
  private _logger: LogService = inject(LogService);
  private _http: HttpClient = inject(HttpClient);

  public get<TOutput>(pContext: string, pId?: string, pUserId?: string): Observable<HttpResponse<TOutput>> {
    const url: string = this._getUrl(pContext, pId);
    const headers: HttpHeader = this._getHeaders(pUserId);

    this._logger.log(`GET ${url}, user-id: ${pUserId}.`);

    return this._http.get<TOutput>(url,
      {
        headers: headers,
        observe: "response"
      }
    );
  }

  public post<TOutput>(pContext: string, pId?: string, pUserId?: string): Observable<HttpResponse<TOutput>> {
    const url: string = this._getUrl(pContext, pId);
    const headers: HttpHeader = this._getHeaders(pUserId);

    this._logger.log(`POST ${url}, user-id: ${pUserId}.`);

    return this._http.post<TOutput>(url, null,
      {
        headers: headers,
        observe: "response"
      }
    );
  }

  public put<TInput, TOutput>(pContext: string, pItem: TInput, pId?: string, pUserId?: string): Observable<HttpResponse<TOutput>> {
    const url: string = this._getUrl(pContext, pId);
    const headers: HttpHeader = this._getHeaders(pUserId);

    this._logger.log(`PUT ${url}, user-id: ${pUserId}.`);

    return this._http.put<TOutput>(url, pItem,
      {
        headers: headers,
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

  private _getHeaders(pUserId?: string): HttpHeader {
    let headers: Record<string, string | string[]> = {};

    if (pUserId) {
      headers["userId"] = pUserId
    }

    return !headers ? undefined : headers;
  }
}
