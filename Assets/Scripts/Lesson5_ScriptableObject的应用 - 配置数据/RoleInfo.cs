using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoleInfo",menuName = "ScriptableObject/RoleData")]
public class RoleInfo : ScriptableObject
{
    [System.Serializable]
    public class RoleData
    {
        public int id;
        public string resName;
        public int atk;
        public string tips;
        public int money;
        public int type;
        public string hitEff;
    }

    public List<RoleData> roleList;  
}
