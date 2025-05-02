using NUnit.Framework;
using UnityEngine;

public class mainScripts : MonoBehaviour
{

}

public class Moveble : MonoBehaviour
{
    /// <summary>
    /// Передвижение по X 
    /// </summary>
    /// <param name="t"></param>
    /// <param name="sens"></param>
    public void MoveX2D(Transform t, float sens)
    {
        Vector2 now = t.position;
        now.x = now.x + sens;
        t.position = now;
    }
    /// <summary>
    /// Передвижение по Y
    /// </summary>
    /// <param name="t"></param>
    /// <param name="sens"></param>
    public void MoveY2D(Transform t, float sens)
    {
        Vector2 now = t.position;
        now.y = now.y + sens;
        t.position = now;
    }
    public void MoveZ(Transform t, float sens)
    {
        Vector3 now = t.position;
        now.z = now.z + sens;
        t.position = now;
    }
    /// <summary>
    /// Передвижение по 2D полю
    /// </summary>
    /// <param name="t"></param>
    /// <param name="sens"></param>
    public void Move2D(Transform t, Vector2 sens)
    {
        Vector3 nowPosition = t.position;
        nowPosition.x = nowPosition.x + sens.x;
        nowPosition.y = nowPosition.y + sens.y;
        t.position = nowPosition;
    }
    public void Move3D(Transform t, Vector3 sens)
    {
        Vector3 nowPosition = t.position;
        t.position = t.position + sens;
    }
}
public class sCamera
{
    public void OrthoZoom(Camera camera, float zoom)
    {
        camera.orthographicSize += zoom;
    }
}
