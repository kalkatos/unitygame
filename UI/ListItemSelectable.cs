using UnityEngine;
using UnityEngine.UI;

namespace Kalkatos.UnityGame.UI
{
    public abstract class ListItemSelectable : ListItem
    {
        [SerializeField] protected Button button;

        private void Awake ()
        {
            button.onClick.AddListener(HandleButtonClicked);
        }

        private void OnDestroy ()
        {
            button.onClick.RemoveListener(HandleButtonClicked);
        }

        public  void HandleButtonClicked ()
        {
            HandleItemSelected(Data);
        }

        public abstract void HandleItemSelected (ScriptableObject item);
    }
}