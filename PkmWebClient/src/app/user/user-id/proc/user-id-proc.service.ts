import { inject, Injectable } from "@angular/core";
import { HttpResponse } from "@angular/common/http";
import { Observable, ReplaySubject } from "rxjs";

import { LogService } from "@core/logs";
import { CreateUserResult, ValidateUserResult } from "@user/user-id/models";
import { UserIdApiService, UserIdStateService } from "@user/user-id/services";

@Injectable({
  providedIn: "root"
})
export class UserIdProcService {
  // #region Services
  private _logger: LogService = inject(LogService);
  private _userIdApi: UserIdApiService = inject(UserIdApiService);
  private _userIdState: UserIdStateService = inject(UserIdStateService);
  // #endregion

  private _userIdReadySrc: ReplaySubject<string> = new ReplaySubject<string>(1);
  public userIdReady$: Observable<string> = this._userIdReadySrc.asObservable();

  public getAndValidateUserId(): void {
    const userId: string = this._userIdState.loadUserIdFromCookie();
    this._logger.log(`Received user id from cookie: ${userId}`);
    if (!userId) {
      this.createNewUser();
    }
    else {
      this.ensureValidUser(userId);
    }
  }

  public createNewUser(): void {
    this._logger.log(`Creating a new user.`);
    this._userIdApi.createNewUser().subscribe({
      next: (resp: HttpResponse<CreateUserResult>): void => this._onCreateNewUserOk(resp)
    });
  }

  public ensureValidUser(pUserId: string): void {
    this._logger.log(`Ensuring valid userId: ${pUserId}.`);
    this._userIdApi.validateUser(pUserId).subscribe({
      next: (resp: HttpResponse<ValidateUserResult>): void => this._onEnsureValidUserOk(resp, pUserId),
      error: (resp: HttpResponse<ValidateUserResult>): void => { }
    });
  }

  private _onCreateNewUserOk(pResp: HttpResponse<CreateUserResult>): void {
    if (!pResp.body) {
      return;
    }

    const userId: string = pResp.body.userId
    this._logger.log(`Created new user: ${userId}.`);

    this._userIdState.saveUserIdToCookie(userId);
    this._userIdReadySrc.next(userId);
  }

  private _onEnsureValidUserOk(pResp: HttpResponse<ValidateUserResult>, pUserId: string): void {
    if (!pResp.body) {
      return;
    }

    if (pResp.body.userExists) {
      this._logger.log(`UserId exists: ${pUserId}.`);
      this._userIdReadySrc.next(pUserId);
    }
    else {
      this.createNewUser();
    }
  }
}
