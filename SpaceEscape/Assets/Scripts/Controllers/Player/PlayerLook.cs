using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField]
    private Transform _character;
    private Vector2 _currentMouseLook;
    private Vector2 _appliedMouseDelta;

    [SerializeField]
    private float _sensitivity = 1f;
    [SerializeField]
    private float _smoothing = 2f;

    public GameObject target = null;
    
    public bool _lookingAt = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Get smooth mouse look.
        Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * _sensitivity * _smoothing);
        _appliedMouseDelta = Vector2.Lerp(_appliedMouseDelta, smoothMouseDelta, 1 / _smoothing);
        _currentMouseLook += _appliedMouseDelta;
        _currentMouseLook.y = Mathf.Clamp(_currentMouseLook.y, -90, 90);
      
        // Rotate camera and controller.
        transform.localRotation = Quaternion.AngleAxis(-_currentMouseLook.y, Vector3.right);     
        _character.localRotation = Quaternion.AngleAxis(_currentMouseLook.x, Vector3.up);

        if (_lookingAt)
        {
            if (target != null) 
            {
                transform.LookAt(target.transform);
            }        
        }
    }

}
