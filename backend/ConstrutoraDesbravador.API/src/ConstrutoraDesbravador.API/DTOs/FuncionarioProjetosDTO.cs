using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.API.DTOs
{
    public class FuncionarioProjetosDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }

        public IEnumerable<ProjetoSemResponsavelDTO> ProjetosResponsavel { get; set; }
        public IEnumerable<ProjetoSemResponsavelDTO> ProjetosVinculados { get; set; }
    }
}
