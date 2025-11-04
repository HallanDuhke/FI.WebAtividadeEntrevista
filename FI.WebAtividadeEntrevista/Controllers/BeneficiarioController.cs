using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        private readonly BoBeneficiario bo = new BoBeneficiario();

        [HttpGet]
        public PartialViewResult Modal(long? idCliente)
        {
            ViewBag.IdCliente = idCliente ?? 0;

            // Ajuste o caminho caso sua partial esteja em outro local
            return PartialView("~/Views/Cliente/Beneficiario/_PartialBeneficiario.cshtml", ViewBag.IdCliente);
        }

        [HttpGet]
        public JsonResult Listar(long idCliente)
        {
            var lista = bo.ListarPorCliente(idCliente)
                          .Select(x => new
                          {
                              x.Id,
                              CPF = FormatarCpf(x.CPF),
                              x.Nome,
                              x.IdCliente
                          });

            return Json(new { Result = "OK", Records = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            try
            {
                var id = bo.Incluir(new Beneficiario
                {
                    CPF = model.CPF,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                });

                return Json(new { Result = "OK", Record = new { Id = id, CPF = FormatarCpf(model.CPF), model.Nome, model.IdCliente } });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Alterar(BeneficiarioModel model)
        {
            try
            {
                bo.Alterar(new Beneficiario
                {
                    Id = model.Id,
                    CPF = model.CPF,
                    Nome = model.Nome,
                    IdCliente = model.IdCliente
                });
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Excluir(long id)
        {
            try
            {
                bo.Excluir(id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        private static string FormatarCpf(string cpf)
        {
            var d = new string((cpf ?? "").Where(char.IsDigit).ToArray());
            if (d.Length != 11) return cpf ?? "";
            return $"{d.Substring(0, 3)}.{d.Substring(3, 3)}.{d.Substring(6, 3)}-{d.Substring(9, 2)}";
        }
    }

}