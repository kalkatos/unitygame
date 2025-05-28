using DG.Tweening;
using UnityEngine;

namespace Kalkatos.UnityGame
{
	public class UiPanelPresenter : MonoBehaviour
	{
		[Header("Config")]
		[SerializeField] private Vector2 offset;
		[Header("References")]
		[SerializeField] private Canvas canvas;
		[SerializeField] private RectTransform panel;
		[SerializeField] private CanvasGroup canvasGroup;

		private bool isShowing;

		private void Awake ()
		{
			if (canvas == null)
				canvas = GetComponentInParent<Canvas>();
			canvasGroup.alpha = 0;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}

		private void OnDestroy ()
		{
			DOTween.Kill(canvasGroup);
		}

		public void Toggle ()
		{
			if (isShowing)
				Hide();
			else
				Show();
		}

		public void Show ()
		{
			if (isShowing)
				return;
			isShowing = true;
			panel.localScale = Vector3.one * 0.5f;
			panel.DOScale(1f, 0.25f);
			canvasGroup.alpha = 0;
			canvasGroup.DOFade(1f, 0.25f);
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
		}

		public void Hide ()
		{
			if (!isShowing)
				return;
			isShowing = false;
			panel.DOScale(0.5f, 0.25f);
			canvasGroup.DOFade(0f, 0.25f);
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}

		public void Position (Component comp)
		{
			Position(comp.transform);
		}

		public void Position (Transform origin)
		{
			Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, origin.position);
			panel.anchoredPosition = screenPos / canvas.scaleFactor + offset;
		}
	}
}