namespace Task.Web.Models
{
    public class TransferVM
    {
        public Guid FromUser { get; set; }
        public string PhoneNumber { get; set; }
        public decimal balance { get; set; }
    }
}
