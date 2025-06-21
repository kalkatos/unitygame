using System;
using System.Collections;
using UnityEngine;

namespace Kalkatos.UnityGame
{
	public static class MonoExtensions
	{
		public static IEnumerator WaitCoroutine (float time, Action callback)
		{
			float endTime = Time.time + time;
			while (Time.time < endTime)
				yield return null;
			callback?.Invoke();
		}

		public static IEnumerator WaitFramesCoroutine (int count, Action callback)
		{
			for (int i = 0; i < count; i++)
				yield return null;
			callback?.Invoke();
		}

		public static void Wait (this MonoBehaviour mono, float time, Action callback)
		{
			mono.StartCoroutine(WaitCoroutine(time, callback));
		}

		public static void WaitFrames (this MonoBehaviour mono, int frameCount, Action callback)
		{
			mono.StartCoroutine(WaitFramesCoroutine(frameCount, callback));
		}

		public static void SetLayer (this GameObject gameObject, int layer)
		{
			gameObject.layer = layer;
			foreach (Transform child in gameObject.transform)
				child.gameObject.SetLayer(layer);
		}
	}
}