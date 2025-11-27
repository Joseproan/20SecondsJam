using UnityEngine;

public class SunController : MonoBehaviour
{
    [Header("Configuración")]
    public float startWidth = 0.1f;     
    public float growSpeed = 0.5f;      
    public float acceleration = 0.2f;   

    private SpriteRenderer sr;
    private Camera cam;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        AjustarAltura();
    }

    void Update()
    {
        growSpeed += acceleration * Time.deltaTime;

        transform.localScale += new Vector3(growSpeed * Time.deltaTime, 0, 0);
    }

    void AjustarAltura()
    {
        float screenHeight = cam.orthographicSize * 2f;
        float spriteHeight = sr.bounds.size.y;

        float heightScale = screenHeight / spriteHeight;

        transform.localScale = new Vector3(startWidth, heightScale, 1);
    }
}