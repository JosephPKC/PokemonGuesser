import { HttpClient, HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root"
})
export class ApiService {
  private http: HttpClient = inject(HttpClient);

  public get<TOutput>(pContext: string, pId: string, pUserId?: string): Observable<HttpResponse<TOutput>> {
    const url: string = `${pContext}/${pId}`;
    console.log(`GET ${url}.`);
    const headers: Record<string, string | string[]> | undefined = this.getHeaders(pUserId);
    return this.http.get<TOutput>(url, { headers: headers, observe: "response" });
  }

  public post<TOutput>(pContext: string, pId: string, pUserId?: string): Observable<HttpResponse<TOutput>> {
    const url: string = `${pContext}/${pId}`;
    console.log(`POST ${url}.`);
    const headers: Record<string, string | string[]> | undefined = this.getHeaders(pUserId);
    return this.http.post<TOutput>(url, null, { headers: headers, observe: "response" });
  }

  public put<TInput, TOutput>(pContext: string, pId: string, pItem: TInput, pUserId?: string): Observable<HttpResponse<TOutput>> {
    const url: string = `${pContext}/${pId}`;
    console.log(`PUT ${url}.`);
    const headers: Record<string, string | string[]> | undefined = this.getHeaders(pUserId);
    return this.http.put<TOutput>(url, pItem, { headers: headers, observe: "response" });
  }

  private getHeaders(pUserId?: string): Record<string, string | string[]> | undefined {
    let headers: Record<string, string | string[]> = {};
    if (pUserId) {
      console.log(`Adding header: ${pUserId}`);
      headers["userId"] = pUserId
    }

    if (!headers) {
      return undefined;
    }

    return headers;
  }
}
