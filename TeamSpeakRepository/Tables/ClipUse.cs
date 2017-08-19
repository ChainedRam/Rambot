namespace KLD.TeamSpeak.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClipUse")]
    public partial class ClipUse
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string UniqueId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string ClipName { get; set; }

        public int? playedCount { get; set; }

        public virtual Clip Clip { get; set; }

        public virtual TeamSpeakUser TeamSpeakUser { get; set; }
    }
}
