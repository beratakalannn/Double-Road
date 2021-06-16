using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static MapGenerator instance;

    public GameObject
        roadPrefab,
        grassPrefab,
        groundPrefab1,
        groundPrefab2,
        groundPrefab3,
        groundPrefab4,
        grassBottomPrefab,
        landPrefab1,
        landPrefab2,
        landPrefab3,
        landPrefab4,
        landPrefab5,
        bigGrassPrefab,
        bigGrassBottomPrefab,
        treePrefab1,
        treePrefab2,
        treePrefab3,
        bigTreePrefab;

    public GameObject
        roadHolder,
        topNearSideWalkHolder,
        topFarSideWalkHolder,
        bottomNearSideWalkHolder,
        bottomFarSideWalkHolder;

    public int
        startRoadTile,
        startGrassTile,
        startGround3Tile,
        startLandTile;

    public List<GameObject>
        roadTiles,
        topNearGrassTiles,
        topFarGrassTiles,
        bottomNearGrassTiles,
        bottomFarLandF1Tiles,
        bottomFarLandF2Tiles,
        bottomFarLandF3Tiles,
        bottomFarLandF4Tiles,
        bottomFarLandF5Tiles;

    public int[] posForTopGround1;


    public int[] posForTopGround2;

    public int[] posForTopGround4;

    public int[] posForTopBigGrass;

    public int[] posForTopTree1;
    public int[] posForTopTree2;
    public int[] posForTopTree3;

    public int posForRoadTile1;
    public int posForRoadTile2;
    public int posForRoadTile3;
    public int[] posForBottomBigGrass;
    public int[] posForBottomTree1;
    public int[] posForBottomTree2;
    public int[] posForBottomTree3;

    [HideInInspector]
    public Vector3
        lastPosOfRoadTile,
        lastPosOfTopNearGrass,
        lastPosOfTopFarGrass,
        lastPosOfBottomNearGrass,
        lastPosOfBottomFarLandF1,
        lastPosOfBottomFarLandF2,
        lastPosOfBottomFarLandF3,
        lastPosOfBottomFarLandF4,
        lastPosOfBottomFarLandF5;

    [HideInInspector]
    public int
        lastOrderOfRoad,
        lastOrderofTopNearGrass,
        lastOrderofTopFarGrass,
        lastOrderOfBottomNearGrass,
        lastOrderOfBottomFarLandF1,
        lastOrderOfBottomFarLandF2,
        lastOrderOfBottomFarLandF3,
        lastOrderOfBottomFarLandF4,
        lastOrderOfBottomFarLandF5;



    void Awake()
    {
        MakeInstance();
    }


    void Start()
    {
        Initalized();
    }



    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }


    void Initalized()
    {
        InitializePlatform(roadPrefab, ref lastPosOfRoadTile, roadPrefab.transform.position, startRoadTile, roadHolder, ref roadTiles, ref lastOrderOfRoad, new Vector3(1.5f, 0f, 0f));
        InitializePlatform(grassPrefab, ref lastPosOfTopNearGrass, grassPrefab.transform.position, startGrassTile, topNearSideWalkHolder, ref topNearGrassTiles, ref lastOrderofTopNearGrass, new Vector3(1.2f, 0f, 0f));
        InitializePlatform(groundPrefab3, ref lastPosOfTopFarGrass, groundPrefab3.transform.position, startGround3Tile, topFarSideWalkHolder, ref topFarGrassTiles, ref lastOrderofTopFarGrass, new Vector3(4.8f, 0f, 0f));
        InitializePlatform(grassBottomPrefab, ref lastPosOfBottomNearGrass, new Vector3(2.0f, grassBottomPrefab.transform.position.y, 0f), startGrassTile, bottomNearSideWalkHolder, ref bottomNearGrassTiles, ref lastOrderOfBottomNearGrass, new Vector3(1.2f, 0f, 0f)); ;

        InıtiliazedBottomFarLand();
    }


    void InitializePlatform(GameObject prefab, ref Vector3 last_Pos, Vector3 last_Pos_of_Tile, int amountTile, GameObject holder, ref List<GameObject> listTile, ref int lastOrder, Vector3 offset)
    {
        int orderInLayer = 0;

        last_Pos = last_Pos_of_Tile;

        for (int i = 0; i < amountTile; i++)
        {
            GameObject clone = Instantiate(prefab, last_Pos, prefab.transform.rotation) as GameObject;
            clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;

            if (clone.tag == MyTags.TOP_NEAR_GRASS)
            {
                SetNearScene(bigGrassPrefab, ref clone, ref orderInLayer, posForTopBigGrass, posForTopTree1, posForTopTree2, posForTopTree3);
            }
            else if (clone.tag == MyTags.BOTTOM_NEAR_GRASS)
            {
                SetNearScene(bigGrassBottomPrefab, ref clone, ref orderInLayer, posForBottomBigGrass, posForBottomTree1, posForBottomTree2, posForBottomTree3);
            }
            else if (clone.tag == MyTags.BOTTOM_FAR_LAND_2)
            {
                if (orderInLayer == 5)
                {
                    CreateTreeOrGround(bigTreePrefab, ref clone, new Vector3(-0.57f, -1.34f, 0f));
                }
            }
            else if (clone.tag == MyTags.TOP_FAR_GRASS)
            {
                CreateGround(ref clone, ref orderInLayer);
            }

            clone.transform.SetParent(holder.transform);
            listTile.Add(clone);

            orderInLayer++;
            lastOrder = orderInLayer;

            last_Pos += offset;
        }
    }

    void CreateScene(GameObject bigGrassPrefab, ref GameObject tileClone, int orderInLayer)
    {
        GameObject clone = Instantiate(bigGrassPrefab, tileClone.transform.position, bigGrassPrefab.transform.rotation) as GameObject;

        clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        clone.transform.SetParent(tileClone.transform);
        clone.transform.localPosition = new Vector3(-0.183f, 0.106f, 0f);

        CreateTreeOrGround(treePrefab1, ref clone, new Vector3(0f, 1.52f, 0f));

        tileClone.GetComponent<SpriteRenderer>().enabled = false;
    }

    void CreateTreeOrGround(GameObject prefab, ref GameObject tileClone, Vector3 localPos)
    {
        GameObject clone = Instantiate(prefab, tileClone.transform.position, prefab.transform.rotation) as GameObject;

        SpriteRenderer tileCloneRenderer = tileClone.GetComponent<SpriteRenderer>();
        SpriteRenderer cloneRenderer = clone.GetComponent<SpriteRenderer>();

        cloneRenderer.sortingOrder = tileCloneRenderer.sortingOrder;
        clone.transform.SetParent(tileClone.transform);
        clone.transform.localPosition = localPos;

        if (prefab == groundPrefab1 || prefab == groundPrefab2 || prefab == groundPrefab4)
        {
            tileCloneRenderer.enabled = false;
        }
    }

    void CreateGround(ref GameObject clone, ref int orderInLayer)
    {
        for (int i = 0; i < posForTopGround1.Length; i++)
        {
            if (orderInLayer == posForTopGround1[i])
            {
                CreateTreeOrGround(groundPrefab1, ref clone, Vector3.zero);
                break;
            }
        }

        for (int i = 0; i < posForTopGround2.Length; i++)
        {
            if (orderInLayer == posForTopGround2[i])
            {
                CreateTreeOrGround(groundPrefab2, ref clone, Vector3.zero);
                break;
            }
        }

        for (int i = 0; i < posForTopGround4.Length; i++)
        {
            if (orderInLayer == posForTopGround4[i])
            {
                CreateTreeOrGround(groundPrefab4, ref clone, Vector3.zero);
                break;
            }
        }
    }

    void SetNearScene(GameObject bigGrassPrefab, ref GameObject clone, ref int orderInLayer, int[] posForBigGrass, int[] posForTree1, int[] posForTree2, int[] posForTree3)
    {
        for (int i = 0; i < posForBigGrass.Length; i++)
        {
            if (orderInLayer == posForBigGrass[i])
            {
                CreateScene(bigGrassPrefab, ref clone, orderInLayer);
                break;
            }
        }

        for (int i = 0; i < posForTree1.Length; i++)
        {
            if (orderInLayer == posForTree1[i])
            {
                CreateTreeOrGround(treePrefab1, ref clone, new Vector3(0f, 1.15f, 0f));
                break;
            }
        }

        for (int i = 0; i < posForTree2.Length; i++)
        {
            if (orderInLayer == posForTree2[i])
            {
                CreateTreeOrGround(treePrefab2, ref clone, new Vector3(0f, 1.15f, 0f));
                break;
            }
        }

        for (int i = 0; i < posForTree3.Length; i++)
        {
            if (orderInLayer == posForTree3[i])
            {
                CreateTreeOrGround(treePrefab3, ref clone, new Vector3(0f, 1.15f, 0f));
                break;
            }
        }
    }

    void InıtiliazedBottomFarLand()
    {
        InitializePlatform(landPrefab1, ref lastPosOfBottomFarLandF1, landPrefab1.transform.position, startLandTile, bottomFarSideWalkHolder, ref bottomFarLandF1Tiles, ref lastOrderOfBottomFarLandF1, new Vector3(1.6f, 0f, 0f));
        InitializePlatform(landPrefab2, ref lastPosOfBottomFarLandF2, landPrefab2.transform.position, startLandTile - 3, bottomFarSideWalkHolder, ref bottomFarLandF2Tiles, ref lastOrderOfBottomFarLandF2, new Vector3(1.6f, 0f, 0f));
        InitializePlatform(landPrefab3, ref lastPosOfBottomFarLandF3, landPrefab3.transform.position, startLandTile - 4, bottomFarSideWalkHolder, ref bottomFarLandF3Tiles, ref lastOrderOfBottomFarLandF3, new Vector3(1.6f, 0f, 0f));
        InitializePlatform(landPrefab4, ref lastPosOfBottomFarLandF4, landPrefab4.transform.position, startLandTile - 7, bottomFarSideWalkHolder, ref bottomFarLandF4Tiles, ref lastOrderOfBottomFarLandF4, new Vector3(1.6f, 0f, 0f));
        InitializePlatform(landPrefab5, ref lastPosOfBottomFarLandF5, landPrefab5.transform.position, startLandTile - 10, bottomFarSideWalkHolder, ref bottomFarLandF5Tiles, ref lastOrderOfBottomFarLandF5, new Vector3(1.6f, 0f, 0f));
    }
}
















































