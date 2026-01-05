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
    
    [Header("Slide Parameters")]
    [SerializeField] private float _slideDuration = 0.5f;
    [SerializeField] private Transform[] _slideTargets;
    
    [Header("Components")]
    [SerializeField] private Animator _animator;
    
    [Header("Debug")]
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool _isSliding;
    [SerializeField] private int _currentLaneIndex = 1;
    
    private Coroutine _slideCoroutine;
    
    private const string JUMP_PARAMETER = "IsJumping";
    private const string GROUNDED_PARAMETER = "Grounded";
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            HandleJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_isSliding)
            {
                StopCoroutine(_slideCoroutine);
            }
            
            if (_currentLaneIndex == 0)
            {
                return;
            }
            
            _currentLaneIndex--;
            
            _slideCoroutine = StartCoroutine(SlideCoroutine(_slideTargets[_currentLaneIndex]));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_isSliding)
            {
                StopCoroutine(_slideCoroutine);
            }
            
            if (_currentLaneIndex == _slideTargets.Length - 1)
            {
                return;
            }
            
            _currentLaneIndex++;
            
            _slideCoroutine = StartCoroutine(SlideCoroutine(_slideTargets[_currentLaneIndex]));
        }
    }

    /// <summary>
    /// Handle jump input.
    /// </summary>
    private void HandleJump()
    {
        if (_isJumping)
        {
            return;
        }
        
        StartCoroutine(JumpCoroutine());
    }

    private IEnumerator JumpCoroutine()
    {
        _isJumping = true;
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
        _isJumping = false;
    }

    private IEnumerator SlideCoroutine(Transform target)
    {
        _isSliding = true;
        var slideTimer = 0f;

        while (slideTimer < _slideDuration)
        {
            slideTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(slideTimer / _slideDuration);
            var targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, normalizedTime);
            
            // Wait for the next frame.
            yield return null;
        }
        
        _isSliding = false;
    }
}