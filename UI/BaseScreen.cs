using DG.Tweening;
using UnityEngine;

namespace Kalkatos.UnityGame
{
	[RequireComponent(typeof(CanvasGroup))]
    public class BaseScreen : MonoBehaviour
	{
		public CanvasGroup CanvasGroup;
		public FloatValueGetter OpenTime;
		public FloatValueGetter CloseTime;

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
			float time = OpenTime.GetValue();
			CanvasGroup.DOFade(1, time);
			transform.DOScale(Vector3.one, time);
		}

		public virtual void Close ()
		{
			float time = CloseTime.GetValue();
			CanvasGroup.DOFade(0, time);
            transform.DOScale(Vector3.one * 0.1f, time);
            CanvasGroup.interactable = false;
            CanvasGroup.blocksRaycasts = false;
        }
	}
}