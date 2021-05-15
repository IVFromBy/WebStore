namespace WebStore.Domain.Entites.DTO
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int ProductCount { get; set; }
    }

    public class SectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
        public int ProductCount { get; set; }
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public BrandDto Brand { get; set; }
        public SectionDto Section { get; set; }
    }

}
