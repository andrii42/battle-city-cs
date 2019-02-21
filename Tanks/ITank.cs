using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tanks
{
    public interface ITank
    {
        // ID of the tank:
        Tanks TankID
        {
            get;
            set;
        }

        // Type of the projectile:
        ProjectileTypes ProjectileType
        {
            get;
            set;
        }

        // Indicates if it is invinsible:
        bool Invincible
        {
            get;
            set;
        }

        // Armour:
        int Armour
        {
            get;
            set;
        }

        //Projectile limit:
        int ProjectileLimit
        {
            get;
            set;
        }


        bool Enabled
        {
            get;
            set;
        }

        void Upgrade();

        // Shoot method:
        void Shoot();

        // Sets a tank invinsible:
        void SetInvinsible(int time);
    }
}
