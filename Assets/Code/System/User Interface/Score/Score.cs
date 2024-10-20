using UnityEngine;
using TMPro;

namespace UI.Display
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private string _prefix = "Score:";
        [SerializeField] private TextMeshProUGUI _textUI;
        
        private float _score;

        public void AddScore(float amount) { _score += amount; UpdateScore(); }
        public void RemoveScore(float amount) { _score = Mathf.Clamp(_score -= amount, 0, int.MaxValue); UpdateScore(); }

        private void UpdateScore() => _textUI?.SetText($"{ _prefix } { (int)_score }");
    }
}