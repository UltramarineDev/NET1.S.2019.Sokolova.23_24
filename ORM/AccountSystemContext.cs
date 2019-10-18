using System.Data.Entity;


namespace ORM
{
    public class AccountSystemContext : DbContext
    {
        public AccountSystemContext() : base() { }

        //Database.SetInitializer<AccountSystemContext>(new CreateDatabaseIfNotExists<AccountSystemContext>());

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountOwner> Owners { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.AccountNumber);

            modelBuilder.Entity<AccountOwner>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.AccountOwner)
                .HasForeignKey(e => e.AccountOwnerId)
                .WillCascadeOnDelete(false);
        }
    }
}
