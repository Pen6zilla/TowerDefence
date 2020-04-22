using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform EnemyPrefab;
    public Transform SpawnPoint; 
    public Text WaveCountDownText;
    public Text Win;

    public float TimeBetweenWaves = 5f;

    [SerializeField]
    private float _countDown = 2f;
    [SerializeField]
    private float _waitSeconds = 0.5f;

    public int WavesToWin = 3;
    private int _waveIndex = 0;

    void Update()
    {
        if (_countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countDown = TimeBetweenWaves;
        }

        _countDown -= Time.deltaTime;
        WaveCountDownText.text = Mathf.Round(_countDown).ToString();

        if(_waveIndex >= WavesToWin)
        {
            Time.timeScale = 0;
            Win.gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnWave()
    {
        _waveIndex++;

        for (int i = 0; i < _waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_waitSeconds);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(EnemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
    }
}
