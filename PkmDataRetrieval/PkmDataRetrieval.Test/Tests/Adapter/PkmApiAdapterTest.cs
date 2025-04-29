using FluentAssertions;

using PkmApi.Dtos;
using PkmApi.Dtos.Game.Generation;
using PkmApi.Dtos.Game.Pokedex;
using PkmApi.Dtos.Game.VersionGroup;
using PkmApi.Dtos.Item.Item;
using PkmApi.Dtos.Machine.Machine;
using PkmApi.Dtos.Move.Move;
using PkmApi.Dtos.Move.MoveDamageClass;
using PkmApi.Dtos.Move.MoveLearnMethod;
using PkmApi.Dtos.Pokemon.Ability;
using PkmApi.Dtos.Pokemon.Form;
using PkmApi.Dtos.Pokemon.Pokemon;
using PkmApi.Dtos.Pokemon.Species;
using PkmApi.Dtos.Pokemon.Type;

using PkmDataRetrieval.Adapter;
using PkmDataRetrieval.Retrieval;
using PkmDataRetrieval.Retrieval.Models;
using PkmDataRetrieval.Retrieval.Models.Ability;
using PkmDataRetrieval.Retrieval.Models.Form;
using PkmDataRetrieval.Retrieval.Models.Generation;
using PkmDataRetrieval.Retrieval.Models.Item;
using PkmDataRetrieval.Retrieval.Models.Machine;
using PkmDataRetrieval.Retrieval.Models.Move;
using PkmDataRetrieval.Retrieval.Models.MoveDamageClass;
using PkmDataRetrieval.Retrieval.Models.MoveLearnMethod;
using PkmDataRetrieval.Retrieval.Models.Pokedex;
using PkmDataRetrieval.Retrieval.Models.Pokemon;
using PkmDataRetrieval.Retrieval.Models.Species;
using PkmDataRetrieval.Retrieval.Models.Type;
using PkmDataRetrieval.Retrieval.Models.VersionGroup;

using PkmDataRetrieval.Test.Fakes;
using PkmDataRetrieval.Test.Fakes.TestEndpointHandler;
using PkmDataRetrieval.Test.Fakes.TestValues;

namespace PkmDataRetrieval.Test.Tests.Adapter
{
    public class PkmApiAdapterTest
    {
        #region GetAll
        [Fact]
        public void GetAll_AllValidTypes_ReturnBasicRetList()
        {
            static void Test<TRet>() where TRet : BaseRetModel
            {
                //  Arrange
                TestPkmApiConfigs configs = new()
                {
                    ReturnThisResLiDto = TestResLiDtos.GetResLiDto()
                };
                IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));
                IEnumerable<BasicRetModel> expected = TestRets.TestBasicRetList;

                //  Act
                IEnumerable<BasicRetModel>? actual = gateway.GetAll<TRet>();

                //  Assert
                actual.Should().BeEquivalentTo(expected);
            }

