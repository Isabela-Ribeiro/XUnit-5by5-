using System;

namespace ProjApiAtividade.Api.Models
{
    public class Atividade
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public Responsavel Responsavel{ get; set; }
    }
}