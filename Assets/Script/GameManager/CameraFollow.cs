using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;

    private Transform targetPlayerTransform;

    internal void SetTarget(Transform transform)
    {
        throw new NotImplementedException();
    }

    void Awake()
    {
        FindPlayerTarget();
    }

    private void FindPlayerTarget()
    {
        if (Player.Instance != null)
        {
            targetPlayerTransform = Player.Instance.transform;
        }
        else
        {
            Debug.LogWarning("CameraFollow: Player Instance not found yet.");
        }
    }

    void LateUpdate()
    {
        if (targetPlayerTransform == null)
        {
            FindPlayerTarget();
            if (targetPlayerTransform == null)
            {
                return;
            }
        }

        Vector3 newPos = new Vector3(targetPlayerTransform.position.x, targetPlayerTransform.position.y + yOffset, transform.position.z);

        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}