using ConstrutoraDesbravador.Business.Enums;
using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.API.DTOs
{
    public class ProjetoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public StatusProjetoEnum StatusProjeto { get; set; }
        public RiscoProjetoEnum RiscoProjeto { get; set; }
        public FuncionarioDTO Responsavel { get; set; }

        public ICollection<FuncionarioDTO> Funcionarios { get; set; }
    }
}
