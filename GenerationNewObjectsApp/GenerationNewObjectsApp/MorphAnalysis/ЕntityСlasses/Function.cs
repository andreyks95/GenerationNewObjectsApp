namespace MorphAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Function
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Function()
        {
            SolutionsOfFunctions = new HashSet<SolutionsOfFunction>();
            MorphObjects = new HashSet<MorphObject>();
        }

        [Key]
        public int id_function { get; set; }

        [Required]
        [StringLength(4000)]
        public string name { get; set; }

        [StringLength(4000)]
        public string characteristics { get; set; }

        public decimal? weight { get; set; }

        public int? id_classification { get; set; }

        public virtual Classification Classification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolutionsOfFunction> SolutionsOfFunctions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MorphObject> MorphObjects { get; set; }
    }
}
