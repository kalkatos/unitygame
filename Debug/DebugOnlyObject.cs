using UnityEngine;

namespace Kalkatos.UnityGame.Debug
{
	public class DebugOnlyObject : MonoBehaviour
	{
		private void Awake ()
		{
			if (!UnityEngine.Debug.isDebugBuild)
				Destroy(gameObject);
		}
	}
}