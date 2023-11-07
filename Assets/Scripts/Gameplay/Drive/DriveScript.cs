using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveScript : MonoBehaviour
{
    // Unity editor fields section
    [SerializeField] private Rigidbody2D _frontTireRigidBody; // Front axis tire object
    [SerializeField] private Rigidbody2D _backTireRigidBody;  // Back axis tire object
    [SerializeField] private Rigidbody2D _carRigidBody; 
    [SerializeField] public float _speed = 150f;
    [SerializeField] public float _rotationSpeed = 325f; 
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _movingSound;

    // Movement section
    private float _moveInput;
    private bool _isMoving; // field for activating the sound

    // Sound section
    private float _targetVolume = 0.6f; // max volume
    private float _fadeSpeed = 2.5f; // fade of sound speed

    // Update is called once per frame
    private void Update()
    {
        _moveInput = Input.GetAxisRaw("Horizontal");
        _isMoving = _moveInput != 0;
    }

    private void FixedUpdate()
    {
        // Movement
        _frontTireRigidBody.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
        _backTireRigidBody.AddTorque(-_moveInput * _speed * Time.fixedDeltaTime);
        _carRigidBody.AddTorque(_moveInput * _rotationSpeed * Time.fixedDeltaTime);
    
        // Sound system for a car
        if (_audioSource != null)
        {
            // fading the volume of audio
             _audioSource.volume = Mathf.Lerp(_audioSource.volume, _targetVolume, Time.deltaTime * _fadeSpeed);
            _targetVolume = _isMoving ? 1.0f : 0.0f;

            if (_isMoving && _audioSource.volume < 0.1f)
            {
                // activating the song
                _audioSource.clip = _movingSound;
                _audioSource.Play();
            }
        }
        else
        {
            if (_audioSource != null && _audioSource.isPlaying)
            {
                // stop the song
                _audioSource.Stop();
            }
        }
    
    }
}
