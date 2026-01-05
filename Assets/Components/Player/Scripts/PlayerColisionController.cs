using UnityEngine;

public class PlayerColisionController : MonoBehaviour
{
    [Header("Sphere Parameters")]
    [SerializeField] private Vector3 _sphereCenter;
    [SerializeField] private float _sphereRadius;
    
    [Header("Invulnerability ")]
    [SerializeField] private bool _isHit;
    
    private readonly Collider[] _hitResults = new Collider[1];
    
    private void Update()
    {
        var hitCount = Physics.OverlapSphereNonAlloc(transform.position + _sphereCenter, _sphereRadius, _hitResults);
        
        if (hitCount > 0 && !_isHit)
        {
            Debug.Log("Player take damage");
            _isHit = true;
        }
        // Reset is hit flag when no collision is detected.
        else if (hitCount == 0)
        {
            _isHit = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + _sphereCenter, _sphereRadius);
    }
}