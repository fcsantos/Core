﻿using System;
using System.Threading.Tasks;
using Core.Business.Models;

namespace Core.Business.Interfaces
{
    public interface IAppControllerService : IDisposable
    {
        Task Create(AppController controller);
        Task Update(AppController controler);
        Task Delete(Guid id);
    }
}
