using Microsoft.AspNetCore.Mvc;
using Prova_CRM_Joao_Santos.Application.ViewModels;
using Prova_CRM_Joao_Santos.Domain.Entities;
using Prova_CRM_Joao_Santos.Domain.Interfaces;

namespace Prova_CRM_Joao_Santos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfertaController : ControllerBase
    {
        private readonly IOfertaRepository _ofertaRepository;

        public OfertaController(IOfertaRepository ofertaRepository)
        {
            _ofertaRepository = ofertaRepository ?? throw new ArgumentNullException(nameof(ofertaRepository));
        }

        [HttpPost("add")]
        public IActionResult Add(OfertaViewModel oferta)
        {
            try
            {
                _ofertaRepository.Add(new Oferta(oferta));
                return Ok(oferta.Nome + " Cadastrada com sucesso!!");
            }
            catch
            {
                return StatusCode(500, "Erro ao adicionar oferta.");
            }
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var ofertas = _ofertaRepository.GetAll();
                return ofertas.Any() ? Ok(ofertas) : NotFound("Nenhuma oferta encontrada.");
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar ofertas.");
            }
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var oferta = _ofertaRepository.GetById(id);
                return oferta != null ? Ok(oferta) : NotFound("Oferta não encontrada.");
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar a oferta.");
            }
        }

        [HttpPut("update")]
        public IActionResult Update(Oferta oferta)
        {
            try
            {
                _ofertaRepository.Update(oferta);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Erro ao atualizar a oferta.");
            }
        }

        [HttpDelete("deletebyid/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _ofertaRepository.Delete(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Erro ao excluir a oferta.");
            }
        }
    }

}
