using DominandoEF.Api.Data;
using DominandoEF.Api.Data.Repository;
using DominandoEF.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DominandoEF.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartamentoController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        //private readonly IDepartamentoRepository _repository;      

        public DepartamentoController(          
            //  IDepartamentoRepository repository, 
            IUnitOfWork uow)
        {            
            // _repository = repository;
            _uow = uow;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var departamentos = await _uow.DepartamentoRepository
                .GetDataAsync(include: p => p.Include(p => p.Colaboradores));

            return Ok(departamentos);
        }

        [HttpGet("descricao")]
        public async Task<IActionResult> GetByDescricao([FromQuery] string descricao)
        {
            var departamentos = await _uow.DepartamentoRepository
                .GetDataAsync(
                    p => p.Descricao.Contains(descricao),
                    p => p.Include(p => p.Colaboradores),
                    take: 3);

            return Ok(departamentos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
            int id /*, [FromServices] IDepartamentoRepository _repository */)
        {
            //var departamento = await _repository.GetByIdAsync(id);
            var departamento = await _uow.DepartamentoRepository.GetByIdAsync(id);

            return Ok(departamento);
        }

        [HttpPost]
        public IActionResult CreateDepartamento(Departamento departamento)
        {
            //_repository.Add(departamento);
            _uow.DepartamentoRepository.Add(departamento);
            var saved = _uow.Commit();

            if (saved)
            {
                return Ok(departamento);
            }

            return BadRequest("Falha ao salvar departamento");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveDepartamento(int id)
        {
            //_repository.Add(departamento);
            var departamento = await _uow.DepartamentoRepository.GetByIdAsync(id);
            _uow.DepartamentoRepository.Remove(departamento);

            _uow.Commit();

            return Ok(departamento);
        }
    }
}
