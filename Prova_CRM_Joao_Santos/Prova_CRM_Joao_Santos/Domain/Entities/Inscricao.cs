using Prova_CRM_Joao_Santos.Application.ViewModels;

namespace Prova_CRM_Joao_Santos.Domain.Entities
{
   public class Inscricao
    {
        public int Id { get; set; }
        public int NumeroInscricao { get; set; }
        public DateTime DataInscricao { get; set; }
        public string StatusInscricao { get; set; }

        public int ProcessoSeletivoId { get; set; }
        public ProcessoSeletivo ProcessoSeletivo { get; set; }

        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }

        public int OfertaId { get; set; }
        public Oferta Oferta { get; set; }

        public Inscricao() { }
        public Inscricao(InscricaoViewModel inscricaoViewModel)
        {
            NumeroInscricao = inscricaoViewModel.NumeroInscricao;
            DataInscricao = DateTime.Now;
            StatusInscricao = "";
            ProcessoSeletivoId = inscricaoViewModel.ProcessoSeletivoId;
            CandidatoId = inscricaoViewModel.CandidatoId;
            OfertaId = inscricaoViewModel.OfertaId;
        }
    }
}
