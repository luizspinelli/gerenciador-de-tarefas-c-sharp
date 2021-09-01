using GerenciadorDeTarefas.Models;

namespace GerenciadorDeTarefas.Repository.impl
{
  public class UsuarioRepositoryImpl : IUsuarioRepository
  {
      private readonly GerenciadorDeTarefasContext _contexto;
      public UsuarioRepositoryImpl (GerenciadorDeTarefasContext contexto)
      {
          _contexto = contexto;
      }
    public void Salvar(Usuario usuario)
    {
      _contexto.Usuario.Add(usuario);
      _contexto.SaveChanges();
    }
  }
}