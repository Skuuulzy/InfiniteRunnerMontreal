using System.Collections;
using UnityEngine;

/// <summary>
/// Handle player movement by listening to inputs.
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    [Header("Jump Parameters")]
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AnimationCurve _fallCurve;
    
    [Header("Components")]
    [SerializeField] private Animator _animator;
    
    private const string JUMP_PARAMETER = "IsJumping";
    private const string GROUNDED_PARAMETER = "Grounded";
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleJump();
        }
    }

    /// <summary>
    /// Handle jump input.
    /// </summary>
    private void HandleJump()
    {
        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        _animator.SetBool(JUMP_PARAMETER, true);
        
        var halfJumpDuration = _jumpDuration / 2f;
        
        var jumpTimer = 0f;
        
        // Jump logic
        while (jumpTimer < halfJumpDuration)
        {
            jumpTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(jumpTimer / halfJumpDuration);
            
            //var targetHeight = Mathf.Lerp(0, _jumpHeight, normalizedTime);
            var targetHeight = _jumpCurve.Evaluate(normalizedTime) * _jumpHeight;
            
            var targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
            transform.position = targetPosition;
            
            // Wait for the next frame.
            yield return null;
        }
        
        _animator.SetBool(JUMP_PARAMETER, false);
        
        // Fall logic
        jumpTimer = 0f;

        while (jumpTimer < halfJumpDuration)
        {
            jumpTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(jumpTimer / halfJumpDuration);
            
            var targetHeight = _fallCurve.Evaluate(normalizedTime) * _jumpHeight;
            
            var targetPosition = new Vector3(transform.position.x, targetHeight, transform.position.z);
            transform.position = targetPosition;
            
            yield return null;
        }
        
        _animator.SetTrigger(GROUNDED_PARAMETER);
    }
}
