namespace ConstrutoraDesbravador.Business.Models
{
    public class Funcionario: Entity
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }

        public ICollection<Projeto> ProjetosResponsavel { get; set; }
        public ICollection<Projeto> ProjetosVinculados { get; set; }
    }
}
