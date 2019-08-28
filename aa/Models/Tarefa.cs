using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace aa.Models
{
    public enum Status
    {
        NãoAtribuida,
        EmAndamento,
        Finalizado
    }
    public class Tarefa
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int? UsuarioId { get; set; }
        [DisplayName("Responsável")]
        public virtual Usuario Usuario { get; set; }

        public Status Status { get; set; }

    }
}