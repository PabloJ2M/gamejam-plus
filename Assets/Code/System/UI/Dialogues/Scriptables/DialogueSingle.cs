using UnityEngine;

namespace UI.Dialogues
{
    [CreateAssetMenu(fileName = "dialogue", menuName = "System/Dialogue")]
    public class DialogueSingle : Dialogue
    {
        [SerializeField] private string _header;
        [SerializeField] private string _text;

        public string Header => _header;
        public string Text => _text;
    }
}