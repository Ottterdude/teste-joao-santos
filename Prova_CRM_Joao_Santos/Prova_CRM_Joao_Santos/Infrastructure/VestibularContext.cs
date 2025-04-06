using Microsoft.EntityFrameworkCore;
using Prova_CRM_Joao_Santos.Domain.Entities;

namespace Prova_CRM_Joao_Santos.Infra
{
    public class VestibularContext : DbContext
    {
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<ProcessoSeletivo> ProcessosSeletivos { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }

        public VestibularContext(DbContextOptions<VestibularContext> options) : base(options)
        {

        }
    }
}
