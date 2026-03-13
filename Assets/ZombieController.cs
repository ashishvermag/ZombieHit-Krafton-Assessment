using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 2.0f;
    
    [Header("Crash Settings")]
    public float crashDistance = 1.8f; // How close the car needs to be to trigger the crash
    
    private Rigidbody[] ragdollBones;
    private Animator animator;
    private bool isDead = false;
    private Transform carTransform;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null) animator = GetComponentInChildren<Animator>();

        ragdollBones = GetComponentsInChildren<Rigidbody>();

        // BULLETPROOF FIX: Find the car automatically by its script! No tags needed.
        PrometeoCarController car = FindObjectOfType<PrometeoCarController>();
        if (car != null)
        {
            carTransform = car.transform;
        }

        // Set to "Alive" mode
        ToggleRagdoll(false);
    }

    void Update()
    {
        if (!isDead)
        {
            // Walk forward slowly
            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);

            // ---------------------------------------------------------
            // THE BULLETPROOF WALL FIX (Pure Math!)
            // If the zombie walks further than 22 meters from the center (0,0,0)
            if (Vector3.Distance(Vector3.zero, transform.position) > 50f)
            {
                // Instantly point the zombie back toward the center of the arena
                Vector3 directionToCenter = Vector3.zero - transform.position;
                directionToCenter.y = 0; // Keep it perfectly flat so it doesn't tilt up/down
                
                transform.rotation = Quaternion.LookRotation(directionToCenter);
            }
            // ---------------------------------------------------------

            // Check distance to the car for the crash
            if (carTransform != null)
            {
                float distance = Vector3.Distance(transform.position, carTransform.position);
                if (distance <= crashDistance)
                {
                    CrashIntoZombie();
                }
            }
        }
    }

    // void OnTriggerEnter(Collider other)
    // {
    //     // Instead of Tags, we just check if the object's name contains the word "Wall"
    //     Debug.Log("Maa ki lund isski" );
    //     if (other.gameObject.name.Contains("Wall") && !isDead)
    //     {
    //         Debug.Log("Maa ki chut isski" );
    //         transform.Rotate(0, 180, 0);
    //         // Spin 180 degrees to walk the other way!
    //     }
    // }
    void CrashIntoZombie()
    {
        isDead = true;
        GameManager.Instance.ZombieHit();
        // Turn on the ragdoll!
        ToggleRagdoll(true);

        // Push the zombie away from the car
        Rigidbody mainBone = ragdollBones[0];
        
        Vector3 pushDirection = transform.position - carTransform.position;
        pushDirection.y = 0.5f; // Give it a slight upward lift so it flies nicely
        pushDirection = pushDirection.normalized;

        mainBone.AddForce(pushDirection * 50f, ForceMode.Impulse);
    }

    void ToggleRagdoll(bool isRagdoll)
    {
        if (animator != null) animator.enabled = !isRagdoll;

        foreach (Rigidbody bone in ragdollBones)
        {
            bone.isKinematic = !isRagdoll; 
            
            Collider boneCollider = bone.GetComponent<Collider>();
            if (boneCollider != null)
            {
                // Turn bone colliders ON when dead so they bounce on the ground!
                boneCollider.enabled = isRagdoll;
            }
        }
    }
}