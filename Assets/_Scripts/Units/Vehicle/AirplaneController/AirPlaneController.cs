using UnityEngine;

public class AirPlaneController : MonoBehaviour
{
    private Rigidbody rigid;
    
    private Vector3 input;

    [Header("Speed Properties")]
    [SerializeField]
    private float flyAcceleration   = 2500f;
    [SerializeField]
    private float flyDeacceleration = 3500f;

    [SerializeField]
    private float maxFlyForce = 1500f;
    private float flyForce;

    [Header("Maneuvering Properties")]
    [SerializeField]
    private float maneuverLerpSpeed   = 15f;
    [SerializeField]
    private float verticalTurnSpeed   = 90f;
    [SerializeField]
    private float horizontalTurnSpeed = 90f;
    [SerializeField]
    private float leanSpeed           = 135f;

    [SerializeField]
    private float maxLeanAngle = 25f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>(); 
    }

    private void Update()
    {
        // If we're holding down the Accelerate button then disable gravity, otherwise enable it.
        rigid.useGravity = Input.GetKey(KeyCode.W) ? false : true;
        // When flying reset the angularVelocity so that we won't get twitchy movement.
        rigid.angularVelocity = Input.GetKey(KeyCode.W) ? Vector3.zero : rigid.angularVelocity;
        
        SetInput();

        Accelerate();
        Maneuver();
    }

    private void SetInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        input = Vector3.Normalize(input);
    }

    private void Accelerate()
    {
        if (Input.GetKey(KeyCode.W))  flyForce.IncreaseValue(maxFlyForce, flyAcceleration * Time.deltaTime); 
        if (!Input.GetKey(KeyCode.W)) flyForce.DecreaseValue(flyDeacceleration * Time.deltaTime); 
    }

    private float xRotation, yRotation, zRotation;
    private void Maneuver()
    {
        // Important: When we aren't controlling anything, we want the transform/rigidbody to be able to rotate based on collisions.
        // that's why we set the x,y,z rotations to the transforms eulerangles, and not to custom input rotations (as seen below if block).
        if ((Mathf.Abs(input.x) == 0 && Mathf.Abs(input.z) == 0) && !Input.GetKey(KeyCode.Space))
        {
            xRotation = transform.eulerAngles.x;
            yRotation = transform.eulerAngles.y;
            zRotation = transform.eulerAngles.z;
            return;
        }

        // Increment/Decrement rotations based on input.
        xRotation += input.z * verticalTurnSpeed * Time.deltaTime;
        yRotation += input.x * horizontalTurnSpeed * Time.deltaTime;
        zRotation -= input.x * leanSpeed * Time.deltaTime;

        zRotation = Mathf.Clamp(zRotation, -maxLeanAngle, maxLeanAngle);

        var targetRotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        // Important: We lerp here to avoid snappy rotations (stuff like rotating 180 degress in 0.001 secs etc). 
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, maneuverLerpSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        var force = transform.forward * flyForce * Time.fixedDeltaTime;
        rigid.AddForce(force);
    }
}
