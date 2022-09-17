using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform pfBullet;
    [SerializeField] private float fireRate;

    private float fireTimer;

    Transform endpointPosition;

    // Start is called before the first frame update
    void Start()
    {
        endpointPosition = transform.Find("EndPointPosition");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot(Vector3 shootPosition)
    {
        fireTimer = Time.time + fireRate;

        shootPosition.y = transform.position.y;
        Transform bulletTransform = Instantiate(pfBullet, endpointPosition.position, Quaternion.identity);
        Vector3 shootDir = (shootPosition - endpointPosition.position).normalized;
        bulletTransform.GetComponent<BulletBehavior>().Setup(shootDir);
        bulletTransform.rotation = transform.rotation;
    }
    
    public bool CanShoot()
    {
        return Time.time > fireTimer;
    }
}

