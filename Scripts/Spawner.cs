using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float coolTimeEnemy = 10;    //敵出現のクールタイム
    [SerializeField] private float coolTimeItem = 5;        //アイテム出現のクールタイム
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private EnemyDatabase enemyDatabase;     //敵出現テーブル
    [SerializeField] private List<GameObject> itemPrefabs;
    [SerializeField] private Transform enemies; //enemiesオブジェクト(出現している敵をまとめる用オブジェクト)
    [SerializeField] private Transform items;  //上のアイテム版
    private Coroutine enemySpawnCoroutine;
    private Coroutine itemSpawnCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnLoop());
    }

    //有効化の時に呼び出される
    public void Activate()
    {
        enemySpawnCoroutine = StartCoroutine(EnemySpawnLoop());//敵スポナーコルーチン
        itemSpawnCoroutine = StartCoroutine(ItemSpawnLoop());//アイテムスポナーコルーチン
        Debug.Log("timelimit表示のUIsもcanvasにした方がいい？");
        Debug.Log("rabbitの挙動がおかしい");
        Debug.Log("playerの位置を1行で変えられるようにPlayerControllerの中にメソッドを作成する");
    }

    //無効化のときに呼び出される
    public void Disactivate()
    {
        StopCoroutine(enemySpawnCoroutine);
        StopCoroutine(itemSpawnCoroutine);
        Debug.Log("スポナーコルーチンストップ");
    }

    private IEnumerator EnemySpawnLoop()
    {
        while (true)
        {
            //1~10のランダム距離作成、敵をランダムに選ぶ
            var distance = Random.Range(1f, 10f);
            //var prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
            var prefabs = enemyDatabase.GetEnemiesOfDay(playerStatus.GetDayCount());
            var prefab = prefabs[Random.Range(0, prefabs.Count)];

            //敵を複製
            CreateObjectInRomdomPlace(distance, prefab, enemies);

            yield return new WaitForSeconds(coolTimeEnemy);//coolTime秒待つ

            //プレイヤーが死んだらループ終了
            if(playerStatus.Life <= 0)
            {
                break;
            }
        }
        yield return null;
    }

    private IEnumerator ItemSpawnLoop()
    {
        while (true)
        { 
            //1~20のランダム距離作成、アイテムをランダムに選ぶ
            var distance = Random.Range(1f, 20f);
            var prefab = itemPrefabs[Random.Range(0, itemPrefabs.Count)];

            //アイテムを複製
            CreateObjectInRomdomPlace(distance, prefab, null, true);

            yield return new WaitForSeconds(coolTimeItem);//coolTime秒待つ

            //プレイヤーが死んだらループ終了
            if (playerStatus.Life <= 0)
            {
                break;
            }
        }
        yield return null;
    }

    private void CreateObjectInRomdomPlace(float distance, GameObject prefab, Transform objects, bool isItem=false)  //distanceはプレイヤーからの距離
                                                                                                  //objectsは生成したオブジェクトを管理する用のオブジェクト(enemiesとか)
    {
        //1~10のランダム距離のベクトル
        var distanceVector = new Vector3(distance, 0);

        //プレイヤーからオブジェクトの出現位置までの座標差分
        var spawnPositionFromPlayer = Quaternion.Euler(0, Random.Range(0, 360f), 0) * distanceVector;

        //オブジェクトの出現位置
        var spawnPosition = playerStatus.transform.position + spawnPositionFromPlayer;

        //出現位置から一番近いnavmeshの座標を探す
        NavMeshHit navMeshHit;
        if (NavMesh.SamplePosition(spawnPosition, out navMeshHit, 10, NavMesh.AllAreas))
        {
            //1個複製する
            var obj = Instantiate(prefab, navMeshHit.position, Quaternion.identity, objects);

            //複製するものがアイテムなら初期化をする
            if (isItem)
            {
                Item item = obj.GetComponent<Item>();

                if(item != null)
                {
                    item.Initialize();
                }
                else
                {
                    Debug.LogError($"{obj.name}にitemコンポーネントがアタッチされていません。");
                }
            }
        }
    }
}
