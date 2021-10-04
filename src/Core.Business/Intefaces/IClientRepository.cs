﻿using Core.Business.Models;
using System.Threading.Tasks;

namespace Core.Business.Intefaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<Client> GetClientByUserId(string userId);
        Task<Client> GetClientByApiKey(string apiKey);
    }
}