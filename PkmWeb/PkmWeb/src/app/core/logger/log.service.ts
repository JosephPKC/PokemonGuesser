import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class LogService {
  public log(pMessage: any | null | undefined): void {
    console.log(pMessage);
  }
}