            Test<AbilityRetModel>();
            Test<FormRetModel>();
            Test<GenerationRetModel>();
            Test<ItemRetModel>();
            Test<MachineRetModel>();
            Test<MoveRetModel>();
            Test<MoveDamageClassRetModel>();
            Test<MoveLearnMethodRetModel>();
            Test<PokedexRetModel>();
            Test<PkmRetModel>();
            Test<SpeciesRetModel>();
            Test<TypeRetModel>();
            Test<VersionGroupRetModel>();
        }

        [Fact]
        public void GetAll_AllValidTypes_NullCount_ReturnEmptyBasicRetList()
        {
            static void Test<TRet>() where TRet : BaseRetModel
            {
                //  Arrange
                TestPkmApiConfigs configs = new()
                {
                    ReturnThisResLiDto = TestResLiDtos.GetResLiDtoWithNullCount()
                };
                IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));
                IEnumerable<BasicRetModel> expected = [];

                //  Act
                IEnumerable<BasicRetModel>? actual = gateway.GetAll<TRet>();

                //  Assert
                actual.Should().BeEquivalentTo(expected);
            }

            Test<AbilityRetModel>();
            Test<FormRetModel>();
            Test<GenerationRetModel>();
            Test<ItemRetModel>();
            Test<MachineRetModel>();
            Test<MoveRetModel>();
            Test<MoveDamageClassRetModel>();
            Test<MoveLearnMethodRetModel>();
            Test<PokedexRetModel>();
            Test<PkmRetModel>();
            Test<SpeciesRetModel>();
            Test<TypeRetModel>();
            Test<VersionGroupRetModel>();
        }

        [Fact]
        public void GetAll_AllValidTypes_ZeroCount_ReturnEmptyBasicRetList()
        {
            static void Test<TRet>() where TRet : BaseRetModel
            {
                //  Arrange
                TestPkmApiConfigs configs = new()
                {
                    ReturnThisResLiDto = TestResLiDtos.GetResLiDtoWithZeroCount()
                };
                IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));
                IEnumerable<BasicRetModel> expected = [];

                //  Act
                IEnumerable<BasicRetModel>? actual = gateway.GetAll<TRet>();

                //  Assert
                actual.Should().BeEquivalentTo(expected);
            }

            Test<AbilityRetModel>();
            Test<FormRetModel>();
            Test<GenerationRetModel>();
            Test<ItemRetModel>();
            Test<MachineRetModel>();
            Test<MoveRetModel>();
            Test<MoveDamageClassRetModel>();
            Test<MoveLearnMethodRetModel>();
            Test<PokedexRetModel>();
            Test<PkmRetModel>();
            Test<SpeciesRetModel>();
            Test<TypeRetModel>();
            Test<VersionGroupRetModel>();
        }

        [Fact]
        public void GetAll_AllValidTypes_NullResults_ReturnEmptyBasicRetList()
        {
            static void Test<TRet>() where TRet : BaseRetModel
            {
                //  Arrange
                TestPkmApiConfigs configs = new()
                {
                    ReturnThisResLiDto = TestResLiDtos.GetResLiDtoWithNullResults()
                };
                IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));
                IEnumerable<BasicRetModel> expected = [];

                //  Act
                IEnumerable<BasicRetModel>? actual = gateway.GetAll<TRet>();

                //  Assert
                actual.Should().BeEquivalentTo(expected);
            }

            Test<AbilityRetModel>();
            Test<FormRetModel>();
            Test<GenerationRetModel>();
            Test<ItemRetModel>();
            Test<MachineRetModel>();
            Test<MoveRetModel>();
            Test<MoveDamageClassRetModel>();
            Test<MoveLearnMethodRetModel>();
            Test<PokedexRetModel>();
            Test<PkmRetModel>();
            Test<SpeciesRetModel>();
            Test<TypeRetModel>();
            Test<VersionGroupRetModel>();
        }

        [Fact]
        public void GetAll_AllValidTypes_NullResLiDto_ReturnNull()
        {
            static void Test<TRet>() where TRet : BaseRetModel
            {
                //  Arrange
                TestPkmApiConfigs configs = new()
                {
                    ReturnThisResLiDto = null
                };
                IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));

                //  Act
                IEnumerable<BasicRetModel>? actual = gateway.GetAll<TRet>();

                //  Assert
                actual.Should().BeNull();
            }

            Test<AbilityRetModel>();
            Test<FormRetModel>();
            Test<GenerationRetModel>();
            Test<ItemRetModel>();
            Test<MachineRetModel>();
            Test<MoveRetModel>();
            Test<MoveDamageClassRetModel>();
            Test<MoveLearnMethodRetModel>();
            Test<PokedexRetModel>();
            Test<PkmRetModel>();
            Test<SpeciesRetModel>();
            Test<TypeRetModel>();
            Test<VersionGroupRetModel>();
        }

        [Fact]
        public void GetAll_InvalidType_ReturnNull()
        {
            //  Arrange
            IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory());

            //  Act
            IEnumerable<BasicRetModel>? actual = gateway.GetAll<BaseRetModel>();

            //  Assert
            actual.Should().BeNull();
        }
        #endregion

        #region GetById
        [Fact]
        public void GetById_AllValidTypes_DtoFound_ReturnRet()
        {
            static void Test<TDto, TRet>() where TDto : class, IPkmApiDto where TRet : BaseRetModel
            {
                //  Arrange
                TestPkmApiConfigs configs;

                TDto? dto = TestDtos.GetTestBuilder<TDto>()?.GetBasic();
                if (dto is null)
                {
                    configs = new()
                    {
                        ReturnThisDto = []
                    };
                }
                else
                {
                    configs = new()
                    {
                        ReturnThisDto = { { typeof(TDto), dto } }
                    };
                }

                IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));
                int id = 1;

                //  Act
                TRet? actual = gateway.GetById<TRet>(id);

                //  Assert
                actual.Should().NotBeNull();
            }

            Test<AbilityDto, AbilityRetModel>();
            Test<FormDto, FormRetModel>();
            Test<GenerationDto, GenerationRetModel>();
            Test<ItemDto, ItemRetModel>();
            Test<MachineDto, MachineRetModel>();
            Test<MoveDto, MoveRetModel>();
            Test<MoveDamageClassDto, MoveDamageClassRetModel>();
            Test<MoveLearnMethodDto, MoveLearnMethodRetModel>();
            Test<PokedexDto, PokedexRetModel>();
            Test<PkmDto, PkmRetModel>();
            Test<SpeciesDto, SpeciesRetModel>();
            Test<TypeDto, TypeRetModel>();
            Test<VersionGroupDto, VersionGroupRetModel>();
        }

        [Fact]
        public void GetById_AllValidTypes_DtoWithEmptyListsFound_ReturnRet()
        {
            static void Test<TDto, TRet>() where TDto : class, IPkmApiDto where TRet : BaseRetModel
            {
                //  Arrange
                TestPkmApiConfigs configs;

                TDto? dto = TestDtos.GetTestBuilder<TDto>()?.GetEmpty();
                if (dto is null)
                {
                    configs = new()
                    {
                        ReturnThisDto = []
                    };
                }
                else
                {
                    configs = new()
                    {
                        ReturnThisDto = { { typeof(TDto), dto } }
                    };
                }

                IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));
                int id = 1;

                //  Act
                TRet? actual = gateway.GetById<TRet>(id);

                //  Assert
                actual.Should().NotBeNull();
            }

            Test<AbilityDto, AbilityRetModel>();
            Test<FormDto, FormRetModel>();
            Test<GenerationDto, GenerationRetModel>();
            Test<ItemDto, ItemRetModel>();
            Test<MachineDto, MachineRetModel>();
            Test<MoveDto, MoveRetModel>();
            Test<MoveDamageClassDto, MoveDamageClassRetModel>();
            Test<MoveLearnMethodDto, MoveLearnMethodRetModel>();
            Test<PokedexDto, PokedexRetModel>();
            Test<PkmDto, PkmRetModel>();
            Test<SpeciesDto, SpeciesRetModel>();
            Test<TypeDto, TypeRetModel>();
            Test<VersionGroupDto, VersionGroupRetModel>();
        }

        [Fact]
        public void GetById_AllValidTypes_PartialDtoFound_ReturnRet()
        {
            static void Test<TDto, TRet>() where TDto : class, IPkmApiDto where TRet : BaseRetModel
            {
                //  Arrange
                TestPkmApiConfigs configs;

                TDto? dto = TestDtos.GetTestBuilder<TDto>()?.GetShallow();
                if (dto is null)
                {
                    configs = new()
                    {
                        ReturnThisDto = []
                    };
                }
                else
                {
                    configs = new()
                    {
                        ReturnThisDto = { { typeof(TDto), dto } }
                    };
                }

                IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));
                int id = 1;

                //  Act
                TRet? actual = gateway.GetById<TRet>(id);

                //  Assert
                actual.Should().NotBeNull();
            }

            Test<AbilityDto, AbilityRetModel>();
            Test<FormDto, FormRetModel>();
            Test<GenerationDto, GenerationRetModel>();
            Test<ItemDto, ItemRetModel>();
            Test<MachineDto, MachineRetModel>();
            Test<MoveDto, MoveRetModel>();
            Test<MoveDamageClassDto, MoveDamageClassRetModel>();
            Test<MoveLearnMethodDto, MoveLearnMethodRetModel>();
            Test<PokedexDto, PokedexRetModel>();
            Test<PkmDto, PkmRetModel>();
            Test<SpeciesDto, SpeciesRetModel>();
            Test<TypeDto, TypeRetModel>();
            Test<VersionGroupDto, VersionGroupRetModel>();
        }

        [Fact]
        public void GetById_AllValidTypes_FullDtoFound_ReturnRet()
        {
            static void Test<TDto, TRet>() where TDto : class, IPkmApiDto where TRet : BaseRetModel
            {
                //  Arrange
                TestPkmApiConfigs configs;

                TDto? dto = TestDtos.GetTestBuilder<TDto>()?.GetFull();
                if (dto is null)
                {
                    configs = new()
                    {
                        ReturnThisDto = []
                    };
                }
                else
                {
                    configs = new()
                    {
                        ReturnThisDto = { { typeof(TDto), dto } }
                    };
                }

                IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));
                int id = 1;

                //  Act
                TRet? actual = gateway.GetById<TRet>(id);

                //  Assert
                actual.Should().NotBeNull();
            }

            Test<AbilityDto, AbilityRetModel>();
            Test<FormDto, FormRetModel>();
            Test<GenerationDto, GenerationRetModel>();
            Test<ItemDto, ItemRetModel>();
            Test<MachineDto, MachineRetModel>();
            Test<MoveDto, MoveRetModel>();
            Test<MoveDamageClassDto, MoveDamageClassRetModel>();
            Test<MoveLearnMethodDto, MoveLearnMethodRetModel>();
            Test<PokedexDto, PokedexRetModel>();
            Test<PkmDto, PkmRetModel>();
            Test<SpeciesDto, SpeciesRetModel>();
            Test<TypeDto, TypeRetModel>();
            Test<VersionGroupDto, VersionGroupRetModel>();
        }

        [Fact]
        public void GetById_AllValidTypes_DtoNotFound_ReturnNull()
        {
            static void Test<TDto, TRet>() where TDto : class, IPkmApiDto where TRet : BaseRetModel
            {
                //  Arrange
                TestPkmApiConfigs configs = new()
                {
                    ReturnThisDto = []
                };

                IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));
                int id = 1;

                //  Act
                TRet? actual = gateway.GetById<TRet>(id);

                //  Assert
                actual.Should().BeNull();
            }

            Test<AbilityDto, AbilityRetModel>();
            Test<FormDto, FormRetModel>();
            Test<GenerationDto, GenerationRetModel>();
            Test<ItemDto, ItemRetModel>();
            Test<MachineDto, MachineRetModel>();
            Test<MoveDto, MoveRetModel>();
            Test<MoveDamageClassDto, MoveDamageClassRetModel>();
            Test<MoveLearnMethodDto, MoveLearnMethodRetModel>();
            Test<PokedexDto, PokedexRetModel>();
            Test<PkmDto, PkmRetModel>();
            Test<SpeciesDto, SpeciesRetModel>();
            Test<TypeDto, TypeRetModel>();
            Test<VersionGroupDto, VersionGroupRetModel>();
        }

        [Fact]
        public void GetById_InvalidType_ReturnNull()
        {
            //  Arrange
            TestPkmApiConfigs configs = new()
            {
                ReturnThisDto = []
            };
            IPkmGateway gateway = PkmGatewayFactory.CreateGateway(new TestPkmApiFactory(configs));
            int id = 1;

            //  Act
            BaseRetModel? actual = gateway.GetById<BaseRetModel>(id);

            //  Assert
            actual.Should().BeNull();
        }
        #endregion
    }
}
