//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dentistry
{
    using System;
    using System.Collections.Generic;
    
    public partial class Услуги
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Услуги()
        {
            this.Оказанные_Услуги = new HashSet<Оказанные_Услуги>();
        }
    
        public int Код_Услуги { get; set; }
        public string Наименование_Услуги { get; set; }
        public Nullable<int> Стоимость_Услуги { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Оказанные_Услуги> Оказанные_Услуги { get; set; }
    }
}
