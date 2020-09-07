using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNOProjectCO3.Game_Connection_Algorithms;
using UNOProjectCO3.Games;

namespace UNOProjectCO3.Game_Connection_Algorithms
{
    public interface IGameHostCreation
    {
        GameHost Create();
        gameConnection CreateConnection();
    }
}
