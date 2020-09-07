using System;

namespace UNOProjectCO3
{
    public enum HostMessage : byte
    {
        Connect = 1, Disconnect, KeepAlive, ChatMessage, GameData, GetReadyState, SetReadyState, GetPlayerInfo, GetPlayersInfo,
    }
}
