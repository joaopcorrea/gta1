using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PersonMovementBehavior movement;

    private Vector2 direction;
    private Vector3 lookAt;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PersonMovementBehavior>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        CalcRotation();
        movement.Walk(direction, lookAt);
    }

    void CalcRotation()
    {
        //Rotation
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            lookAt = hit.point;
            lookAt.y = transform.position.y;
        }
    }
}
