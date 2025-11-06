
using UnityEngine;

public class Pointer : MonoBehaviour {

    GameObject _source;
    Vector3 _destination;
    public void Bind(GameObject source, Vector3 destination)
    {
        _source = source;   
        _destination = destination;   
    }

    void Update()
    {
        transform.position = ClampPointToCircleXZ(_destination, _source.transform.position, 5f);
        Vector3 flatDirection = new Vector3(_destination.x - _source.transform.position.x, 1f, _destination.z - _source.transform.position.z);
        flatDirection.Normalize();
        float horizontalAngle = Mathf.Atan2(flatDirection.x, flatDirection.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, horizontalAngle + 90, 0);
    }
    
    public static Vector3 ClampPointToCircleXZ(Vector3 point, Vector3 pivot, float radius)
    {
        Vector2 pointXZ = new Vector2(point.x, point.z);
        Vector2 pivotXZ = new Vector2(pivot.x, pivot.z);
        float distance = Vector2.Distance(pointXZ, pivotXZ);

        if (distance > radius)
        {
            Vector2 direction = (pointXZ - pivotXZ).normalized;
            Vector2 clampedXZ = pivotXZ + direction * radius;
            return new Vector3(clampedXZ.x, point.y, clampedXZ.y);
        }

        return point;
    }



}