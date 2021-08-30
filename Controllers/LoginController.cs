using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GerenciadorDeTarefas.Dtos;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace GerenciadorDeTarefas.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class LoginController : ControllerBase
  {
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    public IActionResult EfetuarLogin([FromBody] LoginRequisicaoDto requisicao)
    {
      try
      {
        if (requisicao == null || requisicao.Login == null || requisicao.Senha == null)
        {
          return BadRequest(new ErroRespostaDTO()
          {
            Status = StatusCodes.Status400BadRequest,
            Erro = "Parametros de entrada invalido"
          });
        }

        return Ok("Usuário autenticado com sucesso");
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