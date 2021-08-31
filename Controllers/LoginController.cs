using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GerenciadorDeTarefas.Dtos;
using System.Web;
using Microsoft.AspNetCore.Http;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Services;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorDeTarefas.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LoginController : BaseController
  {
    private readonly ILogger<LoginController> _logger;
    private readonly string loginTeste = "admin@admin.com";
    private readonly string senhaTeste = "Admin1234@";

    public LoginController(ILogger<LoginController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto requisicao)
    {
      try
      {
        if (
            requisicao == null
            || string.IsNullOrEmpty(requisicao.Login)
            || string.IsNullOrWhiteSpace(requisicao.Login)
            || string.IsNullOrEmpty(requisicao.Senha)
            || string.IsNullOrWhiteSpace(requisicao.Senha)
            || requisicao.Login != loginTeste
            || requisicao.Senha != senhaTeste
          )
        {
          return BadRequest(new ErroRespostaDTO()
          {
            Status = StatusCodes.Status400BadRequest,
            Erro = "Parametros de entrada invalido"
          });
        }

        var usuarioTeste = new Usuario()
        {
          Id = 1,
          Nome = "Usuario Teste",
          Email = loginTeste,
          Senha = senhaTeste
        };

        var token = TokenService.CriarToken(usuarioTeste);

        return Ok(new LoginRespostaDTO()
        {
          Email = usuarioTeste.Email,
          Nome = usuarioTeste.Nome,
          Token = token
        });
      }
      catch (Exception e)
      {
        _logger.LogError($"Ocorreu erro ao efetuar login: {e.Message}", e, requisicao);
        return StatusCode(StatusCodes.Status500InternalServerError, new ErroRespostaDTO()
        {
          Status = StatusCodes.Status500InternalServerError,
          Erro = "Ocorreu erro ao efetuar login, tente novamente"
        });
      }
    }
  }
}