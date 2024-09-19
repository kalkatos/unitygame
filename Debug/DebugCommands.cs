using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kalkatos.UnityGame
{
    [DefaultExecutionOrder(-100)]
    public class DebugCommands : MonoBehaviour
    {

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TMP_Text debugText;
        [SerializeField] private TMP_InputField inputField;

        private static DebugCommands instance;
        private bool isOpen;
        private Dictionary<string, Action> methods = new();
        private int lastCommandIndex;
        private List<string> lastCommands = new();

        private void Awake ()
        {
            if (!UnityEngine.Debug.isDebugBuild)
            {
                Destroy(gameObject);
                return;
            }
            if (instance == null)
                instance = this;
            else if (instance != this)
            {
                Destroy(this);
                return;
            }

            Application.logMessageReceived += HandleLogReceived;

            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy () 
        {
            Application.logMessageReceived -= HandleLogReceived;
            inputField.onSubmit.RemoveListener(HandleInputSubmit);
        }

        private void Update ()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Toggle();
            if (Input.GetKeyDown(KeyCode.UpArrow) && isOpen)
            {
                if (lastCommands.Count == 0)
                    return;
                string lastCommand = lastCommands[lastCommandIndex];
                lastCommandIndex = (lastCommandIndex + 1) % lastCommands.Count;
                inputField.SetTextWithoutNotify(lastCommand);
                FocusInputText();
            }
        }

        public static void AddDebugMethod (string name, Action method)
        {
            if (instance == null)
                return;
            if (!instance.methods.ContainsKey(name))
                instance.methods.Add(name, method);
        }

        private void Toggle ()
        {
            if (isOpen)
                Close();
            else
                Open();
        }

        private void FocusInputText ()
        {
            EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
            inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }

        private void Open ()
        {
            if (isOpen)
                return;
            isOpen = true;
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            inputField.onSubmit.AddListener(HandleInputSubmit);
            FocusInputText();
        }

        private void Close ()
        {
            if (!isOpen)
                return;
            isOpen = false;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            inputField.onSubmit.RemoveListener(HandleInputSubmit);
        }

        private void HandleInputSubmit (string input)
        {
            inputField.SetTextWithoutNotify("");
            string[] split = input.Split(' ');
            if (split != null && split.Length > 0)
            {
                if (string.IsNullOrEmpty(split[0]))
                    return;
                lastCommandIndex = 0;
                if (lastCommands.Contains(split[0]))
                    lastCommands.RemoveAt(lastCommands.IndexOf(split[0]));
                lastCommands.Insert(0, split[0]);
                if (methods.TryGetValue(split[0], out Action action))
                {
                    action.Invoke();
                    Logger.Log($"Command {split[0]} executed.");
                    return;
                }
                else
                    Logger.Log("Command not found.");
            }
        }

        private void HandleLogReceived (string message, string stackTrace, LogType type)
        {
            debugText.text = $"{debugText.text}\n{message}";
        }
    }
}
