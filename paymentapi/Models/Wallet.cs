namespace paymentapi.Models
{
    public class Wallet
    {
        public long WalletId { get; set; }
        public string? WalletName { get; set; }
        public long WalletVersion { get; set; }
        public long WalletBalance { get; set; }
        
    }
}
