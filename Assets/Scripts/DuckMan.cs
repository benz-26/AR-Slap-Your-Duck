using UnityEngine;

public class DuckMan : MonoBehaviour
{
    [SerializeField] AudioSource audioSource; // Audio source to play the sound
    [SerializeField] AudioClip[] pemanisSound; // Sound to play when the duck is destroyed

    private void OnTriggerEnter(Collider other)
    {
        int randomIndex = Random.Range(0, pemanisSound.Length); // Get a random index

        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(pemanisSound[randomIndex]); // Play the sound
            Destroy(this.gameObject); // Destroy the duck
            GameManager.instance.UpdateScore();

            Debug.Log("Duck Destroyed");
        }
        else if (other.CompareTag("Border"))
        {
            audioSource.PlayOneShot(pemanisSound[0]);
            GameManager.instance.UpdateTrigger();
            Debug.Log("Border Triggered");
        }
    }
}
