using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SingScriptableObject<T> : ScriptableObject where T: ScriptableObject
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //为空就加载数据文件
                instance = Resources.Load<T>("ScriptableObject/" + typeof(T).Name);
            }
            //如果没有文件 为了安全 创建一个空的出去
            if (instance == null)
            {
                instance = ScriptableObject.CreateInstance<T>();
            }

            return instance;
        }
    }
}
