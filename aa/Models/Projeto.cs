using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace aa.Models
{
    public enum StatusProjeto
    {
        [Description("Em Andamento")]
        EmAndamento,
        Pausado,
        Finalizado
    }

    public class Projeto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [DisplayName("Membros")]
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public StatusProjeto Status { get; set; }
    }
}