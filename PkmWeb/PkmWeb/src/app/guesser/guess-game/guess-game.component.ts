import {
  Component, computed, inject, input, InputSignal, OnDestroy, OnInit,
  Signal, signal, WritableSignal
} from "@angular/core";

import { GameService } from "@guesser/game";
import { 
GuessInput, GuessResultTypes, GuessService, ProcessGuessResult 
} from "@guesser/guess";
import { HintInput, HintService } from "@guesser/hint";
import { GameResultTypes, GameState, HintTypes } from "@guesser/models";
import { Subject, takeUntil } from "rxjs";

@Component({
  selector: "guess-game",
  templateUrl: "./guess-game.component.html",
  styleUrl: "./guess-game.component.css",
  standalone: false
})
export class GuessGameComponent implements OnDestroy, OnInit {
  // #region Services
  private _gameServ: GameService = inject(GameService);
  private _guessServ: GuessService = inject(GuessService);
  private _hintServ: HintService = inject(HintService);
  // #endregion

  // #region Inputs & Outputs
  public userId: InputSignal<string> = input.required<string>();
  // #endregion

  // #region Subs
  private _unsub: Subject<void> = new Subject<void>();
  // #endregion

  // #region State
  protected _isDataReady: WritableSignal<boolean> = signal<boolean>(false);
  protected _state: WritableSignal<GameState | null> = signal<GameState | null>(null);

  protected _guess: WritableSignal<string> = signal<string>("");
  protected _prevGuess: WritableSignal<string> = signal<string>("");
  protected _guessResult: WritableSignal<GuessResultTypes | null> = signal<GuessResultTypes | null>(null);
  // #endregion

  // #region Calcs
  protected _typeString: Signal<string> = computed<string>(() => {
    if (!this._state()) {
      return "";
    }

    if (!this._state()!.type2) {
      return this._state()!.type1;
    }

    return `${this._state()!.type1}/${this._state()!.type2}`;
  });

  protected _gameResult: Signal<string | null> = computed<string | null>(() => {
    if (!this._state()) {
      return null;
    }

    switch (this._state()!.result) {
      case GameResultTypes.LOSE: {
        return "Game is over. You lose!";
      }
      case GameResultTypes.WIN: {
        return "Game is over. You win!";
      }
    }

    return null;
  });

  protected _guessResultString: Signal<string | null> = computed<string | null>(() => {
    if (this._prevGuess() == "" || !this._guessResult()) {
      return null;
    }

    return `Your guess of \'${this._prevGuess()}\' was ${GuessResultTypes[this._guessResult()!]}.`;
  });

  // #endregion

  public ngOnInit(): void {
    this._addServiceSubs();

    this._gameServ.loadOrCreateGame(this.userId());
  }

  public ngOnDestroy(): void {
    this._unsub.next();
    this._unsub.complete();
  }

  private _addServiceSubs(): void {
    this._gameServ.gameReady$
      .pipe(takeUntil(this._unsub))
      .subscribe({
      next: (val: GameState) => {
        this._state.set(val);
        this._isDataReady.set(true);
      }
    });

    this._guessServ.processGuessReady$
      .pipe(takeUntil(this._unsub))
      .subscribe({
      next: (res: ProcessGuessResult) => {
        this._state.set(res.newState);
        this._guessResult.set(res.result);
      }
    });

    this._hintServ.revealHintReady$
      .pipe(takeUntil(this._unsub))
      .subscribe({
      next: (val: GameState) => {
        this._state.set(val);
      }
    });
  }

  protected _onChangeGuess(pValue: string): void {
    if (!pValue) {
      return;
    }

    this._guess.set(pValue);
  }

  protected _onClickGuess(): void {
    if (!this._guess() || this._guess() === "") {
      return;
    }

    const guess: GuessInput = {
      guess: this._guess()
    };

    this._guessServ.processGuess(this.userId(), guess);
    this._prevGuess.set(this._guess());
    this._guess.set("");
  }

  protected _onClickRevealHint(pMoveId: number, pHintType: HintTypes): void {
    if (pMoveId < 0) {
      return;
    }

    const hint: HintInput = {
      hintType: pHintType,
      moveId: pMoveId
    };

    this._hintServ.revealHint(this.userId(), hint);
  }

  protected _onClickNewGame(): void {
    this._guess.set("");
    this._prevGuess.set("");
    this._guessResult.set(null);
    this._isDataReady.set(false);

    this._gameServ.createNewGame(this.userId());
  }
}
