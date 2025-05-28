using System.Linq;
using UnityEngine;

namespace Kalkatos.UnityGame
{
	public class InfoBoxList : MonoBehaviour
	{
		[SerializeField] private InfoBox infoBoxPrefab;
		[SerializeField] private Transform container;

		public void Dispose ()
		{
			for (int i = container.childCount - 1; i >= 0; i--)
				Destroy(container.GetChild(i).gameObject);
		}

		public void Setup (IInfoProvider[] infoProviders)
		{
			Dispose();
			foreach (var provider in infoProviders)
			{
				InfoBox newBox = Instantiate(infoBoxPrefab, container);
				newBox.Receive(provider.GetInfo());
			}
		}

		public void Setup (ScriptableObject so)
		{
			if (so is not ScriptableObjectContainer)
				throw new System.Exception($"InfoBoxList {name} expects a ScriptableObjectContainer to be set up.");
			ScriptableObjectContainer container = so as ScriptableObjectContainer;
			if (!container.Objects.All(x => x is IInfoProvider))
				throw new System.Exception($"InfoBoxList {name} expects an array of IInfoProvider to be set up.");
			Setup(container.Objects.Select(x => (IInfoProvider)x).ToArray());
		}
	}
}