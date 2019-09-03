﻿using Dcs.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dcs.Core.DbContexts
{
    public class DcsDbContext : DbContext
    {
        private readonly string _ConnectionString;
        protected readonly DbContextOptions<DcsDbContext> _DbContectOptions;

        public DcsDbContext(DbContextOptions<DcsDbContext> options, string connectionString) : base()
        {
            _DbContectOptions = options;
            _ConnectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_ConnectionString);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected void OnModelCreatingBaseEntity<T>(ModelBuilder modelBuilder)
            where T: BaseEntity
        {
            modelBuilder.Entity<T>()
                .Property(b => b.Deleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<T>()
                .Property(b => b.Version)
                .HasDefaultValue(1);

            modelBuilder.Entity<T>()
                .Property(b => b.Created)
                .HasDefaultValueSql("GETDATE()");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingBaseEntity<UserAccount>(modelBuilder);
        }
        //Sample
        //public DbSet<BaseEntity> BaseEntity { get; set; }
        // dotnet ef migrations add "xxx"
        // dotnet ef database update
        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}
