using UnityEngine;
using UnityEngine.Splines;

public class Conveyor : MonoBehaviour
{
    public static Conveyor Instance { get; private set; }


    [SerializeField] private SplineContainer _mainSplineContainer;
    [SerializeField] private SplineContainer _teleportSplineContainer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of conveyor");
        }
    }

    public SplineContainer GetMainSplineContainer()  
	{
        if (_mainSplineContainer)
            return _mainSplineContainer;
        else return null;
    }
	
    public SplineContainer GetTeleportSplineContainer()
    {
        if (_teleportSplineContainer)
        {
            return _teleportSplineContainer;
        }else return null;
    }


}
