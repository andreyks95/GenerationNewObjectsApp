namespace EntityFramework6_ConsoleApp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CodeFirstFromDatabase : DbContext
    {
        public CodeFirstFromDatabase()
            : base("name=CodeFirstFromDatabase")
        {
        }

        public virtual DbSet<Table> Table { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table>()
                .Property(e => e.FN)
                .IsFixedLength();

            modelBuilder.Entity<Table>()
                .Property(e => e.LastName)
                .IsFixedLength();
        }
    }
}
