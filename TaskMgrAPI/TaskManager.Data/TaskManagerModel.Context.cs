﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaskManager.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public partial class TaskManagerCapsuleEntities : DbContext
    {
        public TaskManagerCapsuleEntities()
            : base("name=TaskManagerCapsuleEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ParentTask> ParentTasks { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
    }
}
