using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public class CObjectCreator
    {
        public static IGameObject CreateStaticObject(StaticObjects objectId, float x, float y)
        {
            switch (objectId) 
            {
                case StaticObjects.Brick : return new CBrick(x, y);

                case StaticObjects.Garden: return new CGarden(x, y);

                case StaticObjects.Iron: return new CIron(x, y);

                case StaticObjects.Water: return new CWater(x, y);

                case StaticObjects.Ice: return new CIce(x, y);               

                default: return new CTile();
            }
        }

        public static CPoints CreatePoints(float x, float y, int points)
        {
            return new CPoints(x, y, points);
        }

        public static CBonus CreateBonus(float x, float y, Bonuses bonustype)
        {
            switch (bonustype)
            {
                case Bonuses.Clock: return new CBonusClock(x,y); 

                case Bonuses.Granade: return new CBonusGranade(x, y); 

                case Bonuses.Helmet: return new CBonusHelmet(x, y); 

                case Bonuses.Life: return new CBonusLife(x, y); 

                case Bonuses.Spade: return new CBonusSpade(x, y); 

                case Bonuses.Star: return new CBonusStar(x, y);

                default: return new CBonusLife(x, y);
            }           
        }

        public static CForceField CreateForceField(CTank owner, int lifetime)
        {
            return new CForceField(owner, lifetime);
        }
 

        public static CTank CreateTank(Tanks tankId, bool bonus)
        {
            switch (tankId)
            {
                case Tanks.Enemy1: return new CTankEnemy1(0, 0, bonus);
                
                case Tanks.Enemy2: return new CTankEnemy2(0, 0, bonus);
                
                case Tanks.Enemy3: return new CTankEnemy3(0, 0, bonus);
                
                case Tanks.Enemy4: return new CTankEnemy4(0, 0, bonus);
                
                default: return new CTankEnemy1(0, 0, bonus); 
            }
        }

    }
}
