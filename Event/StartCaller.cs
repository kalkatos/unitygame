using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Kalkatos.UnityGame
{
	public class StartCaller : MonoBehaviour
	{
		[HorizontalGroup("Options"), LabelText("Awake")] public bool UseAwake;
		[HorizontalGroup("Options"), LabelText("OnEnable")] public bool UseOnEnable;
		[HorizontalGroup("Options"), LabelText("Start")] public bool UseStart;
		[ShowIf(nameof(UseAwake))] public UnityEvent onAwakeEvent;
		[ShowIf(nameof(UseOnEnable))] public UnityEvent onEnableEvent;
		[ShowIf(nameof(UseStart))] public UnityEvent onStartEvent;

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