using UnityEngine;

public class InGameButtun : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPos()
    {
        GameObject inGameManager = GameObject.FindGameObjectWithTag("GameManager");
        ReadySystem readySystem = inGameManager.GetComponent<ReadySystem>();
        readySystem.ResetButton();
    }
}
