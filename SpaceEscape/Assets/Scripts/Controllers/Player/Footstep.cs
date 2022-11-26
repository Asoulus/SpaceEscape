using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    [SerializeField]
    private GroundCheck _groundCheck = null;

    private AudioSource _audio = null;

    [SerializeField]
    private FirstPersonMovement _player = null;

    private PauseMenu _pause = null;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();

        _player = PlayerReference.instance.GetComponent<FirstPersonMovement>();

        _pause = PauseMenu.instance.GetComponent<PauseMenu>();
    }

    private void Update()
    {
        if (_groundCheck._isGrounded && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) && !_audio.isPlaying && !_player.IsInteracting && !_pause.IsPaused)
        {
            _audio.volume = Random.Range(0.5f, 0.8f);
            _audio.pitch = Random.Range(0.75f, 1.1f);
            _audio.Play();
        }
    }
}
