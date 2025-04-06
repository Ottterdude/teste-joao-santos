using Microsoft.EntityFrameworkCore;
using Prova_CRM_Joao_Santos.Domain.Entities;
using Prova_CRM_Joao_Santos.Domain.Interfaces;
using Prova_CRM_Joao_Santos.Infra;

namespace Prova_CRM_Joao_Santos.Infrastructure.Repositorys
{
    public class CandidatoRepository : BaseRepository<Candidato>, ICandidatoRepository
    {
        public CandidatoRepository(VestibularContext context) : base(context) { }

    }
}
