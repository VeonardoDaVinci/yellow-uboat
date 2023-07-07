using DG.Tweening;
using ExtensionMethods;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SubmarineController : MonoBehaviour
{
    [Header("Submarine")]
    [SerializeField] private GameObject submarineObject;
    [SerializeField] private RotationController propeller;
    [SerializeField] private float maxSubmarineSpeed = 5f;

    [Header("Inputs")]
    [SerializeField] private InputAction movement;
    [SerializeField] private InputAction drive;
    [SerializeField] private InputAction rotate;

    [Header("GUI")]
    [SerializeField] private ButtonClicked upButton;
    [SerializeField] private ButtonClicked downButton;
    [SerializeField] private ButtonClicked rotateButton;
    [SerializeField] private ButtonClicked driveButton;
    [SerializeField] private ButtonClicked photoButton;
    [SerializeField] private ToggleButton powerToggle;
    [SerializeField] private ClockController clock;

    private bool isUpPressed = false;
    private bool isDownPressed = false;
    private bool isEngineOn = false;


    private Rigidbody2D submarineRigidbody;
    private Vector2 submarineDirection = Vector2.zero;
    private Vector2 submarineVelocity = Vector2.zero;

    private bool isRotating = false;
    private bool isMoving = false;

    private float detectionRadius = 1f;
    private Vector3 originalScale;

    [Header("Gameplay Elements")]
    [SerializeField] private Transform visibilityMask;
    [SerializeField] private WinCondition winCondition;

    private void Awake()
    {
        ControlsInit();
    }

    private void ControlsInit()
    {
        rotateButton.pointerDownEvent.AddListener(OnRotateButton);

        driveButton.pointerDownEvent.AddListener(DriveStartButton);
        driveButton.pointerUpEvent.AddListener(DriveStopButton);

        upButton.pointerDownEvent.AddListener(() => { isUpPressed = true; });
        upButton.pointerUpEvent.AddListener(() => { isUpPressed = false; });

        downButton.pointerDownEvent.AddListener(() => { isDownPressed = true; });
        downButton.pointerUpEvent.AddListener(() => { isDownPressed = false; });

        powerToggle.ToggleButtonEvent.AddListener((bool isOn) => { isEngineOn = isOn; propeller.isRotationActive = isOn; });

        photoButton.pointerDownEvent.AddListener(OnPhotoButton);

        rotate.performed += Rotate;
        drive.performed += DriveStart;
        drive.canceled += DriveStop;
    }

    private void OnEnable()
    {
        movement.Enable();
        rotate.Enable();
        drive.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        rotate.Disable();
        drive.Disable();
    }

    private void Start()
    {
        submarineRigidbody = GetComponent<Rigidbody2D>();
        submarineDirection.x = 1f;
        originalScale = visibilityMask.transform.localScale;
    }

    private void Update()
    {
        ScaleVisibilityMask();
        GetDirectionFromButtons();
        VelocityUpdate();
    }

    private void VelocityUpdate()
    {
        BleedMomentum();
        ChangeMomentumOnInput();
    }

    private void ChangeMomentumOnInput()
    {
        if (isMoving && isEngineOn)
        {
            submarineVelocity.x += submarineDirection.x * 5f * Time.deltaTime;
            propeller.AnimationRampKey += Time.deltaTime;
        }

        submarineVelocity.y += submarineDirection.y * 5f * Time.deltaTime;

        Mathf.Clamp(submarineVelocity.y, -maxSubmarineSpeed, maxSubmarineSpeed);
        Mathf.Clamp(submarineVelocity.x, -maxSubmarineSpeed, maxSubmarineSpeed);
    }

    private void ScaleVisibilityMask()
    {
        if (detectionRadius <= 10f)
        {
            detectionRadius = 10f - 9 * clock.Time / 120f;
            visibilityMask.transform.localScale = originalScale * detectionRadius;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            submarineVelocity = (transform.position - collision.transform.position) * 5f;
            clock.RemoveTime(5);
            SoundManager.I.SfxSource.PlayOneShot(SoundManager.I.ExplosionClip);
            return;
        }
        submarineVelocity = Vector2.zero;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        submarineVelocity = Vector2.zero;
    }

    private void DriveStart(InputAction.CallbackContext callbackContext)
    {
        isMoving = true;
    }

    private void DriveStop(InputAction.CallbackContext callbackContext)
    {
        isMoving = false;
    }
    private void DriveStartButton()
    {
        isMoving = true;
    }

    private void DriveStopButton()
    {
        isMoving = false;
    }

    private void GetDirectionFromButtons()
    {
        if (!isEngineOn) { return; }
        if (isDownPressed) { submarineDirection.y = -1f; }
        else if (isUpPressed) { submarineDirection.y = 1f; }
        else { submarineDirection.y = movement.ReadValue<Vector2>().y; }
    }
    private void Rotate(InputAction.CallbackContext callbackContext)
    {
        if (!isEngineOn) { return; }
        if (isRotating) { return; }
        StartCoroutine(RotateSubmarine());
        submarineDirection.x *= -1;
    }

    private void OnRotateButton()
    {
        if (!isEngineOn) { return; }
        if (isRotating) { return; }
        StartCoroutine(RotateSubmarine());
        submarineDirection.x *= -1;
    }

    private void OnPhotoButton()
    {
        string date = System.DateTime.Now.ToString();
        date = date.Replace("/", "-");
        date = date.Replace(" ", "_");
        date = date.Replace(":", "-");
        Directory.CreateDirectory(Application.dataPath + "/Screenshots");
        ScreenCapture.CaptureScreenshot(Application.dataPath + "/Screenshots/" + date + ".png");
        if (Vector3.Distance(transform.position, winCondition.transform.position) < detectionRadius)
        {
            ConnectionManager.I.SendClientMessage("KONIEC " + clock.Time.ParseIntToTimeString());
            ScoreManager.I.AddNewHighScore(clock.Time);
            SceneManager.LoadScene("EndScreen");
        }
    }

    private void FixedUpdate()
    {
        submarineRigidbody.velocity = submarineVelocity;
    }

    private void BleedMomentum()
    {
        if (submarineDirection.y == 0)
        {
            if (Mathf.Abs(submarineVelocity.y) < 0.1f)
            {
                submarineVelocity.y = 0f;
            }
            else if (submarineVelocity.y > 0f)
            {
                submarineVelocity.y -= Time.deltaTime;
            }
            else if (submarineVelocity.y < 0f)
            {
                submarineVelocity.y += Time.deltaTime;
            }
        }

        if (!isMoving)
        {
            if (Mathf.Abs(submarineVelocity.x) < 0.1f)
            {
                submarineVelocity.x = 0f;
                propeller.AnimationRampKey = 0f;
            }
            else if (submarineVelocity.x > 0f)
            {
                submarineVelocity.x -= Time.deltaTime;
                propeller.AnimationRampKey -= Time.deltaTime;
            }
            else if (submarineVelocity.x < 0f)
            {
                submarineVelocity.x += Time.deltaTime;
                propeller.AnimationRampKey -= Time.deltaTime;
            }
        }
    }

    private IEnumerator RotateSubmarine()
    {
        isRotating = true;
        submarineObject.transform.DORotate(submarineObject.transform.eulerAngles + new Vector3(0f, 180f, 0f), 0.5f);
        yield return new WaitForSeconds(0.5f);
        isRotating = false;
    }
}
