using System.Data.Entity;
using Credits1.Models.Mapping;

namespace Credits1.Models
{
    public partial class CreditsFirmsContext : DbContext
    {
        static CreditsFirmsContext()
        {
            Database.SetInitializer<CreditsFirmsContext>(null);
        }

        public CreditsFirmsContext()
            : base("Name=CreditsFirmsContext")
        {
        }

        public DbSet<Accrued_interest> Accrued_interest { get; set; }
        public DbSet<Collateral> Collaterals { get; set; }
        public DbSet<Credit> Credits { get; set; }
        public DbSet<Currencies_directory> Currencies_directory { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FineOnCredit> FineOnCredits { get; set; }
        public DbSet<FineOnInterest> FineOnInterests { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<Individual> Individuals { get; set; }
        public DbSet<Interest_rate> Interest_rate { get; set; }        
        public DbSet<Monitoring_collateral> Monitoring_collateral { get; set; }
        public DbSet<Overdue_interest> Overdue_interest { get; set; }
        public DbSet<Overdue_principal_debt> Overdue_principal_debt { get; set; }
        public DbSet<Principal_balance> Principal_balance { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<TypeCollateral> TypeCollaterals { get; set; }
        public DbSet<FormCollateral> FormCollaterals { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Accrued_interestMap());
            modelBuilder.Configurations.Add(new CollateralMap());
            modelBuilder.Configurations.Add(new CreditMap());
            modelBuilder.Configurations.Add(new Currencies_directoryMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new EmployeeMap());
            modelBuilder.Configurations.Add(new FineOnCreditMap());
            modelBuilder.Configurations.Add(new FineOnInterestMap());
            modelBuilder.Configurations.Add(new FirmMap());
            modelBuilder.Configurations.Add(new IndividualMap());
            modelBuilder.Configurations.Add(new Interest_rateMap());           
            modelBuilder.Configurations.Add(new Monitoring_collateralMap());
            modelBuilder.Configurations.Add(new Overdue_interestMap());
            modelBuilder.Configurations.Add(new Overdue_principal_debtMap());
            modelBuilder.Configurations.Add(new Principal_balanceMap());
            modelBuilder.Configurations.Add(new PropertyMap());
            modelBuilder.Configurations.Add(new TypeCollateralMap());
            modelBuilder.Configurations.Add(new FormCollateralMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
        }
    }
}
