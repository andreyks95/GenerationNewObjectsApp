namespace DB_EF6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Solution
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Solution()
        {
            ParametersGoalsForSolutions = new HashSet<ParametersGoalsForSolution>();
            SolutionsOfFunctions = new HashSet<SolutionsOfFunction>();
            Modifications = new HashSet<Modification>();
        }

        [Key]
        public int id_solution { get; set; }

        [Required]
        [StringLength(4000)]
        public string name { get; set; }

        [StringLength(4000)]
        public string characteristic { get; set; }

        [StringLength(4000)]
        public string bibliographic_description { get; set; }

        public decimal? weight { get; set; }

        public int? id_classification { get; set; }

        public virtual Classification Classification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ParametersGoalsForSolution> ParametersGoalsForSolutions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolutionsOfFunction> SolutionsOfFunctions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Modification> Modifications { get; set; }
    }
}
