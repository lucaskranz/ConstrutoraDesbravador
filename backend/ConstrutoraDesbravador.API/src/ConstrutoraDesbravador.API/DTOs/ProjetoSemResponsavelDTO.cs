using ConstrutoraDesbravador.Business.Enums;

namespace ConstrutoraDesbravador.API.DTOs
{
    public class ProjetoSemResponsavelDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public StatusProjetoEnum StatusProjeto { get; set; }
        public RiscoProjetoEnum RiscoProjeto { get; set; }
        public int ResponsavelId { get; set; }
    }
}
