using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not assigned in CameraFollow!");
            return;
        }
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, target.position.z + offset.z);
            transform.position = targetPosition;
        }
    }
}
