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
    
    public partial class Записи_На_Прием
    {
        public int Код_Записи { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public string Время_Приема { get; set; }
        public System.DateTime Дата_Приема { get; set; }
        public Nullable<int> Кабинет { get; set; }
        public Nullable<int> Код_Врача { get; set; }
        public Nullable<int> Номер_Карты { get; set; }
    
        public virtual Врачи Врачи { get; set; }
        public virtual Карта Карта { get; set; }
    }
}
