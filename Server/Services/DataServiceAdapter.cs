using Server.Models;

namespace Server.Services
{
    public class DataServiceAdapter
    {
        private PkmModel[] pkms = [
            new()
            {
                Id = 0,
                Name = "Shiftry",
                Types = new()
                {
                    Type1 = "Grass",
                    Type2 = "Dark"
                },
                Moves = [
                    new MoveModel() {
                        Id = 0,
                        LevelLearned = 1,
                        Name = "Tackle",
                        Power = 35,
                        Accuracy = 100,
                        Pp = 35,
                        DamageClassHint = new() {
                            Id = 0,
                            HintType = "damageclass",
                            Hint = "Physical",
                            ScoreCost = 5
                        },
                        TypeHint = new() {
                            Id = 1,
                            HintType = "type",
                            Hint = "Normal",
                            ScoreCost = 10
                        },
                        FlavorTextHint = new() {
                            Id = 2,
                            HintType = "flavortext",
                            Hint = "Basic tackling.",
                            ScoreCost = 20
                        }
                        
                    },
                    new MoveModel() {
                        Id = 1,
                        LevelLearned = 5,
                        Name = "Razor Leaf",
                        Power = 50,
                        Accuracy = 85,
                        Pp = 20,
                        DamageClassHint = new() {
                            Id = 0,
                            HintType = "damageclass",
                            Hint = "Physical",
                            ScoreCost = 5
                        },
                        TypeHint = new() {
                            Id = 1,
                            HintType = "type",
                            Hint = "Grass",
                            ScoreCost = 10
                        },
                        FlavorTextHint = new() {
                            Id = 2,
                            HintType = "flavortext",
                            Hint = "Razor-sharp leaves.",
                            ScoreCost = 20
                        }
                    },
                    new MoveModel() {
                        Id = 2,
                        LevelLearned = 25,
                        Name = "Solarbeam",
                        Power = 100,
                        Accuracy = 100,
                        Pp = 10,
                        DamageClassHint = new() {
                            Id = 0,
                            HintType = "damageclass",
                            Hint = "Special",
                            ScoreCost = 5
                        },
                        TypeHint = new() {
                            Id = 1,
                            HintType = "type",
                            Hint = "Grass",
                            ScoreCost = 10
                        },
                        FlavorTextHint =new() {
                            Id = 2,
                            HintType = "flavortext",
                            Hint = "Charge up a beam of sunlight.",
                            ScoreCost = 20
                        }
                    }
                ]
            },
            //new()
            //{
            //    Id = 1,
            //    Name = "Houndoom",
            //    Types = new()
            //    {
            //        Type1 = "Fire",
            //        Type2 = "Dark"
            //    },
            //    Moves = [
            //        new MoveModel() {
            //            Id = 0,
            //            LevelLearned = 1,
            //            Name = "Flare Blitz",
            //            DamageClass = "Physical",
            //            Type = "Fire",
            //            Power = 100,
            //            Accuracy = 95,
            //            Pp = 35,
            //            FlavorText = "Super tackle."
            //        },
            //        new MoveModel() {
            //            Id = 1,
            //            LevelLearned = 1,
            //            Name = "Roar",
            //            DamageClass = "Status",
            //            Type = "Normal",
            //            Power = 0,
            //            Accuracy = 100,
            //            Pp = 15,
            //            FlavorText = "Terrifying howl."
            //        },
            //        new MoveModel() {
            //            Id = 2,
            //            LevelLearned = 50,
            //            Name = "Fire Blast",
            //            DamageClass = "Special",
            //            Type = "Fire",
            //            Power = 120,
            //            Accuracy = 50,
            //            Pp = 5,
            //            FlavorText = "Super fire."
            //        }
            //    ]
            //},
            //new()
            //{
            //    Id = 2,
            //    Name = "Cradily",
            //    Types = new()
            //    {
            //        Type1 = "Grass",
            //        Type2 = "Rock"
            //    },
            //    Moves = [
            //        new MoveModel() {
            //            Id = 0,
            //            LevelLearned = 5,
            //            Name = "Ancient Power",
            //            DamageClass = "Physical",
            //            Type = "Rock",
            //            Power = 80,
            //            Accuracy = 100,
            //            Pp = 15,
            //            FlavorText = "Something ancient."
            //        },
            //        new MoveModel() {
            //            Id = 1,
            //            LevelLearned = 15,
            //            Name = "Rock Smash",
            //            DamageClass = "Physical",
            //            Type = "Fighting",
            //            Power = 80,
            //            Accuracy = 100,
            //            Pp = 15,
            //            FlavorText = "Smash a rock."
            //        },
            //        new MoveModel() {
            //            Id = 2,
            //            LevelLearned = 30,
            //            Name = "Surf",
            //            DamageClass = "Special",
            //            Type = "Water",
            //            Power = 90,
            //            Accuracy = 100,
            //            Pp = 10,
            //            FlavorText = "A tsunami."
            //        }
            //    ]
            //}
        ];


        public PkmModel GetRandomPkm()
        {
            Random rand = new();
            int index = rand.Next(0, pkms.Length);
            Console.WriteLine($"Got random: {index}.");
            return pkms[index];
        }
    }
}
