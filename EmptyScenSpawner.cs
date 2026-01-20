using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmptyScenSpawner : MonoBehaviour
{
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

    public TMP_Text messageText;

    public static GameObject emptySpawnObject;

    [SerializeReference] public LayerMask empSpawnObjLayer;

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
    private GameObject EmptySpawnColObject;

    int noOfSceneryPerTile;
    int noOfJumpSceneryPerTile;

   
    public static List<GameObject> jumpSpawnColCheckObjs = new List<GameObject>();
    public static List<GameObject> spawnColCheckObjs = new List<GameObject>();
    public static List<GameObject> jumpSpawnColCheckObjsToInstantiate = new List<GameObject>();
    public static List<GameObject> spawnColCheckObjsToInstantiate = new List<GameObject>();
    public static List<GameObject> jumpObjsToInstantiateList = new List<GameObject>();
    public static List<GameObject> objsToInstantiateList = new List<GameObject>();
    public static List<int> jumpObjsToInstantiateScenIndNo = new List<int>();
    public static List<int> objsToInstantiateScenIndNo = new List<int>();

    public static List<Vector3> resizedEmptyStatObjScales = new List<Vector3>();
    public static List<float> yDimList = new List<float>();

    public static List<float> xRadList = new List<float>();
    public static List<float> zRadList = new List<float>();

    public static List<float> xRadJumpList = new List<float>();
    public static List<float> zRadJumpList = new List<float>();

    public static List<Vector3> resizedEmptyJumpObjScales = new List<Vector3>();
    public static List<float> yDimJumpList = new List<float>();

    public static List<float> yAdjSphereCheckList = new List<float>();
    public static List<float> yAdjJumpSphereCheckList = new List<float>();
    float yAdj;
    float yJumpAdj;

    int sceneryIndexNo;
    int statSceneryIndexNo;
    int jumpColIndex;
    int statColIndex;

    public static float treeDimY = 3.10674f;

    float lampPostDimY = 3.78927f;
    float snowmanDimY = 1.49165f;
    float constructionSign1DimY = 0.922263f;
    float constructionSign2DimY = 0.444583f;
    float sign1DimY = 1.07017f;
    float sign2DimZ = 0.453181f;
    float sign3DimY = 1.31066f;
    float iglooDimY = 1.2335f;
    float obstructionDimY = 0.488035f;
    float coneDimY = 0.619646f;
    float crateDimY = 0.740437f;

    float sign1DimX = 0.660316f;
    float sign1DimZ = 0.069146f;
    float sign2DimX = 2.8477f;
    float sign2DimY = 1.8825f;

    float treeDimX = 2.27265f;
    float treeDimZ = 2.55637f;

    float lampPostDimX = 0.849789f;
    float lampPostDimZ = 0.685811f;

    float snowmanDimX = 0.96891f;
    float snowmanDimZ = 0.744114f;

    float constructionSign1DimX = 1.39765f;
    float constructionSign1DimZ = 1.086f;

    float constructionSign2DimX = 2.49329f;
    float constructionSign2DimZ = 2.07088f;


    float sign3DimX = 0.62829f;
    float sign3DimZ = 0.045374f;

    float iglooDimX = 2.42699f;
    float iglooDimZ = 2.80601f;

    float obstructionDimX = 0.70624f;
    float obstructionDimZ = 1.19431f;

    float coneDimX = 0.623683f;
    float coneDimZ = 0.831992f;

    float crateDimX = 0.7317f;
    float crateDimZ = 0.7317f;

    float treeYAdjustment;
    float lampPostYAdjustment;
    float snowmanYAdjustment;
    float constructionSign1YAdjustment;
    float constructionSign2YAdjustment;
    float sign1YAdjustment;
    float sign2YAdjustment;
    float sign3YAdjustment;
    float iglooYAdjustment;
    float obstructionYAdjustment;
    float coneYAdjustment;
    float crateYAdjustment;

    Vector3 stationaryScenYAdjustment;

    Vector3 tree1Scale;
    Vector3 lampPost1Scale;
    Vector3 snowman1Scale;
    Vector3 snowman2Scale;
    Vector3 constructionSign1Scale;
    Vector3 constructionSign2Scale;
    Vector3 sign1Scale;
    Vector3 sign2Scale;
    Vector3 sign3Scale;
    Vector3 igloo1Scale;
    Vector3 coneScale;
    Vector3 crateScale;
    Vector3 obstructionScale;

    public static Mesh meshFilterForScen;

    bool cappedOrNot;

    float xPositionForScen;
    float zPositionforScen;

    float yscenTileHeightAfterCapping;

    float yScaleForEmptyColJump;

    float yScaleForEmptyColStat;

    float prevScaleY;
    float prevCapY;
    float prevScaleX;
    float prevScaleZ;

    public GameObject[] StationarySceneryArray = { tree1, lampPost1, snowman1, snowman2, constructionSign1, constructionSign2, igloo1, obstruction, cone, crate, sign3, sign2, sign1 };

    int tree1UpperPercentile = 50;
    int lampPost1UpperPercentile = 50;
    int snowman1UpperPercentile = 55;
    int snowman2UpperPercentile = 60;
    int constructionSign1UpperPercentile = 64;
    int constructionSign2UpperPercentile = 68;
    int igloo1UpperPercentile = 70;
    int obstructionUpperPercentile = 85;
    int coneUpperPercentile = 93;
    int crateUpperPercentile = 100;
    int sign3UpperPercentile = 100;
    int sign2UpperPercentile = 100;
    int sign1UpperPercentile = 100;

    public GameObject[] JumpPlatformSceneryArray = { tree1, lampPost1, cone, crate, snowman1, snowman2, constructionSign1, constructionSign2, sign3, sign2, sign1 };

    int tree1UpperPercentileJump = 20;
    int lampPost1UpperPercentileJump = 50;
    int coneUpperPercentileJump = 55;
    int crateUpperPercentileJump = 60;
    int snowman1UpperPercentileJump = 65;
    int snowman2UpperPercentileJump = 70;
    int constructionSign1UpperPercentileJump = 75;
    int constructionSign2UpperPercentileJump = 80;
    int sign3UpperPercentileJump = 86;
    int sign2UpperPercentileJump = 94;
    int sign1UpperPercentileJump = 100;

    public static float leftXBoundary;
    public static float rightXBoundary;
    public static float frontZBoundary;
    public static float backZBoundary;

    public static bool hasJumpColDetectionHappened = false;
    public static bool hasColDetectionHappened = false;
    int jumpColCount;
    int prevJumpColCount;
    int statColCount;
    int prevStatColCount;

    int attemptsToSpawn;
   
    float yRotation;
    Vector3 scenPositionStat;
    Vector3 scenPositionJump;
    float yScenTileVerticeValue;

    //USED IN TESTING
    
    /*
    float[] xRadArray;
    float[] zRadArray;
    float[] yAdjArray;
    float[] xRadJumpArray;
    float[] zRadJumpArray;
    float[] yAdjJumpArray;
    GameObject[] sphereArray;
    GameObject[] jumpSphereArray;
    */
    

    public static bool switchOnSphereChecks = false;
    //This is to space everything out more
    float sphereCheckScalar;

    //These are to reduce overlap of individual objects e.g. the lampposts were overlapping a fair bit
    //Changing the height so don't think comment above is correct.
    float emptyScenTreeScalar = 1f;
    float emptyScenLampPostScalar = 1f;
    float emptyScenSnowmanScalar = 1f;
    float emptyScenConstructionSign1Scalar = 1f;
    float emptyScenConstructionSign2Scalar = 1f;
    float emptySign1Scalar = 1f;
    float emptySign2Scalar = 1f;
    float emptySign3Scalar = 1f;
    float emptyIglooScalar = 1f;
    float emptyObstructionScalar = 1f;
    float emptyConeScalar = 1f;
    float emptyCrateScalar = 1f;

    //this is so only one wooden sign picked per platform.
    public static bool hasWoodenSignBeenPicked;
    int randomNumberJump;

    Mesh meshForScenJump;

    float xScaleForEmptyI;
    float zScaleForEmptyI;

    float xSize;
    float ySize;
    float zSize;

    float xRadius;
    float zRadius;

    bool spheresAtBase = true;

    public static float tree1YUB;
    float tree1XLB, tree1XUB;
    float lampPost1XLB, lampPost1XUB;
    float snowman1XLB, snowman1XUB;
    float snowman2XLB, snowman2XUB;
    float constructionSign1XLB, constructionSign1XUB;
    float constructionSign2XLB, constructionSign2XUB;
    float sign1XLB, sign1XUB;
    float sign2XLB, sign2XUB;
    float sign3XLB, sign3XUB;
    float igloo1XLB, igloo1XUB;
    float obstructionXLB, obstructionXUB;
    float coneXLB, coneXUB;
    float crateXLB, crateXUB;

    //BoxCollider emptyCollider;

    int initialNoOfSceneryPerTile = 20;
    //int initialNoOfJumpSceneryPerTile = 4;
    int maxScenPerTile = 250;
    //int endJumpScenPerTile = 8;

    float dimX, dimY, dimZ;

    void Start()
    {
        jumpColCount = 0;
        noOfJumpSceneryPerTile = 0;

        Tree1.transform.localScale = new Vector3(1, 1, 1);
        LampPost1.transform.localScale = new Vector3(1, 1, 1);
        Snowman1.transform.localScale = new Vector3(1, 1, 1);
        Snowman2.transform.localScale = new Vector3(1, 1, 1);
        ConstructionSign1.transform.localScale = new Vector3(1, 1, 1);
        ConstructionSign2.transform.localScale = new Vector3(1, 1, 1);
        Sign1.transform.localScale = new Vector3(1, 1, 1);
        Sign2.transform.localScale = new Vector3(1, 1, 1);
        Sign3.transform.localScale = new Vector3(1, 1, 1);
        Igloo1.transform.localScale = new Vector3(1, 1, 1);
        Obstruction.transform.localScale = new Vector3(1, 1, 1);
        Cone.transform.localScale = new Vector3(1, 1, 1);
        Crate.transform.localScale = new Vector3(1, 1, 1);
        
        tree1Scale = Tree1.transform.localScale;
        lampPost1Scale = LampPost1.transform.localScale;
        snowman1Scale = Snowman1.transform.localScale;
        snowman2Scale = Snowman2.transform.localScale;
        constructionSign1Scale = ConstructionSign1.transform.localScale;
        constructionSign2Scale = ConstructionSign2.transform.localScale;
        sign1Scale = Sign1.transform.localScale;
        sign2Scale = Sign2.transform.localScale;
        sign3Scale = Sign3.transform.localScale;
        igloo1Scale = Igloo1.transform.localScale;
        obstructionScale = Obstruction.transform.localScale;
        coneScale = Cone.transform.localScale;
        crateScale = Crate.transform.localScale;

        treeYAdjustment = treeDimY / 2 - treeDimY / 9;
        lampPostYAdjustment = lampPostDimY / 2 - lampPostDimY / 80;
        snowmanYAdjustment = snowmanDimY / 2 - snowmanDimY / 8;
        constructionSign1YAdjustment = constructionSign1DimY / 2 - constructionSign1DimY / 8;
        constructionSign2YAdjustment = (constructionSign2DimY / 2 - constructionSign2DimY / 10)*0 + 0.9f;
        sign1YAdjustment = sign1DimY / 2 - sign1DimY / 10;
        sign2YAdjustment = (sign2DimY / 2 - sign2DimY / 10)*0 + 0.7f;
        sign3YAdjustment = (sign3DimY / 2 - sign3DimY / 10)*0 + 1;
        iglooYAdjustment = iglooDimY / 2 - iglooDimY / 8;
        //plus sign to make sure penguin starts above the ground
        obstructionYAdjustment = obstructionDimY / 2 + obstructionDimY / 8;
        coneYAdjustment = coneDimY / 2 + coneDimY / 10;
        crateYAdjustment = (crateDimY / 2 + crateDimY / 10)*0 + 1;

        hasWoodenSignBeenPicked = false;
        messageText.SetText("New message");

        {
            //StartCoroutine(ABC());
        }
    }

    void Update()
    {
        if (JumpPlatformScenSpawner.startEmptyJumpSpawner)
        {
            //if (GenerateInfinite.tileNameIncrementInLoop > 2)
            //{
            EmptyJumpSpawnProcess();
            //}
        }
        else if (StationaryScenerySpawner.startEmptyScenSpawner)
        {
            EmptyScenSpawnProcess();

        }

        if(GenerateInfinite.tileNameIncrementInLoop == 1)
        {
            sphereCheckScalar = 0.8f;
        }
        else
        {
            sphereCheckScalar = 2.0f;
            //sphereCheckScalar = Mathf.Max(1.0f - (PlayerController.metresTravelled / 1000f)*0.05f, 0.7f);
        }

        //USED IN TESTING
        /*
        if (spawnColCheckObjs.Count > 0)
        {
            sphereArray = spawnColCheckObjs.ToArray();
            xRadArray = xRadList.ToArray();
            zRadArray = zRadList.ToArray();
            yAdjArray = yAdjSphereCheckList.ToArray();
        }

        if (jumpSpawnColCheckObjs.Count > 0)
        {
            jumpSphereArray = jumpSpawnColCheckObjs.ToArray();
            xRadJumpArray = xRadJumpList.ToArray();
            zRadJumpArray = zRadJumpList.ToArray();
            yAdjJumpArray = yAdjJumpSphereCheckList.ToArray();
        }
        */
        
    }

    private void EmptyScenSpawnProcess()
    {
        setProgressionScenSizes();
        StationaryScenerySpawner.startEmptyScenSpawner = false;
        hasColDetectionHappened = false;
        noOfSceneryPerTile = Mathf.Min(initialNoOfSceneryPerTile + Mathf.FloorToInt(PlayerController.metresTravelled / 154f), maxScenPerTile);
        //this is to set woodenSign being picked back to false so it only appears once on each jump platform
        hasWoodenSignBeenPicked = false;

        //This deletes spheres much later when the spherechecks switch is true. If spherechecks switch in stationmaryscenspawner is false then will delete empty objects right away
        foreach (GameObject gameObject in spawnColCheckObjs)
            if (gameObject != null)
            {
                Destroy(gameObject);
            }

        spawnColCheckObjs.Clear();
        spawnColCheckObjsToInstantiate.Clear();
        objsToInstantiateScenIndNo.Clear();
        objsToInstantiateList.Clear();
        resizedEmptyStatObjScales.Clear();       
        yDimList.Clear();

        if (switchOnSphereChecks)
        {
            xRadList.Clear();
            zRadList.Clear();
            yAdjSphereCheckList.Clear();

        }
         
        prevStatColCount = 0;
        if (!hasColDetectionHappened)
        {
            statColCount = 1;
            attemptsToSpawn = 0;
            while (statColCount > 0 && statColCount < noOfSceneryPerTile)
            {
                attemptsToSpawn++;
                if (prevStatColCount == 0)
                {
                    statColIndex = 0;
                }
                else
                {
                    statColIndex = noOfSceneryPerTile - prevStatColCount;
                }

                statColCount = 0;

                for (int i = statColIndex; i < noOfSceneryPerTile; i++)
                {
                    CalculationPositions();

                    EmptySpawnColObject = Instantiate(EmptySpawnObject, scenPositionStat + stationaryScenYAdjustment, Quaternion.Euler(0, yRotation, 0));
                    
                    spawnColCheckObjs.Add(EmptySpawnColObject);
                    spawnColCheckObjs[i - statColCount].layer = 0;

                    spawnColCheckObjs[i - statColCount].transform.localScale = new Vector3(xSize, ySize, zSize);

                    xScaleForEmptyI = spawnColCheckObjs[i - statColCount].transform.localScale.x * dimX / 2f;
                    zScaleForEmptyI = spawnColCheckObjs[i - statColCount].transform.localScale.z * dimZ / 2f;
                    xRadius = xScaleForEmptyI;
                    zRadius = zScaleForEmptyI;

                    if (!spheresAtBase)
                    {
                        yAdj = (ySize - 1) * yScaleForEmptyColStat / 2f;
                    }
                    else
                    {
                        yAdj = 0;
                    }

                    //emptyBoxCollider = EmptySpawnColObject.AddComponent<BoxCollider>();
                    //emptyBoxCollider.isTrigger = true;
                    //emptyBoxCollider.transform.localScale = new Vector3(spawnColCheckObjs[i - statColCount].transform.localScale.x, spawnColCheckObjs[i - statColCount].transform.localScale.y, spawnColCheckObjs[i - statColCount].transform.localScale.z);
                    //emptyCollider = GetComponent<BoxCollider>();
                    //emptyCollider.transform.localScale = new Vector3(spawnColCheckObjs[i - statColCount].transform.localScale.x, spawnColCheckObjs[i - statColCount].transform.localScale.y, spawnColCheckObjs[i - statColCount].transform.localScale.z);

                    if (Physics.CheckSphere(new Vector3(spawnColCheckObjs[i - statColCount].transform.position.x, spawnColCheckObjs[i - statColCount].transform.position.y + yAdj - (spawnColCheckObjs[i - statColCount].transform.localScale.y / 2f)*0, spawnColCheckObjs[i - statColCount].transform.position.z),
                        Mathf.Max(xRadius, zRadius) * sphereCheckScalar, empSpawnObjLayer))
                    {
                        spawnColCheckObjs.Remove(EmptySpawnColObject);
                        Destroy(EmptySpawnColObject);
                        statColCount = Mathf.Min(statColCount + 1, noOfSceneryPerTile);
                    }
                    else
                    {
                        spawnColCheckObjs[i - statColCount].layer = 14;
                        spawnColCheckObjsToInstantiate.Add(EmptySpawnColObject);
                        objsToInstantiateScenIndNo.Add(statSceneryIndexNo);
                        resizedEmptyStatObjScales.Add(spawnColCheckObjs[i - statColCount].transform.localScale);

                        spawnColCheckObjs[i - statColCount].transform.localScale = new Vector3(spawnColCheckObjs[i - statColCount].transform.localScale.x * dimX, spawnColCheckObjs[i - statColCount].transform.localScale.y * dimY, spawnColCheckObjs[i - statColCount].transform.localScale.z * dimZ);

                        yDimList.Add(yScaleForEmptyColStat);
                        if (switchOnSphereChecks)
                        {
                            xRadList.Add(xRadius);
                            zRadList.Add(zRadius);
                            yAdjSphereCheckList.Add(yAdj);
                        }
                    }
                }                
                prevStatColCount = statColCount;
                if (attemptsToSpawn > 1)
                {
                    break;
                }
            }
            hasColDetectionHappened = true;
        }
        objsToInstantiateList = spawnColCheckObjsToInstantiate;
    }

    private void EmptyJumpSpawnProcess()
    {
        setProgressionScenSizes();
        JumpPlatformScenSpawner.startEmptyJumpSpawner = false;
        hasJumpColDetectionHappened = false;
        if (GenerateInfinite.tileNameIncrementInLoop == 1)
        {
            noOfJumpSceneryPerTile = Random.Range(20, 50);
        }
        else if(PlayerController.playerX < 1000)
        {
            //noOfJumpSceneryPerTile = Mathf.Min(initialNoOfJumpSceneryPerTile + Mathf.FloorToInt(PlayerController.metresTravelled / 2500f), endJumpScenPerTile);
            noOfJumpSceneryPerTile = Random.Range(2, 5);
        }
        else if (PlayerController.playerX >= 1000 && PlayerController.playerX < 2500)
        {
            //noOfJumpSceneryPerTile = Mathf.Max(endJumpScenPerTile - Mathf.FloorToInt(PlayerController.metresTravelled / 5000f), initialNoOfJumpSceneryPerTile + 2);
            noOfJumpSceneryPerTile = Random.Range(2, 6);
        }
        else if (PlayerController.playerX >= 2500 && PlayerController.playerX < 5000)
        {
            noOfJumpSceneryPerTile = Random.Range(2, 7);
        }
        else if (PlayerController.playerX >= 5000 && PlayerController.playerX < 10000)
        {
            noOfJumpSceneryPerTile = Random.Range(3, 7);
        }
        else if (PlayerController.playerX >= 10000 && PlayerController.playerX < 15000)
        {
            noOfJumpSceneryPerTile = Random.Range(3, 6);
        }
        else if (PlayerController.playerX >= 15000 && PlayerController.playerX < 20000)
        {
            noOfJumpSceneryPerTile = Random.Range(3, 5);
        }
        else if (PlayerController.playerX >= 20000)
        {
            noOfJumpSceneryPerTile = Random.Range(2, 5);
        }

        //Testing
        //noOfJumpSceneryPerTile = noOfJumpSceneryPerTile * 10;

        //This deletes spheres much later when the spherechecks switch is true. If spherechecks switch in stationmaryscenspawner is false then will delete empty objects right away
        foreach (GameObject gameObject in jumpSpawnColCheckObjs)
            if (gameObject != null)
            {
                Destroy(gameObject);
            }

        jumpSpawnColCheckObjs.Clear();
        jumpSpawnColCheckObjsToInstantiate.Clear();
        jumpObjsToInstantiateScenIndNo.Clear();
        jumpObjsToInstantiateList.Clear();

        resizedEmptyJumpObjScales.Clear();

        yDimJumpList.Clear();

        if (switchOnSphereChecks)
        {
            xRadJumpList.Clear();
            zRadJumpList.Clear();
            yAdjJumpSphereCheckList.Clear();

        }
         
        prevJumpColCount = 0;
        if (!hasJumpColDetectionHappened)
        {
            jumpColCount = 1;
            attemptsToSpawn = 0;
            while (jumpColCount > 0 && jumpColCount < noOfJumpSceneryPerTile)
            {
                attemptsToSpawn++;
                if (prevJumpColCount == 0)
                {
                    jumpColIndex = 0;
                }
                else
                {
                    jumpColIndex = noOfJumpSceneryPerTile - prevJumpColCount;
                }

                jumpColCount = 0;

                for (int i = jumpColIndex; i < noOfJumpSceneryPerTile; i++)
                {
                    CalcJumpPlatformScenPos();

                    //this if statement around the whole jump empty scen spawn process is to avoid empty objects spawnining in the first frame on pos 0
                    if (scenPositionJump.z + stationaryScenYAdjustment.z != 0 && scenPositionJump.x + stationaryScenYAdjustment.x != 0)
                    {
                        EmptySpawnColObject = Instantiate(EmptySpawnObject, scenPositionJump + stationaryScenYAdjustment, Quaternion.Euler(0, yRotation, 0));

                        

                        jumpSpawnColCheckObjs.Add(EmptySpawnColObject);
                        jumpSpawnColCheckObjs[i - jumpColCount].layer = 0;

                        jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale = new Vector3(xSize, ySize, zSize);

                        xScaleForEmptyI = jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.x * dimX / 2f;
                        zScaleForEmptyI = jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.z * dimZ / 2f;

                        xRadius = xScaleForEmptyI;
                        zRadius = zScaleForEmptyI;


                        if (!spheresAtBase)
                        {
                            yJumpAdj = (ySize - 1) * yScaleForEmptyColJump / 2f;
                        }
                        else
                        {
                            yJumpAdj = 0;
                        }

                        //emptyBoxCollider = EmptySpawnColObject.GetComponent<BoxCollider>();
                        //emptyBoxCollider.isTrigger = true;
                        //emptyBoxCollider.transform.localScale = new Vector3(jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.x, jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.y, jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.z);

                        //emptyCollider.transform.localScale = new Vector3(jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.x, jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.y, jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.z);
                        //emptyCollider = EmptySpawnColObject.GetComponent<BoxCollider>();
                        //emptyCollider.transform.localScale = new Vector3(2, 2, 2);
                        //emptyCollider.transform.localScale = new Vector3(jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.x, jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.z);

                        if (Physics.CheckSphere(new Vector3(jumpSpawnColCheckObjs[i - jumpColCount].transform.position.x, jumpSpawnColCheckObjs[i - jumpColCount].transform.position.y + yJumpAdj - (jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.y / 2f) * 0, jumpSpawnColCheckObjs[i - jumpColCount].transform.position.z),
                            Mathf.Max(xRadius, zRadius) * sphereCheckScalar, empSpawnObjLayer))
                        {
                            //print("happening");
                            jumpSpawnColCheckObjs.Remove(EmptySpawnColObject);
                            Destroy(EmptySpawnColObject);
                            jumpColCount = Mathf.Min(jumpColCount + 1, noOfJumpSceneryPerTile);
                        }
                        else
                        {
                            jumpSpawnColCheckObjs[i - jumpColCount].layer = 14;
                            jumpSpawnColCheckObjsToInstantiate.Add(EmptySpawnColObject);
                            jumpObjsToInstantiateScenIndNo.Add(sceneryIndexNo);

                            //Add the localscale into resized list here so can be picked up in the stationaryScenSpawner when instantiating object
                            resizedEmptyJumpObjScales.Add(jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale);

                            jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale = new Vector3(jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.x * dimX, jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.y * dimY, jumpSpawnColCheckObjs[i - jumpColCount].transform.localScale.z * dimZ);

                            yDimJumpList.Add(yScaleForEmptyColJump);
                            if (switchOnSphereChecks)
                            {
                                xRadJumpList.Add(xRadius);
                                zRadJumpList.Add(zRadius);
                                yAdjJumpSphereCheckList.Add(yJumpAdj);
                            }
                        }
                    }
                }
                prevJumpColCount = jumpColCount;
                if (attemptsToSpawn > 2 && GenerateInfinite.tileNameIncrementInLoop > 1)
                {
                    break;
                } else if (attemptsToSpawn > 1 && GenerateInfinite.tileNameIncrementInLoop == 1)
                {
                    break;
                }
            }
            hasJumpColDetectionHappened = true;
        }
        jumpObjsToInstantiateList = jumpSpawnColCheckObjsToInstantiate;
    }

    private void CalcJumpPlatformScenPos()
    {
        cappedOrNot = GenerateInfinite.prevMaxHeightTorF;
        prevScaleY = GenerateInfinite.prevNewScaleY;
        prevCapY = GenerateInfinite.prevYScalingCap;
        prevScaleX = GenerateInfinite.prevNewScaleX;
        prevScaleZ = GenerateInfinite.prevNewScaleZ;

        bool newCappedOrNot = GenerateInfinite.maxHeightTorF;
        float newScaleY = GenerateInfinite.newScaleY;
        float newCapY = GenerateInfinite.yScalingCap;
        float newScaleX = GenerateInfinite.newScaleX;
        float newScaleZ = GenerateInfinite.newScaleZ;

        if(GenerateInfinite.tileNameIncrementInLoop == 1)
        {
            cappedOrNot = newCappedOrNot;
            prevScaleY = newScaleY;
            prevCapY = newCapY;
            prevScaleX = newScaleX;
            prevScaleZ = newScaleZ;
        }

        if(GenerateInfinite.tileNameIncrementInLoop < 3)
        {
            randomNumberJump = Random.Range(0, tree1UpperPercentileJump);
        }
        else if (!hasWoodenSignBeenPicked)
        {
            randomNumberJump = Random.Range(0, 100);
        }
        else
        {
            randomNumberJump = Random.Range(0, constructionSign2UpperPercentileJump);
        }

        if (randomNumberJump >= 0 && randomNumberJump < tree1UpperPercentileJump)
        {
            sceneryIndexNo = 0;
        }
        else if (randomNumberJump >= tree1UpperPercentileJump && randomNumberJump < lampPost1UpperPercentileJump)
        {
            sceneryIndexNo = 1;
        }
        else if (randomNumberJump >= lampPost1UpperPercentileJump && randomNumberJump < coneUpperPercentileJump)
        {
            sceneryIndexNo = 2;
        }
        else if (randomNumberJump >= coneUpperPercentileJump && randomNumberJump < crateUpperPercentileJump)
        {
            sceneryIndexNo = 3;
        }
        else if (randomNumberJump >= crateUpperPercentileJump && randomNumberJump < snowman1UpperPercentileJump)
        {
            sceneryIndexNo = 4;
        }
        else if (randomNumberJump >= snowman1UpperPercentileJump && randomNumberJump < snowman2UpperPercentileJump)
        {
            sceneryIndexNo = 5;
        }
        else if (randomNumberJump >= snowman2UpperPercentileJump && randomNumberJump < constructionSign1UpperPercentileJump)
        {
            sceneryIndexNo = 6;
        }
        else if (randomNumberJump >= constructionSign1UpperPercentileJump && randomNumberJump < constructionSign2UpperPercentileJump)
        {
            sceneryIndexNo = 7;
        }
        else if (randomNumberJump >= constructionSign2UpperPercentileJump && randomNumberJump < sign3UpperPercentileJump)
        {
            sceneryIndexNo = 8;
        }
        else if (randomNumberJump >= sign3UpperPercentileJump && randomNumberJump < sign2UpperPercentileJump)
        {
            sceneryIndexNo = 9;
        }
        else if (randomNumberJump >= sign2UpperPercentileJump && randomNumberJump < sign1UpperPercentileJump)
        {
            sceneryIndexNo = 10;
        }


        //for testing
        //sceneryIndexNo = 1;

        if (sceneryIndexNo == 10 || sceneryIndexNo == 9 || sceneryIndexNo == 8)
        {
            hasWoodenSignBeenPicked = true;
        }

        int randomVerticeNo = 0;

        //Tree
        if (sceneryIndexNo == 0 && GenerateInfinite.tileNameIncrementInLoop > 1)
        {
            do
            {
                randomVerticeNo = Random.Range(0, 121);
            } while (randomVerticeNo % 11 == 0 || randomVerticeNo % 11 == 10 || Mathf.FloorToInt(randomVerticeNo / 11) == 0 || Mathf.FloorToInt(randomVerticeNo / 11) == 10 ||
                     randomVerticeNo % 11 == 1 || randomVerticeNo % 11 == 9 || Mathf.FloorToInt(randomVerticeNo / 11) == 1 || Mathf.FloorToInt(randomVerticeNo / 11) == 9 ||
                     Mathf.FloorToInt(randomVerticeNo / 11) == 4 || Mathf.FloorToInt(randomVerticeNo / 11) == 5 || Mathf.FloorToInt(randomVerticeNo / 11) == 6 ||
                     Mathf.FloorToInt(randomVerticeNo / 11) == 8 || Mathf.FloorToInt(randomVerticeNo / 11) == 3);
        }
        else if (sceneryIndexNo == 0 && GenerateInfinite.tileNameIncrementInLoop == 1)
        {
            do
            {
                randomVerticeNo = Random.Range(0, 121);
            } while (randomVerticeNo % 11 == 0 || randomVerticeNo % 11 == 10 || Mathf.FloorToInt(randomVerticeNo / 11) == 0 || Mathf.FloorToInt(randomVerticeNo / 11) == 10 ||
                     randomVerticeNo % 11 == 1 || randomVerticeNo % 11 == 9 || Mathf.FloorToInt(randomVerticeNo / 11) == 1 || Mathf.FloorToInt(randomVerticeNo / 11) == 9 ||
                     Mathf.FloorToInt(randomVerticeNo / 11) == 4 || Mathf.FloorToInt(randomVerticeNo / 11) == 5 || Mathf.FloorToInt(randomVerticeNo / 11) == 6 ||
                     Mathf.FloorToInt(randomVerticeNo / 11) == 8 || Mathf.FloorToInt(randomVerticeNo / 11) == 3
                    || randomVerticeNo % 11 == 6 || Mathf.FloorToInt(randomVerticeNo % 11) == 5 || randomVerticeNo % 11 == 4);
        }
        //LampPost
        else if(sceneryIndexNo == 1)
        {
            do
            {
                randomVerticeNo = Random.Range(0, 121);
            } while (randomVerticeNo % 11 == 0 || randomVerticeNo % 11 == 10 || Mathf.FloorToInt(randomVerticeNo / 11) == 0 || Mathf.FloorToInt(randomVerticeNo / 11) == 10 ||
                     randomVerticeNo % 11 == 1 || randomVerticeNo % 11 == 9 || Mathf.FloorToInt(randomVerticeNo / 11) == 1 || Mathf.FloorToInt(randomVerticeNo / 11) == 9 ||
                     Mathf.FloorToInt(randomVerticeNo / 11) == 4 || Mathf.FloorToInt(randomVerticeNo / 11) == 5 || Mathf.FloorToInt(randomVerticeNo / 11) == 6 ||
                     Mathf.FloorToInt(randomVerticeNo / 11) == 8 || Mathf.FloorToInt(randomVerticeNo / 11) == 3 || randomVerticeNo == 29 || randomVerticeNo == 27 || randomVerticeNo == 25 || randomVerticeNo == 84 || randomVerticeNo == 82 || randomVerticeNo == 80);
        }
        //construction signs, crates, cones, snowmen
        else if (sceneryIndexNo > 1 && sceneryIndexNo <= 7)
        {
            randomVerticeNo = Random.Range(57, 63);
        }
        //Sign
        else if (sceneryIndexNo == 9)
        {
            randomVerticeNo = 27;
        }
        else if (sceneryIndexNo == 10 || sceneryIndexNo == 8)
        {
            randomVerticeNo = 104;
        }

        if (GenerateInfinite.tileNameIncrementInLoop < 2)
        {
            meshForScenJump = JumpPlatformScenSpawner.initMeshFilterForScen;
        }

        else if (GenerateInfinite.tileNameIncrementInLoop > 1)
        {
            meshForScenJump = JumpPlatformScenSpawner.meshFilterForScen;
        }

        if (meshForScenJump != null)
        {
            yScenTileVerticeValue = meshForScenJump.vertices[randomVerticeNo].y;
        }

        if (cappedOrNot)
        {
            yscenTileHeightAfterCapping = yScenTileVerticeValue * prevCapY;
        }
        else
        {
            yscenTileHeightAfterCapping = yScenTileVerticeValue * prevScaleY;
        }

        //If lamp post then restrict to the vertice point so can't place multiple next to each other
        if (meshForScenJump != null)
        {
            if (sceneryIndexNo == 1)
            {
                xPositionForScen = GenerateInfinite.frontTileXPosition + Random.Range((meshForScenJump.vertices[randomVerticeNo].x) * prevScaleX, (meshForScenJump.vertices[randomVerticeNo].x) * prevScaleX);
            }
            else if (GenerateInfinite.tileNameIncrementInLoop <= 1)
            {
                xPositionForScen = GenerateInfinite.frontTileXPosition + Random.Range((meshForScenJump.vertices[randomVerticeNo].x - 0.2f) * prevScaleX, (meshForScenJump.vertices[randomVerticeNo].x + 0.2f) * prevScaleX);
            }
            else
            {
                xPositionForScen = GenerateInfinite.frontTileXPosition + Random.Range((meshForScenJump.vertices[randomVerticeNo].x - 0.5f) * prevScaleX, (meshForScenJump.vertices[randomVerticeNo].x + 0.5f) * prevScaleX);
            }


            //makes it so wooden sign isn't off ledge
            if (sceneryIndexNo > 7)
            {
                zPositionforScen = GenerateInfinite.frontTileZPosition + Random.Range((meshForScenJump.vertices[randomVerticeNo].z + 0.3f) * prevScaleZ, (meshForScenJump.vertices[randomVerticeNo].z + 0.5f) * prevScaleZ);
            }
            else
            {
                zPositionforScen = GenerateInfinite.frontTileZPosition + Random.Range((meshForScenJump.vertices[randomVerticeNo].z - 0.5f) * prevScaleZ, (meshForScenJump.vertices[randomVerticeNo].z + 0.5f) * prevScaleZ);
            }
        }

        switch (sceneryIndexNo)
        {
            case 0:
                stationaryScenYAdjustment = new Vector3(0, treeYAdjustment * tree1Scale.y, 0);
                yScaleForEmptyColJump = treeYAdjustment * 2 * emptyScenTreeScalar * tree1Scale.y;
                xSize = Random.Range(tree1XLB, tree1XUB);
                zSize = xSize;
                ySize = Random.Range(2f, 3f) * xSize;
                dimX = treeDimX; dimY = treeDimY; dimZ = treeDimZ;
                break;
            case 1:
                stationaryScenYAdjustment = new Vector3(0, lampPostYAdjustment * lampPost1Scale.y, 0);
                yScaleForEmptyColJump = lampPostYAdjustment * 2 * emptyScenLampPostScalar * lampPost1Scale.y;
                xSize = Random.Range(lampPost1XLB, lampPost1XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = lampPostDimX; dimY = lampPostDimY; dimZ = lampPostDimZ;
                break;
            case 2:
                stationaryScenYAdjustment = new Vector3(0, coneYAdjustment * coneScale.y, 0);
                yScaleForEmptyColJump = coneYAdjustment * 2 * emptyConeScalar * coneScale.y;
                xSize = Random.Range(coneXLB, coneXUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = coneDimX; dimY = coneDimY; dimZ = coneDimZ;
                break;
            case 3:
                stationaryScenYAdjustment = new Vector3(0, crateYAdjustment * crateScale.y, 0);
                yScaleForEmptyColJump = crateYAdjustment * 2 * emptyCrateScalar * crateScale.y;
                xSize = Random.Range(crateXLB, crateXUB);
                zSize = xSize;
                ySize = Random.Range(0.9f, 1.1f) * xSize;
                //Due to scaling in Blender not being 1
                dimX = crateDimX/0.36585f; dimY = crateDimY/0.36585f; dimZ = crateDimZ/0.36585f;
                break;
            case 4:
                stationaryScenYAdjustment = new Vector3(0, snowmanYAdjustment * snowman1Scale.y, 0);
                yScaleForEmptyColJump = snowmanYAdjustment * 2 * emptyScenSnowmanScalar * snowman1Scale.y;
                xSize = Random.Range(snowman1XLB, snowman1XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = snowmanDimX; dimY = snowmanDimY; dimZ = snowmanDimZ;
                break;
            case 5:
                stationaryScenYAdjustment = new Vector3(0, snowmanYAdjustment * snowman2Scale.y, 0);
                yScaleForEmptyColJump = snowmanYAdjustment * 2 * emptyScenSnowmanScalar * snowman2Scale.y;
                xSize = Random.Range(snowman2XLB, snowman2XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = snowmanDimX; dimY = snowmanDimY; dimZ = snowmanDimZ;
                break;
            case 6:
                stationaryScenYAdjustment = new Vector3(0, constructionSign1YAdjustment * constructionSign1Scale.y, 0);
                yScaleForEmptyColJump = constructionSign1YAdjustment * 2 * emptyScenConstructionSign1Scalar * constructionSign1Scale.y;
                xSize = Random.Range(constructionSign1XLB, constructionSign1XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = constructionSign1DimX; dimY = constructionSign1DimY; dimZ = constructionSign1DimZ;
                break;
            case 7:
                stationaryScenYAdjustment = new Vector3(0, constructionSign2YAdjustment * constructionSign2Scale.y, 0);
                yScaleForEmptyColJump = constructionSign2YAdjustment * 2 * emptyScenConstructionSign2Scalar * constructionSign2Scale.y;
                xSize = Random.Range(constructionSign2XLB, constructionSign2XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = constructionSign2DimX; dimY = constructionSign2DimY; dimZ = constructionSign2DimZ;
                break;
            case 8:
                stationaryScenYAdjustment = new Vector3(0, sign3YAdjustment * sign3Scale.y, 0);
                yScaleForEmptyColJump = sign3YAdjustment * 2 * emptySign3Scalar * sign3Scale.y;
                xSize = Random.Range(sign3XLB, sign3XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = sign3DimX; dimY = sign3DimY; dimZ = sign3DimZ;
                break;
            case 9:
                stationaryScenYAdjustment = new Vector3(0, sign2YAdjustment * sign2Scale.y, 0);
                yScaleForEmptyColJump = sign2YAdjustment * 2 * emptySign2Scalar * sign2Scale.y;
                xSize = Random.Range(sign2XLB, sign2XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = sign2DimX; dimY = sign2DimY; dimZ = sign2DimZ;
                break;
            case 10:
                stationaryScenYAdjustment = new Vector3(0, sign1YAdjustment * sign1Scale.y, 0);
                yScaleForEmptyColJump = sign1YAdjustment * 2 * emptySign1Scalar * sign1Scale.y;
                xSize = Random.Range(sign1XLB, sign1XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = sign1DimX; dimY = sign1DimY; dimZ = sign1DimZ;
                break;

        }

        float xRotation = Random.Range(0, 360);
        yRotation = Random.Range(0f, 360f);
        float zRotation = Random.Range(0, 360);

        //To Ensure wooden signs always facing forward and lampposts slight angled
        if(sceneryIndexNo == 1 || sceneryIndexNo == 8 || sceneryIndexNo == 9 || sceneryIndexNo == 10)
        {
            yRotation = Random.Range(-1, 1);
        }

        //Snowmen to face camera
        if (sceneryIndexNo == 4 || sceneryIndexNo == 5)
        {
            yRotation = Random.Range(-210, -150);
        }

        //construction signs rotated
        if (sceneryIndexNo == 6 || sceneryIndexNo == 7)
        {
            yRotation = Random.Range(250, 290);
        }
        scenPositionJump = new Vector3(xPositionForScen, yscenTileHeightAfterCapping, zPositionforScen);
    }

    private void CalculationPositions()
    {
        cappedOrNot = GenerateInfinite.prevMaxHeightTorF;
        prevScaleY = GenerateInfinite.prevNewScaleY;
        prevCapY = GenerateInfinite.prevYScalingCap;
        prevScaleX = GenerateInfinite.prevNewScaleX;
        prevScaleZ = GenerateInfinite.prevNewScaleZ;

        int randomNumber = Random.Range(0, 100);

        if (randomNumber >= 0 && randomNumber < tree1UpperPercentile)
        {
            statSceneryIndexNo = 0;
        }
        else if (randomNumber >= tree1UpperPercentile && randomNumber < lampPost1UpperPercentile)
        {
            statSceneryIndexNo = 1;
        }
        else if (randomNumber >= lampPost1UpperPercentile && randomNumber < snowman1UpperPercentile)
        {
            statSceneryIndexNo = 2;
        }
        else if (randomNumber >= snowman1UpperPercentile && randomNumber < snowman2UpperPercentile)
        {
            statSceneryIndexNo = 3;
        }
        else if (randomNumber >= snowman2UpperPercentile && randomNumber < constructionSign1UpperPercentile)
        {
            statSceneryIndexNo = 4;
        }
        else if (randomNumber >= constructionSign1UpperPercentile && randomNumber < constructionSign2UpperPercentile)
        {
            statSceneryIndexNo = 5;
        }
        else if (randomNumber >= constructionSign2UpperPercentile && randomNumber < igloo1UpperPercentile)
        {
            statSceneryIndexNo = 6;
        }
        else if (randomNumber >= igloo1UpperPercentile && randomNumber < obstructionUpperPercentile)
        {
            statSceneryIndexNo = 7;
        }
        else if (randomNumber >= obstructionUpperPercentile && randomNumber < coneUpperPercentile)
        {
            statSceneryIndexNo = 8;
        }
        else if (randomNumber >= coneUpperPercentile && randomNumber < crateUpperPercentile)
        {
            statSceneryIndexNo = 9;
        }
        else if (randomNumber >= crateUpperPercentile && randomNumber < sign3UpperPercentile)
        {
            statSceneryIndexNo = 10;
        }
        else if (randomNumber >= sign3UpperPercentile && randomNumber < sign2UpperPercentile)
        {
            statSceneryIndexNo = 11;
        }
        else if (randomNumber >= sign2UpperPercentile && randomNumber < sign1UpperPercentile)
        {
            statSceneryIndexNo = 12;
        }

        int randomVerticeNo = 0;

        //use this for testing one object. MUST USE THIS HERE TO GET CORRECT HEIGHTS
        //statSceneryIndexNo = 2;

        do
        {
            randomVerticeNo = Random.Range(0, 121);
        }         
        while (randomVerticeNo == 63 || randomVerticeNo == 46 || randomVerticeNo == 57 || randomVerticeNo == 68 || randomVerticeNo == 35 || randomVerticeNo == 79 ||
            Mathf.FloorToInt(randomVerticeNo / 11) == 0 || Mathf.FloorToInt(randomVerticeNo / 11) == 1 || Mathf.FloorToInt(randomVerticeNo / 11) == 9 || Mathf.FloorToInt(randomVerticeNo / 11) == 10 ||
            randomVerticeNo % 11 == 0 || randomVerticeNo % 11 == 1 || randomVerticeNo % 11 == 9 || randomVerticeNo % 11 == 10);
                
        yScenTileVerticeValue = StationaryScenerySpawner.meshFilterForScen.vertices[randomVerticeNo].y;

        if (cappedOrNot)
        {
            yscenTileHeightAfterCapping = yScenTileVerticeValue * prevCapY;
        }
        else
        {
            yscenTileHeightAfterCapping = yScenTileVerticeValue * prevScaleY;
        }

        xPositionForScen = GenerateInfinite.frontTileXPosition + Random.Range((StationaryScenerySpawner.meshFilterForScen.vertices[randomVerticeNo].x - 0.5f) * prevScaleX, (StationaryScenerySpawner.meshFilterForScen.vertices[randomVerticeNo].x + 0.5f) * prevScaleX);
        zPositionforScen = GenerateInfinite.frontTileZPosition + Random.Range((StationaryScenerySpawner.meshFilterForScen.vertices[randomVerticeNo].z - 0.5f)* prevScaleZ, (StationaryScenerySpawner.meshFilterForScen.vertices[randomVerticeNo].z + 0.5f) * prevScaleZ);

        switch (statSceneryIndexNo)
        {
            case 0:
                stationaryScenYAdjustment = new Vector3(0, treeYAdjustment * tree1Scale.y, 0);
                yScaleForEmptyColStat = treeYAdjustment * 2 * emptyScenTreeScalar * tree1Scale.y;
                xSize = Random.Range(tree1XLB, tree1XUB);
                zSize = xSize;
                ySize = Random.Range(2f, 3f) * xSize;
                dimX = treeDimX; dimY = treeDimY; dimZ = treeDimZ;
                break;
            case 1:
                stationaryScenYAdjustment = new Vector3(0, lampPostYAdjustment * lampPost1Scale.y, 0);
                yScaleForEmptyColStat = lampPostYAdjustment * 2 * emptyScenLampPostScalar * lampPost1Scale.y;
                xSize = Random.Range(lampPost1XLB, lampPost1XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = lampPostDimX; dimY = lampPostDimY; dimZ = lampPostDimZ;
                break;            
            case 2:
                stationaryScenYAdjustment = new Vector3(0, snowmanYAdjustment * snowman1Scale.y, 0);
                yScaleForEmptyColStat = snowmanYAdjustment * 2 * emptyScenSnowmanScalar * snowman1Scale.y;
                xSize = Random.Range(snowman1XLB, snowman1XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = snowmanDimX; dimY = snowmanDimY; dimZ = snowmanDimZ;
                break;
            case 3:
                stationaryScenYAdjustment = new Vector3(0, snowmanYAdjustment * snowman2Scale.y, 0);
                yScaleForEmptyColStat = snowmanYAdjustment * 2 * emptyScenSnowmanScalar * snowman2Scale.y;
                xSize = Random.Range(snowman2XLB, snowman2XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = snowmanDimX; dimY = snowmanDimY; dimZ = snowmanDimZ;
                break;
            case 4:
                stationaryScenYAdjustment = new Vector3(0, constructionSign1YAdjustment * constructionSign1Scale.y, 0);
                yScaleForEmptyColStat = constructionSign1YAdjustment * 2 * emptyScenConstructionSign1Scalar * constructionSign1Scale.y;
                xSize = Random.Range(constructionSign1XLB, constructionSign1XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = constructionSign1DimX; dimY = constructionSign1DimY; dimZ = constructionSign1DimZ;
                break;
            case 5:
                stationaryScenYAdjustment = new Vector3(0, constructionSign2YAdjustment * constructionSign2Scale.y, 0);
                yScaleForEmptyColStat = constructionSign2YAdjustment * 2 * emptyScenConstructionSign2Scalar * constructionSign2Scale.y;
                xSize = Random.Range(constructionSign2XLB, constructionSign2XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = constructionSign2DimX; dimY = constructionSign2DimY; dimZ = constructionSign2DimZ;
                break;
            case 6:
                stationaryScenYAdjustment = new Vector3(0, iglooYAdjustment * igloo1Scale.y, 0);
                yScaleForEmptyColStat = iglooYAdjustment * 2 * emptyIglooScalar * igloo1Scale.y;
                xSize = Random.Range(igloo1XLB, igloo1XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = iglooDimX; dimY = iglooDimY; dimZ = iglooDimZ;
                break;
            case 7:
                stationaryScenYAdjustment = new Vector3(0, obstructionYAdjustment * obstructionScale.y, 0);
                yScaleForEmptyColStat = obstructionYAdjustment * 2 * emptyObstructionScalar * obstructionScale.y;
                xSize = Random.Range(obstructionXLB, obstructionXUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = obstructionDimX; dimY = obstructionDimY; dimZ = obstructionDimZ;
                break;
            case 8:
                stationaryScenYAdjustment = new Vector3(0, coneYAdjustment * coneScale.y, 0);
                yScaleForEmptyColStat = coneYAdjustment * 2 * emptyConeScalar * coneScale.y;
                xSize = Random.Range(coneXLB, coneXUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = coneDimX; dimY = coneDimY; dimZ = coneDimZ;
                break;
            case 9:
                stationaryScenYAdjustment = new Vector3(0, crateYAdjustment * crateScale.y, 0);
                yScaleForEmptyColStat = crateYAdjustment * 2 * emptyCrateScalar * crateScale.y;
                xSize = Random.Range(crateXLB, crateXUB);
                zSize = xSize;
                ySize = Random.Range(0.9f, 1.1f) * xSize;
                //Due to scaling in Blender not being 1
                dimX = crateDimX / 0.36585f; dimY = crateDimY / 0.36585f; dimZ = crateDimZ / 0.36585f;
                break;
            case 10:
                stationaryScenYAdjustment = new Vector3(0, sign3YAdjustment * sign3Scale.y, 0);
                yScaleForEmptyColStat = sign3YAdjustment * 2 * emptySign3Scalar * sign3Scale.y;
                xSize = Random.Range(sign3XLB, sign3XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = sign3DimX; dimY = sign3DimY; dimZ = sign3DimZ;
                break;
            case 11:
                stationaryScenYAdjustment = new Vector3(0, sign2YAdjustment * sign2Scale.y, 0);
                yScaleForEmptyColStat = sign2YAdjustment * 2 * emptySign2Scalar * sign2Scale.y;
                xSize = Random.Range(sign2XLB, sign2XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = sign2DimX; dimY = sign2DimY; dimZ = sign2DimZ;
                break;
            case 12:
                stationaryScenYAdjustment = new Vector3(0, sign1YAdjustment * sign1Scale.y, 0);
                yScaleForEmptyColStat = sign1YAdjustment * 2 * emptySign1Scalar * sign1Scale.y;
                xSize = Random.Range(sign1XLB, sign1XUB);
                zSize = xSize;
                ySize = Random.Range(1f, 1f) * xSize;
                dimX = sign1DimX; dimY = sign1DimY; dimZ = sign1DimZ;
                break;
        }

        float xRotation = Random.Range(0, 360);
        yRotation = Random.Range(0f, 360f);
        float zRotation = Random.Range(0, 360);

        //snowmen rotated to face camera
        if (statSceneryIndexNo == 2 || statSceneryIndexNo == 3)
        {
            yRotation = Random.Range(-120, -60);
        }

        //construction signs rotated
        if (statSceneryIndexNo == 4 || statSceneryIndexNo == 5)
        {
            yRotation = Random.Range(250, 290);
        }

        scenPositionStat = new Vector3(xPositionForScen, yscenTileHeightAfterCapping, zPositionforScen);
    }

    
    private void OnDrawGizmos()
    {
        //Used in Testing
        //THIS IS THE LAST SPHERE TEST THAT KEPT THEM DRAWN ON. hAVE TO CHANGE SPHERE CHECK BOOL AT TOP OF THIS SCRIPT
        
        /*
        if (sphereArray != null)
        {
                for (int i = 0; i < sphereArray.Length; i++)
                {
                        Gizmos.DrawSphere(new Vector3(sphereArray[i].transform.position.x, sphereArray[i].transform.position.y + yAdjArray[i] - (sphereArray[i].transform.localScale.y / 2f)*0, sphereArray[i].transform.position.z),
                            Mathf.Max(xRadArray[i], zRadArray[i]) * sphereCheckScalar);

                }
        }

        if (jumpSphereArray != null)
        {
                for (int i = 0; i < jumpSphereArray.Length; i++)
                {
                    Gizmos.DrawSphere(new Vector3(jumpSphereArray[i].transform.position.x, jumpSphereArray[i].transform.position.y + yAdjJumpArray[i] - (jumpSphereArray[i].transform.localScale.y / 2f)*0, jumpSphereArray[i].transform.position.z),
                        Mathf.Max(xRadJumpArray[i], zRadJumpArray[i]) * sphereCheckScalar);
                }
        }   
        */
    }
    

    IEnumerator ABC()
    {

        //returning 0 will make it wait 1 frame

        for (int i = 0; i < 2; i++)
        {
            yield return null;
        }
            EmptyJumpSpawnProcess();            

    }

    private void setProgressionScenSizes()
    {
        float xPlayer = PlayerController.playerX;
        //float metresChange = 1000f;

        tree1XLB = Mathf.Min(3.5f + (xPlayer / 5000f), 7.5f); tree1XUB = Mathf.Min(5.5f + xPlayer / 5000f, 8.5f);
        lampPost1XLB = 4f; ; lampPost1XUB = 4f;
        snowman1XLB = Mathf.Min(4f + (xPlayer / 7500f), 6); snowman1XUB = Mathf.Min(6f + (xPlayer / 7500f), 8);
        snowman2XLB = Mathf.Min(4f + (xPlayer / 7500f), 6); snowman2XUB = Mathf.Min(6f + (xPlayer / 7500f), 8);
        //coneXLB = 4f; ; coneXUB = 4f;
        coneXLB = 5f; ; coneXUB = 5f;
        crateXLB = Mathf.Min(1f + (xPlayer / 7500f), 3); crateXUB = Mathf.Min(2f + (xPlayer / 7500f), 4);
        //crateXLB = 1.0f; crateXUB = 1.0f;
        igloo1XLB = Mathf.Min(3f + (xPlayer / 10000f), 4); igloo1XUB = Mathf.Min(4f + (xPlayer / 10000f), 5);
        constructionSign1XLB = 4; constructionSign1XUB = 4;
        constructionSign2XLB = 4; constructionSign2XUB = 4;
        sign1XLB = 9; sign1XUB = 9;
        sign2XLB = 8; sign2XUB = 8;
        sign3XLB = 7; sign3XUB = 7;
        obstructionXLB = 2; obstructionXUB = 5;
    }

}
