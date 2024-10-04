using DG.Tweening;
using UnityEngine;

namespace Kalkatos.UnityGame
{
	[RequireComponent(typeof(CanvasGroup))]
    public class BaseScreen : MonoBehaviour
	{
		public CanvasGroup CanvasGroup;

        private void Awake ()
        {
            if (CanvasGroup == null)
				CanvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Open ()
		{
			transform.localScale = Vector3.one * 0.5f;
			CanvasGroup.alpha = 0;
			CanvasGroup.interactable = true;
			CanvasGroup.blocksRaycasts = true;
			CanvasGroup.DOFade(1, 0.5f);
			transform.DOScale(Vector3.one, 0.5f);
		}

		public virtual void Close ()
		{
			CanvasGroup.DOFade(0, 0.5f);
            transform.DOScale(Vector3.one * 0.1f, 0.5f);
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
        }
	}
}