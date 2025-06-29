using UnityEngine;

public class Plane2Controller : MonoBehaviour
{
    public GameObject Route1;
    public GameObject Route2;

    void Start()
    {
        Vector3 routePostion1 = new Vector3(Random.Range(0, 2) * 60 - 30, 0, 0);
        Route1.transform.localPosition = routePostion1;
        routePostion1.x = -routePostion1.x;
        Route2.transform.localPosition = routePostion1;
    }
}
