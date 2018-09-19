namespace MorphAnalysis
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MorphModel : DbContext
    {
        public MorphModel()
            : base("name=MorphModel")
        {
        }

        public virtual DbSet<Classification> Classifications { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<Modification> Modifications { get; set; }
        public virtual DbSet<MorphObject> MorphObjects { get; set; }
        public virtual DbSet<ParametersGoal> ParametersGoals { get; set; }
        public virtual DbSet<ParametersGoalsForModification> ParametersGoalsForModifications { get; set; }
        public virtual DbSet<ParametersGoalsForSolution> ParametersGoalsForSolutions { get; set; }
        public virtual DbSet<Solution> Solutions { get; set; }
        public virtual DbSet<SolutionsOfFunction> SolutionsOfFunctions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Classification>()
                .HasMany(e => e.Classifications1)
                .WithOptional(e => e.Classification1)
                .HasForeignKey(e => e.id_parentClassification);

            modelBuilder.Entity<Function>()
                .Property(e => e.weight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Function>()
                .HasMany(e => e.MorphObjects)
                .WithMany(e => e.Functions)
                .Map(m => m.ToTable("FunctionsOfMorphObjects").MapLeftKey("id_function").MapRightKey("id_object"));

            modelBuilder.Entity<Goal>()
                .Property(e => e.weight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Modification>()
                .HasMany(e => e.Solutions)
                .WithMany(e => e.Modifications)
                .Map(m => m.ToTable("ModificationsOfSolutions").MapLeftKey("id_modification").MapRightKey("id_solution"));

            modelBuilder.Entity<ParametersGoal>()
                .Property(e => e.avg)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ParametersGoalsForModification>()
                .Property(e => e.rating)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ParametersGoalsForSolution>()
                .Property(e => e.rating)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Solution>()
                .Property(e => e.weight)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SolutionsOfFunction>()
                .Property(e => e.rating)
                .HasPrecision(18, 0);
        }
    }
}
