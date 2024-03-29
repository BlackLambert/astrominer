using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
    public abstract class ItemPropertyDisplay<T> : MonoBehaviour, Injectable
    {
		[SerializeField]
		private TextMeshProUGUI _text;

		protected T _item;

		public virtual void Inject(Resolver resolver)
		{
			_item = resolver.Resolve<T>();
		}

		private void Reset()
		{
			_text = GetComponent<TextMeshProUGUI>();
		}

		protected virtual void OnEnable()
		{
			SetText();
		}

		protected void SetText()
		{
			_text.text = GetText();
		}

		protected abstract string GetText();
	}
}
