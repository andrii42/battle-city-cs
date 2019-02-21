using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tanks
{
   
    // Bonuses:
    public enum Bonuses : int { Star = 0, Clock = 1, Granade = 2, Helmet = 3, Life = 4, Spade = 5};

    // Posible directions:
    public enum Directions : int { Up = 0, Down = 1, Left = 2, Right = 3 };

    public enum ProjectileTypes : int { Slow, Fast, Max  }
    // 
    public enum KeyMessageID : int { KeyUp, KeyDown };

    public enum KeyIndex : int { Unknown = -1, Up = 0, Down = 1, Left = 2, Right = 3, Shoot = 4 };

    public enum EventId : int { Move, Destroy, Shoot, CheckMoveForward, ChangeDirection, CheckTargetHit };

    // Enum representing the states of the brick:
    // State (0x0F) : 0b1111 : | Left High | Left Low | Right High | Right Low |
    public enum BrickState : int
    {
        Full = 0x0F, RightHalf = 0x03, LeftHalf = 0x0C, UpHalf = 0x0A, DownHalf = 0x05,
        LeftHigh = 0x08, LeftLow = 0x04, RightHigh = 0x02, RightLow = 0x01,
        Destroyed = 0x00
    };

    // List of available blocks:
    public enum Blocks : int
    {
        WallFull = 0x04, WallUp = 0x03, WallDown = 0x01, WallRight = 0x00, WallLeft = 0x02,
        IronFull = 0x09, IronUp = 0x08, IronDown = 0x06, IronLeft = 0x07, IronRight = 0x05,
        Water = 0x0A, Garden = 0x0B, Ice = 0x0C, Empty = 0x0D, Eagle = 0x0F
    };

    // Static objects:
    public enum StaticObjects : int
    {
        Brick = 1, Iron = 2, Garden = 3, Water = 4, Ice = 5, Eagle = 6, Empty = 0
    }

    // Enemies:
    public enum Tanks : int
    {
        Enemy1 = 0, Enemy2 = 1, Enemy3 = 2, Enemy4 = 3, Player1, Player2, Unknown
    }

    // enum for game modes:
    public enum GameModes { Player1, Player2, Construct, Menu };

    // status of the game:
    public enum GameStatuses { GameOver, PlayerWin, Playing, ShowStatistics }

}