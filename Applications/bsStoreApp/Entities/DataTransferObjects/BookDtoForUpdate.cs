
namespace Entities.DataTransferObjects
{
    public record BookDtoForUpdate(int Id, String Title, decimal Price);
    public record BookDto(int Id, String Title, decimal Price);
}
