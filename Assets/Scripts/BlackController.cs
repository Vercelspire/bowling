using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackController : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody rb;

    public AudioClip collisionSound;
    public AudioClip backgroundMusic;
    private AudioSource audioSource;

    public float collisionVolume = 1f; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Freeze all rotation to prevent camera tilt
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Setup AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.spatialBlend = 0f; 
            if (backgroundMusic != null)
            {
                audioSource.clip = backgroundMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("AudioSource missing on " + gameObject.name);
        }
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ) * moveSpeed * Time.fixedDeltaTime;

        // Use MovePosition for physics movement without rotation
        rb.MovePosition(rb.position + movement);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectible"))
        {
            // Play collision sound 
            if (audioSource != null && collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound, collisionVolume);
            }

            ScoreManager.Instance.AddScore(1);
            Destroy(other.gameObject);
        }
    }
}
