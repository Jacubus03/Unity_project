using UnityEngine;

public class Crosshair : MonoBehaviour
{
    void OnGUI()
    {
        float size = 6f;
        float x = (Screen.width - size) / 2;
        float y = (Screen.height - size) / 2;
        GUI.Box(new Rect(x, y, size, size), "");
    }
}
