using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
    [SerializeField] private float _parallaxEffect = 100;
    [SerializeField] private float _speed = 3;

    private Vector2 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        var pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        var rect = ((RectTransform) transform).rect;
        var maxXShift = rect.width / 2f;
        var maxYShift = rect.height / 2f;

        var shiftX = Mathf.Clamp(_startPos.x + pz.x * _parallaxEffect, 0, maxXShift);
        var shiftY = Mathf.Clamp(_startPos.y + pz.y * _parallaxEffect, 0, maxYShift);

        var position = transform.position;
        var posX = Mathf.Lerp(position.x, shiftX, _speed * Time.deltaTime);
        var posY = Mathf.Lerp(position.y, shiftY, _speed * Time.deltaTime);

        transform.position = new Vector3(posX, posY);
    }
}