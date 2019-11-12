using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace aa.Models
{
    [NotMapped]
    public class ProjetoViewModel : Projeto
    {
        public List<SelectListItem> UsuariosDisponiveis { get; set; }
        public List<string> UsuariosSelecionados { get; set; }

        public ProjetoViewModel()
        {
            UsuariosDisponiveis = new List<SelectListItem>();
            UsuariosSelecionados = new List<string>();
        }
    }
}