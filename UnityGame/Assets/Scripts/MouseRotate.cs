using UnityEngine;
using System.Collections;

public class MouseRotate : MonoBehaviour {
    /***
    public enum RotationAxes {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 2.0f;
    public float sensitivityVert = 2.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0;
    ***/

    [SerializeField] private float horizontalRotationSpeed = 3f;
    [SerializeField] private float verticalRotationSpeed = 3f;
    private float currentHorizontalRotation = 0f;
    private float currentVerticalRotation = 0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        currentHorizontalRotation += mouseX * horizontalRotationSpeed;
        currentVerticalRotation -= mouseY * verticalRotationSpeed;
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, -45f, 45f);

        // Rotate the character around the y-axis based on the mouse input
        transform.rotation = Quaternion.Euler(currentVerticalRotation, currentHorizontalRotation, 0f);
    }
 /**
    void Update() {
        
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
        float rotationY = transform.localEulerAngles.y;
        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

       
        if (axes == RotationAxes.MouseX){
            // horizontal rotation here
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY){
            // vertical rotation here
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else {
            // both horizontal and vertical rotation here
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        
    }
    ***/

    void Start() {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null){
            body.freezeRotation = true;
        }
    }
}