import {
  inject, Injectable, Signal, signal, WritableSignal
} from "@angular/core";
import { CookieService } from "ngx-cookie-service";

@Injectable({
  providedIn: "root"
})
export class UserIdStateService {
  // #region Services
  private cookie: CookieService = inject(CookieService);
  // #endregion

  // #region UserId
  private _userId: WritableSignal<string> = signal<string>("");

  public get userId(): Signal<string> {
    return this._userId.asReadonly();
  }

  public set userId(pUserId: string) {
    this._userId.set(pUserId);
  }
  // #endregion

  public saveUserIdToCookie(pUserId?: string): void {
    if (pUserId) {
      this._userId.set(pUserId);
    }

    this.cookie.set("USERID", this._userId(), 1);
  }

  public loadUserIdFromCookie(): string {
    this._userId.set(this.cookie.get("USERID"));
    return this._userId();
  }
}
