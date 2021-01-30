using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField]
    private float _maxGroundDistance = .35f;

    public bool _isGrounded;

    [SerializeField]
    private LayerMask _layer;

    void Update()
    {
        _isGrounded = Physics.Raycast(this.transform.position, -transform.up, _maxGroundDistance, _layer);
    }

    void OnDrawGizmosSelected()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _maxGroundDistance))
            Debug.DrawLine(transform.position, hit.point, Color.white);
        else
            Debug.DrawLine(transform.position, transform.position + Vector3.down * _maxGroundDistance, Color.red);
    }
}
