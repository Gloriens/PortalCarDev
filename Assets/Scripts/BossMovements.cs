using System;
using System.Collections;
using UnityEngine;

public class BossMovements : MonoBehaviour
{
    public GameObject player;
    private Rigidbody rb;
    public float waitTime = 5f;
    private Vector3 offset = new Vector3(-40, 10, 0);

    private bool timeHasCome = false;
    private bool planeDown = true;

    private float descendSpeed = 2f;
    private float targetHeight = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(TeleportToPlayerOffset());
    }

    private void Update()
    {
        if (timeHasCome)
        {
            transform.position = new Vector3(player.transform.position.x + -40, transform.position.y, -12.5f);
            if (planeDown) 
            {
                Debug.Log("İnişe geçiyoz");
                StartCoroutine(DramaticEntrance());
            }
        }
    }

    IEnumerator TeleportToPlayerOffset()
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = player.transform.position + offset;
        timeHasCome = true;
    }

    IEnumerator DramaticEntrance()
    {
        planeDown = true;
        float targetY = 3.5f;  // Hedef Y pozisyonu
        float threshold = 0.1f;  // Hedefe çok yakın olup olmadığını kontrol etmek için eşik

        while (planeDown)
        {
            // Y pozisyonu hedefe çok yakınsa durdur
            if (Mathf.Abs(transform.position.y - targetY) < threshold)
            {
                Debug.Log("Hedefe yaklaşıldı, duruluyor.");
                planeDown = false;
                rb.velocity = Vector3.zero; 
                StartCoroutine(FlipOnce()); // Yalnızca bir kere flip işlemi başlatıyoruz
            }
            else
            {
                // Y ekseninde aşağıya doğru hareket et
                Debug.Log("İniyor...");
                rb.velocity = new Vector3(rb.velocity.x, -descendSpeed, rb.velocity.z); 
                yield return null;
            }
        }
    }

    IEnumerator FlipOnce()
    {
        // Takla için başlangıç ve bitiş açılarını belirleyelim
        float startRotationX = transform.eulerAngles.x;
        float targetRotationX = startRotationX + 360f;  // Takla: 360 derece X ekseninde dönüş

        float flipSpeed = 200f;  // Flip hızını ayarlayabilirsiniz
        float step = flipSpeed * Time.deltaTime;  // Her frame'de yapılacak dönüş miktarı

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.x, targetRotationX)) > 1f)  // Taklaya yaklaşana kadar devam et
        {
            float currentRotationX = Mathf.MoveTowardsAngle(transform.eulerAngles.x, targetRotationX, step);  // Hedef açıya doğru ilerle
            transform.rotation = Quaternion.Euler(currentRotationX, transform.eulerAngles.y, transform.eulerAngles.z);  // Yalnızca X ekseninde dön
            yield return null;
        }

        // Takla işlemi bittiğinde kesin hedef rotasına ulaşmak için
        transform.rotation = Quaternion.Euler(targetRotationX, transform.eulerAngles.y, transform.eulerAngles.z);
        Debug.Log("Takla tamamlandı!");
    }







}