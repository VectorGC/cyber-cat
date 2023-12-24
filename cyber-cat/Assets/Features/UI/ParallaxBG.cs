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

        var posX = Mathf.Lerp(transform.position.x, _startPos.x + (pz.x * _parallaxEffect), _speed * Time.deltaTime);
        var posY = Mathf.Lerp(transform.position.y, _startPos.y + (pz.y * _parallaxEffect), _speed * Time.deltaTime);

        transform.position = new Vector3(posX, posY);
    }
}