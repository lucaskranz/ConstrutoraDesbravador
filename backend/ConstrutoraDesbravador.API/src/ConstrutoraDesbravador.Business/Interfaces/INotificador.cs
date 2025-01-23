using ConstrutoraDesbravador.Business.Notificacoes;

namespace ConstrutoraDesbravador.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
