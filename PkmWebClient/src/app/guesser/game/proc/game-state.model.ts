import { GameResultTypes } from "@guesser/game/models";
import { MoveState } from "@guesser/guess/models";

export interface GameState {
  name: string,
  type1: string,
  type2: string | null,
  moves: MoveState[],
  wrongGuesses: string[],
  lives: number,
  result: GameResultTypes
}
