using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace aa.Classes
{
    public static class Utils
    {
        public static Dictionary<string, string> ObterAtributosNaoCompostos(object Objeto)
        {
            Dictionary<string, string> Dicionario = new Dictionary<string, string>();

            foreach (var atributo in Objeto.GetType().GetProperties())
            {
                if (atributo.PropertyType.IsPrimitive || atributo.PropertyType == typeof(Decimal) || atributo.PropertyType == typeof(String) ||
                    atributo.PropertyType == typeof(string) || atributo.PropertyType.FullName.Equals("aa.Models.Status") ||
                    atributo.Name.Equals("UsuarioId") || atributo.Name.Equals("DataEntrega"))
                {
                    if (atributo.Name.Equals("Id"))
                    {
                        continue;
                    }
                    if (atributo.GetCustomAttributesData().Count > 0 && atributo.GetCustomAttributesData()[0].AttributeType.ToString().Equals("System.ComponentModel.DisplayNameAttribute"))
                    {
                        Dicionario.Add(atributo.GetCustomAttributesData()[0].ConstructorArguments[0].Value.ToString(), atributo.GetValue(Objeto) == null ? "" : atributo.GetValue(Objeto).ToString());
                    }
                    else
                    {
                        Dicionario.Add(atributo.Name, atributo.GetValue(Objeto) == null ? "" : atributo.GetValue(Objeto).ToString());
                    }
                }
            }

            return Dicionario;
        }

        public static string SerializarDicionario(Dictionary<string, string> dicionario)
        {
            if (dicionario == null || dicionario.Count == 0)
            {
                return string.Empty;
            }

            return new JavaScriptSerializer().Serialize(dicionario);
        }

        public static Dictionary<string, string> CompararObjetos(Dictionary<string, string> TarefaAtual, Dictionary<string, string> TarefaAntiga)
        {
            Dictionary<string, string> DicionarioSaida = new Dictionary<string, string>();

            foreach (var key in TarefaAtual.Keys)
            {
                if (!TarefaAtual[key].Equals(TarefaAntiga[key]))
                {
                    DicionarioSaida.Add(key, TarefaAtual[key]);
                }
            }
            return DicionarioSaida;
        }
    }
}