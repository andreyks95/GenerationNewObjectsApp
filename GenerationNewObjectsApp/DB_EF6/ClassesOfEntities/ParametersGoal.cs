namespace DB_EF6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ParametersGoal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ParametersGoal()
        {
            ParametersGoalsForModifications = new HashSet<ParametersGoalsForModification>();
            ParametersGoalsForSolutions = new HashSet<ParametersGoalsForSolution>();
        }

        [Key]
        public int id_parameter { get; set; }

        [Required]
        [StringLength(4000)]
        public string name { get; set; }

        [StringLength(20)]
        public string unit { get; set; }

        public decimal? avg { get; set; }

        public int? id_goal { get; set; }

        public virtual Goal Goal { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParametersGoalsForModification> ParametersGoalsForModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParametersGoalsForSolution> ParametersGoalsForSolutions { get; set; }
    }
}
