using System;

namespace backend.Models.Response
{
    public class ClienteConsultarAgendamentoResponse
    {
        public string Status { get; set; }
        public DateTime? DataHora { get; set; }
        public string Funcionario { get; set; }
        public string Carro { get; set; }
        public int? Estrela { get; set; }
        public string Feedback { get; set; }
    }
}