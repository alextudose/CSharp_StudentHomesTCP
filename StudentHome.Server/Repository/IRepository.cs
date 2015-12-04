using System;
using System.Collections.Generic;

namespace StudentHome.Server.Repository
{
    public interface IRepository<T>
    {
        int Count();
        void Save(T objectToSave);
        Object FindOne(T objectToFind);
        IList<T> FindAll();
    }
}
