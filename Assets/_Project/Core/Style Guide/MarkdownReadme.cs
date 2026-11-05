using UnityEngine;

namespace UnityGameJam.Core.Editor
{
    [CreateAssetMenu(fileName = "StyleGuideReadme", menuName = "Kajo Games/Style Guide Readme")]
    public class MarkdownReadme : ScriptableObject
    {
        [TextArea(15, 30)]
        [SerializeField] private string _markdownContent;

        public string MarkdownContent => _markdownContent;
    }
}