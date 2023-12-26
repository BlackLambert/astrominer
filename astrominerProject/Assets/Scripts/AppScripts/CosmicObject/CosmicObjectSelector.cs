using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class CosmicObjectSelector<TItem> : ItemSelector<TItem> where TItem : CosmicObject
    {
        private ActiveItem<CosmicObject> _selectedCosmicObject;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _selectedCosmicObject = resolver.Resolve<ActiveItem<CosmicObject>>();
        }

        protected override void SelectItem(TItem item)
        {
            base.SelectItem(item);
            _selectedCosmicObject.Value = item;
        }

        protected override void DeselectItem()
        {
            base.DeselectItem();
            _selectedCosmicObject.Value = null;
        }
    }
}
