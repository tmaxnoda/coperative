namespace CyberCooperative_Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CoperativeDB : DbContext
    {
        public CoperativeDB()
            : base("name=CoperativeDB")
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<ContributionPattern_history_Table> ContributionPattern_history_Table { get; set; }
        public virtual DbSet<Employee_Table> Employee_Table { get; set; }
        public virtual DbSet<Loan_Table> Loan_Table { get; set; }
        public virtual DbSet<LoanInterestConfiguration_Table> LoanInterestConfiguration_Table { get; set; }
        public virtual DbSet<LoanRepayment_Table> LoanRepayment_Table { get; set; }
        public virtual DbSet<LoanTrasactionTimeline> LoanTrasactionTimelines { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Transaction_Table> Transaction_Table { get; set; }
        public virtual DbSet<Transaction_TableOld> Transaction_TableOld { get; set; }
        public virtual DbSet<ELMAH_Error> ELMAH_Error { get; set; }
        public virtual DbSet<Employee_TableA> Employee_TableA { get; set; }
        public virtual DbSet<Loan_TableA> Loan_TableA { get; set; }
        public virtual DbSet<Loan_TableBKC> Loan_TableBKC { get; set; }
        public virtual DbSet<LoanRepayment_TableA> LoanRepayment_TableA { get; set; }
        public virtual DbSet<LoanRepayment_TableB> LoanRepayment_TableB { get; set; }
        public virtual DbSet<LoanRepayment_TableCOpy> LoanRepayment_TableCOpy { get; set; }
        public virtual DbSet<LoanRepayment_TableCOpyA> LoanRepayment_TableCOpyA { get; set; }
        public virtual DbSet<LoanRepayment_TableCOpyAx> LoanRepayment_TableCOpyAx { get; set; }
        public virtual DbSet<LoanRepayment_Table_latest> LoanRepayment_Table_latest { get; set; }
        public virtual DbSet<LoanRepayment_TableB1> LoanRepayment_TableB1 { get; set; }
        public virtual DbSet<Transaction_TableBak> Transaction_TableBak { get; set; }
        public virtual DbSet<Transaction_Tablett> Transaction_Tablett { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<ContributionPattern_history_Table>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ContributionPattern_history_Table>()
                .Property(e => e.RegistrationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.PostalAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.ContactAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.Occupation)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.NextOfKin)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.NextOfKinTelephoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.MonthlySavings)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.NumberOfSharesAppliedFor)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.ValuesOfShares)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.RegistrationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Table>()
                .HasMany(e => e.ContributionPattern_history_Table)
                .WithOptional(e => e.Employee_Table)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee_Table>()
                .HasMany(e => e.LoanRepayment_Table)
                .WithOptional(e => e.Employee_Table)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee_Table>()
                .HasMany(e => e.Loan_Table)
                .WithOptional(e => e.Employee_Table)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee_Table>()
                .HasMany(e => e.LoanTrasactionTimelines)
                .WithOptional(e => e.Employee_Table)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee_Table>()
                .HasMany(e => e.Transaction_TableOld)
                .WithOptional(e => e.Employee_Table)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<Employee_Table>()
                .HasMany(e => e.Transaction_Table)
                .WithOptional(e => e.Employee_Table)
                .HasForeignKey(e => e.EmployeeId);

            modelBuilder.Entity<Loan_Table>()
                .Property(e => e.AmountBorrowed)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_Table>()
                .Property(e => e.RepaymentInterest)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_Table>()
                .Property(e => e.MonthlyRepaymentAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_Table>()
                .Property(e => e.TotalMonthlyRepaymentWithContributions)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_Table>()
                .Property(e => e.TotalLoanRepaid)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_Table>()
                .Property(e => e.TotalLoanDue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_Table>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Loan_Table>()
                .HasMany(e => e.LoanRepayment_Table)
                .WithOptional(e => e.Loan_Table)
                .HasForeignKey(e => e.LoanId);

            modelBuilder.Entity<Loan_Table>()
                .HasMany(e => e.LoanTrasactionTimelines)
                .WithOptional(e => e.Loan_Table)
                .HasForeignKey(e => e.LoanId);

            modelBuilder.Entity<LoanInterestConfiguration_Table>()
                .HasMany(e => e.Loan_Table)
                .WithOptional(e => e.LoanInterestConfiguration_Table)
                .HasForeignKey(e => e.LoanInterestConfigId);

            modelBuilder.Entity<LoanRepayment_Table>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_Table>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_Table>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_Table>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_Table>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<LoanRepayment_Table>()
                .Property(e => e.RealLoanPayment)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_Table>()
                .Property(e => e.MonthlyContribution)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanTrasactionTimeline>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<LoanTrasactionTimeline>()
                .Property(e => e.TotalLoandue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanTrasactionTimeline>()
                .Property(e => e.TotalLoanPaid)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanTrasactionTimeline>()
                .Property(e => e.BalanceTobePaid)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanTrasactionTimeline>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<LoanTrasactionTimeline>()
                .Property(e => e.RegistrationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_Table>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_Table>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Transaction_Table>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_Table>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_Table>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<Transaction_TableOld>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_TableOld>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Transaction_TableOld>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_TableOld>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_TableOld>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.PostalAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.ContactAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.Occupation)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.NextOfKin)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.NextOfKinTelephoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.MonthlySavings)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.NumberOfSharesAppliedFor)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.ValuesOfShares)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.RegistrationNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_TableA>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Loan_TableA>()
                .Property(e => e.AmountBorrowed)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableA>()
                .Property(e => e.RepaymentInterest)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableA>()
                .Property(e => e.MonthlyRepaymentAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableA>()
                .Property(e => e.TotalMonthlyRepaymentWithContributions)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableA>()
                .Property(e => e.TotalLoanDue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableA>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Loan_TableBKC>()
                .Property(e => e.AmountBorrowed)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableBKC>()
                .Property(e => e.RepaymentInterest)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableBKC>()
                .Property(e => e.MonthlyRepaymentAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableBKC>()
                .Property(e => e.TotalMonthlyRepaymentWithContributions)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableBKC>()
                .Property(e => e.TotalLoanRepaid)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableBKC>()
                .Property(e => e.TotalLoanDue)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Loan_TableBKC>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableA>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableA>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableA>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableA>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableA>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<LoanRepayment_TableA>()
                .Property(e => e.RealLoanPayment)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableA>()
                .Property(e => e.MonthlyContribution)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableB>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableB>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableB>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableB>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableB>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<LoanRepayment_TableB>()
                .Property(e => e.RealLoanPayment)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableB>()
                .Property(e => e.MonthlyContribution)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableCOpy>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableCOpy>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableCOpy>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableCOpy>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableCOpy>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<LoanRepayment_TableCOpy>()
                .Property(e => e.RealLoanPayment)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableCOpy>()
                .Property(e => e.MonthlyContribution)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableCOpyA>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableCOpyA>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableCOpyA>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableCOpyA>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableCOpyA>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<LoanRepayment_TableCOpyA>()
                .Property(e => e.RealLoanPayment)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableCOpyA>()
                .Property(e => e.MonthlyContribution)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableCOpyAx>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableCOpyAx>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableCOpyAx>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableCOpyAx>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableCOpyAx>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<LoanRepayment_TableCOpyAx>()
                .Property(e => e.RealLoanPayment)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableCOpyAx>()
                .Property(e => e.MonthlyContribution)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_Table_latest>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_Table_latest>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_Table_latest>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_Table_latest>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_Table_latest>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<LoanRepayment_Table_latest>()
                .Property(e => e.RealLoanPayment)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_Table_latest>()
                .Property(e => e.MonthlyContribution)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableB1>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableB1>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableB1>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableB1>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<LoanRepayment_TableB1>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<LoanRepayment_TableB1>()
                .Property(e => e.RealLoanPayment)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LoanRepayment_TableB1>()
                .Property(e => e.MonthlyContribution)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Transaction_TableBak>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_TableBak>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Transaction_TableBak>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_TableBak>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_TableBak>()
                .Property(e => e.Department)
                .IsFixedLength();

            modelBuilder.Entity<Transaction_Tablett>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_Tablett>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Transaction_Tablett>()
                .Property(e => e.Month)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_Tablett>()
                .Property(e => e.Year)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction_Tablett>()
                .Property(e => e.Department)
                .IsFixedLength();
        }
    }
}
