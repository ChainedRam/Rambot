namespace KLD.TeamSpeak.Repository
{
    using System.Data.Entity;

    public partial class TeamSpeakContext : DbContext
    {
        public TeamSpeakContext()
            : base(@"data source=KLD-PC0\ChainedRam;initial catalog=TeamSpeak;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {
        }

        public virtual DbSet<Clip> Clips { get; set; }
        public virtual DbSet<ClipUse> ClipUses { get; set; }
        public virtual DbSet<IntroClip> IntroClips { get; set; }
        public virtual DbSet<TeamSpeakUser> TeamSpeakUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clip>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Clip>()
                .Property(e => e.Source)
                .IsUnicode(false);

            modelBuilder.Entity<Clip>()
                .HasMany(e => e.ClipUses)
                .WithRequired(e => e.Clip)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClipUse>()
                .Property(e => e.UniqueId)
                .IsUnicode(true);

            modelBuilder.Entity<ClipUse>()
                .Property(e => e.ClipName)
                .IsUnicode(false);

            modelBuilder.Entity<IntroClip>()
                .Property(e => e.UniqueId)
                .IsUnicode(true);

            modelBuilder.Entity<IntroClip>()
                .Property(e => e.ClipName)
                .IsUnicode(false);

            modelBuilder.Entity<TeamSpeakUser>()
                .Property(e => e.UniqueId)
                .IsUnicode(true);

            modelBuilder.Entity<TeamSpeakUser>()
                .Property(e => e.Nickname)
                .IsFixedLength();

            modelBuilder.Entity<TeamSpeakUser>()
                .HasMany(e => e.ClipUses)
                .WithRequired(e => e.TeamSpeakUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TeamSpeakUser>()
                .HasOptional(e => e.IntroClip)
                .WithRequired(e => e.TeamSpeakUser);
        }
    }
}
