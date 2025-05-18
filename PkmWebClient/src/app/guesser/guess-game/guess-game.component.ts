import {
  Component, computed, inject, input, InputSignal, OnInit,
  Signal, signal, WritableSignal
} from "@angular/core";

import { LogService } from "@core/logs";

import { GameResultTypes, GameState } from "@guesser/game/models";
import { GameProcService } from "@guesser/game/services";

import { GuessInput, GuessResultTypes, ProcessGuessResult } from "@guesser/guess/models";
import { GuessProcService } from "@guesser/guess/services";

import { HintInput, HintTypes } from "@guesser/hint/models";
import { HintProcService } from "@guesser/hint/services";

import { Stats } from "@guesser/stats/models";
import { StatsProcService } from "@guesser/stats/services";

@Component({
  selector: "guess-game",
  templateUrl: "./guess-game.component.html",
  styleUrl: "./guess-game.component.css",
  standalone: false
})
export class GuessGameComponent implements OnInit {
  // #region Services
  private _logger: LogService = inject(LogService);

  private _gameProc: GameProcService = inject(GameProcService);
  private _guessProc: GuessProcService = inject(GuessProcService);
  private _hintProc: HintProcService = inject(HintProcService);
  private _statsProc: StatsProcService = inject(StatsProcService);
  // #endregion

  // #region Inputs & Outputs
  public userId: InputSignal<string> = input.required<string>();
  // #endregion

  // #region State
  protected _isDataReady: WritableSignal<boolean> = signal<boolean>(false);
  protected _state: WritableSignal<GameState | null> = signal<GameState | null>(null);
  protected _stats: WritableSignal<Stats> = signal<Stats>({
    currentScore: 0,
    maxScore: 0,
    potentialScore: 0,
    nbrCorrect: 0,
    nbrGuesses: 0
  });

  protected _guess: WritableSignal<string> = signal<string>("");
  protected _prevGuess: WritableSignal<string> = signal<string>("");
  protected _guessResult: WritableSignal<GuessResultTypes | null> = signal<GuessResultTypes | null>(null);
  // #endregion

  // #region Calcs
  protected _guessRatio: Signal<string> = computed<string>(() => {
    let ratio: number = 0;
    if (this._stats().nbrGuesses > 0) {
      ratio = (this._stats().nbrCorrect / this._stats().nbrGuesses) * 100;
    }

    return ratio.toFixed(2);
  });

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
    this._addServiceSubscriptions();

    this._gameProc.loadOrCreateGame(this.userId());
  }

  private _addServiceSubscriptions(): void {
    this._gameProc.gameReady$.subscribe({
      next: (val: GameState) => {
        this._state.set(val);
        this._statsProc.getStats(this.userId());
      }
    });

    this._guessProc.processGuessReady$.subscribe({
      next: (res: ProcessGuessResult) => {
        this._state.set(res.newState);
        this._guessResult.set(res.result);
      }
    });

    this._hintProc.revealHintReady$.subscribe({
      next: (val: GameState) => {
        this._state.set(val);
      }
    });

    this._statsProc.getStatsReady$.subscribe({
      next: (val: Stats) => {
        this._stats.set(val);
        this._isDataReady.set(true);
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

    this._guessProc.processGuess(this.userId(), guess);
    this._statsProc.getStats(this.userId());
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

    this._hintProc.revealHint(this.userId(), hint);
    this._statsProc.getStats(this.userId());
  }

  protected _onClickNewGame(): void {
    this._guess.set("");
    this._prevGuess.set("");
    this._guessResult.set(null);
    this._isDataReady.set(false);

    this._gameProc.createNewGame(this.userId());
  }
}
