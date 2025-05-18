import {
  Component, computed, inject, OnInit,
  Signal, signal, WritableSignal
} from "@angular/core";

import { UserIdProcService } from "@user/user-id/services";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrl: "./app.component.css",
  standalone: false
})
export class AppComponent implements OnInit {
  // #region Services
  private _userIdProc: UserIdProcService = inject(UserIdProcService);
  // #endregion

  // #region State
  protected _userId: WritableSignal<string> = signal<string>("");
  protected _isUserIdReady: Signal<boolean> = computed<boolean>(() => {
    return this._userId() !== "";
  });
  // #endregion

  public ngOnInit(): void {
    this._userIdProc.userIdReady$.subscribe({
      next: (val: string): void => {
        this._userId.set(val);
      }
    });

    this._userIdProc.getAndValidateUserId();
  }
}
