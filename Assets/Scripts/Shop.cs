using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public TurretBluprint StdTurret;
    public TurretBluprint MissileLauncher;

    private void Start()
    {
        buildManager = BuildManager.Instance;
    }

    public void SelectStdTurret()
    {
        Debug.Log("Std got");
        buildManager.SetTurretToBuild(StdTurret);
    }

    public void SelectMissileLauncherTurret()
    {
        Debug.Log("Another got");
        buildManager.SetTurretToBuild(MissileLauncher);
    }
}
                                                                                                                                                           