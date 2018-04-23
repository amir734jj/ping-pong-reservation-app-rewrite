using System;
using System.Linq;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.RawModels;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace DataAccessLayer
{
    public sealed class EntityDbContext: DbContext
    {
        private readonly Action<DbContextOptionsBuilder> _onConfiguring;
        
        public Microsoft.EntityFrameworkCore.DbSet<RawReservation> RawPdfInfos { get; set; }

        /// <summary>
        /// Constructor that will be called by startup.cs
        /// </summary>
        /// <param name="dbContextOptionsBuilder"></param>
        /// <param name="onConfiguring"></param>
        public EntityDbContext(DbContextOptionsBuilder dbContextOptionsBuilder, Action<DbContextOptionsBuilder> onConfiguring)
        {
            _onConfiguring = onConfiguring;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => _onConfiguring(optionsBuilder);
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership)
                .ToList()
                .ForEach(x => x.DeleteBehavior = DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}