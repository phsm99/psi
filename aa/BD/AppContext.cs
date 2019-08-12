using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using aa.Models;

namespace aa.BD
{
    public class AppContext : DbContext
    {

        public AppContext() : base("AppContext")
        {
        }

        public DbSet<Tarefa> Tarefas { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}