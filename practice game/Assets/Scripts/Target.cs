using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
   [SerializeField] float health;
   [SerializeField] float dmg;
   [SerializeField] float range; 
   [SerializeField] float fireRate;
   [SerializeField] float speed;

   public void TakeDamage(float amount)
   {
       health -= amount;
       if (health <= 0f)
       {
           Die();
       }
   }
   
   void Die()
   {
       Destroy(gameObject);
   }

}
