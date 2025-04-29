using UnityEngine;

namespace Kalkatos.UnityGame.Audio
{
	[CreateAssetMenu(fileName = "NewSoundEffectEvent", menuName = "Audio/Sound Effect Event", order = 9)]
	public class SfxEvent : ScriptableObject
	{
		public SoundEffect Sfx;

		public void Play () => AudioController.PlaySfx(Sfx);
	}
}
