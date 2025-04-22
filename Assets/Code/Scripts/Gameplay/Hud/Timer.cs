namespace UnityEngine.UI
{
    public class Timer : BarUI
    {
        private bool _isPlaying = true;

        public void StopUpdate() => _isPlaying = false;
        public void PlayUpdate() => _isPlaying = true;
        public override void ResetValue() { base.ResetValue(); _isPlaying = true; }
        public override void FillValue() { base.FillValue(); UpdateUI(); }

        private void Update()
        {
            if (_isLocked || !_isPlaying) return;

            _current -= Time.deltaTime;
            UpdateUI();
        }
    }
}