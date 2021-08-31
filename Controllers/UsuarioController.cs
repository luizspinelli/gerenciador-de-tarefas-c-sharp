using System;
using GerenciadorDeTarefas.Dtos;
using GerenciadorDeTarefas.Models;
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

    [HttpGet]
    public IActionResult ObterUsuario()
    {
      try
      {
        var usuarioTeste = new Usuario()
        {
          Id = 1,
          Nome = "Usuario Teste",
          Email = "'emaail'",
          Senha = "'senhaTeste'"
        };

        return Ok(usuarioTeste);
      }
      catch (Exception e)
      {
        _logger.LogError("Ocorreu erro ao obter usuario", e);
        return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostaDTO()
        {
          Status = StatusCodes.Status500InternalServerError,
          Erro = "Ocorreu erro ao obter o usuario, tente novamente"
        });
      }
    }
  }
}