using Prova_CRM_Joao_Santos.Domain.Entities;

namespace Prova_CRM_Joao_Santos.Domain.Interfaces
{
    public interface IInscricaoRepository : IBaseRepository<Inscricao>
    {
        public List<Inscricao> GetAllComplete();
        public List<Inscricao> GetAllByCpf(string cpf);
        public List<Inscricao> GetAllByOferta(string Oferta);
    }
}
