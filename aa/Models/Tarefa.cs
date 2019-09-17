using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Titulo { get; set; }
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Data Criação")]
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int? UsuarioId { get; set; }
        [DisplayName("Responsável")]
        public virtual Usuario Usuario { get; set; }

        public Status Status { get; set; }

    }
}