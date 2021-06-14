using UnityEngine;

namespace SlimUI.ModernMenu
{
	[ExecuteInEditMode()]
	[System.Serializable]
	public class ThemeUI : MonoBehaviour
	{
		public ThemeEditor themeController;

		protected virtual void OnSkinUI()
		{

		}

		public virtual void Awake()
		{
			OnSkinUI();
		}

		public virtual void Update()
		{
			OnSkinUI();
		}
	}
}
