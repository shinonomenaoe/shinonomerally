using UnityEngine;

public class ShowPlayUI : MonoBehaviour
{
    [SerializeField]GameObject[] _playUi;
    HomeChecker _homeChecker;
    PlayINF _playINF;

    void Start()
    {
        _homeChecker = GetComponent<HomeChecker>();
        _playINF = GetComponent<PlayINF>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_homeChecker._isHome || _playINF._isGoal) 
        {
            foreach (GameObject ui in _playUi)
            {
                ui.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject ui in _playUi)
            {
                ui.SetActive(true);
            }
        }
    }
}
