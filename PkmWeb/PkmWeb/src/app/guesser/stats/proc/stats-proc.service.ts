import { HttpResponse } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { ReplaySubject, Observable } from "rxjs";

import { LogService } from "@core/logs";
import { GetStatsResult, Stats } from "@guesser/stats/models";
import { StatsApiService } from "@guesser/stats/services";

@Injectable({
  providedIn: "root"
})
export class StatsProcService {
  // #region Services
  private _logger: LogService = inject(LogService);
  private _statsApi: StatsApiService = inject(StatsApiService);
  // #endregion

  private _getStatsReadySrc: ReplaySubject<Stats> = new ReplaySubject<Stats>(1);
  public getStatsReady$: Observable<Stats> = this._getStatsReadySrc.asObservable();

  public getStats(pUserId: string): void {
    this._logger.log(`Getting stats for ${pUserId}.`);

    this._statsApi.getStats(pUserId).subscribe({
      next: (resp: HttpResponse<GetStatsResult>): void => this.onGetStatsOk(resp, pUserId)
    });
  }


  private onGetStatsOk(pResp: HttpResponse<GetStatsResult>, pUserId: string): void {
    if (!pResp.body) {
      return;
    }

    this._getStatsReadySrc.next(pResp.body.stats);
  }
}
