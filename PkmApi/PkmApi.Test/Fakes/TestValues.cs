using PkmApi.DTOs.Shared;

namespace PkmApi.Test.Fakes
{
    public static class TestValues
    {
        public static readonly BasicTestDTO Test_BasicTestDTO = new("testName", 1, [new BasicTestSDTO("testVal1"), new BasicTestSDTO("testVal2"), new BasicTestSDTO("testVal3")]);
        public static readonly ResLiDTO Test_ResLiDTO = new(10, "test-next", "test-previous", [new NamedApiResSDTO("test-res1", "test-res1-url"), new NamedApiResSDTO("test-res2", "test-res2-url")]);
    }
}
