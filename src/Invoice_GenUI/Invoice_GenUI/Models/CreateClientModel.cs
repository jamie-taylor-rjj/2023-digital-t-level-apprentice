namespace Invoice_GenUI.Models
{
    public class CreateClientModel
    {
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? ClientAddress { get; set; }
        public string? ContactName { get; set; }
        public string? ContactEmail { get; set; }
    }
}
