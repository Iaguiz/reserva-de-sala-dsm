using reserva_de_sala_dsm.Interfaces;
using reserva_de_sala_dsm.Models;

namespace reserva_de_sala_dsm.Services.Strategy
{
    public class ValidadorDeReservaCapacidade : IValidadorDeReservaStrategy
    {
        private readonly ISalaRepository _context;

        public ValidadorDeReservaCapacidade(ISalaRepository salaRepo)
        {
            _context = salaRepo;
        }

        public async Task<bool> Validar(Reserva reserva)
        {
            var sala = await _context.GetByIdAsync(reserva.Id);
            return reserva.NumeroDePessoas <= sala.Capacidade;
        }
    }
}
