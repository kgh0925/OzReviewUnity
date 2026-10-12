using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class gm022sc : MonoBehaviour
{
    [Header("Player")]
    public Renderer player;

    [Header("Heat")]
    [Range(0f, 100f)] public float hit;
    public float heatIncreaseSpeed = 30f;
    public float heatDecreaseSpeed = 20f;
    public float overheatWaitTime = 2f;

    [Header("Effect")]
    public float maxScaleMultiplier = 1.5f;
    public float shakeAngle = 8f;
    public float shakeSpeed = 25f;

    private bool isHeat;
    private float overheatTimer;
    private Vector3 startScale;
    private Quaternion startRotation;

    void Awake()
    {
        if (player == null)
        {
            player = GetComponent<Renderer>();
        }

        startScale = transform.localScale;
        startRotation = transform.localRotation;
        hit = Mathf.Clamp(hit, 0f, 100f);
    }

    void Update()
    {
        bool isUsing = false;
        bool isCooling = false;

        if (isHeat)
        {
            overheatTimer += Time.deltaTime;

            if (overheatTimer >= overheatWaitTime)
            {
                hit -= heatDecreaseSpeed * Time.deltaTime;
                isCooling = true;

                if (hit <= 20f)
                {
                    hit = 20f;
                    isHeat = false;
                    overheatTimer = 0f;
                    Debug.Log("과열 상태 해제");
                }
            }
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            hit += heatIncreaseSpeed * Time.deltaTime;
            isUsing = true;

            if (hit >= 80f)
            {
                hit = Mathf.Min(hit, 100f);
                isHeat = true;
                overheatTimer = 0f;
                isUsing = false;
                Debug.Log("과열 상태 진입");
            }
        }
        else
        {
            hit -= heatDecreaseSpeed * Time.deltaTime;
            isCooling = hit > 0f;
        }

        hit = Mathf.Clamp(hit, 0f, 100f);

        UpdateColor();
        UpdateScale(isUsing, isCooling);
        UpdateRotation(isUsing);
    }

    void UpdateColor()
    {
        if (hit < 40f)
        {
            player.material.color = Color.blue;
        }
        else if (hit < 80f)
        {
            player.material.color = Color.yellow;
        }
        else
        {
            player.material.color = Color.red;
        }
    }

    void UpdateScale(bool isUsing, bool isCooling)
    {
        float scaleMultiplier = Mathf.Lerp(1f, maxScaleMultiplier, hit / 100f);
        Vector3 targetScale = startScale * scaleMultiplier;

        if (isUsing || isCooling)
        {
            transform.localScale = Vector3.MoveTowards(
                transform.localScale,
                targetScale,
                Time.deltaTime
            );
        }
    }

    void UpdateRotation(bool isUsing)
    {
        if (isUsing)
        {
            float angle = Mathf.Sin(Time.time * shakeSpeed) * shakeAngle;
            transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                startRotation,
                10f * Time.deltaTime
            );
        }
    }
}
