using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    WaveSpawner waveSpawner;
    private TurretBluprint _turretToBuild;

    public static BuildManager Instance;

    public Text MoneyAmount;
    public Text AmountOfHealth;
    public Text Lose;

    public Color CanBuildColor;
    public bool CanBuild { get { return _turretToBuild != null; } }

    private void Awake()
    {
        Instance = this;
    }

    private void OnGUI()
    {
        MoneyAmount.text = PlayerStats.Money.ToString();
        AmountOfHealth.text = PlayerStats.Health.ToString();
        
        if (PlayerStats.Health <= 0)
        {
            Time.timeScale = 0;
            Lose.gameObject.SetActive(true);
        }
    }

    public void SetTurretToBuild(TurretBluprint turret)
    {
        _turretToBuild = turret;
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < _turretToBuild.Cost)
        {
            Debug.Log("Not enough money");
            return;
        }

        PlayerStats.Money -= _turretToBuild.Cost;
        GameObject turret = (GameObject)Instantiate(_turretToBuild.Prefab, node.GetBuildPosition(), Quaternion.identity);
        node.Turret = turret;
        node.ColorOfNode().material.color = CanBuildColor;
        Debug.Log("Money Left: " + PlayerStats.Money);
    }
}
