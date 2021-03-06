namespace MorphAnalysis
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ParametersGoalsForModification
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_parameter { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_modification { get; set; }

        public decimal? rating { get; set; }

        public virtual Modification Modification { get; set; }

        public virtual ParametersGoal ParametersGoal { get; set; }
    }
}
