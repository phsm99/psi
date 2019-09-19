using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aa.Models
{
    public class Usuario
    {
        public enum Tipo
        {
            Funcionario,
            Lider
        }
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public virtual ICollection<Tarefa> Tarefas { get; set; }
        [Required]
        public Tipo Cargo { get; set; }
    }
}