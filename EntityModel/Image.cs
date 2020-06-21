namespace Catmash.EntityModel
{
    public class Image
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public decimal Score { get; set; }
        public ulong Votes { get; set; }
    }
}