namespace DB_EF6
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ParametersGoalsForSolution
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_parameter { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_solution { get; set; }

        public decimal? rating { get; set; }

        public virtual ParametersGoal ParametersGoal { get; set; }

        public virtual Solution Solution { get; set; }
    }
}
