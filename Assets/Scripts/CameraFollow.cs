using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // Цель (Player)
    private Vector3 offset;         // Смещение камеры

    void Start()
    {
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
    }
}
