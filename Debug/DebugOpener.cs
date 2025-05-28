using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kalkatos.UnityGame.Debug
{
	public class DebugOpener : MonoBehaviour
	{
		[SerializeField] private Button debuggerButton;
		[SerializeField] private Button closeButton;
		[SerializeField] private GameObject debuggerScreen;
		[SerializeField] private TMP_Text debugText;

		private float firstClickTime;
		private int clickCounter;

		private void Awake ()
		{
			if (!UnityEngine.Debug.isDebugBuild)
			{
				Destroy(gameObject);
				return;
			}

			debuggerButton.onClick.AddListener(OnDebugButtonClick);
			closeButton.onClick.AddListener(OnCloseButtonClick);
			Application.logMessageReceived += OnLogMessageReceived;
		}

		private void OnDestroy ()
		{
			debuggerButton.onClick.RemoveListener(OnDebugButtonClick);
			closeButton.onClick.RemoveListener(OnCloseButtonClick);
			Application.logMessageReceived -= OnLogMessageReceived;
		}

		private void OnDebugButtonClick ()
		{
			if (clickCounter == 0)
				firstClickTime = Time.time;
			else if (Time.time - firstClickTime > 2)
				clickCounter = 0;
			clickCounter++;
			if (clickCounter >= 3)
			{
				clickCounter = 0;
				debuggerScreen.SetActive(true);
			}
		}

		private void OnCloseButtonClick ()
		{
			clickCounter = 0;
			debuggerScreen.SetActive(false);
		}

		private void OnLogMessageReceived (string message, string stackTrace, LogType type)
		{
			debugText.text = $"{debugText.text}\n{message}";
		}
	}
}