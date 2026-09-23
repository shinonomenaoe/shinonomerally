using UnityEngine;

public class InGameButtun : MonoBehaviour
{
    GameObject _resetPos;
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
        //ReadySystem readySystem = inGameManager.GetComponent<ReadySystem>();
        //readySystem.ResetButton();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CourseLine")
        {
            _resetPos = other.GetComponent<GameObject>();
        }
    }
}
