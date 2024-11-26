namespace Domain.Models.payloads.request
{
    public class DetailRequestV1
    {
        public int ProductId { get; set; }
        public int InvoiceId { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public float SubTotal { get; set; }
    }
}