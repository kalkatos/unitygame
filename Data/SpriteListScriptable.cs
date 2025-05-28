#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System.Collections.Generic;
using UnityEngine;

namespace Kalkatos.UnityGame
{
	[CreateAssetMenu(fileName = "NewSpriteList", menuName = "Sprite List")]
	public class SpriteListScriptable : ScriptableObject
	{
#if ODIN_INSPECTOR
		[PreviewField]
#endif
		public Sprite[] Sprites;

		private Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();

		public Sprite GetByIndex (int index)
		{
			if (index < 0 || index >= Sprites.Length)
			{
				Logger.LogWarning($"Failed to get sprite with index {index}. Must be from 0 to {Sprites.Length}.");
				if (Sprites.Length > 0)
					return Sprites[Random.Range(0, Sprites.Length)];
				return null;
			}
			return Sprites[index];
		}

		public Sprite GetByName (string name)
		{
			if (spriteDict.Count == 0)
				for (int i = 0; i < Sprites.Length; i++)
					spriteDict.Add(Sprites[i].name, Sprites[i]);
			if (!spriteDict.ContainsKey(name))
			{
				Logger.LogWarning($"Failed to get sprite with name {name}.");
				if (Sprites.Length > 0)
					return Sprites[Random.Range(0, Sprites.Length)];
				return null;
			}
			return spriteDict[name];
		}
	}
}