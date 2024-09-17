using UnityEngine;

namespace Kalkatos.UnityGame.UI
{
    public abstract class ListItem : MonoBehaviour
    {
        public ScriptableObject Data;

        public void Setup<T> (T data) where T : ScriptableObject
        {
            Data = data;
        }
    }
}