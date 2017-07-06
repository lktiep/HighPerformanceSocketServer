using Core;

namespace Client
{
	public interface IClient
	{
		ILog Log { get; }
		string IpAddress { get; }
		int Port { get; }

		void Start();
		void Stop();

		void Send(string message);
	}
}