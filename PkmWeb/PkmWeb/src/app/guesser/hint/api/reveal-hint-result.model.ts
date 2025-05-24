import { GameState } from "@guesser/game/models";
import { HintResultTypes } from "@guesser/hint/models";

export interface RevealHintResult {
  result: HintResultTypes,
  newState: GameState
}
