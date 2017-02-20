using StepByStep.Web.Entity;
using StepByStep.Web.Infra.Flow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace StepByStep.Web.Helpers
{
    public static class Helper
    {
        public static MvcHtmlString ProgressBarPara(this HtmlHelper helper, string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));


            StepByStepContext context = new StepByStepContext();
            var passoConcluido = context.Users.First(u => u.UserName == username).PassoFeito;

            var workFlowItensJaFeitos = Enum.GetValues(typeof(WorkFlow)).OfType<WorkFlow>().Where(w => w <= passoConcluido && w != WorkFlow.Start).ToList();

            var div = "<div class=\"col-xs-3 bs-wizard-step $STATUS$\">" +
                        "<div class=\"text-center bs-wizard-stepnum\"> $ELEMENTO$ </div>" +
                        "<div class=\"progress\"><div class=\"progress-bar\"></div></div>" +
                        "$LINK$" +
                        "<div class=\"bs-wizard-info text-center\"></div></div>";
            var resultadoFinal = new StringBuilder();

            if (workFlowItensJaFeitos.Count > 0)
            {
                foreach (var workflowitem in workFlowItensJaFeitos)
                {
                    var url = helper.RouteLink(workflowitem.Description(), BuscarRota(workflowitem), new Dictionary<string, object>() { { "class", "" } });
                    var urlSemNome = HttpUtility.HtmlDecode(url.ToString()).Replace(workflowitem.Description(), "").Replace("class", "class='bs-wizard-dot'");
                    resultadoFinal.Append(div.Replace("$ELEMENTO$", workflowitem.Description())
                                             .Replace("$STATUS$", "complete")
                                             .Replace("$LINK$", urlSemNome));
                }
            }

            div = workFlowItensJaFeitos.Count == 0 ? div.Replace("progress", "progress unico") : div;

            var proximoPasso = passoConcluido + 1;
            var urlPassoAtual = helper.RouteLink(proximoPasso.Description(), BuscarRota(proximoPasso), new Dictionary<string, object>() { { "class", "" } });
            var urlPassoAtualSemNome = HttpUtility.HtmlDecode(urlPassoAtual.ToString()).Replace(proximoPasso.Description(), "").Replace("class", "class='bs-wizard-dot active'");
            resultadoFinal.Append(div.Replace("$ELEMENTO$", proximoPasso.Description())
                                     .Replace("$STATUS$", "active")
                                     .Replace("$LINK$", urlPassoAtualSemNome));

            return new MvcHtmlString(resultadoFinal.ToString());
        }

        public static string Description<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }

        public static RouteValueDictionary BuscarRota(WorkFlow item)
        {
            switch (item)
            {
                case WorkFlow.DadosPessoais:
                    return RedirectTo("DadosPessoais", "Formulario");
                case WorkFlow.DadosDependente:
                    return RedirectTo("DadosDependente", "Formulario");
                case WorkFlow.DadosEndereco:
                    return RedirectTo("DadosEndereco", "Formulario");
                case WorkFlow.Confirmacao:
                    return RedirectTo("Confirmacao", "Formulario");
                default:
                    return RedirectTo("DadosPessoais", "Formulario");
            }
        }

        public static RouteValueDictionary RedirectTo(string action, string controller)
        {
            return new RouteValueDictionary(new { action, controller });
        }
    }
}