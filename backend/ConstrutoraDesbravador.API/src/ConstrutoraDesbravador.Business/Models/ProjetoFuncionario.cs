namespace ConstrutoraDesbravador.Business.Models
{
    public class ProjetoFuncionario
    {
        public int ProjetoId { get; set; }
        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }
        public Projeto Projeto { get; set; }
    }
}
