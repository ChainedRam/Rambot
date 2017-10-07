namespace KLD.TeamSpeak.Repository
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("IntroClip")]
    public partial class IntroClip
    {
        [Key]
        [StringLength(30)]
        public string UniqueId { get; set; }

        [Required]
        [StringLength(20)]
        public string ClipName { get; set; }

        public virtual TeamSpeakUser TeamSpeakUser { get; set; }
    }
}
