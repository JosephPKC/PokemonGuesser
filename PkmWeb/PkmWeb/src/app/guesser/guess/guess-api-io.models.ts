import { GameState } from "@guesser/models";

export enum GuessResultTypes {
  CORRECT,
  INCORRECT,
  ALREADY_GUESSED
}

export interface GuessInput {
  guess: string
}

export interface ProcessGuessResult {
  result: GuessResultTypes,
  newState: GameState
}
