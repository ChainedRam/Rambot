namespace KLD.TeamSpeak.Repository
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TeamSpeakUser")]
    public partial class TeamSpeakUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TeamSpeakUser()
        {
            ClipUses = new HashSet<ClipUse>();
        }

        [Key]
        [StringLength(30)]
        public string UniqueId { get; set; }

        [StringLength(100)]
        public string Nickname { get; set; }

        public int? ClientId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClipUse> ClipUses { get; set; }

        public virtual IntroClip IntroClip { get; set; }
    }
}
