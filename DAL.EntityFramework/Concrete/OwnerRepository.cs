using DAL.Interface.DTO;
using DAL.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using ORM;
using DAL.EntityFramework.Mappers;

namespace DAL.EntityFramework.Concrete
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DbContext context;

        public OwnerRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Add(DalAccountOwner entity)
        {
            context.Set<AccountOwner>().Add(entity.ToAccountOwnerORM());
        }

        public DalAccountOwner Find(Expression<Func<DalAccountOwner, bool>> predicate)
        {
            Expression<Func<DalAccountOwner, AccountOwner>> convert =
             accountOwner => accountOwner.ToAccountOwnerORM();

            var param = Expression.Parameter(typeof(AccountOwner));
            var body = Expression.Invoke(predicate,
              Expression.Invoke(convert, param));

            var lambda = Expression.Lambda<Func<AccountOwner, bool>>(body, param);
            var func = lambda.Compile();

            return context.Set<AccountOwner>().Find(func).ToDalAccountOwner();
        }


        public DalAccountOwner Get(int id)
        {
            var ormAccountOwner = context.Set<AccountOwner>().FirstOrDefault(accountOwner => accountOwner.Id == id);
            return ormAccountOwner.ToDalAccountOwner();
        }

        public IEnumerable<DalAccountOwner> GetAll()
            => context.Set<AccountOwner>().Select(accountOwner => accountOwner.ToDalAccountOwner());

        public void Remove(DalAccountOwner entity)
        {
            var accountOwnerORM = entity.ToAccountOwnerORM();
            var accountOwner = context.Set<AccountOwner>().Single(u => u.Id == accountOwnerORM.Id);
            context.Set<AccountOwner>().Remove(accountOwner);
        }
    }
}
