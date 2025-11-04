using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using FI.WebAtividadeEntrevista.Service;
using FI.WebAtividadeEntrevista.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        private readonly IBeneficiarioService _service;

        public BeneficiarioController()
            : this(new BeneficiarioService(new BoBeneficiario()))
        {
        }

        public BeneficiarioController(IBeneficiarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public PartialViewResult Modal(long? idCliente)
        {
            ViewBag.IdCliente = idCliente ?? 0;
            return PartialView("~/Views/Cliente/Beneficiario/_PartialBeneficiario.cshtml", ViewBag.IdCliente);
        }

        [HttpGet]
        public JsonResult Listar(long idCliente)
        {
            var lista = _service.Listar(idCliente);
            return Json(new { Result = "OK", Records = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            try
            {
                var criado = _service.Incluir(model);
                return Json(new { Result = "OK", Record = new { criado.Id, criado.CPF, criado.Nome, criado.IdCliente } });
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
                _service.Alterar(model);
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
                _service.Excluir(id);
                return Json(new { Result = "OK" });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
    }

}