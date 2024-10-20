using UnityEngine;

namespace UI.Display
{
    public class ScoreTime : Score
    {
        private void Update() => AddScore(Time.deltaTime);
    }
}