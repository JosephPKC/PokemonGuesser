namespace PkmApi.DTOs.Shared
{
    public record VersionGameIndexSDTO(
        int?             GameIndex = null,
        NamedApiResSDTO? Version = null
    );
}
