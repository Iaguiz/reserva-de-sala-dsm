using reserva_de_sala_dsm.Interfaces;
using reserva_de_sala_dsm.Models;

namespace reserva_de_sala_dsm.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }
        public async Task<Usuario> GetByIdAsync(long id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }
        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            var existingUser = await _usuarioRepository.GetByEmailAsync(usuario.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("E-mail já é cadastrado");
            }

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();
            return usuario;
        }
        public async Task UpdateAsync(Usuario usuario)
        {
            _usuarioRepository.Update(usuario);
            await _usuarioRepository.SaveChangesAsync();
        }
        public async Task DeleteAsync(long id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if(usuario != null)
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }
            _usuarioRepository.Delete(usuario);

            await _usuarioRepository.SaveChangesAsync();

        }
        
            
            
        
    }
}
