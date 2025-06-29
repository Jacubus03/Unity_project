using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // other.gameObject.transform.position = new Vector3(0, 10, 0);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
