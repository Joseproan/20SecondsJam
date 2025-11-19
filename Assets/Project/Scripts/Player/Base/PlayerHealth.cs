using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour
{

    [Header("Health params")]
    public int maxHealth;
    public int currentHealth;
    public float invulnerableTime;
    [Header("Slowmotion")]
    public float slowMotionFactor = 0.5f; // Escala de tiempo objetivo
    public float slowMotionDuration = 1f; // Duración total del efecto
    public float transitionDuration = 0.2f; // Duración de la transición a tiempo lento/normal

    private float defaultFixedDeltaTime;

    [Header("Camera Shake")]
    [SerializeField] private CameraShake cameraShake;

    private DamagedFlash damageFlash;


    internal float invulnerableTimer;
    internal bool isInvulnerable;
    public GameObject invulnerableImage;

    private int damageTaken;
    private float enemyPushForce;

    public bool isDead;

    private Player player;
    private Vector3 hitPosition;
    //private bool pushed;
    //private float pushForce;
    internal bool isSlowDown;

    private SpriteRenderer sr;

    private void Awake()
    {
        player = GetComponent<Player>();
        damageFlash = GetComponent<DamagedFlash>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if (invulnerableImage) invulnerableImage.SetActive(false);
        //currentHealth = maxHealth;

        invulnerableTimer = 0;
        //defaultFixedDeltaTime = Time.fixedDeltaTime;
        //playerHealthUI = gameManager.playerHealthUI;
    }
    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
    }
    private void Update()
    {

        if (invulnerableTimer > 0)
        {
            isInvulnerable = true;
            invulnerableTimer -= Time.deltaTime;
            if (invulnerableImage) invulnerableImage.SetActive(true);
        }
        else
        {
            isInvulnerable = false;
            if (invulnerableImage) invulnerableImage.SetActive(false);
        }
        
    }

    private IEnumerator SlowMotionC()
    {
        // Transición al tiempo lento
        yield return StartCoroutine(ChangeTimeScale(1f, slowMotionFactor, transitionDuration));

        // Mantener el tiempo lento durante la duración especificada
        yield return new WaitForSecondsRealtime(slowMotionDuration);

        // Transición de vuelta al tiempo normal
        yield return StartCoroutine(ChangeTimeScale(slowMotionFactor, 1f, transitionDuration));
    }

    private IEnumerator ChangeTimeScale(float from, float to, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Tiempo no afectado por Time.timeScale
            Time.timeScale = Mathf.Lerp(from, to, elapsedTime / duration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale; // Ajustar la física
            yield return null; // Esperar al siguiente frame
        }

        // Asegurar que se alcanza el valor final
        Time.timeScale = to;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    public void Die()
    {
        isDead = true;
        Destroy(gameObject);
    }

    public void Hit(int damage)
    {
        if (currentHealth > 0f)
        {
            currentHealth -= damage;
            cameraShake.ShakeCamera();
            damageFlash.CallDamageFlash();
            StartCoroutine(SlowMotionC());
        }
        if (currentHealth == 0)
        {
            Die();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet") && !isInvulnerable && !player.rollInmunity)
        {
            isInvulnerable = true;
            invulnerableTimer = invulnerableTime;

            //player.isHit = true;
            Hit(damageTaken);
            //pushed = true;
            collision.gameObject.SetActive(false);

            //player.pushForce = enemyDamage.GetPushForce();
            //pushForce = enemyDamage.GetPushForce();
            //player.hitPosition = collision.transform.position;
        }

        if (collision.gameObject.CompareTag("Boss") && !isInvulnerable && !player.rollInmunity)
        {
            isInvulnerable = true;
            invulnerableTimer = invulnerableTime;

            Hit(damageTaken);
        }
    }
    
}
