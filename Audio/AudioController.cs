#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
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

        public static void PlayMusic (BackgroundMusic music)
        {
            instance.musicChannel.Stop();
            instance.musicChannel.clip = music.Clip;
            instance.musicChannel.volume = music.Volume;
            instance.musicChannel.loop = music.Loop;
            instance.musicChannel.Play();
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