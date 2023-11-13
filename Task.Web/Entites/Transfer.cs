
using Task.Web.Enum;

namespace Task.Web.Entites
{
    public class Transfer
    {
        public Guid Id { get; set; }
        public Guid FromUser { get; set; }
        public Guid ToUser { get; set; }
        public decimal balance { get; set; }
        public Status Status { get; set; }
    }
}