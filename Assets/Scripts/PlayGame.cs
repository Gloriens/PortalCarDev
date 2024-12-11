using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    public AudioSource musicSource;  // Inspector üzerinden atama yapılacak
    public Image cross;

    // Start is called before the first frame update
    private void Start()
    {
        cross.enabled = false;
        
        // Eğer musicSource devre dışıysa aktif et
        if (musicSource != null && !musicSource.enabled)
        {
            musicSource.enabled = true;
        }
    }

    public void playGame()
    {
        ResetGame();
        
        // "Game" sahnesini yükle
        SceneManager.LoadScene("Game");
    }

    private void ResetGame()
    {
        Time.timeScale = 1; // Zamanı tekrar başlat
        Debug.Log("Oyun sıfırlandı.");
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void goMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void toggleBackgroundMusic()
    {
        if (musicSource == null)
        {
            Debug.Log("Musicsource boş amınakogtmg");
            musicSource = gameObject.AddComponent<AudioSource>();
        }

        // Eğer AudioSource devre dışıysa, aktif et
        if (!musicSource.enabled)
        {
            musicSource.enabled = true;
        }

        // Müzik çalınıyor mu kontrol et
        if (musicSource.isPlaying)
        {
            Debug.Log("Musicsource çalıyordu durdurdum");
            musicSource.Pause(); 
            setCrossImageActive(true);
        }
        else
        {
            Debug.Log("Musicsource durmuştu başlattım");  
            musicSource.Play();   
            setCrossImageActive(false);
        }
    }

    private void setCrossImageActive(bool isActive)
    {
        if (cross != null)
        {
            cross.enabled = isActive;  
        }
        else
        {
            Debug.LogWarning("Cross Image bulunamadı!");
        }
    }
}