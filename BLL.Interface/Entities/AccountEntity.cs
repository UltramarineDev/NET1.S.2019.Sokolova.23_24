namespace BLL.Interface.Entities
{
    public class AccountEntity
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal BonusPoints { get; set; }
        public string AccountType { get; set; }
        public int AccountId { get; set; }
        public int AccountOwnerId { get; set; }
    }
}
