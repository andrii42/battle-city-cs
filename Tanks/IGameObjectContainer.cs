using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    /// <summary>
    /// Interface for CGameObjectContainer class
    /// </summary>
    /// <typeparam name="T">Type of the objects</typeparam>
    public interface IGameObjectContainer<T>
    {
        // Adds object 
        void Add(T obj);

        // Removes object
        void Remove(T obj);

        // Sorts objects
        void Sort();
    }
}
