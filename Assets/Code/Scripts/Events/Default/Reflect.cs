using UnityEngine;

[RequireComponent(typeof(ConstantForce2D))]
public class Reflect : MonoBehaviour
{
    private ConstantForce2D _constant;

    private void Awake() => _constant = GetComponent<ConstantForce2D>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        _constant.force = Vector2.Reflect(_constant.force, contact.normal);
    }
}