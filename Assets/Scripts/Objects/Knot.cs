using UnityEngine;

public class Knot : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _hoverSprite;
    private SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    private bool _isGrabbed;
    private void Update()
    {
        if (_isGrabbed)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
        }
    }
    private void OnMouseEnter()
    {
        _spriteRenderer.sprite = _hoverSprite;
    }
    private void OnMouseExit()
    {
        _spriteRenderer.sprite = _defaultSprite;
    }

    private void OnMouseDown()
    {
        _isGrabbed = true;
        EventManager.OnKnotGrab();
    }
    private void OnMouseUp()
    {
        if (_isGrabbed)
        {
            EventManager.OnKnotRelease();
            EventManager.OnKnotPosUpdate();
        }
        _isGrabbed = false;
    }
}
