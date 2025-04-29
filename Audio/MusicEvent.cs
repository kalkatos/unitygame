#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Kalkatos.UnityGame.Audio
{
	[CreateAssetMenu(fileName = "NewMusicEvent", menuName = "Audio/Music Event", order = 9)]
	public class MusicEvent : ScriptableObject
	{
#if ODIN_INSPECTOR
		[ShowIf(nameof(IsRandomMusic))]
#endif
		public bool IsRandomMusic;
#if ODIN_INSPECTOR
		[InlineButton(nameof(RandomizeMusic), "Random"), HideIf(nameof(IsRandomMusic))]
#endif
		public BackgroundMusic SingleMusic;
#if ODIN_INSPECTOR
		[ShowIf(nameof(IsRandomMusic))]
#endif
		public BackgroundMusic[] RandomMusic;
		public FloatValueGetter Delay;
		public FloatValueGetter FadeInTime;
		public FloatValueGetter FadeOutTime;

		private BackgroundMusic Music
		{
			get
			{
				if (IsRandomMusic)
					return RandomMusic[Random.Range(0, RandomMusic.Length)];
				return SingleMusic;
			}
		}

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

		public void RandomizeMusic ()
		{
			IsRandomMusic = true;
		}
	}
}
