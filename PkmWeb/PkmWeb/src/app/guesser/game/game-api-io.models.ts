import { GameState } from "@guesser/models";

export interface CreateNewGameResult {
  game: GameState
}

export interface GetActiveGameResult {
  game: GameState
}
