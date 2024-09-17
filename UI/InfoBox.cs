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
            if (dataScriptable == null)
            {
                Receive(null);
                return;
            }
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

            if (info == null)
            {
                foreach (var kv in dict)
                    ClearComp(kv.Value);
                return;
            }

            foreach (var kv in info)
            {
                if (!dict.ContainsKey(kv.Key))
                    continue;
                object data = info[kv.Key];
                Component comp = dict[kv.Key];
                if (data == null)
                {
                    ClearComp(comp);
                    continue;
                }
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
                            var image = (Image)comp;
                            image.enabled = true;
                            image.fillAmount = (float)data;
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
                            var image = (Image)comp;
                            image.enabled = true;
                            image.sprite = (Sprite)data;
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
                            var image = (Image)comp;
                            image.enabled = true;
                            image.color = (Color)data;
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

            void ClearComp (Component comp)
            {
                switch (comp)
                {
                    case TMP_Text:
                        ((TMP_Text)comp).text = "";
                        break;
                    case SpriteRenderer:
                        var spriteRenderer = (SpriteRenderer)comp;
                        spriteRenderer.sprite = null;
                        spriteRenderer.color = Color.white;
                        break;
                    case Image:
                        var image = (Image)comp;
                        image.sprite = null;
                        image.color = Color.white;
                        image.enabled = false;
                        image.fillAmount = 1;
                        break;
                    case RectTransform:
                        RectTransform rect = (RectTransform)comp;
                        Vector2 anchor = rect.anchorMax;
                        anchor.x = 1;
                        rect.anchorMax = anchor;
                        break;
                    case IInfoReceiver:
                        ((IInfoReceiver)comp).Receive(null);
                        break;
                }
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