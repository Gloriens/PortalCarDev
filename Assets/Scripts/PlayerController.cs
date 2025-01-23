using System.Collections;
using Assets.Scripts.Common;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    public Text scoreText;
    private float score;
    private float gameTime;
    
    public float speed;
    private bool isAlive;
    
    public Camera mainCamera;
    public ParticleSystem explode;
    private AudioSource audio;
    public AudioClip explodeClip;
    private UIController uiController;
    

    // Panel referansı
    

    void Start()
    {
        uiController = GameObject.Find("EventSystem").GetComponent<UIController>();
        
        score = 1f;
        gameTime = 0f;
         // Başlangıçta paneli gizle
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
            GameOver();
           
        }
    }

    IEnumerator Explosion()
    {
        // Particle efektini oluştur ve sesi çal
        ParticleSystem temp = Instantiate(explode, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(explodeClip, mainCamera.transform.position, 1);

        // Oyuncuyu devre dışı bırak ama yok etme
        Transform playerCarModel = transform.Find("Player Car");
        playerCarModel.SetActive(false);
        speed = 0;

        // 3 saniye bekle
        yield return new WaitForSeconds(2);

        // Particle efektini yok et
        Destroy(temp);

        // Game Over panelini göster ve oyunu durdur
        uiController.gameOver();
        Time.timeScale = 0;

        // Oyuncuyu yok et
    }

    public void GameOver()
    {
        StartCoroutine(Explosion());
    }

    
}