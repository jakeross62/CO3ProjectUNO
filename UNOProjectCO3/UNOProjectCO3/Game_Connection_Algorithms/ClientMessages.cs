using System;

namespace UNOProjectCO3.Game_Connection_Algorithms
{
    public enum ClientMessages : byte
    {
        JoinGranted = 1, JoinDenied, Kicked, Disconnected, ServerShutdown, Timeout, KeepAlive, OtherPlayerLeft, IsReady, ChatMessage, GameStarted, GameFinished, GameData, PlayerInfo, GeneralPlayersInfo,
    }
}
