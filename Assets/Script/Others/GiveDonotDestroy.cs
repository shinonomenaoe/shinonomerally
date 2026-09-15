using UnityEngine;

public class GiveDonotDestroy : MonoBehaviour
{
    public static GameObject _mine;
    private void Awake()
    {
        if (_mine != null && _mine != this)
        {
            Destroy(gameObject);
            return;
        }
        _mine = this.gameObject;
        DontDestroyOnLoad(gameObject);
    }
}
