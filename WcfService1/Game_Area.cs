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
    
    public partial class Game_Area
    {
        public int ID_Game_Area { get; set; }
        public int ID_Game { get; set; }
        public int ID_Area { get; set; }
        public int Area_Game_Cost { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Games Games { get; set; }
    }
}
