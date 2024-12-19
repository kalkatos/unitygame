using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Kalkatos.UnityGame
{
	[ExecuteAlways]
	public class SpriteColorChanger : MonoBehaviour
	{
		[Header("Config")]
		[SerializeField] private float forceAlpha = 1f;
		[Header("Reference")]
		[SerializeField] private SpriteRenderer[] spriteRenderers;

		private Color[] originalColors;
		private float lastAlpha;

		public float Alpha { set { SetAlpha(value); } }

		private void Awake ()
		{
			lastAlpha = forceAlpha;
			originalColors = new Color[spriteRenderers.Length];
			for (int i = 0; i < spriteRenderers.Length; i++)
				originalColors[i] = spriteRenderers[i].color;
		}

		private void Update ()
		{
			if (forceAlpha != lastAlpha)
			{
				lastAlpha = forceAlpha;
				SetAlpha(forceAlpha);
			}
		}

		public void SetAlpha (float alpha)
		{
			foreach (var renderer in spriteRenderers)
			{
				Color color = renderer.color;
				color.a = alpha;
				renderer.color = color;
			}
		}

		public void SetColor (Color color)
		{
			foreach (var renderer in spriteRenderers)
				renderer.color = color;
		}

		public void SetAlphaOverTime (float alpha, float time)
		{
			foreach (var renderer in spriteRenderers)
				renderer.DOFade(alpha, time);
		}

		public void FadeOut (float time)
		{
			SetAlphaOverTime(0, time);
		}

		public void FadeIn (float time)
		{
			SetAlphaOverTime(1, time);
		}

		public void FlashColor (Color color, float time)
		{
			SetColor(color);
			for (int i = 0; i < spriteRenderers.Length; i++)
				spriteRenderers[i].DOColor(originalColors[i], time);
		}

#if UNITY_EDITOR
		[Button]
		public void GetAllSprites ()
		{
			spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		}
#endif
	}
}