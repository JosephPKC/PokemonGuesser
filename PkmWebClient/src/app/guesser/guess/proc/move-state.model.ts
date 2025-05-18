import { HintState } from "@guesser/hint/models";

export interface MoveState {
  id: number,
  name: string,
  levelLearned: number,
  isAnswered: boolean,
  points: number,
  damageClass: HintState,
  type: HintState,
  stats: HintState,
  flavorText: HintState
}
