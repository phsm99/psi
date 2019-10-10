using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using aa.Models;

namespace aa.Models
{
    public class Equipe
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [DisplayName("Membros")]
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}