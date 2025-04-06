using Prova_CRM_Joao_Santos.Application.ViewModels;

namespace Prova_CRM_Joao_Santos.Domain.Entities
{
    public class Oferta
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Vagas { get; set; }

        public Oferta() { }

        public Oferta(OfertaViewModel oferta)
        {
            Nome = oferta.Nome;
            Descricao = oferta.Descricao;
            Vagas = oferta.Vagas;
        }
    }
}
