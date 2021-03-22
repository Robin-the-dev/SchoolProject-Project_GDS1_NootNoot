using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    // Script for Player Input!!! You can add input here such as jump
    public string moveHorizontalAxisName = "Horizontal";
    public string moveVerticalAxisName = "Vertical";
    public string jumpButtonName = "Jump";
    public string lookAroundButtonName = "Fire2";

    public Vector2 moveInput { get; private set; }
    public Vector3 swimInput { get; private set; }
    public bool jump { get; private set; }
    public bool lookAround { get; private set; }

    private PlayerAudio _playerAudio;

    private void Start()
    {
        _playerAudio = GetComponent<PlayerAudio>();
    }

    private void Update() {
        moveInput = new Vector2(Input.GetAxis(moveHorizontalAxisName), Input.GetAxis(moveVerticalAxisName));
        if (moveInput.sqrMagnitude > 1) moveInput = moveInput.normalized;

        swimInput = new Vector3(Input.GetAxis(moveHorizontalAxisName), Input.GetAxis(jumpButtonName), Input.GetAxis(moveVerticalAxisName));

        jump = Input.GetButton(jumpButtonName);

        lookAround = Input.GetButton(lookAroundButtonName);

        if (Input.GetMouseButtonDown(0))
        {
            _playerAudio.Honk(); // plays honk
        }
    }
}