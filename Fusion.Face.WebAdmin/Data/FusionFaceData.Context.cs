﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fusion.Face.WebAdmin.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FusionFaceDb : DbContext
    {
        public FusionFaceDb()
            : base("name=FusionFaceDb")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ApplicationMaster> ApplicationMasters { get; set; }
        public virtual DbSet<DateMaster> DateMasters { get; set; }
        public virtual DbSet<FunctionMaster> FunctionMasters { get; set; }
        public virtual DbSet<GroupMaster> GroupMasters { get; set; }
        public virtual DbSet<MenuMaster> MenuMasters { get; set; }
        public virtual DbSet<ModuleMaster> ModuleMasters { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<TransactionMaster> TransactionMasters { get; set; }
        public virtual DbSet<TransactionSummary> TransactionSummaries { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }
    }
}