﻿using System.Collections.Generic;
using TP2.REST.Domain.DTO;
using TP2.REST.Domain.Entities;

namespace TP2.REST.Domain.Queries
{
    public interface IAlquilerQuery
    {
        List<ResponseGetAlquilerByEstadoId> GetByEstadoID(int estadoid);

        List<Alquiler> GetReserva(int clienteid, string isbn);

        Libro GetLibro(string isbn);

        List<ResponseGetLibro> GetLibroByCliente(int idcliente);

        bool ExisteCliente(int clienteID);

        bool ExisteLibro(string iSBN);

        bool ExisteStock(string iSBN);
    }
}