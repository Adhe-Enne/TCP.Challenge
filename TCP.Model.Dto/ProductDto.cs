namespace TCP.Model.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? Code { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
