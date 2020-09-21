using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace backend.Business
{
    public class TestDriveBusiness
    {
        Database.TestDriveDatabase db = new Database.TestDriveDatabase();

        /*- Geral -*/

        public int ConsultarIdClientePorLogin(int idLogin)
        {
            if(idLogin <= 0)
                throw new Exception("Usuário não existe.");

            int idCliente = db.ConsultarIdClientePorLogin(idLogin);

            if(idCliente <= 0)
                throw new Exception("Usuário não existe.");

            return idCliente;
        }

        public string ConsultarNomePlacaCarroPorId(int? idCarro)
        {
            if(idCarro <= 0)
                throw new Exception("Carro Inválido");

            string nomePlacaCarro = db.ConsultarNomePlacaCarroPorId(idCarro);

            if(nomePlacaCarro == string.Empty)
                throw new Exception("Carro e Placa não identificados.");

            return nomePlacaCarro;
        }

        public List<Models.TbCarro> ConsultarCarros()
        {
            return db.ConsultarCarros();
        }

        /*- Login -*/

        public Models.TbLogin Login(Models.TbLogin req)
        {
            if(req.DsEmail == string.Empty || !req.DsEmail.Contains('@'))
                throw new Exception("Email inválido.");
            if(req.DsSenha == string.Empty)
                throw new Exception("Senha inválida.");
            
            req = db.Login(req);

            if(req.IdLogin <= 0 || req.DsPermissao == string.Empty)
                throw new Exception("Usuário não existe.");
            if(req.TbCliente.Any() ? req.TbCliente.FirstOrDefault().NmCliente == string.Empty
                                   : req.TbFuncionario.FirstOrDefault().NmFuncionario == string.Empty)
                throw new Exception("Usuário não existe.");

            return req;
        }

        /*- Cliente -*/
        // Consultar

        public List<Models.TbAgendamento> ConsultarClienteAgendamento(int idLogin)
        {
            if(idLogin <= 0)
                throw new Exception("Usuário não existe.");

            List<Models.TbAgendamento> resp = db.ConsultarClienteAgendamento(idLogin);

            if(resp.Count == 0 || resp == null)
                throw new Exception("Não há test-drives agendados.");

            return resp;
        }

        //Cadastrar

        public Models.TbAgendamento CadastrarClienteAgendamento(Models.TbAgendamento req)
        {
            if(req.DtTestDrive <= DateTime.Now ||
               req.DtTestDrive == null ||
               req.DtTestDrive >= DateTime.Now.AddMonths(1))
                throw new Exception("Data inválida.");

            if(req.IdCarro <= 0)
                throw new Exception("Carro inválido.");

            if(req.IdCliente <= 0)
                throw new Exception("Usuário inválido.");

            req = db.CadastrarClienteAgendamento(req);
            
            return req;
        }
    }
}