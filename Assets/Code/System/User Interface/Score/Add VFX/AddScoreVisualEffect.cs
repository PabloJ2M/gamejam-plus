using UnityEngine;
using TMPro;

public class AddScoreVisualEffect : MonoBehaviour
{
    [Header("Static Parameters")]
    [SerializeField] private int _scoreAdded;
    
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI _scoreAddedTextLabel;
    [SerializeField] private RectTransform _rectTransform;

    [Header("Variable Parameters")]
    [SerializeField] private TextMeshProUGUI _endPositionReference;
    [SerializeField] private float _lifetimeSeconds;
    private float _endAlpha;
    private float _startAlpha;
    private float _endScale;
    private float _startScale;
    private Quaternion _endRotation;
    private Quaternion _startRotation;
    private Vector3 _endPosition;
    private Vector3 _startPosition;
    private float _timer;

    [Header("Animations")]
    [SerializeField] private AnimationCurve _alphaVariation;
    [SerializeField] private AnimationCurve _rotationVariation;
    [SerializeField] private AnimationCurve _scaleVariation;
    [SerializeField] private AnimationCurve _positionVariation;

    void Awake()
    {
        if(_scoreAddedTextLabel == null) _scoreAddedTextLabel = GetComponent<TextMeshProUGUI>();
        if(_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        GetVariableParameters();
    }

    public void SetScoreAdded(int amount)
    {
        if(amount <= 0) return;

        _scoreAdded = amount;
        _scoreAddedTextLabel.text = $"+{_scoreAdded}";
    } 

    public void GetVariableParameters()
    {
        if(_endPositionReference == null) return;

        _endAlpha = _endPositionReference.alpha;
        _endScale = _endPositionReference.transform.localScale.x * transform.localScale.x;
        _endRotation = _endPositionReference.transform.rotation;
        _endPosition = _endPositionReference.transform.position;

        _endPositionReference.gameObject.SetActive(false);
    
        _startAlpha = _scoreAddedTextLabel.alpha;
        _startScale = transform.localScale.x;
        _startRotation = transform.rotation;
        _startPosition = transform.position;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        UpdateAlpha();
        UpdateRotation();
        UpdateScale();
        UpdatePosition();

        if(_timer >= _lifetimeSeconds) Destroy(gameObject);
    }

    private void UpdateAlpha()
    {
        float parameter = _alphaVariation.Evaluate(_timer / _lifetimeSeconds);
    
        float actualAlpha = Mathf.Lerp(_startAlpha, _endAlpha, parameter);

        _scoreAddedTextLabel.alpha = actualAlpha;
    }

    private void UpdateRotation()
    {
        float parameter = _rotationVariation.Evaluate(_timer / _lifetimeSeconds);
    
        Quaternion actualRotation = Quaternion.Slerp(_startRotation, _endRotation, parameter);

        transform.rotation = actualRotation;
    }

    private void UpdateScale()
    {
        float parameter = _scaleVariation.Evaluate(_timer / _lifetimeSeconds);
    
        float actualScale = Mathf.Lerp(_startScale, _endScale, parameter);

        transform.localScale = Vector3.one * actualScale;
    }

    private void UpdatePosition()
    {
        float parameter = _positionVariation.Evaluate(_timer / _lifetimeSeconds);

        Vector3 actualPosition = Vector3.Lerp(_startPosition, _endPosition, parameter);

        transform.position = actualPosition;
    }
}
