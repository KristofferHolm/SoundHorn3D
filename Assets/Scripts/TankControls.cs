using UnityEngine;

public class TankControls : MonoBehaviour
{
    // public Variables
    public float MovementSpeed;
    public float DuckingSpeed;
    public float RotationSpeed;
    //private Vars
    float currentSpeed;
    float currentRotSpeed;
    bool ducking = false;
    private Rigidbody rigidbody;

    public Rigidbody Rigidbody
    {
        get
        {
            if(rigidbody != GetComponent<Rigidbody>())
                rigidbody = GetComponent<Rigidbody>();
            return rigidbody;
        }
    }

    private void Start()
    {
        InputManager.Instance.Input_Duck += Duck;
    }

    private void Duck()
    {
        ducking = !ducking;
    }


    void Update()
    {
        currentSpeed = (ducking ? DuckingSpeed : MovementSpeed) * Time.deltaTime;
        currentRotSpeed = RotationSpeed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(InputManager.Instance.CameraMovementX * currentRotSpeed, transform.up);
        //Rigidbody.velocity = transform.rotation * InputManager.Instance.TankMovement()  * currentSpeed;
    }
}
