//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfService1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Games
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Games()
        {
            this.Game_Area = new HashSet<Game_Area>();
            this.Game_Exhibition = new HashSet<Game_Exhibition>();
            this.Game_Genre = new HashSet<Game_Genre>();
        }
    
        public int ID_Game { get; set; }
        public int ID_Developer { get; set; }
        public int ID_Publisher { get; set; }
        public string Game_Name { get; set; }
        public int Year_of_Publication { get; set; }
        public double Brutal_Rating { get; set; }
        public string Official_Site { get; set; }
        public bool Cyber_Discipline { get; set; }
        public bool Network_Mode { get; set; }
    
        public virtual Developers Developers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Area> Game_Area { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Exhibition> Game_Exhibition { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game_Genre> Game_Genre { get; set; }
        public virtual Publishers Publishers { get; set; }
    }
}
