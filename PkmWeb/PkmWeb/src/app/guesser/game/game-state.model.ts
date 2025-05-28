import { MoveState } from "@guesser/guess/models";

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
  currentScore: number,
  potentialScore: number,
  maxScore: number,
  nbrGuesses: number,
  nbrCorrect: number
}