using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    [SerializeField] private bool gamePause;
    [SerializeField] private GameObject gamePauseCanvas;
    [SerializeField] private GameObject gameUICanvas;

    [SerializeField] private bool holdingBP;
    [SerializeField] private bool holdingHammer;

    [SerializeField] private bool isWalking;

    [SerializeField] private GameObject holdingBPGO;

    // Start is called before the first frame update
    void Start()
    {
        holdingBP = false;
        holdingHammer = false;

        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingBP == true){
            holdingBPGO.SetActive(true);
        }
        else{
            holdingBPGO.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            ToggleGamePause();
        }

        if (gamePause == true){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gamePauseCanvas.SetActive(true);
            gameUICanvas.SetActive(false);
        }
        else{
            gamePauseCanvas.SetActive(false);
            gameUICanvas.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            UpdateMouseLook();
            UpdateMovement();
        }
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
        {
            velocityY = 0.0f;
        }

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;
        
        if (Input.GetAxisRaw("Horizontal") != 0.0 || Input.GetAxisRaw("Vertical") != 0.0){
            isWalking = true;
        }
        else{
            isWalking = false;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void ToggleGamePause(){
        gamePause = !gamePause;
    }

    public void SetHoldingBP(bool holding){
        holdingBP = holding;
    }

    public bool GetHoldingBP(){
        return holdingBP;
    }

    public bool GetWalking(){
        return isWalking;
    }

    public void SetHoldingHammer(bool holding){
        holdingHammer = holding;
    }

    public bool GetHoldingHammer(){
        return holdingHammer;
    }
}
