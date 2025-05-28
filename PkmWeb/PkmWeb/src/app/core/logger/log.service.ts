import { Injectable } from "@angular/core";

export enum LogLevel {
  DEBUG,
  INFO,
  WARN,
  ERROR
}

@Injectable({
  providedIn: "root"
})
export class LogService {
  public defaultLevel: LogLevel = LogLevel.INFO;

  public log(pMessage: any | null | undefined, pLevel: LogLevel = LogLevel.INFO): void {
    console.log(`${pLevel}: ${pMessage}`);
  }
}
