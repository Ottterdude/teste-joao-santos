using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova_CRM_Joao_Santos.Application.ViewModels;
using Prova_CRM_Joao_Santos.Domain.Entities;
using Prova_CRM_Joao_Santos.Domain.Interfaces;

namespace Prova_CRM_Joao_Santos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatoController : ControllerBase
    {
        private readonly ICandidatoRepository _candidatoRepository;

        public CandidatoController(ICandidatoRepository candidatoRepository)
        {
            _candidatoRepository = candidatoRepository ?? throw new ArgumentNullException(nameof(candidatoRepository));
        }

        [HttpPost("add")]
        public IActionResult Add(CandidatoViewModel candidato)
        {
            try
            {
                _candidatoRepository.Add(new Candidato(candidato));
                return Ok(candidato.Nome + " Cadastrado com sucesso!");
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("PRIMARY KEY") == true)
            {
                return StatusCode(500, "Chave primaria duplicada. Tente novamente mais tarde.");
            }
            catch
            {
                return StatusCode(500, "Erro ao adicionar candidato. Tente novamente mais tarde.");
            }
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var candidatos = _candidatoRepository.GetAll();
                return candidatos.Any() ? Ok(candidatos) : NotFound("Nenhum candidato encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar candidatos.");
            }
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var candidato = _candidatoRepository.GetById(id);
                return candidato != null ? Ok(candidato) : NotFound("Candidato não encontrado.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao buscar o candidato.");
            }
        }

        [HttpPut("update")]
        public IActionResult Update(Candidato candidato)
        {
            try
            {
                _candidatoRepository.Update(candidato);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao atualizar o candidato.");
            }
        }

        [HttpDelete("deletebyid/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _candidatoRepository.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao excluir o candidato.");
            }
        }
    }
}
