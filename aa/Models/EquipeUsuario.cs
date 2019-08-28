using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using aa.Models;

namespace aa.Models
{
    public class EquipeUsuario
    {
        public int Id { get; set; }
        public int EquipeId { get; set; }
        public virtual Equipe Equipe { get; set; }
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}