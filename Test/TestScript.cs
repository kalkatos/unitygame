using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kalkatos.UnityGame.Test
{
	public class TestScript : MonoBehaviour
	{
#if UNITY_EDITOR

		[MenuItem("Test/Test")]
		public static void Test ()
		{
			Dictionary<string, string> uris = new Dictionary<string, string>
			{
				{ "SetPlayerData", "https://kalkatos-games.azurewebsites.net/api/SetPlayerData?code=oFl2jbDCTLC7yniharMjY1qjJpBq7tqArNehC-SHMa0sAzFuFMdPgg==" },
				{ "LogIn", "https://kalkatos-games.azurewebsites.net/api/LogIn?code=oFl2jbDCTLC7yniharMjY1qjJpBq7tqArNehC-SHMa0sAzFuFMdPgg==" },
				{ "FindMatch", "https://kalkatos-games.azurewebsites.net/api/FindMatch?code=oFl2jbDCTLC7yniharMjY1qjJpBq7tqArNehC-SHMa0sAzFuFMdPgg==" },
				{ "GetMatch", "https://kalkatos-games.azurewebsites.net/api/GetMatch?code=oFl2jbDCTLC7yniharMjY1qjJpBq7tqArNehC-SHMa0sAzFuFMdPgg==" },
				{ "LeaveMatch", "https://kalkatos-games.azurewebsites.net/api/LeaveMatch?code=oFl2jbDCTLC7yniharMjY1qjJpBq7tqArNehC-SHMa0sAzFuFMdPgg==" },
				{ "SendAction", "https://kalkatos-games.azurewebsites.net/api/SendAction?code=oFl2jbDCTLC7yniharMjY1qjJpBq7tqArNehC-SHMa0sAzFuFMdPgg==" },
				{ "GetMatchState", "https://kalkatos-games.azurewebsites.net/api/GetMatchState?code=oFl2jbDCTLC7yniharMjY1qjJpBq7tqArNehC-SHMa0sAzFuFMdPgg==" },
				{ "GetGameConfig", "https://kalkatos-games.azurewebsites.net/api/GetGameConfig?code=oFl2jbDCTLC7yniharMjY1qjJpBq7tqArNehC-SHMa0sAzFuFMdPgg==" }
			};
			File.WriteAllBytes($"{Application.dataPath}/uris", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(uris)));
		}

#endif
	}
}