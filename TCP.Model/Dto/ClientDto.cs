namespace TCP.Model.Dto
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? CUIT { get; set; }
        public string? Adress { get; set; }
        public bool Disabled { get; set; }
    }
}
