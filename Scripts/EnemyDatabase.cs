using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public GameObject prefab;

    [Header("Day Range")]
    [Min(1)] public int startDay = 1;
    [Min(1)] public int endDay = 1000;
}

[CreateAssetMenu(menuName = "Enemy/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    [SerializeField] private List<EnemyData> datas = new();

    public List<GameObject> GetEnemiesOfDay(int day)
    {
        List<EnemyData> dataAll = datas.FindAll(x => x.startDay <= day && day <= x.endDay);
        return dataAll.Select(r => r.prefab).ToList();
    }
}
