//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RambotRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class TeamSpeakUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TeamSpeakUser()
        {
            this.TeamSpeakLocalUsers = new HashSet<TeamSpeakLocalUser>();
        }
    
        public System.Guid Rid { get; set; }
        public string TSuid { get; set; }
        public Nullable<int> TSlid { get; set; }
        public string Nickname { get; set; }
    
        public virtual RamUser RamUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamSpeakLocalUser> TeamSpeakLocalUsers { get; set; }
    }
}
