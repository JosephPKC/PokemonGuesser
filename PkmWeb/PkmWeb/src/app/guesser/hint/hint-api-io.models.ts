import { GameState, HintTypes } from "@guesser/models";

export enum HintResultTypes {
  REVEALED,
  ALREADY_ANSWERED,
  ALREADY_REVEALED
}

export interface HintInput {
  moveId: number,
  hintType: HintTypes
}

export interface RevealHintResult {
  result: HintResultTypes,
  newState: GameState
}
