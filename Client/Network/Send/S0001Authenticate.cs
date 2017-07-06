namespace Client.Network.Send
{
    public class S0001Authenticate : ClientSendPacket
    {
        public S0001Authenticate(IClient connection) : base(connection)
        {
            Buffer.WriteInt16(Connection.UserName.Length);
            Buffer.WriteString(Connection.UserName);
        }
    }
}