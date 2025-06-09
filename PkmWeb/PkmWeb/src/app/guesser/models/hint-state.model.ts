export enum HintTypes {
  DAMAGE_CLASS,
  FLAVOR_TEXT,
  STATS,
  TYPE
}

export interface HintState {
  hintType: HintTypes,
  hint: string,
  scoreCost: number,
  isRevealed: boolean
}
