using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public Text scoreText;
    private float score;
    private float gameTime;
    private int gold;
    public float speed;

    // Panel referansı
    public GameObject gameOverPanel;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        score = 1f;
        gameTime = 0f;
        gameOverPanel.SetActive(false); // Başlangıçta paneli gizle
    }

    private void Update()
    {
        // Oyuncuyu hareket ettir
        transform.Translate(-speed * Time.deltaTime, 0, 0);

        // Geçen süreyi güncelle
        gameTime += Time.deltaTime;

        // Skoru artır
        score += 2 * Time.deltaTime;

        // Skoru ekranda güncelle
        scoreText.text = "Score: " + (int)score;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Tetiklenen collider'ın parent'ına ulaş
        GameObject rootObject = other.transform.root.gameObject;

        if (rootObject.CompareTag("gold"))
        {
            gold++;
            Debug.Log("Golda çarptım");
            Debug.Log("Önceki skor: " + score);

            // Altın toplandığında süreye göre ek puan ekle
            score += gameTime * 5; 
            scoreText.text = "Score: " + (int)score;
            Debug.Log("Sonraki skor: " + score);

            // Gold objesini yok et
            Destroy(rootObject);
        }
        else if (rootObject.CompareTag("enemy"))
        {
            // Oyun bitişini göstermek ve durdurmak
            Debug.Log("Yok olmam lazım");

            // GameOver panelini aktif et
            gameOverPanel.SetActive(true);

            // Oyunu durdur
            Time.timeScale = 0;

            // Oyuncuyu yok et
            Destroy(gameObject);
        }
    }
}