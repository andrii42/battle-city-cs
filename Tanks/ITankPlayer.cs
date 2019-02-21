using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public interface ITankPlayer
    {   
        int TankLevel
        {
            get;
            set;
        }

        int Life
        {
            get;
            set;
        }

        int Points
        {
            get;
            set;
        }               

        void Reset(bool full);

        void ResetEnemyHits();

        void AddHit(Tanks tank);

        int[] GetEnemiesHit();

    }
}
