#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kalkatos.UnityGame.Audio
{
    public class AudioController : MonoBehaviour
    {
        private static AudioController instance;

        [SerializeField] private AudioSource musicChannel;
        [SerializeField] private AudioSource[] sfxChannels;

        private static Queue<AudioSource> sfxChannelQueue = new();

        void Awake ()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
            {
                Destroy(this);
                return;
            }
            DontDestroyOnLoad(gameObject);
            for (int i = 0; i < sfxChannels.Length; i++)
                sfxChannelQueue.Enqueue(sfxChannels[i]);
        }

        public static void PlayMusic (BackgroundMusic music, float fadeInTime = 0f, float delay = 0f)
        {
            if (delay > 0)
            {
                instance.Wait(delay, () => PlayMusic(music, fadeInTime));
                return;
            }
            instance.musicChannel.Stop();
            instance.musicChannel.clip = music.Clip;
            instance.musicChannel.loop = music.Loop;
            instance.musicChannel.Play();
            if (fadeInTime > 0)
                instance.StartCoroutine(instance.FadeMusic(fadeInTime, 0, music.Volume));
            else
                instance.musicChannel.volume = music.Volume;
        }

        public static void StopMusic (float fadeOutTime = 0f)
        {
            if (fadeOutTime > 0)
                instance.StartCoroutine(instance.FadeMusic(fadeOutTime, instance.musicChannel.volume, 0, instance.musicChannel.Stop));
            else
                instance.musicChannel.Stop();
        }

        public static void PlaySfx (SoundEffect sfx)
        {
            var source = sfxChannelQueue.Dequeue();
            source.Stop();
            source.clip = sfx.ClipVariations[Random.Range(0, sfx.ClipVariations.Length)];
            source.pitch = Random.Range(sfx.Pitch.x, sfx.Pitch.y);
            source.volume = sfx.Volume;
            source.Play();
            sfxChannelQueue.Enqueue(source);
        }

        private IEnumerator FadeMusic (float time, float startVolume, float endVolume, System.Action callback = null)
        {
            instance.musicChannel.volume = startVolume;
            float startTime = Time.time;
            float elapsed = 0;
            while (elapsed < time)
            {
                elapsed = Time.time - startTime;
                instance.musicChannel.volume = Mathf.Lerp(startVolume, endVolume, elapsed / time);
                yield return null;
            }
            instance.musicChannel.volume = endVolume;
            callback?.Invoke();
        }
    }

    [System.Serializable]
    public class BackgroundMusic
    {
        public AudioClip Clip;
        [Range(0f, 1f)]
        public float Volume = 1f;
        public bool Loop;
    }

    [System.Serializable]
    public class SoundEffect
    {
        public AudioClip[] ClipVariations;
        [Range(0f, 1f)]
        public float Volume = 1f;
#if ODIN_INSPECTOR
        [MinMaxSlider(0.5f, 1.5f, ShowFields = true)]
#endif
        public Vector2 Pitch = new(1f, 1f);
    }
}