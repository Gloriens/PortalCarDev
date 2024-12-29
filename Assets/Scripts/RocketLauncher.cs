using System.Collections;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public GameObject rocket;  // Roket prefab'ı
    public GameObject player;  // Oyuncu objesi
    public float launchInterval = 2f;
    private int min = 4;
    private int max = 8;// Roketin atılma aralığı (saniye cinsinden)

    // Start is called before the first frame update
    void Start()
    {
        // Coroutine başlat
        StartCoroutine(RocketLaunch());
    }

    // Update is called once per frame
    private void Update()
    {
        // Diğer oyun mantıkları burada olabilir
    }

    private IEnumerator RocketLaunch()
    {
        while (true)
        {
            // Oyuncunun konumunu al
            Vector3 playerPosition = player.transform.position;

            // Yeni roketi yarat ve oyuncunun konumuna yerleştir
            Instantiate(rocket, playerPosition, Quaternion.identity);

            // Belirli bir süre bekle
            yield return new WaitForSeconds(getRandomInt(min,max));
        }
    }

    private int getRandomInt(int min, int max)
    {
        return Random.Range(min, max);
    }
}