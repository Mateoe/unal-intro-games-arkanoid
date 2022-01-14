using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{  

   [SerializeField]
    private PowerUp _powerUp;
     
   private void OnCollisionEnter2D(Collision2D other)
   {
      if (other.transform.TryGetComponent<Ball>(out Ball ball))
      {
          ArkanoidEvent.OnBallReachDeadZoneEvent?.Invoke(ball);
      }
   }

   private void OnTriggerEnter2D(Collider2D collider) {
        PowerUpData powerUpCollision;
        if (!collider.TryGetComponent(out powerUpCollision)) {
            return;
        }
        powerUpCollision.DestroyPowerUp();
        _powerUp.IsOut(powerUpCollision.GetPowerUpId());
    }


}
