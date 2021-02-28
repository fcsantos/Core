using MRP.Business.Intefaces;
using MRP.Business.Models;
using MRP.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MRP.Business.Services
{
    public class PacienteService : BaseService, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        public PacienteService(IPacienteRepository pacienteRepository,
                               IEnderecoRepository enderecoRepository,
                               INotificador notificador) : base(notificador)
        {
            _pacienteRepository = pacienteRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Paciente paciente)
        {
            if (!ExecutarValidacao(new PacienteValidation(), paciente)
                || !ExecutarValidacao(new EnderecoValidation(), paciente.Endereco)) return;

            if (_pacienteRepository.Buscar(p => p.NumeroIdentificacao == paciente.NumeroIdentificacao).Result.Any())
            {
                Notificar("Já existe um paciente com este número de identificação infomado.");
                return;
            }

            await _pacienteRepository.Adicionar(paciente);
        }

        public async Task Atualizar(Paciente paciente)
        {
            if (!ExecutarValidacao(new PacienteValidation(), paciente)) return;

            if (_pacienteRepository.Buscar(p => p.NumeroIdentificacao == paciente.NumeroIdentificacao && p.Id != paciente.Id).Result.Any())
            {
                Notificar("Já existe um paciente com este número de identificação infomado.");
                return;
            }

            await _pacienteRepository.Atualizar(paciente);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task Remover(Guid id)
        {
            var endereco = await _enderecoRepository.ObterEnderecoPorFornecedor(id);

            if (endereco != null)
            {
                await _enderecoRepository.Remover(endereco.Id);
            }

            await _pacienteRepository.Remover(id);
        }

        public void Dispose()
        {
            _pacienteRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}
