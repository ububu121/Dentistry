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
    
    public partial class Пользователи
    {
        public int Код_Пользователя { get; set; }
        public string Фамилия_Пользователя { get; set; }
        public string Имя_Пользователя { get; set; }
        public string Отчество_Пользователя { get; set; }
        public string Логин_Пользователя { get; set; }
        public string Пароль_Пользователя { get; set; }
        public int Роль_Пользователя { get; set; }
    
        public virtual Роли Роли { get; set; }
    }
}
