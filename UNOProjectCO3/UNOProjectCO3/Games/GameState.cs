using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOProjectCO3.Games
{
    public enum GameState : byte
    {
        Starting = 1, ShuttingDown = 2, WaitingForPlayers, StartGame, Playing, GamePaused, GameFinished,
    }
}
