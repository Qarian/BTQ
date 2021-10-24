using System;
using UnityEngine;

public class HeadLeg : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform leg;
    [SerializeField] private SceneData sceneData;
    [SerializeField] private SpriteRenderer headRenderer;
    [SerializeField] private SpriteRenderer legRenderer;

    [SerializeField] private PlayerUI playerUIPrefab;

    [Header("Head")]
    [SerializeField] private float rotationAcceleration;
    [SerializeField] private int startLives = 3;

    [Header("Leg")]
    [SerializeField] private Vector2 legBounds;
    [SerializeField] private Vector2 scaleBound;
    [SerializeField] private float hidingSpeed = 0.8f;
    [SerializeField] private float releasingSpeed = 1.5f;
    public float jumpForce = 5;
    
    [Header("Kick")]
    public float baseDmg;

    [NonSerialized]
    public Rigidbody2D rigidbody;
    public CharacterInfo CharacterInfo { get; private set; }
    private PlayerInput input;
    private PlayerUI ui;

    public bool StraighteningLeg => releasing && leg.localPosition.y > legBounds.y;
    
    private float HidingSpeed => Mathf.Abs(legBounds.x - legBounds.y) * hidingSpeed;
    private float ReleasingSpeed => Mathf.Abs(legBounds.x - legBounds.y) * releasingSpeed;
    
    [NonSerialized]
    public float rotationInput;
    private bool releasing = true;
    private int currentLives;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        
        transform.position = sceneData.spawnPositions[input.Id];
        rigidbody.gravityScale = 0;
    }

    public void StartRound(SceneData data, Transform uiParent)
    {
        rigidbody.gravityScale = 1;
        transform.position = data.spawnPositions[input.Id];
        currentLives = startLives;

        ui = Instantiate(playerUIPrefab, uiParent);
        ui.SetUI(CharacterInfo, startLives);
    }

    public void ContractLeg(bool cancelled)
    {
        releasing = cancelled;
    }

    private void FixedUpdate()
    {
        ChangeLeg();
        Rotate();
    }

    private void ChangeLeg()
    {
        var localPosition = leg.localPosition;
        float y = localPosition.y;
        if (releasing)
            y -= ReleasingSpeed * Time.fixedDeltaTime;
        else
            y += HidingSpeed * Time.fixedDeltaTime;
        y = Mathf.Clamp(y, legBounds.y, legBounds.x);

        float maxRange = legBounds.x - legBounds.y;
        float currentRange = y - legBounds.y;
        float percent = 1 - currentRange / maxRange;
        float maxScaleRange = scaleBound.y - scaleBound.x;
        leg.localScale = new Vector3(1, scaleBound.x + percent * maxScaleRange, 1);
        localPosition = new Vector3(localPosition.x, y, localPosition.z);
        leg.localPosition = localPosition;
    }

    private void Rotate()
    {
        rigidbody.AddTorque(rotationInput * rotationAcceleration * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    public void SetCharacter(CharacterInfo characterInfo)
    {
        CharacterInfo = characterInfo;
        headRenderer.sprite = characterInfo.head;
        legRenderer.sprite = characterInfo.leg;
    }

    public void Kick()
    {
        currentLives--;
        ui.RemoveHeart();

        if (currentLives <= 0)
        {
            GameManager.Instance.RemovedPlayer(this);
            Destroy(gameObject);
        }
    }
}
