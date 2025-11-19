using UnityEngine;
using UnityEditor;
using System;

public class CollisionTesterComponent : MonoBehaviour
{
    [SerializeField] SphereCollider sphereCollider = null;

    public Action<bool, GameObject> onCollisionTrigger = null;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    

    public void AddSphereCollider()
    {
        if (sphereCollider)
        {
            sphereCollider.isTrigger = true;
            return;
        }
        sphereCollider = gameObject.GetComponent<SphereCollider>();
        if (sphereCollider)
        {
            sphereCollider.isTrigger = true;
            return;
        }
        sphereCollider = gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (!sphereCollider) return;

        Debug.Log("Enter");
        onCollisionTrigger?.Invoke(true, _other.gameObject);
    }

    private void OnTriggerExit(Collider _other)
    {
        if (!sphereCollider) return;

        Debug.Log("Exit");
        onCollisionTrigger?.Invoke(false, _other.gameObject);
    }

    private void OnDrawGizmos()
    {
        if(sphereCollider)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(sphereCollider.transform.position, sphereCollider.radius);
        }
        Gizmos.color = Color.white;
    }
}
