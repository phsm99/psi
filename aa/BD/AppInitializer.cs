using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using aa.Models;

namespace aa.BD
{
    public class AppInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            var Tar = new List<Tarefa> { new Tarefa { Titulo = "Primeira Tarefa" }, new Tarefa { Titulo = "Segunda Tarefa" } };
            Tar.ForEach(s => context.Tarefas.Add(s));
            context.SaveChanges();
        }
    }
}