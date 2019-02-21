using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    /// <summary>
    /// Holds objects
    /// </summary>
    /// <typeparam name="T">Type of the object to hold</typeparam>
    public class CGameObjectContainer<T> : IGameObjectContainer<T> where T : IGameObject
    {
        // *********************************
        // Private and protected members:
        // *********************************                
        public object SyncRoot = new object();

        private List<T> gameObjectList = new List<T>();

        private List<T> enemiesList = new List<T>();

        private int enemiesCount;
        
        private int tankPortalsCount;

        private bool enemiesEnabled = true;

        // *********************************
        // Properties:
        // *********************************

        public List<T> GameObjectList
        {

            get { return gameObjectList; }

        }

        public List<T> EnemiesList
        {

            get { return enemiesList; }

        }        

        public int EnemiesCount
        {
            get { return enemiesCount; }
            
            set { enemiesCount = value; }
        }       

        public bool EnemiesEnabled
        {
            get { return enemiesEnabled; }
            set { 
                  enemiesEnabled = value; 
                  enemiesList.ForEach(enemie => (enemie as CTank).Enabled = enemiesEnabled);
                }
        }       

        public int TankPortalsCount
        {
            get { return tankPortalsCount; }
            set { tankPortalsCount = value; }
        }

        // *********************************
        // Methods:
        // *********************************

        // Adding object:
        public void Add(T obj)
        {

            // Finding positions index to insert obj:
            int lastObjectGroupIndex = gameObjectList.FindIndex((o) => obj.DrawPriority < o.DrawPriority);

            // If index not found the just add, else insert to wright position:
            if (lastObjectGroupIndex < 0) gameObjectList.Add(obj);

            else gameObjectList.Insert(lastObjectGroupIndex, obj);

            if (obj is CTankEnemy)
            {     
                EnemiesCount++;

                (obj as CTankEnemy).Enabled = EnemiesEnabled;

                enemiesList.Add(obj);
            }

            if (obj is CTankPortal) TankPortalsCount++;

        }

        // Removing object:
        public void Remove(T obj)
        {

            gameObjectList.Remove(obj);

            if (obj is CTankEnemy) EnemiesCount--;

            if (obj is CTankPortal) TankPortalsCount--;

        }

        // Clearing all objects:
        public void Clear()
        {
            gameObjectList.Clear();
        }

        // Sort the list:
        public void Sort()
        {

            gameObjectList.Sort();

        }
    }
}
