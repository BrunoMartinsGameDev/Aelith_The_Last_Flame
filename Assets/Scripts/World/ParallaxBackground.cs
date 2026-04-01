using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxFactor;

    private Camera  _cam;
    private float   _startX;
    private float   _startY;
    private Vector2 _camStartPos;

    void Start()
    {
        _cam         = Camera.main;
        _startX      = transform.position.x;
        _startY      = transform.position.y;
        _camStartPos = _cam.transform.position;
    }

    void LateUpdate()
    {
        float camDeltaX = _cam.transform.position.x - _camStartPos.x;
        float camDeltaY = _cam.transform.position.y - _camStartPos.y;
        transform.position = new Vector3(
            _startX + camDeltaX * parallaxFactor,
            _startY + camDeltaY,
            transform.position.z);
    }
}