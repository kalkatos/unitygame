#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kalkatos.UnityGame
{
	[CreateAssetMenu(menuName = "Scriptable Container")]
	public class ScriptableObjectContainer : ScriptableObject
	{
		public ScriptableObject[] Objects;
		[Space(20)]
		public string TypeToGetAll;

#if UNITY_EDITOR
#if ODIN_INSPECTOR
		[Button]
#endif
		private void GetAll ()
		{
			string[] guids = AssetDatabase.FindAssets($"t:{TypeToGetAll}");
			if (guids != null && guids.Length > 0)
				Objects = guids.Select(g => AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(g))).ToArray();
			else
				Logger.Log($"No asset found with type {TypeToGetAll}");
		}
#endif
	}
}
