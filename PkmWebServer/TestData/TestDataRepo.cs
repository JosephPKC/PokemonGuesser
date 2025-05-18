using PkmWebServer.Services.DataService;

namespace PkmWebServer.TestData
{
    public class TestDataRepo : IPkmDataApi
    {
        private readonly PkmAllApiModel _all = new()
        {
            Ids = [1]
        };

        private readonly Dictionary<int, PkmApiModel> _pkm = new()
        {
            { 
                1,
                new()
                {
                    Id = 1,
                    Name = new()
                    {
                        Name = "Bulbasaur",
                        NameKey = "bulbasaur"
                    },
                    Types = [
                        new() {
                            Name = "Grass",
                            NameKey = "grass"
                        },
                        new() {
                            Name = "Poison",
                            NameKey = "poison"
                        }
                    ],
                    Moves = [
                        new() {
                            Id = 1,
                            Name = new() {
                                Name = "Tackle",
                                NameKey = "tackle"
                            },
                            LevelLearned = 1,
                            Power = 35,
                            Accuracy = 100,
                            Pp = 35,
                            DamageClass = new() {
                                Name = "Physical",
                                NameKey = "physical"
                            },
                            MoveType = new() {
                                Name = "Normal",
                                NameKey = "normal"
                            },
                            FlavorText = "Charge at a foe."
                        },
                        new() {
                            Id = 2,
                            Name = new() {
                                Name = "Razor Leaf",
                                NameKey = "razor-leaf"
                            },
                            LevelLearned = 5,
                            Power = 45,
                            Accuracy = 85,
                            Pp = 25,
                            DamageClass = new() {
                                Name = "Physical",
                                NameKey = "physical"
                            },
                            MoveType = new() {
                                Name = "Grass",
                                NameKey = "grass"
                            },
                            FlavorText = "Sharp leaves."
                        },
                        new() {
                            Id = 3,
                            Name = new() {
                                Name = "Skull Bash",
                                NameKey = "skull-bash"
                            },
                            LevelLearned = 25,
                            Power = 100,
                            Accuracy = 100,
                            Pp = 15,
                            DamageClass = new() {
                                Name = "Physical",
                                NameKey = "physical"
                            },
                            MoveType = new() {
                                Name = "Normal",
                                NameKey = "normal"
                            },
                            FlavorText = "Massive headbutt."
                        },
                        new() {
                            Id = 4,
                            Name = new() {
                                Name = "Growth",
                                NameKey = "growth"
                            },
                            LevelLearned = 15,
                            Power = 0,
                            Accuracy = 100,
                            Pp = 20,
                            DamageClass = new() {
                                Name = "Status",
                                NameKey = "status"
                            },
                            MoveType = new() {
                                Name = "Normal",
                                NameKey = "normal"
                            },
                            FlavorText = "Grow stronger."
                        },
                        new() {
                            Id = 5,
                            Name = new() {
                                Name = "Solarbeam",
                                NameKey = "solarbeam"
                            },
                            LevelLearned = 55,
                            Power = 120,
                            Accuracy = 100,
                            Pp = 5,
                            DamageClass = new() {
                                Name = "Special",
                                NameKey = "special"
                            },
                            MoveType = new() {
                                Name = "Grass",
                                NameKey = "grass"
                            },
                            FlavorText = "Charge up a beam of light."
                        }
                    ]
                } 
            }
        };

        public PkmAllApiModel? GetAllPkm()
        {
            return _all;
        }

        public PkmApiModel? GetPkmById(int pId)
        {
            _pkm.TryGetValue(pId, out PkmApiModel? model);
            return model;
        }
    }
}
