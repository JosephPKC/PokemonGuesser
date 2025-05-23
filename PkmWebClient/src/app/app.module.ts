import { provideHttpClient } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { CookieService } from "ngx-cookie-service";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";

import { GuessGameComponent } from "@guesser/guess-game/guess-game.component";
import { LogService } from "./core/logs";

@NgModule({
  declarations: [
    AppComponent,
    GuessGameComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [provideHttpClient(), CookieService, LogService],
  bootstrap: [AppComponent]
})
export class AppModule { }
