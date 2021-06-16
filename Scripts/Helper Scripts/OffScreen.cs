using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreen : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if (!GeometryUtility.TestPlanesAABB(planes, spriteRenderer.bounds))
        {
            if (transform.position.x - Camera.main.transform.position.x < 0.0f)
            {
                CheckTile();
            }
        }
    }

	void CheckTile()
	{

		if (this.tag == MyTags.ROAD)
		{

			Change(ref MapGenerator.instance.lastPosOfRoadTile,
				new Vector3(1.5f, 0f, 0f),
				ref MapGenerator.instance.lastOrderOfRoad);

		}
		else if (this.tag == MyTags.TOP_NEAR_GRASS)
		{

			Change(ref MapGenerator.instance.lastPosOfTopNearGrass,
				new Vector3(1.2f, 0f, 0f),
				ref MapGenerator.instance.lastOrderofTopNearGrass);

		}
		else if (this.tag == MyTags.TOP_FAR_GRASS)
		{

			Change(ref MapGenerator.instance.lastPosOfTopFarGrass,
				new Vector3(4.8f, 0f, 0f),
				ref MapGenerator.instance.lastOrderofTopFarGrass);

		}
		else if (this.tag == MyTags.BOTTOM_NEAR_GRASS)
		{

			Change(ref MapGenerator.instance.lastPosOfBottomNearGrass,
				new Vector3(1.2f, 0f, 0f),
				ref MapGenerator.instance.lastOrderOfBottomNearGrass);

		}
		else if (this.tag == MyTags.BOTTOM_FAR_LAND_1)
		{

			Change(ref MapGenerator.instance.lastPosOfBottomFarLandF1,
				new Vector3(1.6f, 0f, 0f),
				ref MapGenerator.instance.lastOrderOfBottomFarLandF1);

		}
		else if (this.tag == MyTags.BOTTOM_FAR_LAND_2)
		{

			Change(ref MapGenerator.instance.lastPosOfBottomFarLandF2,
				new Vector3(1.6f, 0f, 0f),
				ref MapGenerator.instance.lastOrderOfBottomFarLandF2);

		}
		else if (this.tag == MyTags.BOTTOM_FAR_LAND_3)
		{

			Change(ref MapGenerator.instance.lastPosOfBottomFarLandF3,
				new Vector3(1.6f, 0f, 0f),
				ref MapGenerator.instance.lastOrderOfBottomFarLandF3);

		}
		else if (this.tag == MyTags.BOTTOM_FAR_LAND_4)
		{

			Change(ref MapGenerator.instance.lastPosOfBottomFarLandF4,
				new Vector3(1.6f, 0f, 0f),
				ref MapGenerator.instance.lastOrderOfBottomFarLandF4);

		}
		else if (this.tag == MyTags.BOTTOM_FAR_LAND_5)
		{

			Change(ref MapGenerator.instance.lastPosOfBottomFarLandF5,
				new Vector3(1.6f, 0f, 0f),
				ref MapGenerator.instance.lastOrderOfBottomFarLandF5);

		}

	}


	void Change(ref Vector3 pos, Vector3 offset, ref int orderInLayer)
    {
        transform.position = pos;
        pos += offset;

        spriteRenderer.sortingOrder = orderInLayer;
        orderInLayer++;
    }
}
