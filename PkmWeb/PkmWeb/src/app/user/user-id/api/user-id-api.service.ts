import { HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { ApiEndpointService, ApiService } from "@core/api";
import { CreateUserResult, ValidateUserResult } from "@user/user-id/models";

@Injectable({
  providedIn: "root"
})
export class UserIdApiService {
  // #region Services
  private _api: ApiService = inject(ApiService);
  private _endpoint: ApiEndpointService = inject(ApiEndpointService);
  // #endregion

  public createNewUser(): Observable<HttpResponse<CreateUserResult>> {
    return this._api.post(this._endpoint.getUserEndpoint());
  }

  public validateUser(pUserId: string): Observable<HttpResponse<ValidateUserResult>> {
    return this._api.getUser(this._endpoint.getUserEndpoint(), pUserId);
  }
}
