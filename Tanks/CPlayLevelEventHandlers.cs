using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{

    public class LevelEventArgs : EventArgs
    {
        GameStatuses gameStatus;

        public GameStatuses GameStatus
        {
            get { return gameStatus; }
            set { gameStatus = value;}
        }
        
        public LevelEventArgs(GameStatuses gs) 
        {
            this.GameStatus = gs;
        }
    }

    public class PointsEventArgs : EventArgs
    {
        int points;

        public int Points
        {
            get { return points; }
            set { points = value; }
        }

        public PointsEventArgs(int points)
        {
            this.points = points;
        }
    }

    public partial class CPlayLevel
    {

        // *********************** //
        // On Move handlers:
        // *********************** //

        // Move the object:
        void OnMove_Object(CMovable movable)
        {
            // Removing object from previous position:
            RemoveObjectFromMap(movable.PrevX, movable.PrevY, movable.Width, movable.Height);

            // Setting back object:
            SetObjectOnMap(movable);
        }

        // Sets Projectile on map:
        void OnMove_Projectile(CMovable p)
        {
            int x = (int)(p.X / cellSize);

            int y = (int)(p.Y / cellSize);

            int prevx = (int)(p.PrevX / cellSize);

            int prevy = (int)(p.PrevY / cellSize);

            // Deleting previous position:
            if ((prevx != x) || (prevy != y)) mapProjectiles[prevx, prevy] = null;

            // Check if somethig is on new position:
            var p2 = mapProjectiles[x, y];

            // If yes then destroy both:
            if (p2 != null)
            {
                // Enemy projectiles cant destory each other:
                if ((p2 != p) && !(((p as CProjectile).Owner is CTankEnemy) && ((p2 as CProjectile).Owner is CTankEnemy)))
                {
                    p.Destroyed = true;

                    p2.Destroyed = true;
                }
            }
            else
            {
                // else take position:
                mapProjectiles[x, y] = (CProjectile)p;
            }
        }


        // *********************** //
        // On Destroy handlers:
        // *********************** //

        // Removes projectile:
        void OnDestroy_Projectile(object projectile, EventArgs e)
        {
            // Casting projectile:
            var p = projectile as CProjectile;

            // Getting owner of the projectile:
            var pOwner = p.Owner as CTank;

            // Increase owners projectile limit:
            if (pOwner.ProjectilesInFlight > 0) pOwner.ProjectilesInFlight--;

            // Explode if nesesary: 
            if (p.Explode) CreateSmallExplotion((IGameObject)projectile);

            // Remove projectile:
            RemoveObject(p);
        }

        // On object destroy event:
        void OnDestroy_Object(object sender, EventArgs e)
        {
            RemoveObject((CGameObject)sender);            
        }

        void OnDestroy_Explotion(object sender, EventArgs e)
        {
            if ((sender as CExplotion).ExplotionLevel > 3)
            {

                var destroyedObject = (sender as CExplotion).ExplodedObject as CTankEnemy;

                if ((destroyedObject != null) && ((destroyedObject as CTankEnemy).Destroyer != null))
                {

                    var points = ((int)(destroyedObject as CTankEnemy).TankID + 1) * 100;

                    OnShow_Points(destroyedObject, new PointsEventArgs(points));
                }

            }
        }

        // On Portal Destroy:
        void OnDestroy_Portal(object sender, EventArgs e)
        {
            // Getting portal:
            var portal = sender as CTankPortal;                       

            // Removing portal:
            RemoveObject((CGameObject)sender);

            // Adding tank:
            AddObject(portal.Tank);

            // Removing enemy tank icon:
            if (!(portal.Tank is CTankPlayer))
            {

                var lastEnemyIcon = StatisticObjects.Last();

                RemoveObject((CGameObject)lastEnemyIcon);

            }
            else
            {
                //OnForceFieldActivate(portal.Tank, 300);
                portal.Tank.SetInvinsible(200);
            }            

        }

        // On Tank Destroy:
        void OnDestroy_Tank(object tank, EventArgs e)
        {            
            // Creating big explotion:
            CreateBigExplotion((IGameObject)tank);

            // Removing a tank:
            RemoveObject((CGameObject)tank); 

            // If it is players tank then reset it,
            var player = tank as CTankPlayer;

            if (player != null)
                if (--player.Life >= 0)
                {
                    player.Reset(true);

                    CreateTankPortal((CTank)player);
                }
                else
                {
                    if (Players.Count == 0) OnPlayersLost();
                }

        }

        // *********************** //
        // On Check handlers:
        // *********************** //

        // On target hit:
        bool OnCheck_IsTargetHit(CProjectile p)
        {
            // fill target coordinates:            
            // there are 4 hit coordinates, each one is in the corner of projectile:
            int[] x = { 0, 0, 0, 0 };

            int[] y = { 0, 0, 0, 0 };

            x[0] = (int)((p.X + 3) / cellSize);

            x[1] = x[0];

            x[2] = (int)((p.X + 4) / cellSize); ;

            x[3] = x[2];

            y[0] = (int)((p.Y + 3) / cellSize);

            y[1] = (int)((p.Y + 4) / cellSize);

            y[2] = y[0];

            y[3] = y[1];

            // Targets:
            IGameObject[] destroyable = new IGameObject[4];

            // hit flag:
            bool object_hit = false;

            // Check targets on map:
            for (int i = 0; i < 4; i++)
            {
                // Getting object:
                var o = this.mapMain[x[i], y[i]];

                // Check if it shoud be hit:
                if ((o != null) && (o != p.Owner) && (o != this.emptyArea) && !(o is CWater))
                {

                    // Enemies cant kill each other:
                    if (!((o is CTankEnemy) && (p.Owner is CTankEnemy)))
                    {

                        if (!destroyable.Contains(o))
                        {
                            // Detecting a hit:
                            object_hit = true;

                            p.Explode = true;

                            if (!((p.Owner is CTankPlayer) && (o is CTankPlayer)))
                            {
                                // Hitting object:
                                o.Hit(p);                                

                                // Blowing up projectile:
                                if ((o.Destroyed) && (o is CTankEnemy)) (p.Owner as CTankPlayer).AddHit((o as CTank).TankID);

                                p.Explode = ((o as CTank != null) && (o as CTank).Invincible) ? false : true;

                                //if ((o as CTank != null) && (o as CTank).Invincible) p.Explode = false;

                                // Adding to targets:
                                destroyable[i] = o;
                            }

                        }
                    }
                }
            };

            return object_hit;
        }

        // Check position ahead:
        bool OnCheck_CanMoveForward(CMovable o)
        {
            //
            int object_width = (int)o.Width / cellSize;

            int object_height = (int)o.Height / cellSize;

            int oX = (int)o.X;// + o.VectorX * o.Speed;

            int oY = (int)o.Y;// + o.VectorY * o.Speed;
            //
            CGameObject tmp;

            // Check X:
            switch (o.VectorX)
            {
                // check right side:
                case 1: if (oX >= maximumXY) return (false);
                    else
                    {
                        //
                        int x = (int)(oX / cellSize);
                        int y = (int)(oY / cellSize);
                        //
                        for (int i = 0; i < object_height; i++)
                        {
                            tmp = mapMain[x + object_height, y + i];
                            if ((tmp != null) && (tmp != o)) return (false);
                        }
                    } break;
                // check left side:
                case -1: if (oX <= minimumXY) return (false);
                    else
                    {
                        //
                        int x = (int)((oX - 1 * o.Speed) / cellSize);
                        int y = (int)(oY / cellSize);
                        //
                        for (int i = 0; i < object_height; i++)
                        {
                            tmp = mapMain[x, y + i];
                            if ((tmp != null) && (tmp != o)) return (false);
                        }
                    } break;
            }
            // Check Y:
            switch (o.VectorY)
            {
                // check bottom side:
                case 1: if (oY >= maximumXY) return (false);
                    else
                    {
                        int x = (int)(oX / cellSize);
                        int y = (int)(oY / cellSize);
                        for (int i = 0; i < object_width; i++)
                        {
                            tmp = mapMain[x + i, y + object_width];
                            if ((tmp != null) && (tmp != o)) return (false);
                        }
                    } break;
                // check top side:
                case -1: if (oY <= minimumXY) return (false);
                    else
                    {
                        int x = (int)(oX / cellSize);
                        int y = (int)((oY - 1 * o.Speed) / cellSize);
                        for (int i = 0; i < object_width; i++)
                        {
                            tmp = mapMain[x + i, y];
                            if ((tmp != null) && (tmp != o)) return (false);
                        }
                    } break;
            }
            // If nothing in front, return true:
            return (true);
        }

        // Checks if bonus is taken by someone:
        object OnCheck_IsBonusTaken(CBonus bonus)
        {
            int mapX = (int)(bonus.X / cellSize);

            int mapY = (int)(bonus.Y / cellSize);

            CTank bonustank = null;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bonustank = (mapMain[mapX + i, mapY + j] as CTankPlayer);

                    if (bonustank != null) return (bonustank);
                }
            }

            return null;
        }

        // *********************** //
        // On Hit handlers:
        // *********************** //

        // On object hit default:
        void OnHit_Object(IGameObject sender)
        {
            if (!sender.Destroyed) ResetTileOnMap((CTile)sender);
        }

        // On Game over:
        void OnHit_HQ(object sender, EventArgs e)
        {

            CreateBigExplotion((IGameObject)sender);

            (sender as IGameObject).Image = Properties.Resources.HQhit;

            OnPlayersLost();
        }               

        // *********************** //
        // On Shoot handlers:
        // *********************** //

        // On shoot:
        void OnShoot_Tank(CTank sender)
        {
            // Check if tank can shoot:
            if (sender.ProjectileLimit > sender.ProjectilesInFlight)
            {
                // Creating projectile:
                var projectile = new CProjectile(sender.X, sender.Y, sender.Direction, sender.ProjectileType);

                // Setting owner:
                projectile.Owner = sender;

                // Adding object:
                AddObject(projectile);

                // Decreasing projectile limit for sender:
                //sender.ProjectileLimit--;

                sender.ProjectilesInFlight++;

            }
        }
       
        // *********************** //
        // On Take Bonus handlers:
        // *********************** //

        void OnBonusCreate(object sender, EventArgs e)
        {
            CreateBonus();
        }

        void OnBonusTake(object sender, EventArgs e)
        {
            (sender as CTankPlayer).Points += 500;
            
            OnShow_Points((e as BonusEventArgs).Bonus, new PointsEventArgs(500));

        }

        void OnBonusTake_Granade(object sender, EventArgs e)
        {
            AllGameObjects.EnemiesList.ForEach(enemie => enemie.Destroyed = true); 
        }

        void OnBonusTake_Spade(object sender, EventArgs e)
        {
            var spadeBonus = ProcessableObjects.Find(p => (p is CBonusSpade) && (p != (e as BonusEventArgs).Bonus));

            if (spadeBonus != null) RemoveObject((CGameObject)spadeBonus);

            //this.hQ.ReceiveFortification();
            for (int i = 0; i < 8; i++)
            {
                var grObject = CObjectCreator.CreateStaticObject(StaticObjects.Iron, hqXs[i], hqYs[i]);
 
                ClearPositionForObject(grObject);

                ((e as BonusEventArgs).Bonus as CBonusSpade).HQobjects.Add(grObject);

                AddObject((CGameObject)grObject);
            }
        }

        void OnBonusTake_Life(object sender, EventArgs e)
        {
            var tank = sender as CTankPlayer;

            if (tank != null)
            {
                if (tank.Life < 99) tank.Life++;
            }            
        }

        void OnBonusTake_Clock(object sender, EventArgs e)
        {
            // Finding and destroying clock bonuses currently active:
            var clockBonus = ProcessableObjects.Find(p => (p is CBonusClock) && ( p != (e as BonusEventArgs).Bonus) );

            if (clockBonus != null) RemoveObject((CGameObject)clockBonus);

            AllGameObjects.EnemiesEnabled = false;
        }

        void OnBonusTake_Helmet(object sender, EventArgs e)
        {
            (sender as CTank).SetInvinsible(800);
        }

        void OnBonusTake_Star(object sender, EventArgs e)
        {
            (sender as CTank).Upgrade();
        }

        void OnBonusExpire_Clock(object sender, EventArgs e)
        {
            AllGameObjects.EnemiesEnabled = true;
        }

        void OnBonusExpire_Spade(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                var grObject = CObjectCreator.CreateStaticObject(StaticObjects.Brick, hqXs[i], hqYs[i]);

                ClearPositionForObject(grObject);                               

                AddObject((CGameObject)grObject);
            }
        }
        // *********************** //
        // Other handlers:
        // *********************** //               

        // Shows points:
        void OnShow_Points(object sender, EventArgs e)
        {
            var go = sender as IGameObject;

            AddObject(CObjectCreator.CreatePoints(go.X, go.Y, (e as PointsEventArgs).Points));
        }

        // On Force field activation:
        void OnActivate_ForceField(CTank sender, int time)
        {            
            // Finding and destroying senders current forsfield if any:
            var forcefield = ProcessableObjects.Find( p => { 
                                                                      if (p is CForceField)
 
                                                                         if ((p as CForceField).Owner == sender) return true; 

                                                                      return false; 

                                                                    } );
            // 
            if (forcefield != null) (forcefield as CForceField ).Destroyed = true;

            // Adding new forcefield:
            AddObject(CObjectCreator.CreateForceField((CTank)sender, time));
        }
        
        // On Show statistics:
        void OnComplete_Level(object sender, EventArgs e)
        {
            ClearLevel();

            var gs = sender is CPlayLevel ? GameStatuses.PlayerWin : GameStatuses.GameOver;

            OnLevelExit(this, new LevelEventArgs(gs));
        }

        void OnComplete_LevelCurtain(object sender, EventArgs e)
        {

            this.ProcessableObjects.Remove(levelCurtain);

            this.KeyEventReceivers.Remove(levelCurtain);

            this.LevelStarted = true;

            this.levelCurtain = null;
        }

        // Runs game over sequence when player(s) lost:
        void OnPlayersLost()
        {
            Players.ForEach(player => player.Enabled = false);

            AddObject(new CGameOverMessage());
        }

    }
}