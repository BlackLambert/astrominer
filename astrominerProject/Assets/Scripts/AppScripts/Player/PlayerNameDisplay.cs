using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class PlayerNameDisplay : ItemPropertyDisplay<Player>
    {
        protected override string GetText()
        {
            return _item.Name;
        }
    }
}
