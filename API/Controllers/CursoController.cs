using Domain.Dto.Cursos;
using Domain.Exceptions;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CursoController : ControllerBase
    {
        private readonly ICursoService _cursoService;
        private readonly ILogger<CursoController> _logger;

        public CursoController
        (
            ICursoService cursoService,
            ILogger<CursoController> logger
        )
        {
            _cursoService = cursoService;
            _logger = logger;
        }



        [HttpPost]
        public async Task<ActionResult> InsertarCurso([FromBody] CrearCursoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _cursoService.GuardarNuevoCurso(request, cancellationToken);

                if (result.IsSuccess)
                {
                    return CreatedAtRoute("GetCursoById", new { id = result.Value.Id }, result.Value);
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
                _logger.LogError(exe, "Error al insertar curso");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarCurso([FromBody] ActualizarCursoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _cursoService.ActualizarInfoCurso(request, cancellationToken);

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
                _logger.LogError(exe, $"Error al actualizar curso {request.Id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarCurso(long id, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _cursoService.EliminarCurso(id, cancellationToken);

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
                _logger.LogError(exe, $"Error al eliminar curso {id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpGet("{id}", Name = "GetCursoById")]
        public async Task<ActionResult<CursoDto>> GetCursoById(long id, CancellationToken cancellationToken)
        {
            try
            {
                var curso = await _cursoService.GetCursoById(id, cancellationToken);

                if (curso is null)
                {
                    return NotFound();
                }

                return Ok(curso);
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al consultar curso {id}");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CursoDto>>> GetCursos(CancellationToken cancellationToken)
        {
            try
            {
                var cursos = await _cursoService.ConsultarCursos(cancellationToken);

                return Ok(cursos);
            }
            catch (EntityNotFoundException exe)
            {
                return NotFound(exe.Message);
            }
            catch (Exception exe)
            {
                _logger.LogError(exe, $"Error al consultar cursos");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpPost("MatricularEstudiante")]
        public async Task<ActionResult> MatricularEstudiante(MatriculacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _cursoService.MatricularEstudianteCurso(request, cancellationToken);

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
                _logger.LogError(exe, "Error al matricular estudiante");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }

        [HttpDelete("DesmatricularEstudiante")]
        public async Task<ActionResult> DesmatricularEstudiante(MatriculacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _cursoService.DesmatricularEstudianteCurso(request, cancellationToken);

                if (result.IsSuccess)
                {
                    return Ok();
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
                _logger.LogError(exe, "Error al desmatricular estudiante");
            }

            return Problem("Ha ocurrido un error al realizar esta acción");
        }
    }
}
