using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Player _player;
    private Vector3 _offset;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _offset = transform.position - _player.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}
