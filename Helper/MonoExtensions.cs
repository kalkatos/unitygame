using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

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

		public static void SetLayer (this GameObject gameObject, LayerMask layer)
		{
			int layerNumber = (int)Mathf.Log(layer.value, 2);
			gameObject.layer = layerNumber;
			foreach (Transform child in gameObject.transform)
				child.gameObject.SetLayer(layerNumber);
		}

		public static float Tangent (this AnimationCurve curve, float time)
		{
			// Evaluate the curve at the target time
			float valueAtTime = curve.Evaluate(time);

			bool isZero = Mathf.Approximately(time, 0f);
			// Calculate a small time offset
			float timeOffset = 0.001f;
			float valueAtOffsetTime = isZero ? curve.Evaluate(time + timeOffset) : curve.Evaluate(time - timeOffset);

			// Calculate the tangent (slope)
			if (isZero)
				return (valueAtOffsetTime - valueAtTime) / timeOffset;
			return (valueAtTime - valueAtOffsetTime) / timeOffset;
		}

		public static Vector3 ToWorldPosition (this PointerEventData eventData, Camera camera, Plane plane)
		{
			if (camera == null)
				camera = Camera.main;
			if (camera == null)
			{
				Logger.LogError("No camera found to convert PointerEventData to world position.");
				return Vector3.zero;
			}
			Ray ray = camera.ScreenPointToRay(eventData.position);
			plane.Raycast(ray, out float distance);
			return ray.GetPoint(distance);
		}
	}
}