using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //[SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            /*collision.GetComponent<Health>()?.TakeDamage(damage);*/
            //Destroy(collision.gameObject);
            Debug.Log("Trung r e nhé!");
    }
}