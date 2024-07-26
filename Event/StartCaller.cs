using UnityEngine;
using UnityEngine.Events;

namespace Kalkatos.UnityGame
{
	public class StartCaller : MonoBehaviour
	{
		[SerializeField] public UnityEvent onAwakeEvent;
		[SerializeField] public UnityEvent onEnableEvent;
		[SerializeField] public UnityEvent onStartEvent;

        private void Awake ()
        {
			onAwakeEvent?.Invoke();
        }

        private void OnEnable ()
        {
            onEnableEvent?.Invoke();
        }

        private void Start ()
		{
			onStartEvent?.Invoke();
		}
	}
}