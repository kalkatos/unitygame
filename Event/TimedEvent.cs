﻿using UnityEngine;
using UnityEngine.Events;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System;
using System.Collections;

namespace Kalkatos.UnityGame
{
	public class TimedEvent : MonoBehaviour
	{
		[SerializeField] private TimedEventBit[] events;
		[SerializeField] private bool isSequence;
#if ODIN_INSPECTOR
		[ShowIf(nameof(isSequence))]
#endif
		[SerializeField] private bool loopSequence;
#if ODIN_INSPECTOR
		[ShowIf(nameof(isSequence)), ShowIf(nameof(loopSequence))]
#endif
		[SerializeField] private int loopSequenceCount;

		// Events
#if ODIN_INSPECTOR
		[ShowIf(nameof(isSequence))]
#endif
		public UnityEvent SequenceTimeoutEvent;
#if ODIN_INSPECTOR
		[ShowIf(nameof(isSequence))]
#endif
		public UnityEvent AnyTimeoutEvent;
#if ODIN_INSPECTOR
		[ShowIf(nameof(isSequence)), ShowIf(nameof(loopSequence))]
#endif
		public UnityEvent FinalTimeoutEvent;

		private int currentEvent = 0;
		private int loopCounter;

		private void Awake ()
		{
			if (events != null)
				foreach (var ev in events)
				{
					ev.SetParent(this);
					ev.TimeoutEvent.AddListener(HandleEventTimeout);
				}
		}

		public static void Create (float time, UnityEvent ev = null)
		{
			GameObject timerGO = new GameObject("Timer");
			var timer = timerGO.AddComponent<TimedEvent>();
			timer.AddTimer(time, ev);
			timer.StartTimer();
		}

		public void Rewind ()
		{
			currentEvent = 0;
			loopCounter = 0;
			foreach (var ev in events)
				ev.Rewind();
		}

		public void StartTimer ()
		{
			events[currentEvent].StartTimer();
		}

		public void Stop (bool invoke = false)
		{
			events[currentEvent].Stop(invoke);
			Rewind();
			if (invoke)
			{
				SequenceTimeoutEvent?.Invoke();
				if (loopSequence)
					FinalTimeoutEvent?.Invoke();
			}
		}

		private void AddTimer (float time, UnityEvent ev)
		{
			events = new TimedEventBit[]
			{
				new TimedEventBit(this)
				{
					timeAsGetter = new FloatValueGetter { SimpleValue = time },
					TimeoutEvent = ev
				}
			};
		}

		private void HandleEventTimeout ()
		{
			AnyTimeoutEvent?.Invoke();
			bool changeEvent = events[currentEvent].IsOverWithLoops;
			if (changeEvent)
				currentEvent++;
			if (currentEvent >= events.Length)
			{
				currentEvent = 0;
				SequenceTimeoutEvent?.Invoke();
				if (loopSequence)
				{
					if (loopSequenceCount == 0 || loopCounter < loopSequenceCount)
					{
						loopCounter++;
						events[currentEvent].StartTimer();
					}
					else
						FinalTimeoutEvent?.Invoke();
				}
			}
			else if (changeEvent)
				events[currentEvent].StartTimer();
		}
	}

	[Serializable]
	public class TimedEventBit
	{
#if ODIN_INSPECTOR
		[InlineProperty]
#endif
		public FloatValueGetter timeAsGetter;
#if ODIN_INSPECTOR
		[HorizontalGroup]
#endif
		public bool loop;
#if ODIN_INSPECTOR
		[HorizontalGroup, HideLabel, ShowIf(nameof(loop))]
#endif
		public int loopCount;
		public bool useUpdateEvent;
		public UnityEvent TimeoutEvent;
#if ODIN_INSPECTOR
		[ShowIf(nameof(loop))]
#endif
		public UnityEvent EndOfLoopEvent;
#if ODIN_INSPECTOR
		[ShowIf(nameof(useUpdateEvent))]
#endif
		public bool InvertUpdateEvent;
#if ODIN_INSPECTOR
		[ShowIf(nameof(useUpdateEvent))]
#endif
		public UnityEvent<float> UpdateEvent;

		private int loopCounter;
		private Coroutine currentWait;
		private TimedEvent parent;

		public bool IsOverWithLoops => !loop || (loopCount > 0 && loopCounter >= loopCount);

		public TimedEventBit (TimedEvent parent)
		{
			SetParent(parent);
		}

		public void SetParent (TimedEvent timedEvent)
		{
			parent = timedEvent;
		}

		public void Rewind ()
		{
			loopCounter = 0;
		}

		public void StartTimer ()
		{
			StartTimer(timeAsGetter.GetValue());
		}

		public void StartTimer (float time)
		{
			Rewind();
			RunTimer(time);
		}

		public void Stop (bool invoke = false)
		{
			if (currentWait != null)
			{
				//Logger.Log("[Timed Event] << STOP >> timed event " + parent.name);
				parent.StopCoroutine(currentWait);
			}
			Rewind();
			if (invoke)
			{
				TimeoutEvent?.Invoke();
			}
		}

		private void RunTimer (float time)
		{
			loopCounter++;
			if (parent.gameObject.activeSelf)
				currentWait = parent.StartCoroutine(WaitCoroutine(time, HandleTimerEnded));
		}

		private IEnumerator WaitCoroutine (float time, Action callback)
		{
			float startingTime = time;
			while (time > 0)
			{
				time -= Time.deltaTime;
				if (time < 0)
					time = 0;
				UpdateEvent?.Invoke(InvertUpdateEvent ? time / startingTime : 1 - time / startingTime);
				yield return null;
			}
			//Logger.Log("[Timed Event] >> INVOKE << callback for " + parent.name);
			callback?.Invoke();
		}

		private void HandleTimerEnded ()
		{
			TimeoutEvent?.Invoke();
			if (loop)
			{
				if (Mathf.Approximately(timeAsGetter.GetValue(), 0))
					Logger.LogWarning($"[{nameof(TimedEvent)}] Time is equal to 0 and is set to loop.");
				else if (loopCount == 0 || loopCounter < loopCount)
					RunTimer(timeAsGetter.GetValue());
				else
				{
					EndOfLoopEvent?.Invoke();
				}
			}
		}
	}
}