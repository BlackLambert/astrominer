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

		protected virtual void Start()
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
