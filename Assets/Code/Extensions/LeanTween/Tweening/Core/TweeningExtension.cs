using Unity.Mathematics;

namespace UI.Effects
{
    public static class TweeningExtension
    {
        public static float2 GetDirection(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Left: return mathf.left;
                case Direction.Right: return mathf.right;
                case Direction.Top: return mathf.up;
                case Direction.Bottom: return mathf.down;
                default: return mathf.zero;
            }
        }
    }
}