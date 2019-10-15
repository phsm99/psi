using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace aa.Classes
{
    public static class ExtensionMethods
    {
        public static MvcHtmlString DropDownForEnum(this HtmlHelper htmlHelper, Enum enume, string nomeCampo)
        {
            string retorno = String.Format(" <select id='{0}' name='{0}' class='form-control'> ", nomeCampo);
            foreach (aa.Models.Status item in Enum.GetValues(enume.GetType()))
            {
                retorno += String.Format("<option value='{0}'> {1} </option>", (int)item, item.GetDisplayName());
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