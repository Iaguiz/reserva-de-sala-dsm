using reserva_de_sala_dsm.Models;

namespace reserva_de_sala_dsm.Interfaces
{
    public interface ISalaRepository
    {
        Task<IEnumerable<Sala>> GetAllAsync();
        Task<Sala> GetByIdAsync(long id);
        Task AddAsync(Sala sala);
        void Update(Sala sala);
        void Delete(Sala sala);
        Task SaveChangesAsync();


    }
}
