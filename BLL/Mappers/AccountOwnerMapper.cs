using DAL.Interface.DTO;
using BLL.Interface.Entities;

namespace BLL.Mappers
{
    public static class AccountOwnerMapper
    {
        public static DalAccountOwner ToDalAccountOwner(this AccountOwnerEntity accountOwner)
        {
            return new DalAccountOwner()
            {
                Email = accountOwner.Email,
                FirstName = accountOwner.FirstName,
                Id = accountOwner.Id,
                LastName = accountOwner.LastName
            };
        }

        public static AccountOwnerEntity ToBllAccountOwner(this DalAccountOwner dalAccountOwner)
        {
            return new AccountOwnerEntity()
            {
                Email = dalAccountOwner.Email,
                FirstName = dalAccountOwner.FirstName,
                Id = dalAccountOwner.Id,
                LastName = dalAccountOwner.LastName
            };
        }
    }
}
