namespace KLD.TeamSpeak.Repository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clip")]
    public partial class Clip
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clip()
        {
            ClipUses = new HashSet<ClipUse>();
        }

        [Key]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Source { get; set; }

        public double? Length { get; set; }

        public int AddedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClipUse> ClipUses { get; set; }
    }
}
