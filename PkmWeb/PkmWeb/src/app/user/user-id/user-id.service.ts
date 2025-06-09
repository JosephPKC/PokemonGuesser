import { inject, Injectable, OnDestroy, signal, Signal, WritableSignal } from "@angular/core";
import { HttpResponse } from "@angular/common/http";
import { Observable, ReplaySubject } from "rxjs";
import { CookieService } from "ngx-cookie-service";

import { ApiService } from "@core/api";
import { LogLevel, LogService } from "@core/logger";
import { CreateUserResult, ValidateUserResult } from "@user/user-id";

@Injectable({
  providedIn: "root"
})
export class UserIdService implements OnDestroy {
  // #region Services
  private _api: ApiService = inject(ApiService);
  private _cookie: CookieService = inject(CookieService);
  private _logger: LogService = inject(LogService);
  // #endregion

  private _endpoint: string = "api/user";

  private _userIdReadySrc: ReplaySubject<string> = new ReplaySubject<string>(1);
  public userIdReady$: Observable<string> = this._userIdReadySrc.asObservable();

  public ngOnDestroy(): void {
    this._userIdReadySrc.complete();
  }

  // #region UserId
  private _userId: WritableSignal<string> = signal<string>("");

  public get userId(): Signal<string> {
    return this._userId.asReadonly();
  }

  public set userId(pUserId: string) {
    this._userId.set(pUserId);
  }

  private _saveUserIdToCookie(pUserId?: string): void {
    if (pUserId) {
      this._userId.set(pUserId);
    }

    this._cookie.set("USERID", this._userId(), 1);
  }

  private _loadUserIdFromCookie(): string {
    this._userId.set(this._cookie.get("USERID"));
    return this._userId();
  }
  // #endregion

  public getAndValidateUserId(): void {
    const userId: string = this._loadUserIdFromCookie();
    this._logger.log(`Received user id from cookie: ${userId}`, LogLevel.DEBUG);
    if (!userId) {
      this.createNewUser();
    }
    else {
      this.ensureValidUser(userId);
    }
  }

  public createNewUser(): void {
    this._logger.log(`Creating a new user.`, LogLevel.DEBUG);
    this._api.post<CreateUserResult>(this._endpoint).subscribe({
      next: (resp: HttpResponse<CreateUserResult>): void => this._onCreateNewUserOk(resp)
    });
  }

  public ensureValidUser(pUserId: string): void {
    this._logger.log(`Ensuring valid userId: ${pUserId}.`, LogLevel.DEBUG);
    this._api.get<ValidateUserResult>(this._endpoint, pUserId).subscribe({
      next: (resp: HttpResponse<ValidateUserResult>): void => this._onEnsureValidUserOk(resp, pUserId),
      error: (resp: HttpResponse<ValidateUserResult>): void => { }
    });
  }

  private _onCreateNewUserOk(pResp: HttpResponse<CreateUserResult>): void {
    if (!pResp.body) {
      return;
    }

    const userId: string = pResp.body.userId
    this._logger.log(`Created new user: ${userId}.`, LogLevel.DEBUG);

    this._saveUserIdToCookie(userId);
    this._userIdReadySrc.next(userId);
  }

  private _onEnsureValidUserOk(pResp: HttpResponse<ValidateUserResult>, pUserId: string): void {
    if (!pResp.body) {
      return;
    }

    if (pResp.body.userExists) {
      this._logger.log(`UserId exists: ${pUserId}.`, LogLevel.DEBUG);
      this._userIdReadySrc.next(pUserId);
    }
    else {
      this.createNewUser();
    }
  }
}
