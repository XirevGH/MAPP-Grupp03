using UnityEngine;

public class Yoyo : Projectile
{
    [Header("Orbit & Rotation Settings")]
    [SerializeField] private float baseRotationSpeed = 120f;
    [SerializeField] private float superModeMultiplier = 3f;

    [Header("Components & Transforms")]
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject yoyoString;
    [SerializeField] private CircleCollider2D circleColl;

    [Header("Positions & Offsets")]
    [SerializeField] private Vector3 ballStartPosition;
    [SerializeField] private Vector3 ballSuperModePosition;
    [SerializeField] private Vector3 stringStartPosition;
    [SerializeField] private Vector3 stringSuperModePosition;
    [SerializeField] private Vector3 stringStartScale;
    [SerializeField] private Vector3 stringSuperModeScale;
    [SerializeField] private float colliderStartingOffset;
    [SerializeField] private float colliderSuperModeOffset;

    public float angle;

    private bool isSuperMode = false;
    private float superModeDuration;
    private float superModeTimer;
    private float triggerNoteValue;

    private void Awake()
    {
        if (circleColl == null) circleColl = GetComponent<CircleCollider2D>();
        ResetSuperMode();

        if (YoyoController.Instance != null)
        {
            triggerNoteValue = TriggerController.Instance.GetTrigger(YoyoController.Instance.BaseItemData.BeatNumber).noteValue;
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        float currentBPM = SoundManager.Instance != null ? SoundManager.Instance.GetCurrentBPM() : 120f;
        float speedMultiplier = isSuperMode ? superModeMultiplier : 1f;
        float currentRotateSpeed = (currentBPM / 60f) * baseRotationSpeed * speedMultiplier;

        angle = Mathf.Repeat(angle + (currentRotateSpeed * Time.deltaTime), 360f);
        transform.eulerAngles = new Vector3(0f, 0f, angle);

        if (isSuperMode)
        {
            UpdateSuperMode();
        }
    }

    public void ActivateSuperMode()
    {
        float bpm = SoundManager.Instance != null ? SoundManager.Instance.GetCurrentBPM() : 120f;
        float pitch = SoundManager.Instance != null ? SoundManager.Instance.transform.GetChild(0).GetComponent<AudioSource>().pitch : 1f;
        float noteValue = TriggerController.Instance != null && YoyoController.Instance != null
            ? TriggerController.Instance.GetTrigger(YoyoController.Instance.BaseItemData.BeatNumber).noteValue
            : 1f;

        superModeDuration = Mathf.Max(0.1f, ((60f / (bpm / noteValue)) / pitch) / 2f);
        superModeTimer = 0f;
        isSuperMode = true;
    }

    private void UpdateSuperMode()
    {
        superModeTimer += Time.deltaTime;
        float progress = Mathf.Clamp01(superModeTimer / superModeDuration);

        float extendProgress = progress < 0.5f
            ? progress / 0.5f
            : (1f - progress) / 0.5f;

        ball.transform.localPosition = Vector3.Lerp(ballStartPosition, ballSuperModePosition, extendProgress);
        yoyoString.transform.localPosition = Vector3.Lerp(stringStartPosition, stringSuperModePosition, extendProgress);
        yoyoString.transform.localScale = Vector3.Lerp(stringStartScale, stringSuperModeScale, extendProgress);

        if (circleColl != null)
        {
            circleColl.offset = new Vector2(Mathf.Lerp(colliderStartingOffset, colliderSuperModeOffset, extendProgress), 0f);
        }

        if (progress >= 1f)
        {
            ResetSuperMode();
        }
    }

    public void ResetSuperMode()
    {
        isSuperMode = false;
        superModeTimer = 0f;

        ball.transform.localPosition = ballStartPosition;
        yoyoString.transform.localPosition = stringStartPosition;
        yoyoString.transform.localScale = stringStartScale;

        if (circleColl != null)
        {
            circleColl.offset = new Vector2(colliderStartingOffset, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            DealDamage(other);
        }
    }
}