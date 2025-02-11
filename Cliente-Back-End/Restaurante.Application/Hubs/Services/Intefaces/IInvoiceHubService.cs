﻿using Restaurante.Domain.Pedidos.Models.Enum;
using System.Threading.Tasks;

namespace Restaurante.Clientes.Application.Hubs.Services.Intefaces
{
    public interface IInvoiceHubService
    {
        Task InvoiceUpdatedNotification(int customerId, string invoiceId, InvoiceStatus invoiceStatus);
        Task AddNewConnection(string connectionId, int customerId);
        Task RemoveConnection(string connectionId);
    }
}
