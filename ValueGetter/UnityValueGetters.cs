#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System;
using UnityEngine;

namespace Kalkatos.UnityGame
{
#if ODIN_INSPECTOR
	[InlineProperty]
#endif
	[Serializable]
	public class FloatValueGetter : IValueGetter<float>
	{
#if ODIN_INSPECTOR
		[HorizontalGroup(0.3f), HideLabel]
#endif
		public ValueType Type;
#if ODIN_INSPECTOR
		[HorizontalGroup(0.7f), HideLabel, ShowIf(nameof(Type), ValueType.Simple)]
#endif
		public float SimpleValue;
#if ODIN_INSPECTOR
		[HorizontalGroup(0.7f), HideLabel, ShowIf(nameof(Type), ValueType.Random)]
#endif
		public Vector2 RandomValue;
#if ODIN_INSPECTOR
		[HorizontalGroup(0.7f), HideLabel, ShowIf(nameof(Type), ValueType.Scriptable)]
#endif
		public ScriptableObject ScriptableValue;

		public enum ValueType { Simple, Random, Scriptable }

		public float GetValue ()
		{
			switch (Type)
			{
				case ValueType.Simple:
					return SimpleValue;
				case ValueType.Random:
					return UnityEngine.Random.Range(RandomValue.x, RandomValue.y);
				case ValueType.Scriptable:
					if (ScriptableValue != null)
					{
						if (ScriptableValue is IValueGetter<float>)
							return ((IValueGetter<float>)ScriptableValue).GetValue();
						Logger.LogWarning($"[FloatValueGetter] The scriptable {ScriptableValue.name} is not a IValueGetter.");
					}
					else
						Logger.LogWarning($"[FloatValueGetter] The scriptable value is null.");
					break;
			}
			return default;
		}

		public static implicit operator FloatValueGetter (float value)
		{
			return new FloatValueGetter { Type = ValueType.Simple, SimpleValue = value };
		}
	}

#if ODIN_INSPECTOR
	[InlineProperty]
#endif
	[Serializable]
	public class IntValueGetter : IValueGetter<int>
	{
#if ODIN_INSPECTOR
		[HorizontalGroup(0.3f), HideLabel]
#endif
		public ValueType Type;
#if ODIN_INSPECTOR
		[HorizontalGroup(0.7f), HideLabel, ShowIf(nameof(Type), ValueType.Simple)]
#endif
		public int SimpleValue;
#if ODIN_INSPECTOR
		[HorizontalGroup(0.7f), HideLabel, ShowIf(nameof(Type), ValueType.Random)]
#endif
		public Vector2Int RandomValue;
#if ODIN_INSPECTOR
		[HorizontalGroup(0.7f), HideLabel, ShowIf(nameof(Type), ValueType.Scriptable)]
#endif
		public ScriptableObject ScriptableValue;

		public enum ValueType { Simple, Random, Scriptable }

		public int GetValue ()
		{
			switch (Type)
			{
				case ValueType.Simple:
					return SimpleValue;
				case ValueType.Random:
					return UnityEngine.Random.Range(RandomValue.x, RandomValue.y);
				case ValueType.Scriptable:
					if (ScriptableValue != null)
					{
						if (ScriptableValue is IValueGetter<int>)
							return ((IValueGetter<int>)ScriptableValue).GetValue();
						Logger.LogWarning($"[IntValueGetter] The scriptable {ScriptableValue.name} is not a IValueGetter.");
					}
					else
						Logger.LogWarning($"[IntValueGetter] The scriptable value is null.");
					break;
			}
			return default;
		}

		public static implicit operator IntValueGetter (int value)
		{
			return new IntValueGetter { Type = ValueType.Simple, SimpleValue = value };
		}
	}

#if ODIN_INSPECTOR
	[InlineProperty]
#endif
	[Serializable]
	public class StringValueGetter : IValueGetter<string>
	{
#if ODIN_INSPECTOR
		[HorizontalGroup(0.3f), HideLabel]
#endif
		public ValueType Type;
#if ODIN_INSPECTOR
		[HorizontalGroup(0.7f), HideLabel, ShowIf(nameof(Type), ValueType.Simple)]
#endif
		public string SimpleValue;
#if ODIN_INSPECTOR
		[HorizontalGroup(0.7f), HideLabel, ShowIf(nameof(Type), ValueType.Scriptable)]
#endif
		public ScriptableObject ScriptableValue;

		public enum ValueType { Simple, Scriptable }

		public string GetValue ()
		{
			switch (Type)
			{
				case ValueType.Simple:
					return SimpleValue;
				case ValueType.Scriptable:
					if (ScriptableValue != null)
					{
						if (ScriptableValue is IValueGetter<string>)
							return ((IValueGetter<string>)ScriptableValue).GetValue();
						Logger.LogWarning($"[StringValueGetter] The scriptable {ScriptableValue.name} is not a IValueGetter.");
					}
					else
						Logger.LogWarning($"[StringValueGetter] The scriptable value is null.");
					break;
			}
			return default;
		}

		public static implicit operator StringValueGetter (string value)
		{
			return new StringValueGetter { Type = ValueType.Simple, SimpleValue = value };
		}
	}

#if ODIN_INSPECTOR
	[InlineProperty]
#endif
	[Serializable]
	public class BoolValueGetter : IValueGetter<bool>
	{
#if ODIN_INSPECTOR
		[HorizontalGroup(0.3f), HideLabel]
#endif
		public ValueType Type;
#if ODIN_INSPECTOR
		[HorizontalGroup(0.7f), HideLabel, ShowIf(nameof(Type), ValueType.Simple)]
#endif
		public bool SimpleValue;
#if ODIN_INSPECTOR
		[HorizontalGroup(0.7f), HideLabel, ShowIf(nameof(Type), ValueType.Scriptable)]
#endif
		public ScriptableObject ScriptableValue;

		public enum ValueType { Simple, Scriptable }

		public bool GetValue ()
		{
			switch (Type)
			{
				case ValueType.Simple:
					return SimpleValue;
				case ValueType.Scriptable:
					if (ScriptableValue != null)
					{
						if (ScriptableValue is IValueGetter<bool>)
							return ((IValueGetter<bool>)ScriptableValue).GetValue();
						Logger.LogWarning($"[BoolValueGetter] The scriptable {ScriptableValue.name} is not a IValueGetter.");
					}
					else
						Logger.LogWarning($"[BoolValueGetter] The scriptable value is null.");
					break;
			}
			return default;
		}

		public static implicit operator BoolValueGetter (bool value)
		{
			return new BoolValueGetter { Type = ValueType.Simple, SimpleValue = value };
		}
	}
}
