using UnityEditor;
using UnityEngine;
using UnityGameJam.Core.Editor;

namespace KajoGames.MyGame.Core.Editor
{
    [CustomEditor(typeof(MarkdownReadme))]
    public class MarkdownReadmeEditor : UnityEditor.Editor
    {
        private GUIStyle _titleStyle;
        private GUIStyle _headingStyle;
        private GUIStyle _subHeadingStyle;
        private GUIStyle _bodyStyle;
        private GUIStyle _codeStyle;
        private GUIStyle _quoteStyle;
        private bool _isEditing;

        private void InitStyles()
        {
            if (_titleStyle != null) return;

            _titleStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 18,
                richText = true,
                wordWrap = true,
                normal = { textColor = EditorGUIUtility.isProSkin ? new Color(0.9f, 0.9f, 0.9f) : Color.black }
            };

            _headingStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 14,
                richText = true,
                wordWrap = true,
                normal = { textColor = EditorGUIUtility.isProSkin ? new Color(0.4f, 0.7f, 1f) : new Color(0.1f, 0.3f, 0.7f) }
            };

            _subHeadingStyle = new GUIStyle(EditorStyles.boldLabel)
            {
                fontSize = 12,
                richText = true,
                wordWrap = true,
                normal = { textColor = EditorGUIUtility.isProSkin ? new Color(0.8f, 0.8f, 0.8f) : Color.darkGray }
            };

            _bodyStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 11,
                richText = true,
                wordWrap = true,
                normal = { textColor = EditorGUIUtility.isProSkin ? new Color(0.8f, 0.8f, 0.8f) : Color.black }
            };

            _codeStyle = new GUIStyle(EditorStyles.textArea)
            {
                fontSize = 10,
                wordWrap = false,
                font = Font.CreateDynamicFontFromOSFont("Consolas", 11),
                normal = {
                    background = MakeTex(1, 1, EditorGUIUtility.isProSkin ? new Color(0.18f, 0.18f, 0.18f) : new Color(0.92f, 0.92f, 0.92f)),
                    textColor = EditorGUIUtility.isProSkin ? new Color(0.85f, 0.85f, 0.85f) : new Color(0.1f, 0.1f, 0.1f)
                }
            };

            _quoteStyle = new GUIStyle(EditorStyles.helpBox)
            {
                fontSize = 11,
                richText = true,
                wordWrap = true
            };
        }

        public override void OnInspectorGUI()
        {
            InitStyles();
            MarkdownReadme readme = (MarkdownReadme)target;

            EditorGUILayout.Space(5);
            using (new EditorGUILayout.HorizontalScope())
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(_isEditing ? "Preview Mode" : "Edit Raw Text", GUILayout.Width(120), GUILayout.Height(25)))
                {
                    _isEditing = !_isEditing;
                }
            }
            EditorGUILayout.Space(5);

            if (_isEditing)
            {
                DrawDefaultInspector();
                return;
            }

            // Render Markdown cleanly
            string content = readme.MarkdownContent;
            if (string.IsNullOrEmpty(content))
            {
                EditorGUILayout.HelpBox("Markdown content is empty. Click 'Edit Raw Text' to paste your .md contents.", MessageType.Info);
                return;
            }

            string[] lines = content.Split('\n');
            bool inCodeBlock = false;
            string codeBuffer = "";

            foreach (var rawLine in lines)
            {
                string line = rawLine.TrimEnd('\r');

                // Handle Code Blocks
                if (line.StartsWith("```"))
                {
                    if (inCodeBlock)
                    {
                        // Render accumulated code block
                        EditorGUILayout.Space(2);
                        EditorGUILayout.SelectableLabel(codeBuffer, _codeStyle, GUILayout.Height(Mathf.Min(codeBuffer.Split('\n').Length * 14 + 10, 150)), GUILayout.ExpandWidth(true));
                        EditorGUILayout.Space(2);
                        codeBuffer = "";
                    }
                    inCodeBlock = !inCodeBlock;
                    continue;
                }

                if (inCodeBlock)
                {
                    codeBuffer += line + "\n";
                    continue;
                }

                // Handle Headers
                if (line.StartsWith("# "))
                {
                    EditorGUILayout.Space(10);
                    EditorGUILayout.LabelField(FormatInlineMarkdown(line.Substring(2)), _titleStyle);
                    EditorGUILayout.Space(4);
                }
                else if (line.StartsWith("## "))
                {
                    EditorGUILayout.Space(8);
                    EditorGUILayout.LabelField(FormatInlineMarkdown(line.Substring(3)), _headingStyle);
                    DrawUILine(new Color(0.5f, 0.5f, 0.5f, 0.4f), 1, 4);
                }
                else if (line.StartsWith("### "))
                {
                    EditorGUILayout.Space(6);
                    EditorGUILayout.LabelField(FormatInlineMarkdown(line.Substring(4)), _subHeadingStyle);
                }
                else if (line.StartsWith("- ") || line.StartsWith("* "))
                {
                    string bulletText = "• " + FormatInlineMarkdown(line.Substring(2));
                    EditorGUILayout.LabelField(bulletText, _bodyStyle);
                }
                else if (line.StartsWith("---"))
                {
                    DrawUILine(Color.gray, 1, 10);
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    EditorGUILayout.Space(4);
                }
                else
                {
                    EditorGUILayout.LabelField(FormatInlineMarkdown(line), _bodyStyle);
                }
            }
        }

        private string FormatInlineMarkdown(string text)
        {
            // Simple bold parser conversion for rich text: **text} -> <b>text</b>
            while (text.Contains("**"))
            {
                int first = text.IndexOf("**");
                int second = text.IndexOf("**", first + 2);
                if (second != -1)
                {
                    text = text.Substring(0, first) + "<b>" + text.Substring(first + 2, second - (first + 2)) + "</b>" + text.Substring(second + 2);
                }
                else break;
            }
            return text;
        }

        private void DrawUILine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
            r.height = thickness;
            r.y += padding / 2;
            r.x = 0;
            r.width = EditorGUIUtility.currentViewWidth;
            EditorGUI.DrawRect(r, color);
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i) pix[i] = col;
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}