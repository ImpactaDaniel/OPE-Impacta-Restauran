﻿using MediatR;
using Restaurante.Application.Comum;
using Restaurante.Application.Usuarios.Clientes.Requsicoes.Comum;
using Restaurante.Clientes.Application.Comum;
using Restaurante.Clientes.Domain.Comum.Modelos.Intefaces;
using Restaurante.Domain.Usuarios.Repositorios.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Clientes.Application.Usuarios.Clientes.Requsicoes.Autenticar
{
    public class AutenticarClienteRequisicao : IRequest<RespostaRequisicao<OutToken>>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public AutenticarClienteRequisicao()
        {
        }

        internal class AutenticarClienteRequisicaoHandler : IRequestHandler<AutenticarClienteRequisicao, RespostaRequisicao<OutToken>>
        {
            private readonly IClienteDomainRepositorio _clientRepository;
            private readonly ITokenService _tokenService;
            public AutenticarClienteRequisicaoHandler(IClienteDomainRepositorio clienteRepository, ITokenService tokenService)
            {
                _clientRepository = clienteRepository;
                _tokenService = tokenService;
            }
            public async Task<RespostaRequisicao<OutToken>> Handle(AutenticarClienteRequisicao request, CancellationToken cancellationToken)
            {
                var respostaConsulta = await _clientRepository.Logar(request.Email, request.Senha, cancellationToken);

                if (!respostaConsulta.Sucesso)
                    return new RespostaRequisicao<OutToken>(respostaConsulta);

                return new RespostaRequisicao<OutToken>(_tokenService.GenerateToken(respostaConsulta.Resposta));
            }
        }
    }
}
