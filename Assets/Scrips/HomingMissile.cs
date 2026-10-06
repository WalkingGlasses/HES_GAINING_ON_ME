using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [Header("Homing")]
    public float speed = 4f;
    public float rotationSpeed = 3f;

    [Header("Hit Detection")]
    public float hitDistance = 1f;
    
    [Header("Lifetime")]
    public float missileLifetime = 5f;

    private Transform player;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }
        Vector3 direction = player.position - transform.position;

        //We ignore the 'Y' realm in the lower mortal realms even though this venerable immortal one has seen beyond Mount Tai,
        //for the sake of the lowly mortals with no spirit root and no jade we must lower ourselves to their level
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation =
                Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        transform.position +=
            transform.forward * speed * Time.deltaTime;
        
        float distance =
            Vector3.Distance(transform.position, player.position);

        if (distance <= hitDistance)
        {
            player.GetComponent<PlayerController>().TakeHit();

            Destroy(gameObject);
        }
        
        //you should destroy yourself NOW
        missileLifetime -= Time.deltaTime;
        if ( missileLifetime < 0 )
        {
            Destroy(gameObject);
        }
        
    }
}