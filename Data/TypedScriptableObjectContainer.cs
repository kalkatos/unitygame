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
	[CreateAssetMenu(menuName = "Typed Scriptable Container")]
	public abstract class TypedScriptableObjectContainer<T> : ScriptableObject where T : ScriptableObject
	{
		public virtual T[] Objects { get; set; }

#if UNITY_EDITOR
#if ODIN_INSPECTOR
		[Button]
#endif
		private void GetAll ()
		{
			string typeName = typeof(T).Name;
			string[] guids = AssetDatabase.FindAssets($"t:{typeName}");
			if (guids != null && guids.Length > 0)
			{
				Logger.Log($"Loading {guids.Length} assets of type {typeName}");
				Objects = (T[])guids.Select(g => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(g))).ToArray();
			}
			else
				Logger.Log($"No asset found with type {typeName}");
		}
#endif
	}
}
