using UnityEngine;

public class CardFromConveyorAnimation : MonoBehaviour
{
    private Transform _boxHeadTransform;   
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;
    private Vector3 _initialScale;
    private Transform _initialParent;

    [Header("Animation Settings")]
    public float flightDuration = 1.5f;
    public float horizontalOffset = 2f;
    public AnimationCurve flightCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 1, 1, 0.8f);
    public AnimationCurve rotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 180f);

    [Header("Buffer Stack Settings")]
    public Vector3 cardOffset = new Vector3(0.5f, 0, 0);
    public Vector3 cardRotationOffset = Vector3.left;
    public bool useLocalOffset = true;

    private bool _isAnimating = false;
    private float _animationProgress = 0f;
    private int _targetBoxIndex = 0;


    public void SendCardToBoxAnimation(Box box)
    {
        _boxHeadTransform = box.GetBoxHeadTransform();

        if (_isAnimating || _boxHeadTransform == null) return;

        // Определяем индекс карты в буфере
        _targetBoxIndex = box.GetCardsInBoxCount();

        // Сохраняем начальные параметры
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialScale = transform.localScale;
        _initialParent = transform.parent;

        // Отключаем физику если есть
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // Отключаем коллайдеры на время анимации
        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        var collider2D = GetComponent<Collider2D>();
        if (collider2D != null)
        {
            collider2D.enabled = false;
        }

        _isAnimating = true;
        _animationProgress = 0f;
    }

    private void Update()
    {
        if (!_isAnimating) return;

        _animationProgress += Time.deltaTime / flightDuration;

        if (_animationProgress >= 1f)
        {
            CompleteAnimation();
            return;
        }

        AnimateCard();
    }

    private void AnimateCard()
    {
        float curveValue = flightCurve.Evaluate(_animationProgress);
        float scaleValue = scaleCurve.Evaluate(_animationProgress);
        float rotationValue = rotationCurve.Evaluate(_animationProgress);

        // Движение по синусоидальной траектории в 2D
        Vector3 currentPosition = Calculate2DPosition(_animationProgress);

        // Плавное вращение
        Quaternion targetRotation = _initialRotation * Quaternion.Euler(0, 0, rotationValue);

        transform.position = currentPosition;
        transform.rotation = Quaternion.Slerp(_initialRotation, targetRotation, curveValue);
        transform.localScale = _initialScale * scaleValue;
    }

    private Vector3 Calculate2DPosition(float progress)
    {
        Vector3 startPos = _initialPosition;
        Vector3 endPos = CalculateFinalPosition(); // Конечная позиция с учетом смещения

        // Синусоидальная траектория в 2D (вправо/влево)
        Vector3 direction = (endPos - startPos).normalized;
        Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0);

        float horizontalMovement = Mathf.Sin(progress * Mathf.PI) * -horizontalOffset;

        return Vector3.Lerp(startPos, endPos, progress) + perpendicular * horizontalMovement;
    }

    private Vector3 CalculateFinalPosition()
    {
        Vector3 basePosition = _boxHeadTransform.position == null ? Vector3.forward : _boxHeadTransform.position;

        if (useLocalOffset)
        {
            // Локальное смещение относительно буфера
            return basePosition + _boxHeadTransform.TransformDirection(cardOffset * _targetBoxIndex);
        }
        else
        {
            // Глобальное смещение
            return basePosition + cardOffset * _targetBoxIndex;
        }
    }

    private Quaternion CalculateFinalRotation()
    {
        if (useLocalOffset)
        {
            // Сохраняем вращение буфера + дополнительное смещение
            return _boxHeadTransform.rotation * Quaternion.Euler(cardRotationOffset);
        }
        else
        {
            // Только дополнительное смещение вращения
            return Quaternion.Euler(cardRotationOffset);
        }
    }

    private void CompleteAnimation()
    {
        _isAnimating = false;

        // Устанавливаем финальную позицию с учетом смещения
        transform.position = CalculateFinalPosition();
        transform.rotation = CalculateFinalRotation();
        transform.localScale = _initialScale * scaleCurve.Evaluate(1f);

        // Включаем коллайдеры обратно
        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = true;
        }

        var collider2D = GetComponent<Collider2D>();
        if (collider2D != null)
        {
            collider2D.enabled = true;
        }

        
    }
}

