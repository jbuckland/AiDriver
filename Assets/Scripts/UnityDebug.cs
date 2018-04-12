using UnityEngine;

public class UnityDebug : MonoBehaviour, IDebug
{
    public void Log(string msg)
    {
        Debug.Log(msg);
    }
}