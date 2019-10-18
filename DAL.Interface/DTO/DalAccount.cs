namespace DAL.Interface.DTO
{
    public class DalAccount: IEntity
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal BonusPoints { get; set; }
        public string AccountType { get; set; }

        public int AccountOwnerId { get; set; }
    }
}