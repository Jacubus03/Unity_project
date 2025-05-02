using UnityEngine;

public class PlayerMovementPC : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Time.deltaTime, 0, 0));
    }
}