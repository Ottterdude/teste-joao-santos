using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prova_CRM_Joao_Santos.Application.ViewModels;
using Prova_CRM_Joao_Santos.Domain.Entities;
using Prova_CRM_Joao_Santos.Domain.Interfaces;

namespace Prova_CRM_Joao_Santos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessoSeletivoController : ControllerBase
    {
        private readonly IProcessoSeletivoRepository _repository;

        public ProcessoSeletivoController(IProcessoSeletivoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost("add")]
        public IActionResult Add(ProcessoSeletivoViewModel processo)
        {
            try
            {
                _repository.Add(new ProcessoSeletivo(processo));
                return Ok(processo.Nome + " Cadastrado com sucesso!");
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains("PRIMARY KEY") == true)
            {
                return StatusCode(500, "Chave primaria duplicada. Tente novamente mais tarde.");
            }
            catch
            {
                return StatusCode(500, "Erro ao adicionar processo seletivo.");
            }
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var processos = _repository.GetAll();
                return processos.Any() ? Ok(processos) : NotFound("Nenhum processo seletivo encontrado.");
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar processos seletivos.");
            }
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var processo = _repository.GetById(id);
                return processo != null ? Ok(processo) : NotFound("Processo seletivo não encontrado.");
            }
            catch
            {
                return StatusCode(500, "Erro ao buscar o processo seletivo.");
            }
        }

        [HttpPut("update")]
        public IActionResult Update(int id, ProcessoSeletivo processo)
        {
            try
            {
                _repository.Update(processo);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Erro ao atualizar o processo seletivo.");
            }
        }

        [HttpDelete("deletebyid/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _repository.Delete(id);
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Erro ao excluir o processo seletivo.");
            }
        }
    }

}
