using System.Collections;
using UnityEngine;

/// <summary>
/// Handle player movement by listening to inputs.
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    [Header("Jump Parameters")]
    [SerializeField] private float _jumpDuration = 1f;
    [SerializeField] private float _fastFallDuration = 1f;
    [SerializeField] private float _jumpHeight = 2f;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private AnimationCurve _fallCurve;
    [SerializeField] private AnimationCurve _fastFallCurve;
    
    [Header("Slide Parameters")]
    [SerializeField] private float _slideDuration = 0.5f;
    [SerializeField] private float _slideDownDuration = 0.6f;
    [SerializeField] private Transform[] _slideTargets;
    
    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerColisionController _collisionController;
    
    [Header("Debug")]
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool _isSliding;
    [SerializeField] private bool _isSlidingDown;
    [SerializeField] private int _currentLaneIndex = 1;
    
    private Coroutine _slideCoroutine;
    private Coroutine _jumpCoroutine;
    
    private const string JUMP_PARAMETER = "IsJumping";
    private const string SLIDE_DOWN_PARAMETER = "IsSlidingDown";
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

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            HandleSlideDown();
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
        
        _jumpCoroutine = StartCoroutine(JumpCoroutine());
    }
    
    private void HandleSlideDown()
    {
        if (_isSlidingDown)
        {
            return;
        }

        if (_isJumping)
        {
            StopCoroutine(_jumpCoroutine);
            StartCoroutine(FastFallCoroutine());

            return;
        }

        StartCoroutine(SlideDownCoroutine());
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

    private IEnumerator FastFallCoroutine()
    {
        _animator.SetBool(JUMP_PARAMETER, false);
        
        var startHeight = transform.position.y;
        
        var fastFallTimer = 0f;

        while (fastFallTimer < _fastFallDuration)
        {
            fastFallTimer += Time.deltaTime;
            var normalizedTime = Mathf.Clamp01(fastFallTimer / _fastFallDuration);
            
            var targetHeight = _fastFallCurve.Evaluate(normalizedTime) * startHeight;
            
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

    private IEnumerator SlideDownCoroutine()
    {
        var slideTimer = 0f;
        
        _isSlidingDown = true;
        _animator.SetBool(SLIDE_DOWN_PARAMETER, true);
        _collisionController.ShrinkCollider(true);
        
        while (slideTimer < _slideDownDuration)
        {
            slideTimer += Time.deltaTime;
            yield return null;
        }
        
        _collisionController.ShrinkCollider(false);
        _animator.SetBool(SLIDE_DOWN_PARAMETER, false);
        _isSlidingDown = false;
    }
}