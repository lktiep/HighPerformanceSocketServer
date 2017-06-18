using System;

namespace Server
{
    public delegate void ClientDisconnectedCallBack(Guid clientGuid);

    public interface IInternalClient
    {
        event ClientDisconnectedCallBack OnDisconnect;
    }
}