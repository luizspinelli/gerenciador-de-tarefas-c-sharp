using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeTarefas.Dtos
{
  public class ErroRespostaDTO
  {
    public int Status { get; set; }
    public string Erro { get; set; }
  }
}