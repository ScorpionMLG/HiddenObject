using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] Sprite _defaultSprite;
    [SerializeField] Sprite _overlappedSprite;
    private SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    private List<Transform> _overlap = new();
    private List<Transform> _relatedRopes = new();
    private List<Transform> _attachedKnots;
    private bool _isTransformUpdating;
    private const float NORMALIZE_SIZE = 0.055f;
    private void Update()
    {
        if (_isTransformUpdating)
        {
            transform.position = (_attachedKnots[1].position - _attachedKnots[0].position) / 2 + _attachedKnots[0].position + Vector3.forward / 10;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _attachedKnots[1].position - _attachedKnots[0].position);
            transform.Rotate(Vector3.forward, 90);
            transform.localScale = new Vector3(Vector2.Distance(_attachedKnots[1].position, _attachedKnots[0].position) * NORMALIZE_SIZE, 1, 1);
        }
    }
    private void OnEnable()
    {
        EventManager.RopesInitialized += UpdateRopeColor;
        EventManager.KnotPosUpdated += UpdateRopeColor;
        EventManager.KnotGrabbed += UpdateRopeTransform;
        EventManager.KnotReleased += StopUpdateRopeTransform;
    }
    private void OnDisable()
    {
        EventManager.RopesInitialized -= UpdateRopeColor;
        EventManager.KnotPosUpdated -= UpdateRopeColor;
        EventManager.KnotGrabbed -= UpdateRopeTransform;
        EventManager.KnotReleased -= StopUpdateRopeTransform;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Rope") && !_relatedRopes.Contains(collider.transform))
        {
            _overlap.Add(collider.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Rope") && !_relatedRopes.Contains(collider.transform)) _overlap.Remove(collider.transform);
    }
    private void UpdateRopeColor()
    {
        _spriteRenderer.sprite = _overlap.Count == 0 ? _defaultSprite : _overlappedSprite;
    }
    private void UpdateRopeTransform()
    {
        _isTransformUpdating = true;
    }
    private void StopUpdateRopeTransform()
    {
        _isTransformUpdating = false;
    }
    public int GetOverlapRopesCount()
    {
        return _overlap.Count;
    }
    public void SetAttachedKnots(List<Transform> knots)
    {
        _attachedKnots = knots;
    }
    public void AddRelatedRopes(List<Transform> ropes)
    {
        foreach (Transform rope in ropes) if (!_relatedRopes.Contains(rope)) _relatedRopes.Add(rope);
    }
}
