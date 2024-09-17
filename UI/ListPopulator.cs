using System.Collections.Generic;
using UnityEngine;

namespace Kalkatos.UnityGame.UI
{
    public class ListPopulator : MonoBehaviour
    {
        [SerializeField] protected ListItem prefab;
        [SerializeField] protected Transform container;

        public virtual void Populate<T> (List<T> items) where T : ScriptableObject
        {
            foreach (var item in items)
            {
                ListItem newObj = Instantiate(prefab, container);
                newObj.Setup(item);
            }
        }
    }
}