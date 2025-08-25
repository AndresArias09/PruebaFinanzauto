using Domain.Dto.Calificaciones;
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
    public class CalificacionController : ControllerBase
    {
        private readonly ICalificacionService _calificacionService;
        private readonly ILogger<CalificacionController> _logger;

        public CalificacionController
        (
            ICalificacionService calificacionService,
            ILogger<CalificacionController> logger
        )
        {
            _calificacionService = calificacionService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarCalificacion([FromBody] AgregarCalificacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _calificacionService.AgregarCalificacionEstudiante(request, cancellationToken);

                if (result.IsSuccess)
                {
                    return Created();
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
                _logger.LogError(exe, "Error al insertar calificación");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpPut]
        public async Task<ActionResult> ModificarCalificacion([FromBody] ModificarCalificacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _calificacionService.ModificarCalificacionEstudiante(request, cancellationToken);

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
                _logger.LogError(exe, $"Error al actualizar calificación {request.Id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCalificacion(long id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _calificacionService.EliminarCalificacion(id, cancellationToken);

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
                _logger.LogError(exe, $"Error al eliminar calificación {id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpGet("GetCalificacionesEstudianteCurso/{idEstudiante}/{idCurso}")]
        public async Task<ActionResult<IEnumerable<EstudianteDto>>> GetCalificacionesEstudianteCurso(long idEstudiante, long idCurso, CancellationToken cancellationToken)
        {
            try
            {
                var estudiantes = await _calificacionService.ConsultarCalificacionesByEstudianteYCurso(idEstudiante, idCurso, cancellationToken);

                return Ok(estudiantes);
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al consultar calificaciones");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }
    }
}
