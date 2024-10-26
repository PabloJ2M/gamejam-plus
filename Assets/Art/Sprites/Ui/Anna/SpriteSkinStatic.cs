namespace UnityEngine.U2D.Animation
{
    [RequireComponent(typeof(SpriteSkin))]
    public class SpriteSkinStatic : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private SpriteSkin _skin;

        private Transform _bone;
        private Sprite _default;
        private Vector2 _offset;

        private void Awake() { _renderer = GetComponent<SpriteRenderer>(); _skin = GetComponent<SpriteSkin>(); }
        private void Start() { _default = _renderer.sprite; _bone = _skin.boneTransforms[0]; _offset = -(_bone.position - transform.position); }

        private void Update()
        {
            if (_renderer.sprite == _default) return;
            
            //set transform
            Quaternion target = _bone.rotation * Quaternion.Euler(0, 0, -90);
            transform.position = _bone.position + target * (Vector3)_offset;
            
            //set rotation
            transform.rotation = target;
        }
    }
}