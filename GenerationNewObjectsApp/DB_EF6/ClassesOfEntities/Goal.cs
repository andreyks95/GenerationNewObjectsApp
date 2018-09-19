namespace DB_EF6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Goal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Goal()
        {
            ParametersGoals = new HashSet<ParametersGoal>();
        }

        [Key]
        public int id_goal { get; set; }

        [Required]
        [StringLength(4000)]
        public string name { get; set; }

        [StringLength(4000)]
        public string characteristic { get; set; }

        public decimal? weight { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParametersGoal> ParametersGoals { get; set; }
    }
}
