namespace SBaier.Astrominer
{
    public class PlayerNameDisplay : ItemPropertyDisplay<Player>
    {
        private const string _baseString = "{0} (Computer)";
        
        protected override string GetText()
        {
            return _item.IsHuman ? _item.Name : string.Format(_baseString, _item.Name);
        }
    }
}
