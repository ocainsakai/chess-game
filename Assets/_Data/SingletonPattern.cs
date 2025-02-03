using UnityEngine;

public class SingletonPattern<T> : MonoBehaviour where T : SingletonPattern<T>
{
    [SerializeField] private static T _instance;
     public static T Instance => _instance;

   
    protected virtual void Awake()
    {
        CheckInstance();
        //DontDestroyOnLoad(gameObject);  // Giữ đối tượng không bị huỷ khi chuyển scene

    }

    private void CheckInstance()
    {
        if (_instance != null)
        {
            Debug.LogWarning("only have " + typeof(T));
        }
            _instance = this as T;
    }
}
