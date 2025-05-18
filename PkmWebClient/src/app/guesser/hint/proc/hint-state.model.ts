import { HintTypes } from "@guesser/hint/models";

export interface HintState {
  hintType: HintTypes,
  hint: string,
  scoreCost: number,
  isRevealed: boolean
}
