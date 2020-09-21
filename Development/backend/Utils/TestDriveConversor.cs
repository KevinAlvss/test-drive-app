using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace backend.Utils
{
    public class TestDriveConversor
    {
        /*- Geral -*/

        public List<Models.Response.ConsultarCarroResponse> ToConsultarCarroResponse(List<Models.TbCarro> reqs)
        {
            List<Models.Response.ConsultarCarroResponse> resp = new List<Models.Response.ConsultarCarroResponse>();

            foreach(Models.TbCarro tb in reqs)
            {
                Models.Response.ConsultarCarroResponse x = new Models.Response.ConsultarCarroResponse();

                x.IdCarro = tb.IdCarro;
                x.CarroPlaca = string.Concat(tb.NmCarro, '/', tb.DsPlaca);

                resp.Add(x);
            }

            return resp;
        }

        /*- Login -*/

        public Models.TbLogin ToLoginTable(Models.Request.LoginRequest req)
        {
            Models.TbLogin resp = new Models.TbLogin();
            resp.DsEmail = req.Email;
            resp.DsSenha = req.Senha;

            return resp;
        }

        public Models.Response.LoginResponse ToLoginResponse(Models.TbLogin req)
        {
            Models.Response.LoginResponse resp = new Models.Response.LoginResponse();

            resp.Login = req.IdLogin;
            resp.Permissao = req.DsPermissao;
            resp.Nome = req.TbCliente.Any() ?
                            req.TbCliente.FirstOrDefault()?.NmCliente :
                            req.TbFuncionario.FirstOrDefault()?.NmFuncionario;

            return resp;
        }

        /*- Cliente -*/
        // Consultar

        public List<Models.Response.ClienteConsultarAgendamentoResponse> ToClienteConsultarAgendamentoResponse(List<Models.TbAgendamento> reqs)
        {
            List<Models.Response.ClienteConsultarAgendamentoResponse> resp = new List<Models.Response.ClienteConsultarAgendamentoResponse>();

            foreach(Models.TbAgendamento tb in reqs)
            {
                Models.Response.ClienteConsultarAgendamentoResponse x = new Models.Response.ClienteConsultarAgendamentoResponse();

                x.Carro = string.Concat(tb.IdCarroNavigation.NmCarro, '/', tb.IdCarroNavigation.DsPlaca);
                x.DataHora = tb.DtTestDrive;
                x.Estrela = tb.NrFeedback;
                x.Feedback = tb.DsFeedback;
                x.Funcionario = tb.IdFuncionarioNavigation.NmFuncionario;
                x.Status = tb.DsStatus;

                resp.Add(x); 
            }

            return resp;
        }

        // Cadastrar

        public Models.TbAgendamento ToClienteCadastrarAgendamentoTable(Models.Request.ClienteCadastrarAgendamentoRequest req, int idCliente)
        {
            Models.TbAgendamento resp = new Models.TbAgendamento();

            resp.IdCliente = idCliente;
            resp.IdFuncionario = null;
            resp.IdCarro = req.IdCarro;
            resp.DtTestDrive = req.DataHora;
            resp.DsStatus = "Aguardando";
            resp.NrFeedback = null;
            resp.DsFeedback = null;

            return resp;
        }

        public Models.Response.ClienteCadastrarAgendamentoResponse ToClienteCadastrarAgendamentoResponse(Models.TbAgendamento req, string nomePlacaCarro)
        {
            Models.Response.ClienteCadastrarAgendamentoResponse resp = new Models.Response.ClienteCadastrarAgendamentoResponse();

            resp.DataHora = req.DtTestDrive;
            resp.Carro = nomePlacaCarro;

            return resp;
        }
    }
}