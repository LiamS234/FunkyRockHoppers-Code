using System.Collections.Generic;
using UnityEngine;

public class StationaryScenerySpawner : MonoBehaviour
{
    public Transform target;
    Vector3 relativePos;

    public static GameObject tree1;
    public static GameObject lampPost1;
    public static GameObject snowman1;
    public static GameObject snowman2;
    public static GameObject constructionSign1;
    public static GameObject constructionSign2;
    public static GameObject igloo1;
    public static GameObject obstruction;
    public static GameObject cone;
    public static GameObject crate;
    public static GameObject sign3;
    public static GameObject sign2;
    public static GameObject sign1;

    public static GameObject emptySpawnObject;

    public GameObject Tree1;
    public GameObject LampPost1;
    public GameObject Snowman1;
    public GameObject Snowman2;
    public GameObject ConstructionSign1;
    public GameObject ConstructionSign2;
    public GameObject Igloo1;
    public GameObject Obstruction;
    public GameObject Cone;
    public GameObject Crate;
    public GameObject Sign3;
    public GameObject Sign2;
    public GameObject Sign1;

    public GameObject EmptySpawnObject;

    GameObject StationarySceneryInstance;

    public GameObject[] StationarySceneryArray = { tree1, lampPost1, snowman1, snowman2, constructionSign1, constructionSign2, igloo1, obstruction, cone, crate, sign3, sign2, sign1 };

    List<GameObject> objs = new List<GameObject>();

    public static float destroyMovScenXBound;
    private float metresBehindToDestroyObject = 75f;

    public static Mesh meshFilterForScen;

    public static bool isTileScenPlacedOnALongPlatform;

    public static float leftXBoundary;
    public static float rightXBoundary;
    public static float frontZBoundary;
    public static float backZBoundary;

    float startX;
    float endX;

    float obstructionDistFromLongPlatformEntrance = 20;

    public static bool startEmptyScenSpawner = false;

    public static GameObject nextTileForScen;
    int longPFTileIndex0;

    public static List<float> yLPFHeightForUFORef = new List<float>();

    Vector3 resizedScale;
    Vector3 adjPosForYAfterRescale;

    float maxY;
    float maxYAdj;

    void Start()
    {
        ResetValues();
    }

