using reserva_de_sala_dsm.Models;
using reserva_de_sala_dsm.Services.Strategy;

namespace reserva_de_sala_dsm.Interfaces
{
    public interface IReservaService
    {
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<Reserva> GetByIdAsync(long id);
        Task<Reserva> SaveAsync(Reserva reserva);
        Task DeleteAsync(long id);
        void SetValidator(IValidadorDeReservaStrategy validador);
        Task<bool> ValidateAsync(Reserva reserva);
    }
}
