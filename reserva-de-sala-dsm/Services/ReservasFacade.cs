using reserva_de_sala_dsm.Interfaces;
using reserva_de_sala_dsm.Models;
using reserva_de_sala_dsm.Services.Strategy;

namespace reserva_de_sala_dsm.Services
{
    public class ReservasFacade
    {
        private readonly IReservaService _reservaService;
        private readonly IUsuarioService _usuarioService;
        private readonly ISalaService _salaService;
        private readonly IValidadorDeReservaStrategy _validadorHorario;
        private readonly IValidadorDeReservaStrategy _validadorCapacidade;

        public ReservasFacade(
            IReservaService reservaService,
            IUsuarioService usuarioService,
            ISalaService salaService,
            ValidadorDeReservaHorario validadorHorario,
            ValidadorDeReservaCapacidade validadorCapacidade)
        {
            _reservaService = reservaService;
            _usuarioService = usuarioService;
            _salaService = salaService;
            _validadorHorario = validadorHorario;
            _validadorCapacidade = validadorCapacidade;
        }

        public async Task<List<Reserva>> ListarReservasAsync()
        {
            var reservas = await _reservaService.GetAllAsync();
            return reservas.ToList();
        }

        public async Task<List<Usuario>> ListarUsuariosAsync()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return usuarios.ToList();
        }

        public async Task<List<Sala>> ListarSalasAsync()
        {
            var salas = await _salaService.GetAllSalasAsync();
            return salas.ToList();
        }

        public async Task<Reserva> GetByIdAsync(long id)
        {
            return await _reservaService.GetByIdAsync(id);
        }

        public async Task DeleteAsync(long id)
        {
            await _reservaService.DeleteAsync(id);
        }

        public async Task<string> ReservarAsync(Reserva reserva)
        {
            //Validação de duração mínima
            if(reserva.HoraFim <= reserva.HoraInicio)
            {
                return "A hora do fim deve ser posterior à hora de início";
            }
            //Data no passado
            if(reserva.Data.Date < DateTime.Today)
            {
                return "A data de reserva não pode ser anterior a hoje";
            }
            //Usuário não existe para reservar
            if(await _usuarioService.GetByIdAsync(reserva.UsuarioId) is null)
            {
                return "Usuário não encontrado";
            }
            //Sala não existe para reservar
            if(await _salaService.GetSalasByIdAsync(reserva.SalaId) is null)
            {
                return "Sala não encontrada";
            }
            //Conflito de horários
            _reservaService.SetValidator(_validadorHorario);
            if(!await _reservaService.ValidateAsync(reserva))
            {
                return "Este horário está indisponível. " + "Já existe outra reserva nesse período.";
            }
            //Conflito de capacidade
            _reservaService.SetValidator(_validadorCapacidade);
            if(!await _reservaService.ValidateAsync(reserva))
            {
                return "O número de pessoas excede a capacidade da sala";
            }
            //Salvar
            await _reservaService.SaveAsync(reserva);
            return "Reserva realizada com sucesso!";
        }

    }
}
