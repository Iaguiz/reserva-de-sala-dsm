using reserva_de_sala_dsm.Models;

namespace reserva_de_sala_dsm.Services.Strategy
{
    public interface IValidadorDeReservaStrategy
    {
        Task<bool> Validar(Reserva reserva);
    }
}
