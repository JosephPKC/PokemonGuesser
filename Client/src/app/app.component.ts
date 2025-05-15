import { Component, computed, inject, OnInit, Signal, signal, WritableSignal } from '@angular/core';

import { CookieService } from 'ngx-cookie-service';

import { ApiService } from '@core/api/api.service';

import { HttpResponse } from '@angular/common/http';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private api: ApiService = inject(ApiService);
  private cookie: CookieService = inject(CookieService);

  //pkmData: WritableSignal<PkmData | null> = signal(null);
  state: WritableSignal<GameData | null> = signal(null);

  public guessTentative: WritableSignal<string> = signal<string>("");
  public guessResult: WritableSignal<string> = signal("");
  public guess: WritableSignal<string> = signal<string>("");

  ratio: Signal<string> = computed(() => {
    if (this.state()!.stats.guesses == 0) {
      return "0";
    }

    return ((this.state()!.stats.correct / this.state()!.stats.guesses) * 100).toFixed(2);
  });

  // Cookies
  userId: string = "";
  gettingUserId: WritableSignal<boolean> = signal(false);

  saveUserId() {
    this.cookie.set("userId", this.userId, 1);
  }

  loadUserId() {
    this.userId = this.cookie.get("userId");
  }

  getUserId() {
    // Check if user already has an id
    this.loadUserId();
    console.log(`Loaded user id from cookie: ${this.userId}.`);
    if (!this.userId) {
      // New user
      this.gettingUserId.set(true);
      this.api.post<UserResult>("api/user", "").subscribe(x => {
        this.userId = x.body!.userId

        console.log("USERID: " + this.userId);
        this.saveUserId();
        this.setupGame();
      });
    }
    else {
      // Authenticate the user id
      this.api.post("api/user/test", this.userId).subscribe(x => { console.log("Ran user test"); this.setupGame(); });
    }
  }

  setupGame(): void {
    this.GetState();
  }

  ngOnInit() {
    this.getUserId();


  }

  onChangeGuess(value: string): void {
    if (value !== "" && value !== undefined && value !== null) {
      this.guessTentative.set(value);
    }
    
  }

  onClickGuess(): void {
    if (this.guessTentative() !== "" && this.guessTentative() !== undefined && this.guessTentative() !== null) {
      this.guess.set(this.guessTentative());
      this.guessTentative.set("");

      this.ProcessGuess(this.guess());
    }

  }

  onClickNewGame(): void {
    this.guess.set("");
    this.guessResult.set("");
    this.guessTentative.set("");
    this.CreateGame();

  }

  onClickRevealHint(moveId: number, hintType: string): void {
    console.log(`UserId revealing hint: ${this.userId} - ${moveId} - ${hintType}.`);
    this.api.put<Hint, HintResult>("/api/hint", "", { moveId: moveId, hintType: hintType }, this.userId).subscribe({
      next: x => {
        if (x.body!.isAlreadyAnswered || x.body!.isAlreadyRevealed) {
          console.log("Invalid hint reveal state (already answered or already revealed).");
        }
        else {
          this.state.set(x.body!.state);
        }
      },
      error: err => {
        if (err.status === 404) {
          console.log("404 Not Found. Either user id or move id.")
        }
        else if (err.status === 400) {
          console.log("400 Bad Request. Move has no hint for the hint type.");
        }
      }
    });
  }

  //GetPkm(): void {
  //  console.log(`UserId getting pkm: ${this.userId}.`);
  //  this.api.get<PkmData>("/api/pkm", "1", this.userId).subscribe(x => {
  //    this.pkmData.set(x);
  //  });
  //}

  GetState(): void {
    console.log(`UserId getting state: ${this.userId}.`);
    this.api.get<GameData>("/api/game", "", this.userId).subscribe({
      next: (x: HttpResponse<GameData>): void => {
        console.log(`Got response back: ${x.status}.`);
        console.log(x.body);
        this.state.set(x.body!);
      },
      error: (err: HttpResponse<GameData>): void => {
        console.log(`Got response back: ${err.status}.`);
        if (err.status === 404) {
          this.CreateGame();
        }
      }
    });
  }

  CreateGame(): void {
    console.log(`UserId creating game: ${this.userId}.`);
    this.api.post<GameData>("/api/game/new", "", this.userId).subscribe(x => {
      this.state.set(x.body!);
    });
  }

  ProcessGuess(value: string): void {
    if (this.state() === null) {
      return;
    }

    this.api.put<Guess, GuessResult>("/api/guess", "", { guess: value }, this.userId).subscribe(x => {
      if (x.body!.isDuplicate) {
        this.guessResult.set("Already Guessed!");
      }
      else if (x.body!.isCorrect) {
        this.guessResult.set("Correct!")
      }
      else {
        this.guessResult.set("Wrong!");
      }
      this.state.set(x.body!.state);

      //if (x.body!.state.isDone) {
      //  this.CreateGame();
      //}
      //else {
      //  this.state.set(x.body!.state);
      //}
    });
  }
}


interface PkmData {
  id: number,
  name: string,
  types: PkmType,
  moves: PkmMoveData[]
}
interface PkmMoveData {
  id: number,
  levelLearned: number,
  name: string,
  power: number,
  accuracy: number,
  pp: number,
  isAnswered: boolean,
  damageClassHint: PkmHint,
  typeHint: PkmHint,
  flavorTextHint: PkmHint
};

interface PkmHint {
  id: number,
  hintType: string,
  hint: string,
  scoreCost: number
}

interface PkmType {
  type1: string,
  type2: string
}

interface GuessResult {
  isCorrect: boolean,
  isDuplicate: boolean,
  moveId: number,
  state: GameData
}

interface Guess {
  guess: string
}

interface HintResult {
  isAlreadyAnswered: boolean,
  isAlreadyRevealed: boolean,
  state: GameData
}

interface Hint {
  moveId: number,
  hintType: string
}

interface GameData {
  isDone: boolean,
  isWin: boolean,
  wrongGuesses: string[],
  lives: number,
  moves: MoveData[],
  pkmRef: PkmData,
  stats: StatsData
}

interface MoveData {
  id: number,
  name: string,
  isAnswered: boolean,
  moveRef: PkmMoveData,
  damageClassHint: HintData,
  typeHint: HintData,
  flavorTextHint: HintData,
  points: number
}

interface HintData {
  id: number,
  hintType: string,
  hint: string
  isRevealed: boolean
}

interface StatsData {
  guesses: number,
  correct: number,
  score: number,
  potential: number,
  max: number
}

interface UserResult {
  userId: string,
  test: string
}
