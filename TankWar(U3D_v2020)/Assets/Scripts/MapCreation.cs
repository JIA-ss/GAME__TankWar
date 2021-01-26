using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    public GameObject HeartPrefab;
    public GameObject WallPrefab;
    public GameObject RiverPrefab;
    public GameObject BarriarPrefab;
    public GameObject GrassPrefab;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public GameObject BornEffectPrefab;
    private int[,] PositionUsed = new int[21,17];
    public int WallNum = 20;
    public int BarriarNum = 10;
    public int RiverNum = 25;
    public int GrassNum = 30;
    private void Awake() {

        CreateHeart();
        CreateRandomMap(WallNum,BarriarNum,RiverNum,GrassNum);
        CreateChildGameObject(PlayerPrefab,new Vector3(-2,-8,0),Quaternion.identity);
        CreateEnemy();
    }
    private void CreateEnemy()
    {
        CreateChildGameObject(EnemyPrefab,new Vector3(-10,8,0),Quaternion.identity,false);
        CreateChildGameObject(EnemyPrefab,new Vector3(0,8,0),Quaternion.identity,false);
        CreateChildGameObject(EnemyPrefab,new Vector3(10,8,0),Quaternion.identity,false);
    }
    private void CreateHeart()
    {
        CreateChildGameObject(HeartPrefab,new Vector3(0,-8,0),Quaternion.identity);
        CreateChildGameObject(WallPrefab,new Vector3(-1,-8,0),Quaternion.identity);
        CreateChildGameObject(WallPrefab,new Vector3(1,-8,0),Quaternion.identity);
        CreateChildGameObject(WallPrefab,new Vector3(1,-7,0),Quaternion.identity);
        CreateChildGameObject(WallPrefab,new Vector3(0,-7,0),Quaternion.identity);
        CreateChildGameObject(WallPrefab,new Vector3(-1,-7,0),Quaternion.identity);
    }

    private void CreateRandomMap(int WallNum,int BarriarNum,int RiverNum,int GrassNum)
    {
        for(int i = 0; i < WallNum; i++) 
        {
            CreateChildGameObject(WallPrefab,CreateRandomPos(),Quaternion.identity);
        }
        for(int i = 0; i < BarriarNum; i++)
        {
            CreateChildGameObject(BarriarPrefab,CreateRandomPos(),Quaternion.identity);
        }
        for(int i = 0; i < RiverNum; i++)
        {
            CreateChildGameObject(RiverPrefab,CreateRandomPos(),Quaternion.identity);
        }
        for(int i = 0; i < GrassNum; i++)
        {
            CreateChildGameObject(GrassPrefab,CreateRandomPos(),Quaternion.identity);
        }
    }
    private void CreateChildGameObject(GameObject Obj, Vector3 Pos, Quaternion Rot, bool RecordPos = true) {
        if (!PosIsUsed(Pos)) {
            GameObject _Obj = Instantiate(Obj,Pos,Rot);
            _Obj.transform.parent = gameObject.transform;
            if(RecordPos) RecordUsedPosition(Pos);
        }
    }
    //判断该位置是否被使用
    private bool PosIsUsed(Vector3 Pos) 
    {
        int x = (int)Pos.x + 10;
        int y = (int)Pos.y + 8;
        if (x > 20 || y > 16 || x < 0 || y < 0) return true;
        if (PositionUsed[x,y] == 1) return true;
        else return false;
    }

    //记录使用的位置
    private void RecordUsedPosition(Vector3 Pos) 
    {
        int x = (int)Pos.x + 10;
        int y = (int)Pos.y + 8;
        PositionUsed[x,y] = 1;
    }

    //产生随机位置
    private Vector3 CreateRandomPos()
    {
        while(true)
        {
            Vector3 pos = new Vector3(Random.Range(-9,10),Random.Range(-7,8),0);
            if (!PosIsUsed(pos)) return pos;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
