using Domain.Dto;
using Domain.Dto.Estudiantes;
using Domain.Exceptions;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;
        private readonly ILogger<EstudianteController> _logger;

        public EstudianteController
        (
            IEstudianteService estudianteService,
            ILogger<EstudianteController> logger
        )
        {
            _estudianteService = estudianteService;
            _logger = logger;
        }



        [HttpPost]
        public async Task<ActionResult> InsertarEstudiante([FromBody] CrearEstudianteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _estudianteService.GuardarNuevoEstudiante(request, cancellationToken);

                if (result.IsSuccess)
                {
                    return CreatedAtRoute("GetEstudianteById", new { id = result.Value.Id }, result.Value);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, "Error al insertar estudiante");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarEstudiante([FromBody] ActualizarEstudianteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _estudianteService.ActualizarInfoEstudiante(request, cancellationToken);

                if (result.IsSuccess)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al actualizar estudiante {request.Id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarEstudiante(long id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _estudianteService.EliminarEstudiante(id, cancellationToken);

                if (result.IsSuccess)
                {
                    return NoContent();
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al eliminar estudiante {id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpGet("{id}", Name = "GetEstudianteById")]
        public async Task<ActionResult<EstudianteDto>> GetEstudianteById(long id, CancellationToken cancellationToken)
        {
            try
            {
                var estudiante = await _estudianteService.GetEstudianteById(id, cancellationToken);

                if (estudiante is null)
                {
                    return NotFound();
                }

                return Ok(estudiante);
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al consultar estudiante {id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<EstudianteDto>>> GetEstudiantes(CancellationToken cancellationToken)
        {
            try
            {
                var estudiantes = await _estudianteService.ConsultarEstudiantes(cancellationToken);

                return Ok(estudiantes);
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al consultar estudiantes");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpGet("GetByNumDoc/{numDoc}")]
        public async Task<ActionResult<EstudianteDto>> GetEstudianteByNumDoc(string numDoc, CancellationToken cancellationToken)
        {
            try
            {
                var estudiante = await _estudianteService.GetEstudianteByNumDoc(numDoc, cancellationToken);

                if (estudiante is null)
                {
                    return NotFound();
                }

                return Ok(estudiante);
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al consultar estudiante {numDoc}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpGet("GetPaginated")]
        public async Task<ActionResult<PaginatedCollection<EstudianteDto>>> GetEstudiantesPaginado(int page, int pageSize, CancellationToken cancellationToken)
        {
            try
            {
                var estudiantes = await _estudianteService.GetEstudiantesPaginado(page, pageSize, cancellationToken);

                return Ok(estudiantes);
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al consultar estudiantes");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

    }
}
