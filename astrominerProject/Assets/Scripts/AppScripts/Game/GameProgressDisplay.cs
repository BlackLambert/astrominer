using SBaier.DI;
using UnityEngine;

namespace SBaier.Astrominer
{
    public class GameProgressDisplay : ItemPropertyDisplay<Game>
    {
        [SerializeField] 
        private string _baseString = "Total Exploit: {0}% / {1}%";

        [SerializeField]
        private string _fomat = "F2";

        private TargetExploit _targetExploit;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _targetExploit = resolver.Resolve<TargetExploit>();
        }

        private void Update()
        {
            SetText();
        }

        protected override string GetText()
        {
            string totalExploit = ToString(_item.ExploitedPercentage);
            string targetExploit = ToString(_targetExploit.Target);
            return string.Format(_baseString, totalExploit, targetExploit);
        }

        private string ToString(float value)
        {
            float amount = value * 100;
            return amount.ToString(_fomat);
        }
    }
}
