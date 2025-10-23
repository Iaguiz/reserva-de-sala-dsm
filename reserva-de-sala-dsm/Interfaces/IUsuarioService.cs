using reserva_de_sala_dsm.Models;

namespace reserva_de_sala_dsm.Interfaces
{
    public interface IUsuarioService
    {
        
        Task<IEnumerable<Usuario>> GetAllAsync();
        //Contratos
        //Busca todos os usuários
        Task<Usuario> GetByIdAsync(long id);
        //Busca o usuário por ID
        Task<Usuario> CreateAsync(Usuario usuario);
        //Cria um novo usuário
        Task UpdateAsync(Usuario usuario);
        //Atualiza um usuário
        Task DeleteAsync(long id);
        //Deleta um usuário

    }
}
