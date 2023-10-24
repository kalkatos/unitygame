using UnityEngine;

namespace Kalkatos.UnityGame.Audio
{
    [CreateAssetMenu(fileName = "NewMusicEvent", menuName = "Audio/Music Event", order = 9)]
    public class MusicEvent : ScriptableObject
    {
        public BackgroundMusic Music;
        public FloatValueGetter Delay;
        public FloatValueGetter FadeInTime;
        public FloatValueGetter FadeOutTime;

        public void Play () => Play(FadeInTime);

        public void Play (FloatValueGetter fadeInTime)
        {
            AudioController.PlayMusic(Music, fadeInTime.GetValue(), Delay.GetValue());
        }

        public void Stop () => Stop(FadeOutTime);

        public void Stop (FloatValueGetter fadeOutTime)
        {
            AudioController.StopMusic(fadeOutTime.GetValue());
        }
    }
}
