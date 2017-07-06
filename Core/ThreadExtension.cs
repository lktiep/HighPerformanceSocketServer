using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Core
{
	public static class ThreadExtension
	{
		public static void DelayAndExecute(Action action, int delay, bool oneTime = true)
		{
			ThreadPool.RegisterWaitForSingleObject(
				new ManualResetEvent(false),
				(state, timeout) =>
				{
					try
					{
						action();
					}
					catch (Exception e)
					{

					}
				},
				null,
				TimeSpan.FromMilliseconds(delay),
				oneTime);
		}
	}
}
