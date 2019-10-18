using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public class Account
    {
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        public decimal Balance { get; set; }

        public decimal BonusPoints { get; set; }

        public string AccountType { get; set; }

        public int AccountOwnerId { get; set; }
        public AccountOwner AccountOwner { get; set; }
    }
}