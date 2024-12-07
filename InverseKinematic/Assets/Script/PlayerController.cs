using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    private CharacterController _cc;
    [SerializeField] private GameObject camRoot;
    [SerializeField] private float sensitivity;

    [SerializeField] private GameObject body;
    [SerializeField] private float bodyRotationSpeed;
    
    [SerializeField] private float walk;
    private float _speed;

    private void Start()
    {
        _speed = walk;
        _cc = GetComponent<CharacterController>();
        _cc.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Camera orbital
        if (Input.GetAxis("Mouse X") != 0)
        {
            float horizontalInput = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            camRoot.transform.Rotate(Vector3.up, horizontalInput);
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //Movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 forward = camRoot.transform.forward * vertical;
            Vector3 right = camRoot.transform.right * horizontal;

            _cc.SimpleMove((forward + right) * _speed);

            //Rotate mesh toward direction
            Quaternion curAngle = body.transform.rotation;
            Vector3 newEulerTarget = curAngle.eulerAngles;
            newEulerTarget.y = 
                Mathf.Rad2Deg * Mathf.Atan2(horizontal, vertical) + camRoot.transform.rotation.eulerAngles.y;
            
            body.transform.rotation = Quaternion.Lerp(
                curAngle, Quaternion.Euler(newEulerTarget), Time.deltaTime * bodyRotationSpeed);
            
        }
    }
}
