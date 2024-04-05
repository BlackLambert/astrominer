using UnityEditor;

namespace SBaier.Astrominer
{
    public class EditorAppQuitter : AppQuitter
    {
        public override void Quit()
        {
            EditorApplication.ExitPlaymode();
        }
    }
}
