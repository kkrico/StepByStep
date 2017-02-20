using StepByStep.Web.Entity;
using StepByStep.Web.Helpers;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace StepByStep.Web.Infra.Flow
{
    public class WorkFlowFilter : ActionFilterAttribute
    {
        public WorkFlow PassoAtual { get; set; }
        public WorkFlow PassoRequerido { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            if (request.HttpMethod.Equals("post", StringComparison.InvariantCultureIgnoreCase))
                return;

            var context = new StepByStepContext();
            var usuario = context.Users.FirstOrDefault(u => u.UserName == filterContext.HttpContext.User.Identity.Name);
            if (usuario == null || usuario.PassoFeito == WorkFlow.Start)
                if (PassoAtual != WorkFlow.DadosPessoais)
                    filterContext.Result = RedirectTo("DadosPessoais", "Formulario");

            if (usuario.PassoFeito >= PassoRequerido) return;

            switch (usuario.PassoFeito)
            {
                case WorkFlow.DadosPessoais:
                    filterContext.Result = new RedirectToRouteResult(Helper.BuscarRota(WorkFlow.DadosPessoais));
                    break;
                case WorkFlow.DadosDependente:
                    filterContext.Result = new RedirectToRouteResult(Helper.BuscarRota(WorkFlow.DadosDependente));
                    break;
                case WorkFlow.DadosEndereco:
                    filterContext.Result = new RedirectToRouteResult(Helper.BuscarRota(WorkFlow.DadosEndereco));
                    break;
                case WorkFlow.Confirmacao:
                    filterContext.Result = new RedirectToRouteResult(Helper.BuscarRota(WorkFlow.Confirmacao));
                    break;
            }
        }

        private ActionResult RedirectTo(string action, string controller)
        {
            return new RedirectToRouteResult(new RouteValueDictionary(new { action, controller }));
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var context = new StepByStepContext();
            var usuario = context.Users.FirstOrDefault(u => u.UserName == filterContext.HttpContext.User.Identity.Name);
            if (filterContext.HttpContext.Request.RequestType == "POST" && PassoAtual >= usuario.PassoFeito && filterContext.Controller.ViewData.ModelState.IsValid)
            {
                usuario.PassoFeito = PassoAtual;
                context.SaveChanges();
            }
        }
    }

    public enum WorkFlow
    {
        Start,
        [Description("Dados Pessoais")]
        DadosPessoais,
        [Description("Dados do Dependente")]
        DadosDependente,
        [Description("Dados de Endereço")]
        DadosEndereco,
        [Description("Confirmação")]
        Confirmacao
    }

    public class DemoFilter : ActionFilterAttribute
    {
        
    }
}