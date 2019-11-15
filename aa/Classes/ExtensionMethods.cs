using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace aa.Classes
{
    public static class ExtensionMethods
    {
        public static MvcHtmlString DropDownForEnumStatus(this HtmlHelper htmlHelper, Enum enume, string nomeCampo, Models.Status enumSelecionado)
        {
            string retorno = String.Format(" <select id='{0}' name='{0}' class='form-control'> ", nomeCampo);


            if (enumSelecionado == Models.Status.Finalizada)
            {
                retorno += String.Format("<option selected value='{0}'> {1} </option>", (int)enumSelecionado, enumSelecionado.GetDisplayName());
            }
            else if (enumSelecionado == Models.Status.EmAndamento)
            {
                retorno += String.Format("<option selected value='{0}'> {1} </option>", (int)enumSelecionado, enumSelecionado.GetDisplayName());
                retorno += String.Format("<option  value='{0}'> {1} </option>", (int)Models.Status.Pausada, Models.Status.Pausada.GetDisplayName());
                retorno += String.Format("<option  value='{0}'> {1} </option>", (int)Models.Status.Finalizada, Models.Status.Finalizada.GetDisplayName());
            }
            else if (enumSelecionado == Models.Status.Pausada)
            {
                retorno += String.Format("<option selected value='{0}'> {1} </option>", (int)enumSelecionado, enumSelecionado.GetDisplayName());
                retorno += String.Format("<option  value='{0}'> {1} </option>", (int)Models.Status.EmAndamento, Models.Status.EmAndamento.GetDisplayName());
                retorno += String.Format("<option  value='{0}'> {1} </option>", (int)Models.Status.Finalizada, Models.Status.Finalizada.GetDisplayName());
            }
            else
            {
                foreach (Models.Status item in Enum.GetValues(enume.GetType()))
                {
                    if (item == enumSelecionado)
                    {
                        retorno += String.Format("<option selected value='{0}'> {1} </option>", (int)item, item.GetDisplayName());
                    }
                    else
                    {
                        retorno += String.Format("<option value='{0}'> {1} </option>", (int)item, item.GetDisplayName());
                    }
                }
            }

            retorno += "</select>";
            return new MvcHtmlString(retorno);
        }


        public static string GetDisplayName(this Enum val)
        {
            return val.GetType()
                      .GetMember(val.ToString())
                      .FirstOrDefault()
                      ?.GetCustomAttribute<DescriptionAttribute>(false)
                      ?.Description
                      ?? val.ToString();
        }
    }

}