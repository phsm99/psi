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
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int Idade { get; set; }
        public string CPF { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public virtual ICollection<Tarefa> Tarefas { get; set; }
        public Tipo Cargo { get; set; }
    }
}