using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class CameraLook : MonoBehaviour
{
    private InputAction _lookHold;
    private InputAction _look;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, 0f);
    [SerializeField] private Terrain terrain;
    [SerializeField] private Transform virtualCamera;
    [SerializeField] private float groundMargin = 0.5f;
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float gamepadSensitivity = 120f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    private float yaw;
    private float pitch;

    // Start is called before the first frame update
    void Start()
    {
        var input = GetComponent<PlayerInput>();
        input.currentActionMap.Enable();

        _look = input.currentActionMap.FindAction("Look");
        _lookHold = input.currentActionMap.FindAction("LookHold");

        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;

        var lookValue = _look.ReadValue<Vector2>();

        bool usingMouse = _look.activeControl?.device is Mouse;

        //マウスを使ったとき
        if (usingMouse)
        {
            //右クリックしてる時のみ処理
            if (!_lookHold.IsPressed())
                return;

            yaw += lookValue.x * mouseSensitivity;
            pitch -= lookValue.y * mouseSensitivity;
        }
        else
        {
            yaw += lookValue.x * gamepadSensitivity * Time.deltaTime;
            pitch -= lookValue.y * gamepadSensitivity * Time.deltaTime;
        }

        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);

        Vector3 camWorldPos = virtualCamera.position;

        float groundY = terrain.SampleHeight(camWorldPos) + terrain.transform.position.y;

        if (camWorldPos.y < groundY + groundMargin)
        {
            Vector3 fixedPos = camWorldPos;
            fixedPos.y = groundY + groundMargin;
            virtualCamera.position = fixedPos;
        }
    }
}
