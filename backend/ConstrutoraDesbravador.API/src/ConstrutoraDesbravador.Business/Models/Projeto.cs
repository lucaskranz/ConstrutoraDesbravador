using ConstrutoraDesbravador.Business.Enums;

namespace ConstrutoraDesbravador.Business.Models
{
    public class Projeto: Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public StatusProjetoEnum StatusProjeto { get; set; }
        public RiscoProjetoEnum RiscoProjeto { get; set; }
        public int ResponsavelId { get; set; }
        public Funcionario Responsavel { get; set; }

        public ICollection<Funcionario> Funcionarios { get; set; }
    }
}
