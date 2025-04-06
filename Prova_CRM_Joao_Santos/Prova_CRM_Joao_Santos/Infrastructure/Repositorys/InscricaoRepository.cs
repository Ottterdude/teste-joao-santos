using Microsoft.EntityFrameworkCore;
using Prova_CRM_Joao_Santos.Domain.Entities;
using Prova_CRM_Joao_Santos.Domain.Interfaces;
using Prova_CRM_Joao_Santos.Infra;

namespace Prova_CRM_Joao_Santos.Infrastructure.Repositorys
{
    public class InscricaoRepository : BaseRepository<Inscricao>, IInscricaoRepository
    {
        protected readonly VestibularContext _vestibularContext;

        public InscricaoRepository(VestibularContext context) : base(context)
        {
            _vestibularContext = context;
        }

        public List<Inscricao> GetAllComplete()
        {
            return IncludeAggregates().ToList();
        }

        public List<Inscricao> GetAllByCpf(string cpf)
        {
            return IncludeAggregates().Where(i => i.Candidato.Cpf.Equals(cpf)).ToList();
        }

        public List<Inscricao> GetAllByOferta(string Oferta)
        {
            return IncludeAggregates().Where(i => i.Oferta.Nome.Equals(Oferta)).ToList();
        }

        private IEnumerable<Inscricao> IncludeAggregates() 
        {
            return _vestibularContext.Inscricoes
                .Include(p => p.ProcessoSeletivo)
                .Include(o => o.Oferta)
                .Include(c => c.Candidato);
        }
    }
}
