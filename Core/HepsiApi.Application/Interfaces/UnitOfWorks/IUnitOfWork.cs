using HepsiApi.Application.Interfaces.Repositories;
using HepsiApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepositories<T> GetReadRepository<T>() where T : class,IEntityBase ,new();
        IWriteRepositories<T> GetWriteRepository<T>() where T : class, IEntityBase, new();

        Task<int> SaveAsync();


        int Save();
    }
}
