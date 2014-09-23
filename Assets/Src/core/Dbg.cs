using UnityEngine;
using System.Collections;

namespace core
{
	public static class Dbg
	{
		private static bool UseLogService { get; set; }
		
		public static void Log(string message)
		{
			Debug.Log (message);
		}
		
		public static void LogError(string message)
		{
			throw new UnityException(message);
		}
		
		public static void Assert(bool condition, string message = "")
		{
#if DEBUG
			if (!condition)
				throw new UnityException(message);

#endif
		}
	}
}