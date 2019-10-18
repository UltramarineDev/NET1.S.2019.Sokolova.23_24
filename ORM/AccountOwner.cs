using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public class AccountOwner
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Email { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}

