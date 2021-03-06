﻿using System.Collections.Generic;
using TP2.REST.Domain.DTO;
using TP2.REST.Domain.Entities;
using TP2.REST.Domain.Interfaces.Commands;
using TP2.REST.Domain.Interfaces.Queries;
using TP2.REST.Domain.Interfaces.Services;


namespace TP2.REST.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IGenericRepository _repository;
        private readonly IClienteQuery _query;

        public ClienteService(IGenericRepository repository, IClienteQuery query)
        {
            _repository = repository;
            _query = query;
        }

        public GenericCreatedResponseDTO CreateCliente(ClienteDTO clienteDto)
        {
            var entity = new Cliente
            {
                Nombre = clienteDto.Nombre,
                Apellido = clienteDto.Apellido,
                DNI = clienteDto.DNI,
                Email = clienteDto.Email
            };
            _repository.Add(entity);
            _repository.SaveChanges();
            return new GenericCreatedResponseDTO { Entity = "Cliente", Id = entity.ClienteId.ToString() };
        }

        public List<ResponseGetCliente> GetClientes(string nombre, string apellido, string dni)
        {
            return _query.GetAllClientes(nombre, apellido, dni);
        }

        public string ValidarCliente(ClienteDTO clienteDto)
        {
            if (!Validacion.ValidarSoloLetras(clienteDto.Nombre))
                return "El nombre ingresado no es válido.";
            if (!Validacion.ValidarSoloLetras(clienteDto.Apellido))
                return "El apellido ingresado no es válido.";
            if (!Validacion.ValidarDni(clienteDto.DNI))
                return "El formato del DNI ingresado es incorrecto.";
            if (_query.ExisteDNI(clienteDto.DNI))
                return "Ya existe un cliente registrado con el DNI ingresado.";
            if (!Validacion.ComprobarFormatoEmail(clienteDto.Email))
                return "El formato del mail ingresado no es válido.";
            return "";
        }
    }
}