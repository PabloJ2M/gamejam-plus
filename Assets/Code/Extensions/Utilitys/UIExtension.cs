using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum Orientation { Horizontal, Vertical }

    public static class UIExtension
    {
        public static Vector2 GetDirection(this Image image)
        {
            switch (image.fillMethod)
            {
                case Image.FillMethod.Horizontal: return image.fillOrigin == 0 ? Vector2.left : Vector2.right;
                case Image.FillMethod.Vertical: return image.fillOrigin == 0 ? Vector2.down : Vector2.up;
                default: return Vector2.zero;
            }
        }

        public static Vector2 GetOrientation(this Orientation orientation)
        {
            switch(orientation)
            {
                case Orientation.Horizontal: return Vector2.right;
                case Orientation.Vertical: return Vector2.up;
                default: return Vector2.zero;
            }
        }
    }
}