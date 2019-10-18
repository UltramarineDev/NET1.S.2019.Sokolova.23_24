using DAL.Interface.DTO;
using ORM;

namespace DAL.EntityFramework.Mappers
{
    public static class AccountOwnerMapper
    {
        public static DalAccountOwner ToDalAccountOwner(this AccountOwner accountOwner)
        {
            return new DalAccountOwner()
            {
                Email = accountOwner.Email,
                FirstName = accountOwner.FirstName,
                Id = accountOwner.Id,
                LastName = accountOwner.LastName
            };
        }

        public static AccountOwner ToAccountOwnerORM(this DalAccountOwner dalAccountOwner)
        {
            return new AccountOwner()
            {
                Email = dalAccountOwner.Email,
                FirstName = dalAccountOwner.FirstName,
                Id = dalAccountOwner.Id,
                LastName = dalAccountOwner.LastName
            };
        }
    }
}
