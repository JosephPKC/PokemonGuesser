import { GameState } from "@guesser/game/models";
import { GuessResultTypes } from "@guesser/guess/models";

export interface ProcessGuessResult {
  result: GuessResultTypes,
  newState: GameState
}
