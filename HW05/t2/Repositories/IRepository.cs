﻿namespace t2.Repositories
{
    internal interface IRepository<T> where T : class
    {
        public void Serialize(T catalog, string fileName);
        public T Deserialize(string fileName);
    }
}
