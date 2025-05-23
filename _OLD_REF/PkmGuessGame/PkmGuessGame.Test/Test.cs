//using FluentAssertions;

//namespace PkmGuessGame.Test
//{
//    public class Test
//    {
//        PkmGameModel Model { get; set; } = new()
//        {
//            Moves = new()
//            {
//                { 1,
//                    new()
//                    {
//                        Id = 1,
//                        Name = "Correct",
//                        Type = "Type-1",
//                        LevelLearnedAt = 10,
//                        MoveLearnMethod = "Level-Up",
//                        FlavorText = "Text-1",
//                        MoveDamageClass = "Class-1"
//                    }
//                },
//                { 2,
//                    new()
//                    {
//                        Id = 2,
//                        Name = "Correct-2",
//                        Type = "Type-2",
//                        LevelLearnedAt = 15,
//                        MoveLearnMethod = "Level-Up",
//                        FlavorText = "Text-2",
//                        MoveDamageClass = "Class-2"
//                    }
//                },
//                { 3,
//                    new()
//                    {
//                        Id = 3,
//                        Name = "Correct-3",
//                        Type = "Type-3",
//                        MachineName = "TM08",
//                        MoveLearnMethod = "Machine",
//                        FlavorText = "Text-3",
//                        MoveDamageClass = "Class-3"
//                    }
//                },
//                { 4,
//                    new()
//                    {
//                        Id = 4,
//                        Name = "Correct-Egg",
//                        Type = "Type-4",
//                        MoveLearnMethod = "Egg",
//                        FlavorText = "Text-4",
//                        MoveDamageClass = "Class-4"
//                    }
//                },
//                { 5,
//                    new()
//                    {
//                        Id = 5,
//                        Name = "Correct-Tutor",
//                        Type = "Type-5",
//                        MoveLearnMethod = "Tutor",
//                        FlavorText = "Text-5",
//                        MoveDamageClass = "Class-5"
//                    }
//                },
//                { 6,
//                    new()
//                    {
//                        Id = 6,
//                        Name = "Correct",
//                        Type = "Type",
//                        MoveLearnMethod = "Tutor",
//                        FlavorText = "Text-5",
//                        MoveDamageClass = "Class-5"
//                    }
//                }
//            },
//            OldMoves = new()
//            {
//                {
//                    10,
//                    new()
//                    {
//                        Id = 10,
//                        Name = "Old",
//                        LastVersion = "Version-1",
//                        Type = "Grass"
//                    }
//                }
//            }
//        };

//        [Fact]
//        public void TestProcessGuess()
//        {
//            GuessGameManager manager = new();
//            manager.NewGame(Model);

//            // Wrong Guess
//            ProcessGuessResult result = manager.ProcessGuess("wrong");
//            result.Result.Should().Be(GuessResults.Incorrect);
//            result.Score.Should().Be(-2);
//            result.GuessId.Should().BeNull();
//            result.IsGameDone.Should().BeFalse();
//            result.CurrentTotalScore.Should().Be(-2);

//            // Correct Guess
//            result = manager.ProcessGuess("correct");
//            result.Result.Should().Be(GuessResults.Correct);
//            result.Score.Should().Be(10);
//            result.GuessId.Should().Be(1);
//            result.IsGameDone.Should().BeFalse();
//            result.CurrentTotalScore.Should().Be(8);

//            // Old Guess
//            result = manager.ProcessGuess("old");
//            result.Result.Should().Be(GuessResults.OldMatch);
//            result.Score.Should().Be(0);
//            result.GuessId.Should().Be(10);
//            result.IsGameDone.Should().BeFalse();
//            result.CurrentTotalScore.Should().Be(8);

//            // Duplicate Guesses
//            result = manager.ProcessGuess("wrong");
//            result.Result.Should().Be(GuessResults.Duplicate);
//            result.Score.Should().Be(0);
//            result.GuessId.Should().BeNull();
//            result.IsGameDone.Should().BeFalse();
//            result.CurrentTotalScore.Should().Be(8);

//            result = manager.ProcessGuess("correct");
//            result.Result.Should().Be(GuessResults.Duplicate);
//            result.Score.Should().Be(0);
//            result.GuessId.Should().BeNull();
//            result.IsGameDone.Should().BeFalse();
//            result.CurrentTotalScore.Should().Be(8);

//            result = manager.ProcessGuess("old");
//            result.Result.Should().Be(GuessResults.Duplicate);
//            result.Score.Should().Be(0);
//            result.GuessId.Should().BeNull();
//            result.IsGameDone.Should().BeFalse();
//            result.CurrentTotalScore.Should().Be(8);

//            // Hints
//            // Type
//            RevealHintResult hintRes = manager.RevealHint(2, MoveHintTypes.Type);
//            hintRes.Result.Should().Be(HintResults.Revealed);
//            hintRes.Hint.Should().Be("Type-2");

//            // Damage Class
//            hintRes = manager.RevealHint(2, MoveHintTypes.DamageClass);
//            hintRes.Result.Should().Be(HintResults.Revealed);
//            hintRes.Hint.Should().Be("Class-2");

//            // Flavor Text
//            hintRes = manager.RevealHint(2, MoveHintTypes.FlavorText);
//            hintRes.Result.Should().Be(HintResults.Revealed);
//            hintRes.Hint.Should().Be("Text-2");

//            // Already Revealed
//            hintRes = manager.RevealHint(2, MoveHintTypes.Type);
//            hintRes.Result.Should().Be(HintResults.AlreadyRevealed);
//            hintRes.Hint.Should().BeNull();

//            // Already Answered
//            hintRes = manager.RevealHint(1, MoveHintTypes.Type);
//            hintRes.Result.Should().Be(HintResults.AlreadyAnswered);
//            hintRes.Hint.Should().BeNull();

//            // Check score
//            result = manager.ProcessGuess("correct-2");
//            result.Score.Should().Be(2);
//            result.CurrentTotalScore.Should().Be(10);

//            // Game Done
//            _ = manager.ProcessGuess("correct-3");
//            _ = manager.ProcessGuess("correct-egg");
//            result = manager.ProcessGuess("correct-tutor");
//            result.IsGameDone.Should().BeTrue();
//            result.CurrentTotalScore.Should().Be(40);

//            // Stats
//            GameStats stats = manager.GetStats();
//            stats.NbrOfGuessesTotal.Should().Be(7);
//            stats.NbrOfCorrectGuesses.Should().Be(5);
//            stats.NbrOfOldCorrectGuesses.Should().Be(1);
//            stats.NbrOfIncorrectGuesses.Should().Be(1);
//            stats.TotalScore.Should().Be(40);
//        }
//    }
//}
