﻿using Core.Business.Interfaces;
using Core.Business.Models;
using Core.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Data.Repository
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        private readonly IUser _user;
        public ClientRepository(MyDbContext context, IUser user) : base(context) { _user = user; }

        public async Task<Client> GetClientByUserId(string userId)
        {
            return await Db.Clients.AsNoTracking().Where(p => p.UserId.Equals(userId)).FirstOrDefaultAsync();
        }
    }
}