using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova_CRM_Joao_Santos.Application.ViewModels;
using Prova_CRM_Joao_Santos.Domain.Entities;
using Prova_CRM_Joao_Santos.Domain.Interfaces;

namespace Prova_CRM_Joao_Santos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscricaoController : ControllerBase
    {
        private readonly IInscricaoRepository _inscricaoRepository;
        private readonly IOfertaRepository _ofertaRepository;
        private readonly IProcessoSeletivoRepository _processoSeletivoRepository;

        public InscricaoController(IInscricaoRepository inscricaoRepository, IOfertaRepository ofertaRepository, IProcessoSeletivoRepository processoSeletivoRepository)
        {
            _inscricaoRepository = inscricaoRepository ?? throw new ArgumentNullException(nameof(inscricaoRepository));
            _ofertaRepository = ofertaRepository ?? throw new ArgumentNullException(nameof(ofertaRepository));
            _processoSeletivoRepository = processoSeletivoRepository ?? throw new ArgumentException(nameof(processoSeletivoRepository));
        }

        [HttpPost("add")]
        public IActionResult Add(InscricaoViewModel inscricaovm)
        {
            try
            {
                var inscricao = new Inscricao(inscricaovm);
                var processo = _processoSeletivoRepository.GetById(inscricao.ProcessoSeletivoId);
                var oferta = _ofertaRepository.GetById(inscricao.OfertaId);
                oferta.Vagas--;

                if (inscricao.DataInscricao < processo.DataTermino)
                {
                    if (inscricao.DataInscricao < processo.DataInicio) inscricao.StatusInscricao = "Pronto para inicio";
                    else inscricao.StatusInscricao = "Em andamento";
                }
                else
                    inscricao.StatusInscricao = "Fora do prazo";

                if (oferta.Vagas >= 0)
                {
                    _inscricaoRepository.Add(inscricao);
                    _ofertaRepository.Update(oferta);
                    return Ok("Inscricao de numero " + inscricao.NumeroInscricao + " criada com sucesso!");
                }
                return StatusCode(500, "Oferta sem vagas disponiveis. Tente novamente mais tarde.");
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("PRIMARY KEY") == true)
            {
                return StatusCode(500, "Chave primaria duplicada. Tente novamente mais tarde.");
            }
            catch
            {
                return StatusCode(500, "Erro ao registrar inscrição.");
            }
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var inscricoes = _inscricaoRepository.GetAll();
                return inscricoes.Any() ? Ok(inscricoes) : NotFound("Nenhuma inscrição encontrada.");
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar inscrições.");
            }
        }

        [HttpGet("getallcomplete")]
        public IActionResult GetAllComplete()
        {
            try
            {
                var inscricoes = _inscricaoRepository.GetAllComplete();
                return inscricoes.Any() ? Ok(inscricoes) : NotFound("Nenhuma inscrição encontrada.");
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar inscrições.");
            }
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var inscricao = _inscricaoRepository.GetById(id);
                return inscricao != null ? Ok(inscricao) : NotFound("Inscrição não encontrada.");
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar a inscrição.");
            }
        }

        [HttpGet("getbycpf/{cpf}")]
        public IActionResult GetByCpf(string cpf)
        {
            try
            {
                var inscricao = _inscricaoRepository.GetAllByCpf(cpf);
                return inscricao.Any() ? Ok(inscricao) : NotFound("Inscrição não encontrada.");
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar a inscrição.");
            }
        }

        [HttpGet("getbyoferta/{oferta}")]
        public IActionResult GetByOferta(string oferta)
        {
            try
            {
                var inscricao = _inscricaoRepository.GetAllByOferta(oferta);
                return inscricao.Any() ? Ok(inscricao) : NotFound("Inscrição não encontrada.");
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar a inscrição.");
            }
        }

        [HttpPut("update")]
        public IActionResult Update(Inscricao inscricao)
        {
            try
            {
                _inscricaoRepository.Update(inscricao);
                return Ok("Inscricao de numero " + inscricao.NumeroInscricao + " atualizada com sucesso!");
            }
            catch
            {
                return StatusCode(500, "Erro ao atualizar a inscrição.");
            }
        }

        [HttpDelete("deletebyid/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _inscricaoRepository.Delete(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Erro ao excluir a inscrição.");
            }
        }
    }
}
