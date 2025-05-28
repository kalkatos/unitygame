using UnityEngine;
using UnityEngine.Events;

namespace Kalkatos.UnityGame
{
	public class AnimationEventCaller : MonoBehaviour
	{
		[SerializeField] private UnityEvent animationEvent;

		public void InvokeEvent ()
		{
			animationEvent?.Invoke();
		}
	}
}