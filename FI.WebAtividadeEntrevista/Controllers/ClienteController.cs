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
using WebAtividadeEntrevista.Models;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _service;

        public ClienteController()
            : this(new ClienteService(new BoCliente()))
        {
        }

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Forms()
        {
            return View();
        }

        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            try
            {
                var criado = _service.Incluir(model);
                return Json(new
                {
                    Result = "OK",
                    Message = "Cliente incluído com sucesso.",
                    Record = new
                    {
                        criado.Id,
                        criado.CEP,
                        criado.CPF,
                        criado.Cidade,
                        criado.Email,
                        criado.Estado,
                        criado.Logradouro,
                        criado.Nacionalidade,
                        criado.Nome,
                        criado.Sobrenome,
                        criado.Telefone
                    }
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 400;
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
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

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            var cliente = _service.Consultar(id);
            ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel
                {
                    Id = cliente.Id,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    CPF = cliente.CPF,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd;
                string campo = string.Empty;
                string crescente = string.Empty;

                if (!string.IsNullOrWhiteSpace(jtSorting))
                {
                    var array = jtSorting.Split(' ');
                    if (array.Length > 0)
                        campo = array[0];
                    if (array.Length > 1)
                        crescente = array[1];
                }

                var clientes = _service.Pesquisa(
                    jtStartIndex,
                    jtPageSize,
                    campo,
                    crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase),
                    out qtd
                );

                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
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