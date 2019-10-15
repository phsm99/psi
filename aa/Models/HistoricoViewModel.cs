using aa.BD;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace aa.Models
{
    [NotMapped]
    public class Registro
    {
        public string UsuarioAlterador { get; set; }
        public string Alteracao { get; set; }
        public string DataAlteracao { get; set; }
        public string TipoRegistro { get; set; }
    }

    [NotMapped]
    public class HistoricoViewModel
    {



        AppContext _context;

        public HistoricoViewModel(List<HistoricoTarefa> ListaTarefas)
        {
            _context = new AppContext();
            Registros = new List<Registro>();
            foreach (var item in ListaTarefas)
            {
                Registro reg = new Registro();
                string alteracao = "";
                reg.UsuarioAlterador = "";
                Usuario usuario;
                if (item.UsuarioId != null && item.UsuarioId != 0)
                {
                    usuario = _context.Usuarios.Find(item.UsuarioId);
                    reg.UsuarioAlterador = usuario.Nome + " " + usuario.Sobrenome;
                }

                var altDicionario = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(item.Alteracoes);
                foreach (var alt in altDicionario)
                {
                    if (alt.Key.Equals("UsuarioId"))
                    {
                        if (!string.IsNullOrWhiteSpace(alt.Value))
                        {
                            usuario = _context.Usuarios.Find(Convert.ToInt32(alt.Value));
                            alteracao += "Responsável: " + usuario.Nome + " " + usuario.Sobrenome;
                        }
                        else
                        {
                            alteracao += "Responsável: ";
                        }
                    }
                    else if (alt.Key.Equals("Data de Entrega"))
                    {
                        alteracao += alt.Key + ": " + Convert.ToDateTime(alt.Value).ToShortDateString();
                    }
                    else
                    {
                        alteracao += alt.Key + ": " + alt.Value;
                    }
                    alteracao += "\n";
                }
                reg.Alteracao = alteracao;

                reg.DataAlteracao = item.DataAlteracao.ToString();

                reg.TipoRegistro = item.TipoAlteracao.ToString();

                Registros.Add(reg);
            }
        }

        public List<Registro> Registros { get; set; }


    }
}