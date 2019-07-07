using UnityEngine;
using System.Collections;
using System;

public class ViewDrag : MonoBehaviour {

	Vector3 hitPosition = Vector3.zero;
	Vector3 currentPosition = Vector3.zero;
	Vector3 cameraPosition = Vector3.zero;
	Vector3 direction = Vector3.zero;
	
    [SerializeField]
    private Transform water;
	
	[SerializeField]
    private float height = 4; 
    [SerializeField]
    private float angle = 20;
    [SerializeField]
    private float rotation = 0;  

	[SerializeField]
    private float heightChangeFactor = 40.0f;
    [SerializeField]
    private float angleChangeFactor = 20;
    [SerializeField]
    private float rotationChangeFactor = 20;
    [SerializeField]
    private float fovChangeFactor = 20;    
	[SerializeField]
	private float smooth = 0.05f;	
	[SerializeField]
    private float maxSpeed = 10;
    [SerializeField]
    private float zoomSpeed = 1;
    [SerializeField]
    private Vector2 zoomRange = new Vector2(4, 400);

    [SerializeField]
    private Vector3 center = new Vector3(500, 0, 500);
    [SerializeField]
    private float maxDistance = 500;

    private Camera camera;
	
	// Use this for initialization
	void Start() {
        transform.position = new Vector3(transform.position.x,
										 GetSurfaceHeight(transform.position),
										 transform.position.z);

        camera = GetComponent<Camera>();
	}

	void Update()
    {

        if(Input.touchCount != 2)
            HandleDrag();

        HandleZoom();

		UpdateHeight();
        UpdateRotation();
        UpdateFieldOfView();
        CheckMaxDistance();
    }

    private void CheckMaxDistance()
    {
        Vector3 centerWithCameraY = new Vector3(center.x, transform.position.y, center.z);
        Vector3 clamped = Vector3.ClampMagnitude(transform.position - centerWithCameraY, maxDistance);
        transform.position = clamped + centerWithCameraY;
    }

    private void UpdateHeight()
    {
		
        Vector3 position = transform.position;
        
        float surfaceHeight = GetSurfaceHeight(position);

        position.y = Mathf.Clamp(Mathf.Lerp(position.y,
                                surfaceHeight,
                                heightChangeFactor * Time.deltaTime),
                                water.transform.position.y + zoomRange.x,
                                zoomRange.y);

        if(transform.position.y < position.y || Input.mouseScrollDelta.y != 0) {
            transform.position = position;
        }else{
            height += transform.position.y - surfaceHeight;
        }
    
    }

    private void UpdateRotation()
    {
        rotation += Input.GetAxis("Horizontal") * Time.deltaTime * rotationChangeFactor;
        angle += Input.GetAxis("Vertical") * Time.deltaTime * angleChangeFactor;
        transform.eulerAngles = new Vector3(90f - angle, rotation, 0f);
    }

    private void UpdateFieldOfView() 
    {
        if(Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus)) {
            camera.fieldOfView += fovChangeFactor * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus)) {
            camera.fieldOfView -= fovChangeFactor * Time.deltaTime;
        }
    }

    private void HandleZoom()
    {
        float zoom = 0;

        if (Input.touchCount == 2) //https://unity3d.com/es/learn/tutorials/topics/mobile-touch/pinch-zoom
        {   
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            zoom = prevTouchDeltaMag - touchDeltaMag;
        }else{
            zoom = -Input.mouseScrollDelta.y;
        }
        
        height = Mathf.Clamp(height + zoom * zoomSpeed, zoomRange.x, zoomRange.y);
    }

    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hitPosition = Input.mousePosition;
            cameraPosition = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            currentPosition = Input.mousePosition;
            LeftMouseDrag();
        }
        else
        {
            direction = direction.normalized * Mathf.Lerp(Mathf.Min(direction.magnitude, maxSpeed), 0, smooth);
            direction.y = 0;
            transform.position += direction * Time.deltaTime;
        }
    }

    void LeftMouseDrag(){
		// From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
		// with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
		currentPosition.z = hitPosition.z = cameraPosition.y;

		// Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
		// anyways.  
		Vector3 direction = camera.ScreenToWorldPoint(hitPosition) - camera.ScreenToWorldPoint(currentPosition);

		Vector3 position = cameraPosition + direction;

		this.direction = (position - transform.position) / Time.deltaTime;

        position.y =  camera.transform.position.y;

        transform.position = position;
		
	}

	public float GetSurfaceHeight(Vector3 position) {
        return SurfaceManager.GetSurfaceHeight(position, height);
    }


}