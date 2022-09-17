using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
  [SerializeField] private float lifeTime;
  [SerializeField] private float bulletSpeed;

  private Vector3 shootDir;
  

  // Start is called before the first frame update
  void Start()
  {
    Destroy(gameObject, lifeTime);
  }

  // Update is called once per frame
  void Update()
  {
    transform.position += shootDir * bulletSpeed * Time.deltaTime;
  }

  internal void Setup(Vector3 shootDir)
  {
    this.shootDir = shootDir;
  }


}
