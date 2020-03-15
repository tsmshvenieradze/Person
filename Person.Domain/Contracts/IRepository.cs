using System;

namespace Person.Domain.Contracts
{
    public interface IRepository : IDisposable
    {
        int Save();
        void DetachChanges();
    }
}
