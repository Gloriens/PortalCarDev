using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public AudioSource musicSource;  // Inspector üzerinden atama yapılacak
    public Image cross;
    private GameObject gameOverPanel;
    private GameObject victoryPanel;

    // Start is called before the first frame update
    private void Start()
    {
        gameOverPanel = GameObject.Find("Game Over Panel");
        victoryPanel = GameObject.Find("Victory Panel");
        String currentScene = SceneManager.GetActiveScene().name;

        try
        {
            if (currentScene != "Main Menu")
            {
                gameOverPanel.SetActive(false);
                victoryPanel.SetActive(false);
                Debug.Log("Yorma");
                
                if (cross == null)
                {
                    Debug.LogError("Cross Image referansı eksik!");
                }
                else
                {
                    cross.enabled = false;
                }
            }
            
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    
        // Eğer musicSource devre dışıysa aktif et
        if (musicSource != null && !musicSource.enabled)
        {
            musicSource.enabled = true;
        }
    }
    
    
    public void playNormalGame()
    {
        ResetGame();
        SceneManager.LoadScene("Game");
    }

    public void playGame()
    {
        ResetGame();
        
        String sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    public void playBossMode()
    {
        ResetGame();
        SceneManager.LoadScene("BossMode");
    }

    public void playDrivingMode()
    {
        ResetGame();
        SceneManager.LoadScene("DrivingMode");
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

    public void victory()
    {
        victoryPanel.SetActive(true);
    }

    public void gameOver()
    {
        gameOverPanel.SetActive(true);
    }
}