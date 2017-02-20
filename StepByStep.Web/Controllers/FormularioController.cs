using StepByStep.Web.Infra.Flow;
using StepByStep.Web.Models;
using System.Web.Mvc;

namespace StepByStep.Web.Controllers
{
    [Authorize]
    public class FormularioController : Controller
    {
        // GET: Formulario
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [WorkFlowFilter(PassoAtual = WorkFlow.DadosPessoais)]
        public ActionResult DadosPessoais()
        {
            return View();
        }

        [HttpPost]
        [WorkFlowFilter(PassoAtual = WorkFlow.DadosPessoais)]
        public ActionResult DadosPessoais(DadosPessoaisVM model)
        {
            if (!ModelState.IsValid) return View();
            return RedirectToAction("DadosDependente");
        }

        [HttpGet]
        [WorkFlowFilter(PassoAtual = WorkFlow.DadosDependente, PassoRequerido = WorkFlow.DadosPessoais)]
        public ActionResult DadosDependente()
        {
            return View();
        }

        [HttpPost]
        [WorkFlowFilter(PassoAtual = WorkFlow.DadosDependente, PassoRequerido = WorkFlow.DadosPessoais)]
        public ActionResult DadosDependente(DadosDependenteVM model)
        {
            return RedirectToAction("DadosEndereco");
        }

        [HttpGet]
        [WorkFlowFilter(PassoAtual = WorkFlow.DadosEndereco, PassoRequerido = WorkFlow.DadosDependente)]
        public ActionResult DadosEndereco()
        {
            return View();
        }

        [WorkFlowFilter(PassoAtual = WorkFlow.DadosEndereco, PassoRequerido = WorkFlow.DadosDependente)]
        [HttpPost]
        public ActionResult DadosEndereco(DadosEnderecoVM model)
        {
            return RedirectToAction("Confirmacao");
        }

        [HttpGet]
        [WorkFlowFilter(PassoAtual = WorkFlow.Confirmacao, PassoRequerido = WorkFlow.DadosEndereco)]
        public ActionResult Confirmacao()
        {
            return View();
        }
    }

    public class DadosEnderecoVM
    {
    }

    public class DadosPessoaisVM
    {
        [CPF]
        public string Cpf { get; set; }
    }

    public class DadosDependenteVM
    {
    }
}