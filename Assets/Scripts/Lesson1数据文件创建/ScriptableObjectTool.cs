using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectTool
{
    [MenuItem("ScriptableObject/CreateMyData")]
    public static void CreateMyData()
    {
        //泛型对象必须继承了ScriptableObject
        MyData asset = ScriptableObject.CreateInstance<MyData>();

        //通过编辑器API，根据数据创建一个数据文件
        AssetDatabase.CreateAsset(asset,"Asset/Resources/MydataTest.asset");
        //保存资源
        AssetDatabase.SaveAssets();
        //刷新界面
        AssetDatabase.Refresh();
    }
}
