using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GerenciadorDeTarefas.Models;
using Microsoft.IdentityModel.Tokens;

namespace GerenciadorDeTarefas.Services
{
  public class TokenService
  {
    public static string CriarToken(Usuario usuario)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var chaveCriptografiaEmBytes = Encoding.ASCII.GetBytes(ChaveJWT.ChaveSecreta);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
              new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
              new Claim(ClaimTypes.Name, usuario.Nome)
          }),
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(chaveCriptografiaEmBytes),
            SecurityAlgorithms.RsaSha256Signature
        )
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}