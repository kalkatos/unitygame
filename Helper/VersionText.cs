using TMPro;
using UnityEngine;

namespace Kalkatos.UnityGame
{
    public class VersionText : MonoBehaviour
    {
        [SerializeField] private TMP_Text tmpText;
        [SerializeField] private string prefix = "v";
        [SerializeField] private string sufix;

		private void OnEnable ()
		{
			tmpText?.SetText($"{prefix}{Application.version}{sufix}");
		}
	}
}