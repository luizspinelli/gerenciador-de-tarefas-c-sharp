using System;
using GerenciadorDeTarefas.Dtos;
using GerenciadorDeTarefas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GerenciadorDeTarefas.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UsuarioController : BaseController
  {
    private readonly ILogger<UsuarioController> _logger;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult SalvarUsuario([FromBody] Usuario usuario)
    {
      try
      {
        return Ok(usuario);
      }
      catch (Exception e)
      {
        _logger.LogError("Ocorreu erro ao salvar usuario", e);
        return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostaDTO()
        {
          Status = StatusCodes.Status500InternalServerError,
          Erro = "Ocorreu erro ao salvar o usuario, tente novamente"
        });
      }
    }
  }
}