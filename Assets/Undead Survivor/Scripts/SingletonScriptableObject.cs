using UnityEngine;

// 汎用的なシングルトンScriptableObject
public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // Resourcesフォルダからアセットをロードする
                instance = Resources.Load<T>(typeof(T).Name);
                
                if (instance == null)
                {
                    Debug.LogError("ScriptableObjectシングルトンが見つかりません: " + typeof(T).Name);
                }
            }
            return instance;
        }
    }
}

