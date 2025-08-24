using Domain.Dto.Profesores;
using Domain.Exceptions;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorService _profesorService;
        private readonly ILogger<ProfesorController> _logger;

        public ProfesorController
        (
            IProfesorService profesorService,
            ILogger<ProfesorController> logger
        )
        {
            _profesorService = profesorService;
            _logger = logger;
        }



        [HttpPost]
        public async Task<ActionResult> InsertarProfesor([FromBody] CrearProfesorRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profesorService.GuardarNuevoProfesor(request, cancellationToken);

                if (result.IsSuccess)
                {
                    return CreatedAtRoute("GetProfesorById", new { id = result.Value.Id }, result.Value);
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
                _logger.LogError(exe, "Error al insertar profesor");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarProfesor([FromBody] ActualizarProfesorRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profesorService.ActualizarInfoProfesor(request, cancellationToken);

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
                _logger.LogError(exe, $"Error al actualizar profesor {request.Id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarProfesor(long id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _profesorService.EliminarProfesor(id, cancellationToken);

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
                _logger.LogError(exe, $"Error al eliminar profesor {id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpGet("{id}", Name = "GetProfesorById")]
        public async Task<ActionResult<ProfesorDto>> GetProfesorById(long id, CancellationToken cancellationToken)
        {
            try
            {
                var profesor = await _profesorService.GetProfesorById(id, cancellationToken);

                if (profesor is null)
                {
                    return NotFound();
                }

                return Ok(profesor);
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al consultar profesor {id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<ProfesorDto>>> GetProfesors(CancellationToken cancellationToken)
        {
            try
            {
                var profesors = await _profesorService.ConsultarProfesors(cancellationToken);

                return Ok(profesors);
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al consultar profesors");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpGet("GetByNumDoc/{numDoc}")]
        public async Task<ActionResult<ProfesorDto>> GetProfesorByNumDoc(string numDoc, CancellationToken cancellationToken)
        {
            try
            {
                var profesor = await _profesorService.GetProfesorByNumDoc(numDoc, cancellationToken);

                if (profesor is null)
                {
                    return NotFound();
                }

                return Ok(profesor);
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al consultar profesor {numDoc}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }
    }
}
