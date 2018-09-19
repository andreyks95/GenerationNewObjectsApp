namespace DB_EF6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MorphObject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MorphObject()
        {
            Functions = new HashSet<Function>();
        }

        [Key]
        public int id_object { get; set; }

        [Required]
        [StringLength(4000)]
        public string name { get; set; }

        [StringLength(4000)]
        public string characteristic { get; set; }

        public int? id_classification { get; set; }

        public virtual Classification Classification { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Function> Functions { get; set; }
    }
}
