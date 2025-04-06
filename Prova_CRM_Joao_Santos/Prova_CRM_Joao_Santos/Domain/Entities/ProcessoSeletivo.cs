using Prova_CRM_Joao_Santos.Application.ViewModels;

namespace Prova_CRM_Joao_Santos.Domain.Entities
{
    public class ProcessoSeletivo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }

        public ProcessoSeletivo() { }

        public ProcessoSeletivo(ProcessoSeletivoViewModel processo)
        {
            Nome = processo.Nome;
            DataInicio = processo.DataInicio;
            DataTermino = processo.DataTermino;
        }
    }
}
