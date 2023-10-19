#if KALKATOS_SCRIPTABLE
using Kalkatos.UnityGame.Scriptable;
#endif
using UnityEngine;

namespace Kalkatos.UnityGame.Audio
{
    public class AudioEventContainerForScriptable : MonoBehaviour
    {
#if KALKATOS_SCRIPTABLE
        [SerializeField] private SignalToMusic[] musicEvents;
        [SerializeField] private SignalToSfx[] sfxEvents;

        private void Awake ()
        {
            for (int i = 0; i < musicEvents.Length; i++)
                musicEvents[i].RegisterSignal();
            for (int i = 0; i < sfxEvents.Length; i++)
                sfxEvents[i].RegisterSignal();
        }

        private void OnDestroy ()
        {
            for (int i = 0; i < musicEvents.Length; i++)
                musicEvents[i].UnregisterSignal();
            for (int i = 0; i < sfxEvents.Length; i++)
                sfxEvents[i].UnregisterSignal();
        }
#endif
    }

#if KALKATOS_SCRIPTABLE
    [System.Serializable]
    public class SignalToMusic
    {
        public Signal Signal;
        public BackgroundMusic Music;

        public void Play ()
        {
            AudioController.PlayMusic(Music);
        }

        public void Play (string s) => Play();
        public void Play (float f) => Play();
        public void Play (int i) => Play();
        public void Play (bool b) => Play();

        public void RegisterSignal ()
        {
            if (Signal is TypedSignal<string>)
            {
                var typed = (TypedSignal<string>)Signal;
                typed.OnSignalEmittedWithParam.AddListener(Play);
                return;
            }
            if (Signal is TypedSignal<float>)
            {
                var typed = (TypedSignal<float>)Signal;
                typed.OnSignalEmittedWithParam.AddListener(Play);
                return;
            }
            if (Signal is TypedSignal<int>)
            {
                var typed = (TypedSignal<int>)Signal;
                typed.OnSignalEmittedWithParam.AddListener(Play);
                return;
            }
            if (Signal is TypedSignal<bool>)
            {
                var typed = (TypedSignal<bool>)Signal;
                typed.OnSignalEmittedWithParam.AddListener(Play);
                return;
            }
            Signal.OnSignalEmitted.AddListener(Play);
        }

        public void UnregisterSignal ()
        {
            if (Signal is TypedSignal<string>)
            {
                var typed = (TypedSignal<string>)Signal;
                typed.OnSignalEmittedWithParam.RemoveListener(Play);
                return;
            }
            if (Signal is TypedSignal<float>)
            {
                var typed = (TypedSignal<float>)Signal;
                typed.OnSignalEmittedWithParam.RemoveListener(Play);
                return;
            }
            if (Signal is TypedSignal<int>)
            {
                var typed = (TypedSignal<int>)Signal;
                typed.OnSignalEmittedWithParam.RemoveListener(Play);
                return;
            }
            if (Signal is TypedSignal<bool>)
            {
                var typed = (TypedSignal<bool>)Signal;
                typed.OnSignalEmittedWithParam.RemoveListener(Play);
                return;
            }
            Signal.OnSignalEmitted.RemoveListener(Play);
        }
    }

    [System.Serializable]
    public class SignalToSfx
    {
        public Signal Signal;
        public SoundEffect SoundEffect;

        public void Play ()
        {
            AudioController.PlaySfx(SoundEffect);
        }

        public void Play (string s) => Play();
        public void Play (float f) => Play();
        public void Play (int i) => Play();
        public void Play (bool b) => Play();

        public void RegisterSignal ()
        {
            if (Signal is TypedSignal<string>)
            {
                var typed = (TypedSignal<string>)Signal;
                typed.OnSignalEmittedWithParam.AddListener(Play);
                return;
            }
            if (Signal is TypedSignal<float>)
            {
                var typed = (TypedSignal<float>)Signal;
                typed.OnSignalEmittedWithParam.AddListener(Play);
                return;
            }
            if (Signal is TypedSignal<int>)
            {
                var typed = (TypedSignal<int>)Signal;
                typed.OnSignalEmittedWithParam.AddListener(Play);
                return;
            }
            if (Signal is TypedSignal<bool>)
            {
                var typed = (TypedSignal<bool>)Signal;
                typed.OnSignalEmittedWithParam.AddListener(Play);
                return;
            }
            Signal.OnSignalEmitted.AddListener(Play);
        }

        public void UnregisterSignal ()
        {
            if (Signal is TypedSignal<string>)
            {
                var typed = (TypedSignal<string>)Signal;
                typed.OnSignalEmittedWithParam.RemoveListener(Play);
                return;
            }
            if (Signal is TypedSignal<float>)
            {
                var typed = (TypedSignal<float>)Signal;
                typed.OnSignalEmittedWithParam.RemoveListener(Play);
                return;
            }
            if (Signal is TypedSignal<int>)
            {
                var typed = (TypedSignal<int>)Signal;
                typed.OnSignalEmittedWithParam.RemoveListener(Play);
                return;
            }
            if (Signal is TypedSignal<bool>)
            {
                var typed = (TypedSignal<bool>)Signal;
                typed.OnSignalEmittedWithParam.RemoveListener(Play);
                return;
            }
            Signal.OnSignalEmitted.RemoveListener(Play);
        }
    }
#endif
}