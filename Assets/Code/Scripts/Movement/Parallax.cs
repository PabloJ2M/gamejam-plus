using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallax : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float _velocity;
    [SerializeField, Range(0, 10)] private float _speed = 1;
    private Vector2 _direction = Vector2.right;
    private Material _material;
    private float _multiply = 1;

    public float SpeedMultiply { set => _multiply = value; }

    private void Awake() => _material = GetComponent<SpriteRenderer>().material;

    private void FixedUpdate()
    {
        float velocity = _speed * _velocity * _multiply * Time.fixedDeltaTime;
        _material.mainTextureOffset += velocity * _direction;
    }
}