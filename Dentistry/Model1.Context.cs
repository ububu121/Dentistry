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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dentistryEntities : DbContext
    {
        public dentistryEntities()
            : base("name=dentistryEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Врачи> Врачи { get; set; }
        public virtual DbSet<Диагноз> Диагноз { get; set; }
        public virtual DbSet<Записи_На_Прием> Записи_На_Прием { get; set; }
        public virtual DbSet<Карта> Карта { get; set; }
        public virtual DbSet<Оказанные_Услуги> Оказанные_Услуги { get; set; }
        public virtual DbSet<Пользователи> Пользователи { get; set; }
        public virtual DbSet<Посещения> Посещения { get; set; }
        public virtual DbSet<Приемы> Приемы { get; set; }
        public virtual DbSet<Расписание> Расписание { get; set; }
        public virtual DbSet<Роли> Роли { get; set; }
        public virtual DbSet<Услуги> Услуги { get; set; }
    }
}
