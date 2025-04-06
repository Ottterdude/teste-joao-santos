using Prova_CRM_Joao_Santos.Application.ViewModels;

namespace Prova_CRM_Joao_Santos.Domain.Entities
{
    public class Candidato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }

        public Candidato() { }

        public Candidato(CandidatoViewModel candidatoViewModel) {
            Nome = candidatoViewModel.Nome;
            Email = candidatoViewModel.Email;
            Telefone = candidatoViewModel.Telefone;
            Cpf = candidatoViewModel.Cpf;
        }
    }
}
