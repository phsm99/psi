using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace aa.Models
{
    public enum Status
    {
        [Description("Não Atribuída")]
        NãoAtribuida,
        [Description("Em Andamento")]
        EmAndamento,
        Pausada,
        Finalizada
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


        [DisplayName("Data de Entrega")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DataEntrega { get; set; }

        [DisplayName("Responsável")]
        public virtual Usuario Usuario { get; set; }
        public int? UsuarioId { get; set; }

        public Status Status { get; set; }

        public virtual ICollection<HistoricoTarefa> Historicos { get; set; }
    }
}