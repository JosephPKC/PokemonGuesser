using PkmApi.Dtos.Shared;

namespace PkmApi.Test.Fakes
{
    public static class TestValues
    {
        public static readonly BasicTestDto Test_BasicTestDto = new("testName", 1, [new BasicTestSdto("testVal1"), new BasicTestSdto("testVal2"), new BasicTestSdto("testVal3")]);
        public static readonly ResLiDto Test_ResLiDto = new(10, "test-next", "test-previous", [new NamedApiResDto("test-res1", "test-res1-url"), new NamedApiResDto("test-res2", "test-res2-url")]);
    }
}
