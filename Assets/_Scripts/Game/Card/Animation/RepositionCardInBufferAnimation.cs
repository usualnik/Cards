using UnityEngine;

public class RepositionCardInBufferAnimation : MonoBehaviour
{

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    [Header("Animation Settings")]
    public float animationDuration = 0.5f;
    public float jumpHeight = 1.5f;
    public float horizontalOffset = 1f;
    public AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private bool _isAnimating = false;
    private float _animationProgress = 0f;
    private int _cardIndex = 0;

    public void AnimateToPosition(Vector3 targetPosition, Quaternion targetRotation, int index)
    {
        _targetPosition = targetPosition;
        _targetRotation = targetRotation;
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _cardIndex = index;

        _isAnimating = true;
        _animationProgress = 0f;
    }

    private void Update()
    {
        if (!_isAnimating) return;

        _animationProgress += Time.deltaTime / animationDuration;

        if (_animationProgress >= 1f)
        {
            CompleteAnimation();
            return;
        }

        AnimateMovement();
    }

    private void AnimateMovement()
    {
        float curveValue = movementCurve.Evaluate(_animationProgress);
        float jumpValue = jumpCurve.Evaluate(_animationProgress);

        // Базовое движение
        Vector3 basePosition = Vector3.Lerp(_initialPosition, _targetPosition, curveValue);

        // Добавляем прыжок по дуге
        float jump = Mathf.Sin(_animationProgress * Mathf.PI) * jumpHeight;

        // Горизонтальное смещение (синусоидальное движение)
        float horizontal = Mathf.Sin(_animationProgress * Mathf.PI) * horizontalOffset;

        // Финальная позиция с учетом всех эффектов
        Vector3 finalPosition = basePosition + Vector3.up * jump + Vector3.right * horizontal;

        transform.position = finalPosition;       
    }

    private void CompleteAnimation()
    {
        _isAnimating = false;
        transform.position = _targetPosition;       
    }

    public bool IsAnimating() => _isAnimating;
}
