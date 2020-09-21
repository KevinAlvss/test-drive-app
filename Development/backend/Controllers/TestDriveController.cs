using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestDriveController : ControllerBase
    {
        Utils.TestDriveConversor cnv = new Utils.TestDriveConversor();
        Business.TestDriveBusiness bsn = new Business.TestDriveBusiness();

        /*- Geral -*/

        [HttpGet("consultar/carro")]
        public ActionResult<List<Models.Response.ConsultarCarroResponse>> ConsultarCarros()
        {
            try
            {
                List<Models.TbCarro> req = bsn.ConsultarCarros();

                if(req.Count == 0)
                    return NotFound();

                List<Models.Response.ConsultarCarroResponse> resp = cnv.ToConsultarCarroResponse(req);

                return resp;
            }
            catch(Exception e)
            {
                return BadRequest(
                    new Models.Response.ErroResponse(e.Message, 400)
                );
            }
        }

        /*- Login -*/
        
        [HttpPost]
        public ActionResult<Models.Response.LoginResponse> Login(Models.Request.LoginRequest req)
        {
            try
            {
                Models.TbLogin loginTb = cnv.ToLoginTable(req);

                loginTb = bsn.Login(loginTb);

                Models.Response.LoginResponse resp = cnv.ToLoginResponse(loginTb);

                return resp;
            }
            catch(Exception e)
            {
                return BadRequest(
                    new Models.Response.ErroResponse(e.Message, 400)
                );
            }
        }

        /*- Consultar -- Cliente -*/

        [HttpGet("consultar/cliente")]
        public ActionResult<List<Models.Response.ClienteConsultarAgendamentoResponse>> ConsultarAgendamentosCliente(int idLogin)
        {
            try
            {
                List<Models.TbAgendamento> req = bsn.ConsultarClienteAgendamento(idLogin);

                List<Models.Response.ClienteConsultarAgendamentoResponse> resp = cnv.ToClienteConsultarAgendamentoResponse(req);

                return resp;
            }
            catch(Exception e)
            {
                return BadRequest(
                    new Models.Response.ErroResponse(e.Message, 400)
                );
            }
        }

        /*- Cadastrar -- Cliente -*/

        [HttpPost("cadastrar/cliente")]
        public ActionResult<Models.Response.ClienteCadastrarAgendamentoResponse> CadastrarAgendamentoCliente(Models.Request.ClienteCadastrarAgendamentoRequest req)
        {
            try
            {
                int idCliente = bsn.ConsultarIdClientePorLogin(req.IdLogin);
                
                Models.TbAgendamento tbAgendamento = cnv.ToClienteCadastrarAgendamentoTable(req, idCliente);

                tbAgendamento = bsn.CadastrarClienteAgendamento(tbAgendamento);

                string nomePlacaCarro = bsn.ConsultarNomePlacaCarroPorId(tbAgendamento.IdCarro);

                Models.Response.ClienteCadastrarAgendamentoResponse resp = cnv.ToClienteCadastrarAgendamentoResponse(tbAgendamento, nomePlacaCarro);

                return resp;
            }
            catch(Exception e)
            {
                return BadRequest(
                    new Models.Response.ErroResponse(e.Message, 400)
                );
            }
        }
    }
}