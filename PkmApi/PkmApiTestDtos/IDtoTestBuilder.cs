using PkmApi.Dtos;

namespace PkmApiTestDtos
{
    public interface IDtoTestBuilder<TDto> where TDto : IPkmApiDto
    {
        /// <summary>
        /// Gets the very basic implementation of the TDto.
        /// Only required fields are filled in. All other fields are null.
        /// </summary>
        /// <returns></returns>
        TDto GetBasic();
        
        /// <summary>
        /// Gets an empty implementation of the TDto.
        /// All fields are not null. Objects and primitive fields are filled in.
        /// Lists are empty lists.
        /// This is a shallow implementation.
        /// </summary>
        /// <returns></returns>
        TDto GetEmpty();

        /// <summary>
        /// Gets a full implementation of the TDto.
        /// All fields are filled in.
        /// Every list has ONE item in it.
        /// All object fields also have their fields filled in completely.
        /// </summary>
        /// <returns></returns>
        TDto GetFull();

        /// <summary>
        /// Gets a shallow implementation of the TDto.
        /// All fields are not null.
        /// Lists have ONE item in it, that is a basic implementation of that item.
        /// This is a shallow implementation.
        /// </summary>
        /// <returns></returns>
        TDto GetShallow();
    }
}
