using SBaier.DI;
using TMPro;
using UnityEngine;

namespace SBaier.Astrominer
{
	public abstract class ItemPropertyDisplay : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _text;

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
	
    public abstract class ItemPropertyDisplay<TItem> : ItemPropertyDisplay, Injectable
    {
		protected TItem _item;

		public virtual void Inject(Resolver resolver)
		{
			_item = resolver.Resolve<TItem>();
		}
	}
	
    public abstract class ItemPropertyDisplay<TItem, TSecondItem> : ItemPropertyDisplay, Injectable
    {
		protected TItem _item;
		protected TSecondItem _secondItem;

		public virtual void Inject(Resolver resolver)
		{
			_item = resolver.Resolve<TItem>();
			_secondItem = resolver.Resolve<TSecondItem>();
		}
	}
}