    void Update()
    {
        //-20 sets it to the player x
        destroyMovScenXBound = (PlayerController.metresTravelled - 20) - metresBehindToDestroyObject;

        if (GenerateInfinite.scenTileUpdate)
        {            
            //Using no of tiles on screen - 2. This is because we need tile 5 to be first for alignment to work i.e. scenery objects placing on the right tiles. May need to change if no of tiles on screen changes!!!!!!
            //nextTileForScen = GameObject.Find("Tile_" + ((GenerateInfinite.noOfTilesOnScreen - 2) + GenerateInfinite.tileNameIncrementInLoop));
            nextTileForScen = GameObject.Find("Tile_" + (-1 + GenerateInfinite.tileNameIncrementInLoop));
            //print(GameObject.Find("Tile_" + (-1 + GenerateInfinite.tileNameIncrementInLoop)));
            if (nextTileForScen != null)
            {
                meshFilterForScen = nextTileForScen.GetComponent<MeshFilter>().mesh;
            }

                        
            if (GenerateInfinite.statLongPlatformTileNoList.Count > 0)
            {
                //print("No: " + GenerateInfinite.statLongPlatformTileNoList[0]);
                longPFTileIndex0 = GenerateInfinite.statLongPlatformTileNoList[0];
            }

                if (longPFTileIndex0 == (GenerateInfinite.frontTileNumber - 1) && GenerateInfinite.tileNameIncrementInLoop > 1)
                {
                    isTileScenPlacedOnALongPlatform = true;

                //print("index0: " + longPFTileIndex0 + "       frontTileNo - 1: " + (GenerateInfinite.frontTileNumber - 1));

                GenerateInfinite.statLongPlatformTileNoList.RemoveAt(0);

                

                    startX = nextTileForScen.transform.position.x - (nextTileForScen.transform.localScale.x * 10) / 2 + (nextTileForScen.transform.localScale.x * 10) / 10;
                    endX = nextTileForScen.transform.position.x + (nextTileForScen.transform.localScale.x * 10) / 2 - (nextTileForScen.transform.localScale.x * 10) / 10;

                    frontZBoundary = (-(nextTileForScen.transform.localScale.z * 10) / 2) + ((nextTileForScen.transform.localScale.z * 10) / 10);
                    backZBoundary = (nextTileForScen.transform.localScale.z * 10) / 2 - ((nextTileForScen.transform.localScale.z * 10) / 10);
                    leftXBoundary = startX + obstructionDistFromLongPlatformEntrance;
                    rightXBoundary = endX - GenerateInfinite.longPlatformDistFromEndToSwitchCameraBack;
                }
                else
                {
                    isTileScenPlacedOnALongPlatform = false;
                }

            if (GenerateInfinite.tileNameIncrementInLoop != 1 && isTileScenPlacedOnALongPlatform)
            {
                JumpPlatformScenSpawner.startEmptyJumpSpawner = false;
                startEmptyScenSpawner = true;               
            }           
        }
        
        GenerateInfinite.scenTileUpdate = false;

        if (EmptyScenSpawner.hasColDetectionHappened)
        {
                for (int i = 0; i < EmptyScenSpawner.objsToInstantiateList.Count; i++)
                {
                resizedScale = EmptyScenSpawner.resizedEmptyStatObjScales[i];

                //this picks up resized local scale and applies it to object to be instantiated on screen.
                StationarySceneryArray[EmptyScenSpawner.objsToInstantiateScenIndNo[i]].transform.localScale = resizedScale;
                adjPosForYAfterRescale = new Vector3(EmptyScenSpawner.objsToInstantiateList[i].transform.position.x, EmptyScenSpawner.objsToInstantiateList[i].transform.position.y + ((resizedScale.y - 1) * EmptyScenSpawner.yDimList[i])/2f, EmptyScenSpawner.objsToInstantiateList[i].transform.position.z);

                StationarySceneryInstance = Instantiate(StationarySceneryArray[EmptyScenSpawner.objsToInstantiateScenIndNo[i]], adjPosForYAfterRescale, EmptyScenSpawner.objsToInstantiateList[i].transform.rotation);
                objs.Add(StationarySceneryInstance);                   
                }

            if (EmptyScenSpawner.objsToInstantiateList.Count > 0)
            {
                for (int i = 0; i < EmptyScenSpawner.objsToInstantiateList.Count; i++)
                {
                    maxY = Mathf.Max(EmptyScenSpawner.objsToInstantiateList[i].transform.position.y, maxY);
                }

                for (int i = 0; i < EmptyScenSpawner.resizedEmptyStatObjScales.Count; i++)
                {
                    maxYAdj = Mathf.Max(EmptyScenSpawner.resizedEmptyStatObjScales[i].y, maxYAdj);
                }
                yLPFHeightForUFORef.Add(maxY + (maxYAdj - 1) * EmptyScenSpawner.treeDimY / 2f);
            }

            EmptyScenSpawner.hasColDetectionHappened = false;

            //this is to delete empty objects right away when playing production version i.e. set switch to false.
            if (!EmptyScenSpawner.switchOnSphereChecks)
            {
                foreach (GameObject gameObject in EmptyScenSpawner.spawnColCheckObjs)
                    if (gameObject != null)
                    {
                        Destroy(gameObject);
                    }
            }
        }

        if (objs.Count > 0)
        {
            foreach (GameObject gameObject in objs)
            {
                if (gameObject != null)
                    if (gameObject.name == "Snowman1(Clone)" || gameObject.name == "Snowman2(Clone)")
                    {
                        relativePos = new Vector3(target.position.x, gameObject.transform.position.y - (gameObject.transform.position.y - target.position.y) * 0.25f, target.position.z) - gameObject.transform.position;
                        gameObject.transform.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                    }
            }
        }

        EmptyScenSpawner.spawnColCheckObjs.Clear();
        EmptyScenSpawner.spawnColCheckObjsToInstantiate.Clear();
        EmptyScenSpawner.objsToInstantiateScenIndNo.Clear();
        EmptyScenSpawner.objsToInstantiateList.Clear();
        EmptyScenSpawner.resizedEmptyStatObjScales.Clear();
        EmptyScenSpawner.yDimList.Clear();

        if (EmptyScenSpawner.switchOnSphereChecks)
        {
            EmptyScenSpawner.xRadList.Clear();
            EmptyScenSpawner.zRadList.Clear();
            EmptyScenSpawner.yAdjSphereCheckList.Clear();
        }

        //ToArray just a temporary solution. Not efficient as creates new arrays all the time. Without ToArray an error comes up as the list is being modified within the foreach loop
        foreach (GameObject gameObject in objs.ToArray())
            if (gameObject != null)
            {
                if (gameObject.transform.position.x < destroyMovScenXBound)
                {
                    Destroy(gameObject);
                    objs.Remove(gameObject);
                }
            }
    }

    void ResetValues()
    {
        yLPFHeightForUFORef.Clear();
    }
}


