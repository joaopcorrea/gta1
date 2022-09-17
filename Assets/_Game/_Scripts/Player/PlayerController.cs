using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject graphic;
    private PersonMovementBehavior movement;

    private Vector2 direction;
    private Vector3 lookAt;

    private Camera cam;
    private CameraController cameraController;
    private GunController gunController;

    private CarBehavior currentCar;

    private bool requestEnterCar;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PersonMovementBehavior>();
        gunController = GetComponentInChildren<GunController>();
        cam = Camera.main;
        cameraController = cam.GetComponent<CameraController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        requestEnterCar = Input.GetKeyDown(KeyCode.F);

        if (currentCar == null)
        {
            CalcRotation();
            movement.Walk(direction, lookAt);
            Attack();
        }
        else
        {
            currentCar.Drive(direction.y);
            currentCar.Turn(direction.x);

            if (requestEnterCar)
            {
                ExitCar();
            }
        }
        
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

    void Attack()
    {
        if (Input.GetMouseButton(0) && gunController.CanShoot())
        {
            gunController.Shoot(lookAt);
        }
    }

    void EnterCar(CarBehavior car)
    {
        currentCar = car;
        cameraController.target = car.transform;
        graphic.SetActive(false);
    }

    void ExitCar()
    {
        cameraController.target = transform;
        graphic.SetActive(true);
        transform.position = currentCar.driverDoor.position;
        currentCar = null;
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (currentCar == null && requestEnterCar)
        {
            CarBehavior car = collision.collider.GetComponent<CarBehavior>();
            if (car != null)
            {
                EnterCar(car);
            }
        }
    }
}
