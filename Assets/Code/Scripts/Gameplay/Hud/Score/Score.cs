using UnityEngine;
using TMPro;

namespace UI.Display
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private string _format = "Score: {0}";
        [SerializeField] private TextMeshProUGUI _textUI;
        
        private float _score;

        public void AddScore(float amount) { _score += amount; UpdateScore(); }
        public void RemoveScore(float amount) { _score = Mathf.Clamp(_score -= amount, 0, int.MaxValue); UpdateScore(); }
        public void RestoreScore() { _score = 0; UpdateScore(); }

        private void Start() => UpdateScore();
        private void UpdateScore() => _textUI?.SetText(string.Format(_format, (int)_score));
    }
}