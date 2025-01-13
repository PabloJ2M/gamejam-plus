using System;
using UnityEngine;

namespace UI.Dialogues
{
    [Serializable] public struct Dialogue
    {
        [SerializeField] private string _header;
        [SerializeField, TextArea] private string _text;

        public string Header => _header;
        public string Text => _text;
    }
}