using System.Collections.Generic;

namespace WebStore.Domain.Entites.DTO
{
    public record PageProductDto(IEnumerable<ProductDto> Products, int TotalCount);

}
