namespace Task.Web.Models
{
    public class TransferVM
    {
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
        public decimal balance { get; set; }
    }
}
