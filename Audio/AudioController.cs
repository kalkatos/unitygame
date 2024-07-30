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

        private float masterMusicVolume = 1f;
        private float masterSfxVolume = 1f;

        public float MasterMusicVolume
        {
            get => masterMusicVolume;
            set
            {
                value = Mathf.Clamp01(value);
                masterMusicVolume = value;
                musicChannel.volume = value;
            }
        }

        public float MasterSfxVolume
        {
            get => masterSfxVolume;
            set
            {
                value = Mathf.Clamp01(value);
                masterSfxVolume = value;
                for (int i = 0; i < sfxChannels.Length; i++)
                    sfxChannels[i].volume = value;
            }
        }

        void Awake ()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
            {
                Destroy(gameObject);
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
                //Logger.Log($"[AudioController] Playing music {music.Clip} delayed {delay} seconds");
                instance.Wait(delay, () => PlayMusic(music, fadeInTime));
                return;
            }
            if (instance.musicChannel.isPlaying)
                instance.musicChannel.Stop();
            instance.musicChannel.clip = music.Clip;
            instance.musicChannel.loop = music.Loop;
            instance.musicChannel.Play();
            float targetVolume = music.Volume * instance.masterMusicVolume;
            if (fadeInTime > 0)
            {
                //Logger.Log($"[AudioController] FADING IN music {music.Clip} > > ");
                instance.StartCoroutine(instance.FadeMusic(fadeInTime, 0, targetVolume));//, () => Logger.Log($"[AudioController] Full volume on music {music.Clip} ! ! ! "))); 
            }
            else
            {
                //Logger.Log($"[AudioController] Playing music {music.Clip} > > ");
                instance.musicChannel.volume = targetVolume;
            }
        }

        public static void StopMusic (float fadeOutTime = 0f)
        {
            if (fadeOutTime > 0)
            {
                //Logger.Log($"[AudioController] FADING OUT music {instance.musicChannel.clip} < < ");
                AudioClip fadeOutClip = instance.musicChannel.clip;
                float startingVolume = instance.musicChannel.volume;
                instance.StartCoroutine(instance.FadeMusic(fadeOutTime, startingVolume, 0,
                    () =>
                    {
                        if (instance.musicChannel.clip == fadeOutClip)
                        {
                            instance.musicChannel.Stop();
                            //Logger.Log($"[AudioController] Stopped music {instance.musicChannel.clip} X X "); 
                        }
                    })); 
            }
            else
            {
                //Logger.Log($"[AudioController] Stopping music {instance.musicChannel.clip} [] [] ");
                instance.musicChannel.Stop(); 
            }
        }

        public static void PlaySfx (SoundEffect sfx)
        {
            var source = sfxChannelQueue.Dequeue();
            source.Stop();
            source.clip = sfx.ClipVariations[Random.Range(0, sfx.ClipVariations.Length)];
            source.pitch = Random.Range(sfx.Pitch.x, sfx.Pitch.y);
            source.volume = sfx.Volume * instance.masterSfxVolume;
            source.Play();
            sfxChannelQueue.Enqueue(source);
        }

        public static void SetMasterMusicVolume (float vol)
        {
            instance.MasterMusicVolume = vol;
        }

        public static void SetMasterSfxVolume (float vol)
        {
            instance.MasterSfxVolume = vol;
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