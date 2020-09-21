using System;

namespace backend.Models.Request
{
    public class ClienteCadastrarAgendamentoRequest
    {
        public DateTime DataHora { get; set; }
        public int IdCarro { get; set; }
        public int IdLogin { get; set; }
    }
}