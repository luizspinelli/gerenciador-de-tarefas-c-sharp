using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GerenciadorDeTarefas.Dtos;
using GerenciadorDeTarefas.Models;
using GerenciadorDeTarefas.Repository;
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
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
      _logger = logger;
      _usuarioRepository = usuarioRepository;
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult SalvarUsuario([FromBody] Usuario usuario)
    {
      try
      {
        var erros = new List<string>();

        if (String.IsNullOrEmpty(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Nome) || usuario.Nome.Length < 2)
        {
          erros.Add("Nome inválido");
        }

        if (String.IsNullOrEmpty(usuario.Senha) || string.IsNullOrWhiteSpace(usuario.Senha) || usuario.Senha.Length < 4 && Regex.IsMatch(usuario.Senha, "^[a-zA-Z0-9]+", RegexOptions.IgnoreCase))
        {
          erros.Add("Senha inválida");
        }

        Regex regex = new Regex(@"^([\w\.\-\+\d]+)@([\w\-]+)((\.(\w){2,3})+)$");

        if (String.IsNullOrEmpty(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Email) || !regex.Match(usuario.Email).Success)
        {
          erros.Add("Email inválida");
        }
        if (erros.Count > 0)
        {
          return BadRequest(new ErroRespostaDTO()
          {
            Status = StatusCodes.Status400BadRequest,
            Erros = erros
          });
        }

        _usuarioRepository.Salvar(usuario);

        return Ok(new { msg = "Usuario criado com sucesso" });
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