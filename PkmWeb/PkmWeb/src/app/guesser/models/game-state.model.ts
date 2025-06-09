import { MoveState } from "@guesser/models";

export enum GameResultTypes {
  ONGOING,
  WIN,
  LOSE
}

export interface GameState {
  name: string,
  type1: string,
  type2: string | null,
  moves: MoveState[],
  wrongGuesses: string[],
  lives: number,
  result: GameResultTypes,
  stats: Stats
}

export interface Stats {
  nbrGuesses: number,
  nbrCorrect: number,
  nbrIncorrect: number,

  correctRatio: number,
  incorrectRatio: number,
  guessRatio: number,

  currentScore: number,
  potentialScore: number,
  lostScore: number,
  maxScore: number,

  scoreProgressRatio: number,
  potentialScoreRatio: number,
  lostScoreRatio: number,
  totalScoreRatio: number
}
