using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tanks
{
    // Delegate for
    public delegate void GamePainter(Graphics g);

    // Delegate for onDestroy event:
    public delegate void OnDestroy(object sender);

    // Delegate for onHit event:
    public delegate void OnHit(IGameObject sender);

    // Delegate for shooting:
    public delegate void OnTankShoot(CTank sender);

    // Delegate for check move forvard:
    public delegate bool OnCheckMoveForward(CTank sender);

    // On direction change for players tank:
    public delegate void OnDirectionChange(CMovable m);
    
    // Delegate for movement processing:
    public delegate void OnMove(CMovable sender);

    // Delegate for checking if target is hit:
    public delegate bool OnCheckTargetHit(CProjectile sender);

    // Delegate for Invincibility handling:
    public delegate void OnSetInvinsible(CTank sender, int time);
}