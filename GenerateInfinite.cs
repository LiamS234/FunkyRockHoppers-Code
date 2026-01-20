using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GenerateInfinite : MonoBehaviour
{
    public GameObject player;
    public static int planeSize = 10;
    public static int noOfTilesOnScreen = 5 + 1;
    public static int tileNameIncrementInLoop = 0;
    public static bool scenTileUpdate = false;
    public static bool fishTileUpdate = false;
    public static bool sharkTileUpdate = false;
    public static bool birdTileUpdate = false;
    public static bool jumpPlatformTileUpdate = false;
    public static bool movingScenTileUpdate = false;
    string tilename;
    public static int frontTileNumber = 0;
    public static float prevTileRightVertice = 0f;
    public static float birdTileRightVertice = 0f;
    public static float prevTileRightVerticeForCam = 0f;
    public static float prevTileLeftVertice = 0f;
    private float prevLeftVertice = 0f;

    public static float randomGapSize = 0f;
    public static float furthestPlatformXPoint = -5f;
    public static float frontTileXPosition = 0f;
    public static float frontTileZPosition = 0f;

    public static float newScaleX;
    private float scaleYLowerBound;
    private float scaleYUpperBound;
    public static float newScaleY = 0f;
    public static float newScaleZ;
    
    private float simplifiedMaxGapDistance;
    private float adjustedSimplifiedMaxGapDistance;
    private float simplifiedMinGapDistance;
    private float adjustedSimplifiedMinGapDistance;
    private float fullJumpGravity;
    private float lowJumpGravity;
    public static float maxHeightDifference = 0f;
    public static float playerFullJumpHeight = 0f;
    private float jumpVelocity;

    private float nextTileYAfterHeightCheck;

    public static float frontTileYAfterHeightCheck = 0f;

    public static Mesh meshFilter = null;
    public static Mesh birdMeshFilter = null;

    private Vector3 playerStartPosition;

    private Vector3 pos;
    private Vector3 posAdjusted;

    public static float leftVertice;
    public static float frontTileLeftVerticeX = 0f;
    public static float frontTileRightVerticeX = 0f;
    public static float prevFrontTileLeftVerticeX = 0f;
    public static float prevFrontTileRightVerticeX = 0f;
    float frontTileScaleY = 0f;
    float birdTileScaleY = 0f;
    public static float frontTileScaleX = 0f;

    float nextTileScaleY = 0f;

    public GenerateTerrain plane;

    public GenerateTerrain newTile = null;

    public static int pickALongPlatform;
    public static bool longPlatformOnScreen = false;

    public static int longPlatformTileNumber = 0;
    public static List<int> statLongPlatformTileNoList = new List<int>();
    public static List<int> jumpLongPlatformTileNoList = new List<int>();
    public static bool longPlatformHasBeenPicked = false;

    public static float longPlatformDistFromEndToSwitchCameraBack = 0;
    public static float largestYLongPlatformPos = 0f;

    public static bool cameraSwitch = false;
    public static bool cameraSideOn = false;
    
    //For stationary scenery placement (trees, lampposts etc.)
    public static float yHeightforStationaryScen;
    public static float yScalingCap;    
    public static float positiveZlimit;
    public static float negativeZlimit;

    public static bool maxHeightTorF;
    public static bool prevMaxHeightTorF;
    public static float varForScenVertice;
    public static float scenVertice;

    public static float prevNewScaleY;
    public static float prevYScalingCap;

    public static float prevNewScaleX;
    public static float prevNewScaleZ;

    int iPlusIncrement;

    public static List<float> startXCamSwitchList = new List<float>();
    public static List<float> endXCamSwitchList = new List<float>();
    public static List<float> frontZCamSwitchList = new List<float>();
    public static List<float> backZCamSwitchList = new List<float>();
    public static List<float> startXSideOnCamList = new List<float>();
    public static List<float> endXSideOnCamList = new List<float>();
    float startXCamSwitch;
    float endXCamSwitch;
    float frontZCamSwitch;
    float backZCamSwitch;
    GameObject longPlatformTile;
    public static bool isPlayerXPastSideOnCamSwitch;

    public static float startXSideOnCam;
    float endXSideOnCam;

    public static bool isItALongPlatform = false;

    public static float fishTileHeightDifference;
    public static float yScaledMaxDifference;

    public static GameObject frontTile;
    public static GameObject birdTile;
    GameObject nextTile;
    bool hasNextTileGOBeenFound = false;

    public static bool longGap = false;

    bool hasUFOHeightIndexZeroBeenCleared = false;

    float jPFScaleXLowerBound;
    float jPFScaleZLowerBound;
    float jPFScaleXUpperBound;
    float jPFScaleZUpperBound;

    public GameObject firstTile;
    float speed;

    public static float birdTileEndHeight;

    void Start()
    {
        ResetValues();
        gameObject.transform.position = Vector3.zero;
        jumpVelocity = PlayerController.jumpVelocity;
        playerFullJumpHeight = Mathf.Pow(jumpVelocity, 2) / (2 * Mathf.Abs(Physics.gravity.y) * PlayerController.fullJumpGravityMultiplier);
        fullJumpGravity = Mathf.Abs(Physics.gravity.y * PlayerController.fullJumpGravityMultiplier);
        lowJumpGravity = Mathf.Abs(Physics.gravity.y * PlayerController.lowJumpGravityMultiplier);       
    }

    //3.START     **********************************IF PLAYER MOVES CERTAIN DISTANCE (1/3RD) THEN..... NEW SCALING FACTORS CALCULATED AND TILENAMEINCREMENTINLOOP HAS 1 ADDED*************************//
    void Update()
    {
        speed = PlayerController.speed;

        //determine how far the player has moved since last tile update i.e. resets playerStartPosition to current player position at end of tile update
        int xMove = (int)(player.transform.position.x - playerStartPosition.x);

        //New tiles only created once the movement since last update is more than a 3rd of the distance between right x on furthest right tile and the players x reset start position (x at last tile update)
        if (Mathf.Abs(xMove) >= (furthestPlatformXPoint - playerStartPosition.x) / 2)
        {
            scenTileUpdate = true;
            //print("GI" + tileNameIncrementInLoop);
            hasNextTileGOBeenFound = false;
            //This is to remove the first fish (tileUpdate used in 'FishSpawner' script between the already placed start platform and the first generated platform (avoids manual placement issues))
            if (tilename != "Tile_" + noOfTilesOnScreen)
            {           
                jumpPlatformTileUpdate = true;
                movingScenTileUpdate = true;
                fishTileUpdate = true;
                sharkTileUpdate = true;
                birdTileUpdate = true;               
            }
            else
            {
                jumpPlatformTileUpdate = false;
                movingScenTileUpdate = false;
                fishTileUpdate = true;
                sharkTileUpdate = true;
                birdTileUpdate = true;
            }

            prevNewScaleY = newScaleY;
            prevNewScaleX = newScaleX;
            prevNewScaleZ = newScaleZ;
            prevYScalingCap = yScalingCap;

            scaleYLowerBound = Mathf.Min(0.1f + Mathf.Floor(Mathf.Max(player.transform.position.x, 0) / 100f) / 5f, 3.0f);
            scaleYUpperBound = Mathf.Min(0.1f + Mathf.Floor(Mathf.Max(player.transform.position.x, 0) / 75f) / 5f, 5.5f);
            newScaleY = Random.Range(scaleYLowerBound, scaleYUpperBound);
            
            //For stationary scenery placement (trees, lampposts etc.)
            positiveZlimit = (newScaleZ * planeSize) / 2f - ((newScaleZ * planeSize) / 10f);
            negativeZlimit = -(newScaleZ * planeSize) / 2f + ((newScaleZ * planeSize) / 10f);

            pickALongPlatform = Random.Range(0, 10);

            if(tileNameIncrementInLoop == 0)
            {
                newScaleX = 15;
                newScaleZ = 3;
            }
            //else if (pickALongPlatform > 5 && tileNameIncrementInLoop > 1)
            else if ((pickALongPlatform == 0 || pickALongPlatform == 1) && tileNameIncrementInLoop > 1)
            {
                iPlusIncrement = tileNameIncrementInLoop + 1;
                isItALongPlatform = true;

                float lPFScaleXLowerBound = Mathf.Min(20 + (float)(player.transform.position.x / 500f), 60); float lPFScaleXUpperBound = Mathf.Min(25 + (float)(player.transform.position.x / 500f), 65);
                float lPFScaleZLowerBound = Mathf.Max(10 - (float)(player.transform.position.x / 4000f), 5); float lPFScaleZUpperBound = Mathf.Max(11 - (float)(player.transform.position.x / 4000f), 6);

                newScaleX = Random.Range(lPFScaleXLowerBound, lPFScaleXUpperBound);
                newScaleZ = Random.Range(lPFScaleZLowerBound, lPFScaleZUpperBound);
            } else
            {
                isItALongPlatform = false;
                jPFScaleXLowerBound = Mathf.Max(6 - (float)(player.transform.position.x / 6000f), 3); jPFScaleXUpperBound = Mathf.Max(7 - (float)(player.transform.position.x / 6000f), 4);
                jPFScaleZLowerBound = Mathf.Max(2.5f - (float)(player.transform.position.x / 10000f), 1.5f); jPFScaleZUpperBound = Mathf.Max(3 - (float)(player.transform.position.x / 10000f), 2f);
                newScaleX = Random.Range(jPFScaleXLowerBound, jPFScaleXUpperBound);
                newScaleZ = Random.Range(jPFScaleZLowerBound, jPFScaleZUpperBound);

                //print("XLB: " + jPFScaleXLowerBound + "   XUB: " + jPFScaleXUpperBound + "   ZLB: " + jPFScaleZLowerBound + "   ZUB: " + jPFScaleZUpperBound); 

                //newScaleX = 10;

            }
            tileNameIncrementInLoop++;

            //3.END     **********************************IF PLAYER MOVES CERTAIN DISTANCE (1/3RD) THEN..... NEW SCALING FACTORS CALCULATED AND TILENAMEINCREMENTINLOOP HAS 1 ADDED*************************//

            //4 START *********************************IF frontTileNumber > 5: Applies meshfilter to fronttile, saves new Scaling to frontTile in order to calculate position of next new tile
            //***************************************** gets prevTileRightVertice and lastly it applies the scaling to the NEW tile about to be set **********************************************
            //********************************Give front tile it's string name, instantiates if not in hashtable otherwise sets creationtime=update time to keep it in hashtable (condition later)//            
                if (frontTileNumber > 0)
                {
                //When a new tile is added (further down the code) frontTileNumber is increased by 1 (tileNameIncrementInLoop)
                //frontTile = 6 on first run. if x == 0 is so it only runs this section of code once per tile update
                //print(GameObject.Find("Tile_" + (frontTileNumber)));
                        frontTile = GameObject.Find("Tile_" + (frontTileNumber));
                        meshFilter = frontTile.GetComponent<MeshFilter>().mesh;

                        //This is to save the New scaleX calculated above into the variable frontTileScaleX in order to calculate the furthestPlatformXPoint below
                        frontTileScaleY = frontTile.transform.localScale.y;
                        frontTileScaleX = frontTile.transform.localScale.x;

                        prevTileRightVertice = meshFilter.vertices[56].y;

                        //used to get fish height ranges
                        prevTileLeftVertice = meshFilter.vertices[64].y * frontTileScaleY;
                        prevTileRightVerticeForCam = meshFilter.vertices[56].y * frontTileScaleY;

                        frontTileXPosition = frontTile.transform.position.x;
                        frontTileZPosition = frontTile.transform.position.z;
                        furthestPlatformXPoint = frontTileXPosition + ((frontTileScaleX * planeSize) / 2) - (frontTileScaleX * planeSize) / 10;


                        pos = new Vector3((furthestPlatformXPoint + (newScaleX * planeSize) / 2) - (newScaleX * planeSize) / 10, 0, 0);
                        plane.gameObject.transform.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);

                }
                else
                {
                    //Just for when the last tile number is zero i.e. the default number on the first run of code, so for the first tile... Tile_6
                    plane.gameObject.transform.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);
                }


            if (frontTileNumber > 1)
            {
                birdTile = GameObject.Find("Tile_" + (frontTileNumber - 1));
                birdMeshFilter = birdTile.GetComponent<MeshFilter>().mesh;
                birdTileScaleY = birdTile.transform.localScale.y;
                birdTileRightVertice = birdMeshFilter.vertices[56].y;
            }


            //run a method where all the y edge vertices are shaped.
            //edgeVerticeShaping();

            tilename = "Tile_" + (tileNameIncrementInLoop).ToString();
            //print(tilename);

            //****************************************************************INSTANTIATE NEW TILE HERE and set name. *****************************************************************
            newTile = Instantiate(plane, pos, Quaternion.identity);
                newTile.name = tilename;
            //instantiated new tile

            frontTileNumber = tileNameIncrementInLoop;

            //print("Game object: " + GameObject.Find("Tile_" + (tileNameIncrementInLoop - 1)) + "     inc: " + (tileNameIncrementInLoop - 1));
            if (tileNameIncrementInLoop - 5 > 0)
            {
                //print(GameObject.Find("Tile_" + (tileNameIncrementInLoop - 5)));
                Destroy(GameObject.Find("Tile_" + (tileNameIncrementInLoop - 5)));
            }

            playerStartPosition = player.transform.position;
            plane.gameObject.transform.localScale = new Vector3(1, 1, 1);

            if (tileNameIncrementInLoop == iPlusIncrement && iPlusIncrement != 0)
            {
                //print("incInLoop: " + tileNameIncrementInLoop + "   iplusInc: " + iPlusIncrement);
                longPlatformTileNumber = (tileNameIncrementInLoop);
                statLongPlatformTileNoList.Add(longPlatformTileNumber);
                jumpLongPlatformTileNoList.Add(longPlatformTileNumber);
                
                longPlatformTile = GameObject.Find("Tile_" + (longPlatformTileNumber));
                //print("HERE: " + longPlatformTile.name);
                startXCamSwitch = (longPlatformTile.transform.position.x - (longPlatformTile.transform.localScale.x * planeSize) / 2 + (longPlatformTile.transform.localScale.x * planeSize) / 10);
                endXCamSwitch = (longPlatformTile.transform.position.x + (longPlatformTile.transform.localScale.x * planeSize) / 2 - (longPlatformTile.transform.localScale.x * planeSize) / 10);
                //ExtDebug.DrawBoxCastBox(new Vector3(startXCamSwitch + ((endXCamSwitch - startXCamSwitch) / 2f), 0, 0), new Vector3(((endXCamSwitch - startXCamSwitch) / 2f), 50, 50), Quaternion.identity, transform.up, 25, Color.red);
                frontZCamSwitch = (-(longPlatformTile.transform.localScale.z * 10) / 2) + ((longPlatformTile.transform.localScale.z * 10) / 10);
                backZCamSwitch = (longPlatformTile.transform.localScale.z * 10) / 2 - ((longPlatformTile.transform.localScale.z * 10) / 10);
            }
            
        }

        //TESTING//
        //ExtDebug.DrawBoxCastBox(new Vector3(startXCamSwitch + ((endXCamSwitch - startXCamSwitch) / 2f), 0, 0), new Vector3(((endXCamSwitch - startXCamSwitch) / 2f), 50, 50), Quaternion.identity, transform.up, 40, Color.red);

        //6. START ********************* ONCE THE IF STATEMENT WITH THE FOR LOOP HAS FINISHED:
        //******************************* nextTile is given the frontTileNumber after being increased by 1 above
        //print(hasNextTileGOBeenFound);
        if (!hasNextTileGOBeenFound)
        {
            
            nextTile = GameObject.Find("Tile_" + (frontTileNumber));
            //print(nextTile.name);
            meshFilter = nextTile.GetComponent<MeshFilter>().mesh;
            hasNextTileGOBeenFound = true;
        }
            nextTileScaleY = nextTile.transform.localScale.y;
            leftVertice = meshFilter.vertices[64].y;
            
        

        if (leftVertice != 0 && leftVertice != prevLeftVertice && prevTileRightVertice != 0)
        {
            yScaledMaxDifference = +(prevTileRightVertice * frontTileScaleY - leftVertice * nextTileScaleY);

            birdTileEndHeight = birdTileRightVertice * birdTileScaleY;

            simplifiedMaxGapDistance = (speed / fullJumpGravity) * (jumpVelocity + Mathf.Max(Mathf.Sqrt(Mathf.Pow(jumpVelocity, 2) + (2 * fullJumpGravity * yScaledMaxDifference)), 0));
            adjustedSimplifiedMaxGapDistance = Mathf.Max(simplifiedMaxGapDistance - newScaleX - frontTileScaleX, 5);

            simplifiedMinGapDistance = (speed / lowJumpGravity) * (jumpVelocity + Mathf.Max(Mathf.Sqrt(Mathf.Pow(jumpVelocity, 2) + (2 * lowJumpGravity * yScaledMaxDifference)), 0));
            adjustedSimplifiedMinGapDistance = Mathf.Max(simplifiedMinGapDistance - newScaleX - frontTileScaleX, 20);           
            randomGapSize = Random.Range(adjustedSimplifiedMinGapDistance * 1.2f, adjustedSimplifiedMaxGapDistance * 0.8f);
            float randomGapSizeForMaxHeightDiff = randomGapSize;

            //create a section where you have to dodge shark below... and birds above, just use snowman for now...
            //FIRST IF STATEMENT MEANS A NORMAL JUMP PLATFORM CREATED.
            if(pickALongPlatform >= 2 && pickALongPlatform <= 7 || PlayerController.metresTravelled < 50)
            //if(pickALongPlatform > 5)
            //if(pickALongPlatform == 0 || pickALongPlatform == 1 || pickALongPlatform == 2 || pickALongPlatform == 3 || pickALongPlatform == 4 || pickALongPlatform == 5)
            {
                randomGapSize = Random.Range(adjustedSimplifiedMinGapDistance * 1.2f, adjustedSimplifiedMaxGapDistance * 0.8f);
                longGap = false;
            }
            //else if (pickALongPlatform > 5)
            else if (pickALongPlatform > 7)
            {
                float longGapLowerBound = Mathf.Min(75 + (float)(player.transform.position.x / 67f), 375); float longGapUpperBound = Mathf.Min(125 + (float)(player.transform.position.x / 67f), 425);
                //print("long gap LB: " + longGapLowerBound + "   long gap UB: " + longGapUpperBound); 
                randomGapSize = Random.Range(longGapLowerBound, longGapUpperBound);
                longGap = true;
                startXSideOnCam = (frontTile.transform.position.x + (frontTile.transform.localScale.x * planeSize) / 2 - (frontTile.transform.localScale.x * planeSize) / 10);
                endXSideOnCam = (nextTile.transform.position.x - (nextTile.transform.localScale.x * planeSize) / 2 + (nextTile.transform.localScale.x * planeSize) / 10) + randomGapSize;
                startXSideOnCamList.Add(startXSideOnCam);
                endXSideOnCamList.Add(endXSideOnCam);
            }

            posAdjusted = pos + new Vector3(randomGapSize, 0, 0);
            

            nextTile.gameObject.transform.position = posAdjusted;

            float randomGapSizeForSideOnCamXCalcs = randomGapSize;

            if(frontTileNumber == longPlatformTileNumber)
            {
                
                startXCamSwitch = startXCamSwitch + randomGapSizeForSideOnCamXCalcs;
                float propOfPFToSwitchCamBack = 0.15f;
                endXCamSwitch = endXCamSwitch + randomGapSizeForSideOnCamXCalcs;
                endXCamSwitch = endXCamSwitch - ((endXCamSwitch - startXCamSwitch) * propOfPFToSwitchCamBack);
                startXCamSwitchList.Add(startXCamSwitch);
                endXCamSwitchList.Add(endXCamSwitch);
                frontZCamSwitchList.Add(frontZCamSwitch);
                backZCamSwitchList.Add(backZCamSwitch);
            }
           
            prevMaxHeightTorF = maxHeightTorF;
            maxHeightDifference = (Mathf.Pow((((randomGapSizeForMaxHeightDiff * (Mathf.Abs(Physics.gravity.y) * PlayerController.fullJumpGravityMultiplier)) / speed) - jumpVelocity), 2) - Mathf.Pow(jumpVelocity, 2)) / (2 * Mathf.Abs(Physics.gravity.y) * PlayerController.fullJumpGravityMultiplier);
            yScalingCap = Mathf.Min(leftVertice * nextTileScaleY, prevTileRightVertice * frontTileScaleY + -maxHeightDifference * 0.5f) / leftVertice;

            if (yScaledMaxDifference < maxHeightDifference * 0.5f)
            {
                //The Y term caps the scaling such that the height difference can't be more than 6 (rough figure for how high penguin can jump)
                nextTile.gameObject.transform.localScale = new Vector3(newScaleX, yScalingCap, newScaleZ);
                nextTileYAfterHeightCheck = maxHeightDifference;
                yHeightforStationaryScen = leftVertice * yScalingCap;
                maxHeightTorF = true;
            }
            else
            {
                nextTile.gameObject.transform.localScale = new Vector3(newScaleX, newScaleY, newScaleZ);
                nextTileYAfterHeightCheck = yScaledMaxDifference;
                yHeightforStationaryScen = leftVertice * newScaleY;
                maxHeightTorF = false;
            }

            fishTileHeightDifference = leftVertice * yScalingCap - prevTileRightVertice * frontTileScaleY;
            frontTileYAfterHeightCheck = nextTileYAfterHeightCheck;
            prevLeftVertice = leftVertice;           
        }


        if (IsCamSideOnTrue())
        {
            cameraSideOn = true;
        } else
        {
            cameraSideOn = false;
        }

        if (IsCamSwitchTrue())
        {            
            cameraSwitch = true;
            hasUFOHeightIndexZeroBeenCleared = false;
        }
        else
        {
            cameraSwitch = false;

            if (!hasUFOHeightIndexZeroBeenCleared)
            {
                hasUFOHeightIndexZeroBeenCleared = true;
                if (StationaryScenerySpawner.yLPFHeightForUFORef.Count > 0)
                {
                    StationaryScenerySpawner.yLPFHeightForUFORef.RemoveAt(0);
                }
            }
        }

        if ((endXSideOnCamList.Count > 0 && startXSideOnCamList.Count > 0) && player.transform.position.x >= (endXSideOnCamList[0]))
        {
            int indexToRemove0 = startXSideOnCamList.IndexOf(startXSideOnCamList.Min());
            startXSideOnCamList.RemoveAt(indexToRemove0);

            int indexToRemove1 = endXSideOnCamList.IndexOf(endXSideOnCamList.Min());
            endXSideOnCamList.RemoveAt(indexToRemove1);
        }

        if ((endXCamSwitchList.Count > 0 && startXCamSwitchList.Count > 0) && player.transform.position.x >= (endXCamSwitchList[0]))
        {
            int indexToRemove0 = startXCamSwitchList.IndexOf(startXCamSwitchList.Min());
            startXCamSwitchList.RemoveAt(indexToRemove0);

            int indexToRemove1 = endXCamSwitchList.IndexOf(endXCamSwitchList.Min());
            endXCamSwitchList.RemoveAt(indexToRemove1);

            int indexToRemove2 = frontZCamSwitchList.IndexOf(frontZCamSwitchList.Min());
            frontZCamSwitchList.RemoveAt(indexToRemove2);

            int indexToRemove3 = backZCamSwitchList.IndexOf(backZCamSwitchList.Min());
            backZCamSwitchList.RemoveAt(indexToRemove3);
        }        
    }

    private bool IsCamSideOnTrue()
    {
        if (startXSideOnCamList.Count > 0 && endXSideOnCamList.Count > 0)
        {
            for (int i = 0; i < startXSideOnCamList.Count; i++)
            {
                //print("start x: " + startXSideOnCamList[i] + "   end x: " + endXSideOnCamList[i] + "   increment: " + i);

                if ((player.transform.position.x > startXSideOnCamList[i] && player.transform.position.x < endXSideOnCamList[i]))
                {
                    return true;
                }                
            }            
        }
        return false;
    }

    private bool IsCamSwitchTrue()
    {
        if (startXCamSwitchList.Count > 0 && endXCamSwitchList.Count > 0)
        {
            for (int i = 0; i < startXCamSwitchList.Count; i++)
            {
                if ((player.transform.position.x > startXCamSwitchList[i] && player.transform.position.x < endXCamSwitchList[i]))
                {
                    
                    return true;
                }
            }
        }
        return false;
    }

    public void ResetValues()
    {
        frontTileLeftVerticeX = 0f;
        frontTileRightVerticeX = 0f;
        prevFrontTileLeftVerticeX = 0f;
        prevFrontTileRightVerticeX = 0f;
        tileNameIncrementInLoop = 0;
        scenTileUpdate = false;
        jumpPlatformTileUpdate = false;
        movingScenTileUpdate = false;
        planeSize = 10;
        prevTileRightVertice = 0f;
        prevTileLeftVertice = 0f;
        furthestPlatformXPoint = -5f;
        randomGapSize = 0f;
        frontTileXPosition = 0f;
        newScaleX = 0f;
        newScaleY = 0f;
        maxHeightTorF = false;
        yScalingCap = 0;
        maxHeightDifference = 0f;        
        playerFullJumpHeight = 0f;
        frontTileYAfterHeightCheck = 0f;
        frontTileScaleX = 0f;  
        longPlatformOnScreen = false;
        longPlatformTileNumber = 0;
        largestYLongPlatformPos = 0f;
        longPlatformHasBeenPicked = false;
        longPlatformDistFromEndToSwitchCameraBack = 50;
        cameraSwitch = false;
        isItALongPlatform = false;
        startXCamSwitchList.Clear();
        endXCamSwitchList.Clear();
        frontZCamSwitchList.Clear();
        backZCamSwitchList.Clear();
        startXSideOnCamList.Clear();
        endXSideOnCamList.Clear();
        statLongPlatformTileNoList.Clear();
        jumpLongPlatformTileNoList.Clear();
        frontTileNumber = 0;
    }
}

