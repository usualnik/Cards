using System;
using UnityEngine;

public class CardFromDumperAnimation : MonoBehaviour
{
    private Transform _bufferHeadTransform;
    private Vector3 _bufferHeadOffset = new Vector3(0.2f, 0, 0);
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
    private int _targetBufferIndex = 0;

    private void Start()
    {
        _bufferHeadTransform = FindAnyObjectByType<BufferHead>().transform;
    }

    public void SendCardToBufferAnimation()
    {
        if (_isAnimating || _bufferHeadTransform == null) return;

        // ���������� ������ ����� � ������
        _targetBufferIndex = Buffer.Instance.GetBufferCardsListCount();

        // ��������� ��������� ���������
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
        _initialScale = transform.localScale;
        _initialParent = transform.parent;

        // ��������� ������ ���� ����
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        // ��������� ���������� �� ����� ��������
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

        // �������� �� �������������� ���������� � 2D
        Vector3 currentPosition = Calculate2DPosition(_animationProgress);

        // ������� ��������
        Quaternion targetRotation = _initialRotation * Quaternion.Euler(0, 0, rotationValue);

        transform.position = currentPosition;
        transform.rotation = Quaternion.Slerp(_initialRotation, targetRotation, curveValue);
        transform.localScale = _initialScale * scaleValue;
    }

    private Vector3 Calculate2DPosition(float progress)
    {
        Vector3 startPos = _initialPosition;
        Vector3 endPos = CalculateFinalPosition(); // �������� ������� � ������ ��������

        // �������������� ���������� � 2D (������/�����)
        Vector3 direction = (endPos - startPos).normalized;
        Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0);

        float horizontalMovement = Mathf.Sin(progress * Mathf.PI) * -horizontalOffset;

        return Vector3.Lerp(startPos, endPos, progress) + perpendicular * horizontalMovement;
    }

    private Vector3 CalculateFinalPosition()
    {
        Vector3 basePosition = _bufferHeadTransform.position - _bufferHeadOffset;

        if (useLocalOffset)
        {
            // ��������� �������� ������������ ������
            return basePosition + _bufferHeadTransform.TransformDirection(cardOffset * _targetBufferIndex);
        }
        else
        {
            // ���������� ��������
            return basePosition + cardOffset * _targetBufferIndex;
        }
    }

    private Quaternion CalculateFinalRotation()
    {
        if (useLocalOffset)
        {
            // ��������� �������� ������ + �������������� ��������
            return _bufferHeadTransform.rotation * Quaternion.Euler(cardRotationOffset);
        }
        else
        {
            // ������ �������������� �������� ��������
            return Quaternion.Euler(cardRotationOffset);
        }
    }

    private void CompleteAnimation()
    {
        _isAnimating = false;

        // ������������� ��������� ������� � ������ ��������
        transform.position = CalculateFinalPosition();
        transform.rotation = CalculateFinalRotation();
        transform.localScale = _initialScale * scaleCurve.Evaluate(1f);

        // �������� ���������� �������
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


        Debug.Log($"Card animation completed! Position in buffer: {_targetBufferIndex}");
    }

    //// �������������� �������: �������� � ������� ������� � ������ (���� ������ ��� ����������)
    //private Vector3 CalculateDynamicFinalPosition()
    //{
    //    Vector3 basePosition = _bufferHeadTransform.position + _bufferHeadOffset;
    //    int currentBufferCount = Buffer.Instance.GetBufferCardsListCount();

    //    if (useLocalOffset)
    //    {
    //        return basePosition + _bufferHeadTransform.TransformDirection(cardOffset * currentBufferCount);
    //    }
    //    else
    //    {
    //        return basePosition + cardOffset * currentBufferCount;
    //    }
    //}

    //// ����� ��� ���������� ������� ��� ����������� � ������ ����
    //public static void UpdateBufferCardsPositions(Transform bufferHead, Vector3 offset, bool useLocal = true)
    //{
    //    var bufferCards = Buffer.Instance.GetBufferCardsList();

    //    for (int i = 0; i < bufferCards.Count; i++)
    //    {
    //        if (bufferCards[i] != null)
    //        {
    //            Vector3 cardPosition = bufferHead.position + offset;

    //            if (useLocal)
    //            {
    //                cardPosition += bufferHead.TransformDirection(offset * i);
    //            }
    //            else
    //            {
    //                cardPosition += offset * i;
    //            }

    //            bufferCards[i].transform.position = cardPosition;
    //        }
    //    }
    //}
}