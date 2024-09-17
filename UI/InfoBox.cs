using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kalkatos.UnityGame
{
    public class InfoBox : MonoBehaviour, IInfoReceiver
    {
        [SerializeField] private ComponentBinding[] bindings;

        private Dictionary<string, Component> dict;

        public void Setup (object info)
        {
            Receive((Dictionary<string, object>)info);
        }

        public void Setup (ScriptableObject dataScriptable)
        {
            if (dataScriptable is not IInfoProvider)
            {
                Logger.LogWarning($"Info box received signal from {dataScriptable.name} but it does not implement interface IInfoProvider.");
                return;
            }
            Receive(((IInfoProvider)dataScriptable).GetInfo());
        }

        public void Setup (Component comp)
        {
            if (comp is not IInfoProvider)
            {
                Logger.LogWarning($"Info box received signal from {comp.name} but it does not implement interface IInfoProvider.");
                return;
            }
            Receive(((IInfoProvider)comp).GetInfo());
        }

        public void Receive (Dictionary<string, object> info)
        {
            CheckDict();

            foreach (var kv in info)
            {
                if (!dict.ContainsKey(kv.Key))
                    continue;
                object data = info[kv.Key];
                Component comp = dict[kv.Key];
                switch (data)
                {
                    case string:
                        if (comp is TMP_Text)
                        {
                            ((TMP_Text)comp).text = (string)data;
                            continue;
                        }
                        break;
                    case int:
                        if (comp is TMP_Text)
                        {
                            ((TMP_Text)comp).text = data.ToString();
                            continue;
                        }
                        break;
                    case float:
                        if (comp is TMP_Text)
                        { 
                            ((TMP_Text)comp).text = data.ToString(); 
                            continue; 
                        }
                        else if (comp is RectTransform)
                        {
                            RectTransform rect = (RectTransform)comp;
                            Vector2 anchor = rect.anchorMax;
                            anchor.x = (float)data;
                            rect.anchorMax = anchor;
                            continue;
                        }
                        else if (comp is Image)
                        {
                            ((Image)comp).fillAmount = (float)data;
                            continue;
                        }
                        break;
                    case Sprite:
                        if (comp is SpriteRenderer)
                        {
                            ((SpriteRenderer)comp).sprite = (Sprite)data;
                            continue;
                        }
                        else if (comp is Image)
                        {
                            ((Image)comp).sprite = (Sprite)data;
                            continue;
                        }
                        break;
                    case Color:
                        if (comp is SpriteRenderer)
                        {
                            ((SpriteRenderer)comp).color = (Color)data;
                            continue;
                        }
                        else if (comp is Image)
                        {
                            ((Image)comp).color = (Color)data;
                            continue;
                        }
                        break;
                    case IInfoProvider:
                        if (comp is IInfoReceiver)
                        {
                            ((IInfoReceiver)comp).Receive(((IInfoProvider)data).GetInfo());
                            continue;
                        }
                        break;
                    default:
                        Logger.LogWarning($"Info Box received data {data} of type {data.GetType().Name} which is not supported.");
                        continue;
                }
                Logger.LogWarning($"Info Box received data {data} of type {data.GetType().Name} but component {comp.name} of type {comp.GetType().Name} could nod handle.");
            }
        }

        public void CheckDict ()
        {
            if (dict != null)
                return;
            dict = new Dictionary<string, Component>();
            foreach (var bind in bindings)
                dict.Add(bind.Key, bind.Component);
        }
    }

    public interface IInfoProvider
    {
        public Dictionary<string, object> GetInfo ();
    }

    public interface IInfoReceiver
    {
        void Receive (Dictionary<string, object> info);
    }

    [System.Serializable]
    public class ComponentBinding
    {
        public string Key;
        public Component Component;
    }
}