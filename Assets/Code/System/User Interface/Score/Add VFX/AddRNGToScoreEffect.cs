using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AddRNGToScoreEffect : MonoBehaviour
{
    [SerializeField] private float _maxDisplacement;
    [SerializeField] private float _minRotationDiff;
    [SerializeField] private float _maxRotationDiff;

    public void Awake()
    {
        AddPostionDisplacement();
        AddRotationDisplacement();
    }

    private void AddPostionDisplacement()
    {
        Vector3 displacementDirection = Random.onUnitSphere;
        float displacement = Random.Range(0f, _maxDisplacement);

        displacementDirection.z = 0f;
        displacementDirection.Normalize();

        transform.position += displacementDirection * displacement;
    }

    private void AddRotationDisplacement()
    {
        float displacement = Random.Range(_minRotationDiff, _maxRotationDiff);

        transform.rotation *= Quaternion.Euler(0f, 0f, displacement);
    }
}
