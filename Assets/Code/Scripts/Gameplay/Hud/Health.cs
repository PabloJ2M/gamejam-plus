namespace UnityEngine.UI.Display
{
    public class Health : BarUI
    {
        public override void IncrementValue(float amount)
        {
            base.IncrementValue(amount);
            UpdateUI();
        }
        public override void ReduceValue(float amount)
        {
            base.ReduceValue(amount);
            UpdateUI();
        }
        public override void FillValue()
        {
            base.FillValue();
            UpdateUI();
        }
    }
}