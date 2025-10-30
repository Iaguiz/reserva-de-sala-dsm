using reserva_de_sala_dsm.Interfaces;
using reserva_de_sala_dsm.Models;

namespace reserva_de_sala_dsm.Services.Strategy
{
    public class ValidadorDeReservaHorario : IValidadorDeReservaStrategy
    {
        private readonly IReservaRepository _reserva;

        public ValidadorDeReservaHorario(IReservaRepository reserva)
        {
            _reserva = reserva;
        }

        public async Task<bool> Validar(Reserva reserva)
        {
            //Não permite datas passadas
            if (reserva.Data.Date < DateTime.Today || (reserva.Data.Date == DateTime.Today && reserva.HoraInicio < DateTime.Now.TimeOfDay))
            {
                return false;
            }

            var existentes = await _reserva.FindBySalaIdAndDataAsync(reserva.SalaId, reserva.Data);

            //Ignorar a própria reserva em edição
            existentes = existentes.Where(x => x.Id != reserva.Id).ToList();

            bool overlap = existentes.Any(x => reserva.HoraInicio < x.HoraFim && x.HoraInicio < reserva.HoraFim);
            
            return !overlap;
        }
    }
}
