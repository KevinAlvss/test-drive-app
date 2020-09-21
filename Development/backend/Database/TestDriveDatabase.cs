using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.EntityFrameworkCore;

namespace backend.Database
{
    public class TestDriveDatabase
    {
        Models.dbtestdriveContext ctx = new Models.dbtestdriveContext();

        /*- Geral -*/

        public int ConsultarIdClientePorLogin(int idLogin)
        {
            return ctx.TbCliente.FirstOrDefault(x => x.IdLogin == idLogin).IdCliente;
        }

        public string ConsultarNomePlacaCarroPorId(int? idCarro)
        {
            Models.TbCarro carro = ctx.TbCarro.FirstOrDefault(x => x.IdCarro == idCarro);

            return string.Concat(carro.NmCarro, '/', carro.DsPlaca);
        }

        public List<Models.TbCarro> ConsultarCarros()
        {
            return ctx.TbCarro.ToList();
        }

        /*- Login -*/

        public Models.TbLogin Login(Models.TbLogin req)
        {
            return ctx.TbLogin
                        .Include(x => x.TbCliente)
                        .Include(x => x.TbFuncionario)
                        .FirstOrDefault(x => x.DsEmail == req.DsEmail && x.DsSenha == req.DsSenha);
        }

        /*- Cliente -*/
        // Consultar

        public List<Models.TbAgendamento> ConsultarClienteAgendamento(int idLogin)
        {
            return ctx.TbAgendamento.Include(x => x.IdCarroNavigation)
                                    .Include(x => x.IdClienteNavigation)
                                    .Include(x => x.IdFuncionarioNavigation)
                                    .Where(x => x.IdClienteNavigation.IdLogin == idLogin &&
                                                x.IdCarroNavigation.IdCarro == x.IdCarroNavigation.IdCarro)
                                    .ToList();
        }

        // Cadastrar

        public Models.TbAgendamento CadastrarClienteAgendamento(Models.TbAgendamento req)
        {
            ctx.TbAgendamento.Add(req);
            ctx.SaveChanges();

            return req;
        }
    }
}