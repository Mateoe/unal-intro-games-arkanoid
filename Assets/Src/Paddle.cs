using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _movementLimit = 7;
    private Vector3 _targetPosition;
    [SerializeField]
    private PowerUp _powerUp;

    private Camera _cam;
    private Camera Camera
    {
        get
        {
            if (_cam == null)
            {
                _cam = Camera.main;
            }
            return _cam;
        }
    }

    void Start()
    {

    }

    void Update()
    {
        _targetPosition.x = Camera.ScreenToWorldPoint(Input.mousePosition).x;
        _targetPosition.x = Mathf.Clamp(_targetPosition.x, -_movementLimit, _movementLimit);
        _targetPosition.y = this.transform.position.y;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime*_speed);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        PowerUpData powerUpCollision;
        if (!collider.TryGetComponent(out powerUpCollision)) {
            return;
        }
        powerUpCollision.DestroyPowerUp();
        _powerUp.TouchPaddle(powerUpCollision.GetPowerUpId());
    }

}
