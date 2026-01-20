using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float speed;
    private float minSpeed = 20f;
    private float maxSpeed = 100f;
    private float incrementalSpeedIncrease = 1f;
    private int speedIncrement;
    private int previousSpeedIncrement;
    private float speedIncreaseAmount = 0.60f;
    private float speedDecreaseAmount = 0.50f;

    float speedPreviousIncrement;

    int prevVelInc = 0;
    int currVelInc;

    public static float increasedSpeed;
    private float decreasedSpeed;
    private float currentSpeed;
    private float rightCurrentSpeed;
    private float leftCurrentSpeed;
    private float upCurrentSpeed;
    private float downCurrentSpeed;
    public float modLerpPercComplete;
    public int lerpPercFlooredToInt;
    public int prevLerpPercFlooredToInt;
    private float speedChangeLerpDuration = 0.1f;
    private float speedReturnLerpDuration = 0.1f;
    private float speedIncrementLerpDuration = 0.1f;
    public float speedChangeLerpPercentageComplete;
    public float speedReturnLerpPercentageComplete;
    public float speedIncrementLerpPercentageComplete;
    public static float elapsedTime = 0f;
    float speedChangeElapsedTime = 0f;
    float speedReturnElapsedTime = 0f;
    public float speedIncrementElapsedTime = 0f;
    public static float secondsSinceLastSpeedIncrement = 0f;
    public static float secondsSinceLastSpeedInput = 0f;

    private Vector3 constantVelocity;
    private Vector3 increasedVelocity;
    private Vector3 decreasedVelocity;

    bool hasCurrentSpeedRun = false;
    bool hasRightCurrentSpeedRun = false;
    bool hasLeftCurrentSpeedRun = false;
    bool hasUpCurrentSpeedRun = false;
    bool hasDownCurrentSpeedRun = false;

    [SerializeField] public AnimationCurve speedIncreaseCurve;

    public static float metresTravelled;

    public static float fullJumpGravityMultiplier = 4f;
    public static float lowJumpGravityMultiplier = 10f;
    private float flapVelocity = 30f;
    public static float jumpVelocity = 35f;
    public static float flapsRemaining;
    public static Vector3 cameraAdjustForSpeedIncrement = new Vector3(0, 0, 0);
    private float cameraZAdjustment;
    private float previouscameraZAdjustment;

    public static bool isGrounded;
    public static bool isGroundedExtended;
    private bool tapSpace;
    private bool holdSpace;
    private bool holdLeft;
    private bool holdRight;
    private bool holdUp;
    private bool holdDown;
    private float strafeLeftSpeed = 10f;
    private float strafeRightSpeed = -10f;

    [SerializeField] LayerMask groundLayers;

    private float yGroundHitPoint;
    private float groundRayCheckDistance;
    private float RayFrontCheckDistance;
    private float groundRayCheckDistanceError;
    private float airRayCheckDistance;

    Ray groundRayMid;
    Ray groundAboveRay;
    Ray rayFrontCheck;
    Ray airRay;

    private float groundDegreesRotationPerSecond = 1080f;
    private float jumpRotationPerSecond = 1080f;

    //Based on size of fish
    public int fish1FlapIncrement;
    public int fish2FlapIncrement;

    float lastPressedButton = -9999f;
    float intervalSinceLastButtonPress = 0.05f;

    private Rigidbody rigidbody;
    RaycastHit hitInfoAir;
    bool midRayCastDetect;
    bool groundAboveRayCastDetect;
    bool isPlayerOutsideOfCentreWhileInSideView = false;
    public bool scoreSubmitted = false;
    public static float playerHeight;
    public static float playerX;
    Collider collider;

    public static Vector3 currentScale;

    bool isFallingThruGround = false;

    float yDim = 0.057241f;
    float xDim = 0.248621f;
    //Extended ray to ensure correct SFX plays i.e. jumpBass rather than flap as well as jump rather than flap and ensure rotation works
    Ray groundingRay, fallingThruRay, isGroundedExtendedRay;
    bool fallingThruDetect;
    public static bool isGroundedExtendedRayDetect;
    public static bool groundingDetect;
    Vector3 topPlayerYPoint;
    float heightOfPlayer;

    Vector3 rotationVelocity;
    Vector3 airToGroundRotationVelocity;
    Quaternion deltaRotation;
    Vector3 isGroundedExtendedEndPoint;
    RaycastHit extendedRayHitInfo;
    Vector3 hitInfoNormalGrounding;

    bool testing = false;

    private void Awake()
    {
        scoreSubmitted = false;
        rigidbody = GetComponent<Rigidbody>();
        //hasPlayBeenPressed = false;
    }

    private void Start()
    {
        ResetValues();

        heightOfPlayer = transform.localScale.y * yDim;
        airRayCheckDistance = heightOfPlayer * 10f;

        flapsRemaining = 0;

        if (testing)
        {
            collider = GetComponent<Collider>();
            minSpeed = 15f;
            speedIncreaseAmount = 30f;
            speedDecreaseAmount = 0.99f;
        }

        //currentScale = transform.localScale;
    }

    void Update()
    {

        //print("PlayPressed?   " + hasPlayBeenPressed);

        if (testing)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            collider.enabled = false;
        }

        var screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if(screenPos.y > Screen.height)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -10, rigidbody.velocity.z);
        }

        GetButtonInputs();

        playerHeight = transform.position.y;
        playerX = transform.position.x;

        groundRayCheckDistanceError = 0.5f * transform.localScale.y/5f;
        groundRayCheckDistance = ((transform.localScale.y/5f) / 2f + groundRayCheckDistanceError)*0 + 3f;
        
        RayFrontCheckDistance = (transform.localScale.x / 2f)* xDim + (transform.localScale.x * 0.05f)* xDim;
    }

    private void FixedUpdate()
    {
        GroundingCheck();
        ToggleZMovementRestrictionForCameraChanges();
        metresTravelled = Mathf.Max(Mathf.FloorToInt(transform.position.x + 20), 0);
        elapsedTime += Time.fixedDeltaTime;
        speedChangeLerpPercentageComplete = Mathf.Min(speedChangeElapsedTime / speedChangeLerpDuration, 1);
        speedReturnLerpPercentageComplete = Mathf.Min(speedReturnElapsedTime / speedReturnLerpDuration, 1);
        speedIncrementLerpPercentageComplete = Mathf.Min(speedIncrementElapsedTime / speedIncrementLerpDuration, 1);

        if (GameOver.gameEnded == false)
        {
            Move();
            Jump();
            GroundRotation();
        }
    }

    private void GetButtonInputs()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            holdLeft = true;
        }
        else
        {
            holdLeft = false;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            holdRight = true;
        }
        else
        {
            holdRight = false;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            holdUp = true;
        }
        else
        {
            holdUp = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            holdDown = true;
        }
        else
        {
            holdDown = false;
        }

        if (Input.GetButton("Jump"))
        {
            holdSpace = true;
        }
        else
        {
            holdSpace = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            tapSpace = true;
        }
        else
        {
            //tapSpace = false;
        }
    }

    //***FIXED UPDATE***// --- VIA JUMP FUNCTION
    //***** If statement checks if they are within the distance to ground in order to rotate towards slope, else it will straighten up at peak of jump
    private void AirToGroundRotation()
    {
        //*****Raycast originating from centre of player to the ground
        airRay = new Ray(transform.position, -transform.up);
        //check if raycast hits the ground and only check if the player is falling
        if (Physics.Raycast(airRay, out hitInfoAir, airRayCheckDistance, groundLayers, QueryTriggerInteraction.Ignore) && !GameOver.gameEnded)
        {
            //This cyan line is moving because it's done in the fixed update rather than the update
            Debug.DrawLine(airRay.origin, hitInfoAir.point, Color.cyan);
            Quaternion targetRotationAir = Quaternion.LookRotation(transform.forward, hitInfoAir.normal);
            airToGroundRotationVelocity = new Vector3(0, 0, 1);
            deltaRotation = Quaternion.Euler(airToGroundRotationVelocity * Time.fixedDeltaTime);
            rigidbody.MoveRotation(targetRotationAir * deltaRotation);
        }
        else
        {
            airToGroundRotationVelocity = new Vector3(0, 0, 1);
            deltaRotation = Quaternion.Euler(airToGroundRotationVelocity * Time.fixedDeltaTime);
            rigidbody.MoveRotation(Quaternion.identity * deltaRotation);
        }
    }

    private void GroundingCheck()
    {
        topPlayerYPoint = new Vector3(transform.position.x, transform.position.y + heightOfPlayer / 2f, transform.position.z);
        

        groundingRay = new Ray(topPlayerYPoint, -transform.up);
        fallingThruRay = new Ray(topPlayerYPoint, -transform.up);
        isGroundedExtendedRay = new Ray(transform.position, -transform.up);

        groundingDetect = Physics.Raycast(groundingRay, out RaycastHit midHitInfo, heightOfPlayer * 2.0f, groundLayers, QueryTriggerInteraction.Ignore);
        fallingThruDetect = Physics.Raycast(fallingThruRay, out RaycastHit aboveHitInfo, heightOfPlayer, groundLayers, QueryTriggerInteraction.Ignore);
        isGroundedExtendedRayDetect = Physics.Raycast(isGroundedExtendedRay, out extendedRayHitInfo, heightOfPlayer * 4f, groundLayers, QueryTriggerInteraction.Ignore);

        Vector3 endPoint = new Vector3(transform.position.x, topPlayerYPoint.y - heightOfPlayer * 2.5f, transform.position.z);
        isGroundedExtendedEndPoint = new Vector3(transform.position.x, transform.position.y - heightOfPlayer * 4f, transform.position.z);


        if (groundingDetect)
        {
            isGrounded = true;
            Debug.DrawLine(groundingRay.origin, endPoint, Color.green);
            yGroundHitPoint = midHitInfo.point.y;
            Vector3 hitInfoNormal = midHitInfo.normal;
            //GroundRotation(hitInfoNormal);
        } else
        {
            isGrounded = false;
        }

        if (fallingThruDetect)
        {
            isFallingThruGround = true;
            Debug.DrawLine(fallingThruRay.origin, new Vector3(transform.position.x, topPlayerYPoint.y - heightOfPlayer, transform.position.z), Color.red);
            yGroundHitPoint = aboveHitInfo.point.y;
        }
        else
        {
            isFallingThruGround = false;
        }

        if (isGroundedExtendedRayDetect)
        {
            isGroundedExtended = true;
            //Debug.DrawLine(isGroundedExtendedRay.origin, isGroundedExtendedEndPoint, Color.blue);
            //yGroundHitPoint = midHitInfo.point.y;
            //Vector3 hitInfoNormal = extendedRayHitInfo.normal;
            //GroundRotation(hitInfoNormal);
        }
        else
        {
            isGroundedExtended = false;
        }
      

        //Debug.DrawLine(rayFrontCheck.origin, rayFrontCheck.origin + new Vector3(20, 0, 0), Color.yellow);

        if (!testing)
        {
            rayFrontCheck = new Ray(transform.position, transform.right);
            if (Physics.Raycast(rayFrontCheck, out RaycastHit hitInfoFront, RayFrontCheckDistance, groundLayers, QueryTriggerInteraction.Ignore) && !isGrounded && Vector3.Angle(hitInfoFront.normal, -rayFrontCheck.direction) < 10 && !GameOver.gameEnded)
            {
                //print("hit wall");
                Debug.DrawLine(rayFrontCheck.origin, rayFrontCheck.origin + new Vector3(RayFrontCheckDistance,0,0), Color.yellow);
                if (!GameOver.gameEnded)
                {
                    GameOver.audioSwitchForCrashed = true;
                }
                GameOver.gameEnded = true;
                speed = 0;
                rigidbody.velocity = Vector3.zero;
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            
        }
        
    }

    //Keeps player rotated towards slope while on ground. Method is applied in 'GroundingCheck()'
    private void GroundRotation()
    {
        if (isGroundedExtendedRayDetect)
        {
            Debug.DrawLine(isGroundedExtendedRay.origin, isGroundedExtendedEndPoint, Color.blue);
            //yGroundHitPoint = midHitInfo.point.y;
            hitInfoNormalGrounding = extendedRayHitInfo.normal;

            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, hitInfoNormalGrounding);

            rotationVelocity = new Vector3(0, 0, 2);
            deltaRotation = Quaternion.Euler(rotationVelocity * Time.fixedDeltaTime);
            rigidbody.MoveRotation(targetRotation * deltaRotation);

            //transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * groundDegreesRotationPerSecond);
        }
        

            
        }

    private void Move()
    {
        speedIncrement = Mathf.FloorToInt(metresTravelled / 1000f);
        secondsSinceLastSpeedIncrement += Time.fixedDeltaTime;

        if (MainMenu.hasPlayBeenPressed && !MenuController.isPaused)
        {
            speed = Mathf.Max(Mathf.Min(minSpeed + (speedIncrement * incrementalSpeedIncrease), maxSpeed), minSpeed);
        }


        speedPreviousIncrement = Mathf.Max(Mathf.Min(minSpeed + ((speedIncrement - 1) * incrementalSpeedIncrease), maxSpeed), minSpeed);

        if (speedIncrement != prevVelInc)
        {
            speedIncrementElapsedTime += Time.fixedDeltaTime;
            speed = Mathf.Lerp(speedPreviousIncrement, speed, speedIncreaseCurve.Evaluate(speedIncrementLerpPercentageComplete));
        }

        if (speedIncrementElapsedTime >= 1)
        {
            prevVelInc = currVelInc;
            speedIncrementElapsedTime = 0;
        }


        increasedSpeed = speed * (1 + speedIncreaseAmount);
        decreasedSpeed = speed * (1 - speedDecreaseAmount);

        if (!GenerateInfinite.cameraSwitch || GenerateInfinite.cameraSideOn)
        {
            if (holdRight && holdLeft)
            {
                rigidbody.velocity = new Vector3(speed, rigidbody.velocity.y, rigidbody.velocity.z);
                currentSpeed = rigidbody.velocity.x;
                rightCurrentSpeed = rigidbody.velocity.x;
                leftCurrentSpeed = rigidbody.velocity.x;
                speedChangeElapsedTime = 0;
                speedReturnElapsedTime = 0;
                hasCurrentSpeedRun = true;
                hasLeftCurrentSpeedRun = true;
                hasRightCurrentSpeedRun = true;
            }
            else
            {
                if (!holdRight && !holdLeft)
                {
                    currentSpeed = rigidbody.velocity.x;
                    hasCurrentSpeedRun = false;
                    hasLeftCurrentSpeedRun = false;
                    hasRightCurrentSpeedRun = false;
                    speedChangeElapsedTime = 0;
                    speedReturnElapsedTime += Time.fixedDeltaTime;
                    constantVelocity = new Vector3(speed, rigidbody.velocity.y, rigidbody.velocity.z);
                    rigidbody.velocity = Vector3.Lerp(new Vector3(currentSpeed, rigidbody.velocity.y, rigidbody.velocity.z), constantVelocity, speedIncreaseCurve.Evaluate(speedReturnLerpPercentageComplete));
                }


                if (holdRight && !holdLeft)
                {
                    increasedVelocity = new Vector3(increasedSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
                    speedReturnElapsedTime = 0;
                    speedChangeElapsedTime += Time.fixedDeltaTime;
                    if (!hasRightCurrentSpeedRun)
                    {
                        rightCurrentSpeed = rigidbody.velocity.x;
                    }
                    hasRightCurrentSpeedRun = true;
                    rigidbody.velocity = Vector3.Lerp(new Vector3(rightCurrentSpeed, rigidbody.velocity.y, rigidbody.velocity.z), increasedVelocity, speedIncreaseCurve.Evaluate(speedChangeLerpPercentageComplete));
                }

                if (holdLeft && !holdRight)
                {
                    decreasedVelocity = new Vector3(decreasedSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
                    speedReturnElapsedTime = 0;
                    speedChangeElapsedTime += Time.fixedDeltaTime;

                    if (!hasLeftCurrentSpeedRun)
                    {
                        leftCurrentSpeed = rigidbody.velocity.x;
                    }
                    hasLeftCurrentSpeedRun = true;
                    rigidbody.velocity = Vector3.Lerp(new Vector3(leftCurrentSpeed, rigidbody.velocity.y, rigidbody.velocity.z), decreasedVelocity, speedIncreaseCurve.Evaluate(speedChangeLerpPercentageComplete));
                }

                if (Input.GetKeyUp(KeyCode.RightArrow) && !holdLeft)
                {

                    if (!hasCurrentSpeedRun)
                    {
                        currentSpeed = rigidbody.velocity.x;

                    }
                    hasCurrentSpeedRun = true;
                    speedChangeElapsedTime = 0;
                    speedReturnElapsedTime = 0;
                }

                if (Input.GetKeyUp(KeyCode.LeftArrow) && !holdRight)
                {

                    if (!hasCurrentSpeedRun)
                    {
                        currentSpeed = rigidbody.velocity.x;

                    }
                    hasCurrentSpeedRun = true;
                    speedChangeElapsedTime = 0;
                    speedReturnElapsedTime = 0;
                }
            }
        }

        else if (GenerateInfinite.cameraSwitch)
        {
            if (holdUp && holdDown)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, rigidbody.velocity.z);
                currentSpeed = rigidbody.velocity.x;
                upCurrentSpeed = rigidbody.velocity.x;
                downCurrentSpeed = rigidbody.velocity.x;
                speedChangeElapsedTime = 0;
                speedReturnElapsedTime = 0;
                hasCurrentSpeedRun = true;
                hasDownCurrentSpeedRun = true;
                hasUpCurrentSpeedRun = true;
            }
            else
            {
                if (!holdUp && !holdDown)
                {
                    currentSpeed = rigidbody.velocity.x;
                    hasCurrentSpeedRun = false;
                    hasDownCurrentSpeedRun = false;
                    hasUpCurrentSpeedRun = false;
                    speedChangeElapsedTime = 0;
                    speedReturnElapsedTime += Time.fixedDeltaTime;
                    constantVelocity = new Vector3(speed, rigidbody.velocity.y, rigidbody.velocity.z);
                    rigidbody.velocity = Vector3.Lerp(new Vector3(currentSpeed, rigidbody.velocity.y, rigidbody.velocity.z), constantVelocity, speedIncreaseCurve.Evaluate(speedReturnLerpPercentageComplete));
                }


                if (holdUp && !holdDown)
                {
                    increasedVelocity = new Vector3(increasedSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
                    speedReturnElapsedTime = 0;
                    speedChangeElapsedTime += Time.fixedDeltaTime;
                    if (!hasUpCurrentSpeedRun)
                    {
                        upCurrentSpeed = rigidbody.velocity.x;
                    }
                    hasUpCurrentSpeedRun = true;
                    rigidbody.velocity = Vector3.Lerp(new Vector3(upCurrentSpeed, rigidbody.velocity.y, rigidbody.velocity.z), increasedVelocity, speedIncreaseCurve.Evaluate(speedChangeLerpPercentageComplete));
                }


                if (holdDown && !holdUp)
                {
                    decreasedVelocity = new Vector3(decreasedSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
                    speedReturnElapsedTime = 0;
                    speedChangeElapsedTime += Time.fixedDeltaTime;
                    if (!hasDownCurrentSpeedRun)
                    {
                        downCurrentSpeed = rigidbody.velocity.x;
                    }
                    hasDownCurrentSpeedRun = true;
                    rigidbody.velocity = Vector3.Lerp(new Vector3(downCurrentSpeed, rigidbody.velocity.y, rigidbody.velocity.z), decreasedVelocity, speedIncreaseCurve.Evaluate(speedChangeLerpPercentageComplete));
                }

                if (Input.GetKeyUp(KeyCode.UpArrow) && !holdDown)
                {
                    if (!hasCurrentSpeedRun)
                    {
                        currentSpeed = rigidbody.velocity.x;
                    }
                    hasCurrentSpeedRun = true;
                    speedChangeElapsedTime = 0;
                    speedReturnElapsedTime = 0;
                }

                if (Input.GetKeyUp(KeyCode.DownArrow) && !holdUp)
                {
                    if (!hasCurrentSpeedRun)
                    {
                        currentSpeed = rigidbody.velocity.x;
                    }
                    hasCurrentSpeedRun = true;
                    speedChangeElapsedTime = 0;
                    speedReturnElapsedTime = 0;
                }
            }

            if (holdLeft && holdRight)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0);
            }
            else if (holdLeft & !holdUp & !holdDown)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, strafeLeftSpeed);
            }
            else if (holdRight & !holdUp & !holdDown)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, strafeRightSpeed);
            }
            else if (holdLeft & holdUp)
            {
                increasedVelocity = new Vector3(increasedSpeed, rigidbody.velocity.y, strafeLeftSpeed);
                rigidbody.velocity = Vector3.Lerp(new Vector3(upCurrentSpeed, rigidbody.velocity.y, rigidbody.velocity.z), increasedVelocity, speedIncreaseCurve.Evaluate(speedChangeLerpPercentageComplete));
            }
            else if (holdRight & holdUp)
            {
                increasedVelocity = new Vector3(increasedSpeed, rigidbody.velocity.y, strafeRightSpeed);
                rigidbody.velocity = Vector3.Lerp(new Vector3(upCurrentSpeed, rigidbody.velocity.y, rigidbody.velocity.z), increasedVelocity, speedIncreaseCurve.Evaluate(speedChangeLerpPercentageComplete));
            }
            else if (holdLeft & holdDown)
            {
                decreasedVelocity = new Vector3(decreasedSpeed, rigidbody.velocity.y, strafeLeftSpeed);
                rigidbody.velocity = Vector3.Lerp(new Vector3(downCurrentSpeed, rigidbody.velocity.y, rigidbody.velocity.z), decreasedVelocity, speedIncreaseCurve.Evaluate(speedChangeLerpPercentageComplete));
            }
            else if (holdRight & holdDown)
            {
                decreasedVelocity = new Vector3(decreasedSpeed, rigidbody.velocity.y, strafeRightSpeed);
                rigidbody.velocity = Vector3.Lerp(new Vector3(downCurrentSpeed, rigidbody.velocity.y, rigidbody.velocity.z), decreasedVelocity, speedIncreaseCurve.Evaluate(speedChangeLerpPercentageComplete));
            }
            
            else
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, 0);
            }
        }


        if (previousSpeedIncrement != speedIncrement)
        {
            secondsSinceLastSpeedIncrement = 0;
            cameraZAdjustment = previouscameraZAdjustment - 1;
            cameraAdjustForSpeedIncrement = new Vector3(0, 0, cameraZAdjustment);
        }

        if (GenerateInfinite.cameraSwitch == false && transform.position.z > 0)
        {
            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, strafeRightSpeed), secondsSinceLastSpeedIncrement / 1f);
        }
        else if (GenerateInfinite.cameraSwitch == false && transform.position.z < 0)
        {
            rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, strafeLeftSpeed), secondsSinceLastSpeedIncrement / 1f);
        }
        previousSpeedIncrement = speedIncrement;
        previouscameraZAdjustment = cameraZAdjustment;
    }

    //Jump and Air Rotation
    private void Jump()
    {
        //print(rigidbody.velocity.y);

        if((isGroundedExtended || isFallingThruGround) && (tapSpace || holdSpace) && !GenerateInfinite.cameraSwitch)
        {
            //print("Jump");
            isGrounded = false;
            tapSpace = false;
            holdSpace = false;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpVelocity, rigidbody.velocity.z);
        }
        else if(isGrounded && !isFallingThruGround && !holdSpace && !tapSpace && rigidbody.velocity.y < 2 && rigidbody.velocity.y > -2 && !GenerateInfinite.cameraSwitch)
        {
            //print("Stable small down force");
            //if (rigidbody.velocity.y > 0)
            //{
                //rigidbody.velocity = new Vector3(rigidbody.velocity.x, -0.002f, rigidbody.velocity.z);
            //}
            //else
            //{
                rigidbody.velocity += Vector3.up * Physics.gravity.y * 0.02f * Time.fixedDeltaTime;
            //}
            AirToGroundRotation();
        }
        else if(isGrounded && !holdSpace && !tapSpace && rigidbody.velocity.y < -2 && !GenerateInfinite.cameraSwitch)
        {
            //print("approach ground large upforce");
            //large up force (add do while loop here while yVEL is negative)
            //rigidbody.velocity += Vector3.up * -Physics.gravity.y * (lowJumpGravityMultiplier - 0f) * Time.fixedDeltaTime;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -0.05f, rigidbody.velocity.y);
        } 
        else if(isFallingThruGround && !holdSpace && !tapSpace)
        {
            //print("falling through ground small up force");
            if (rigidbody.velocity.y < 0)
            {
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0.001f, rigidbody.velocity.z);
            }
            else
            {
                rigidbody.velocity += Vector3.up * -Physics.gravity.y * 0.15f * Time.fixedDeltaTime;
            }
        }
        else if(!isGrounded && !isFallingThruGround && !holdSpace)
        {
            //print("falling fast");
            if (!testing) rigidbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpGravityMultiplier) * Time.fixedDeltaTime;
            AirToGroundRotation();
        } else if(!isGrounded && !isFallingThruGround && holdSpace)
        {
            //print("falling slow");
            rigidbody.velocity += Vector3.up * Physics.gravity.y * (fullJumpGravityMultiplier) * Time.fixedDeltaTime;
            AirToGroundRotation();
        } else if(GenerateInfinite.cameraSwitch)
        {
            rigidbody.velocity += Vector3.up * Physics.gravity.y * 0.02f * Time.fixedDeltaTime;
        }
        else
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -0.001f, rigidbody.velocity.z);
        }


        //this is so the penguin can still flap if it falls below the ground level
        if (GenerateInfinite.cameraSideOn || (!isGrounded && rigidbody.velocity.y < -5)) yGroundHitPoint = -50.0f;

        if (!isGrounded && flapsRemaining > 0 && tapSpace && Time.time > (lastPressedButton + intervalSinceLastButtonPress) && (transform.position.y > (yGroundHitPoint + groundRayCheckDistanceError + 0.5f)) && GenerateInfinite.cameraSwitch == false)
            {
            tapSpace = false;
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, flapVelocity, rigidbody.velocity.z);
            flapsRemaining -= 1;
            lastPressedButton = Time.time;
        }
    }

    public void ResetValues()
    {
        fullJumpGravityMultiplier = 4f;
        lowJumpGravityMultiplier = 10f;
        cameraAdjustForSpeedIncrement = new Vector3(0, 0, 0);
        elapsedTime = 0f;
        secondsSinceLastSpeedIncrement = 0f;
        playerX = -32;
        speed = 0;
        //hasPlayBeenPressed = false;
    }

    private void ToggleZMovementRestrictionForCameraChanges()
    {
        if (GenerateInfinite.cameraSwitch == false && (transform.position.z > 0.1f || transform.position.z < -0.1f))
        {
            isPlayerOutsideOfCentreWhileInSideView = true;
        }
        else
        {
            isPlayerOutsideOfCentreWhileInSideView = false;
        }

        if ((GenerateInfinite.cameraSwitch || isPlayerOutsideOfCentreWhileInSideView) && !GameOver.gameEnded)
        {
            //rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            rigidbody.constraints = RigidbodyConstraints.None;
        }
        else if(!GenerateInfinite.cameraSwitch && !GameOver.gameEnded)
        {
            //print("freezing as game not ended");
            rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            fish1FlapIncrement = Mathf.FloorToInt(other.gameObject.transform.localScale.x * 8);
            other.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            //Destroy(other.gameObject);
            flapsRemaining += fish1FlapIncrement;
            GameOver.audioSwitchForFishEaten = true;
        }
        else if (other.gameObject.layer == 8)
        {
            fish2FlapIncrement = Mathf.FloorToInt(other.gameObject.transform.localScale.x * 8);
            other.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            //Destroy(other.gameObject);
            flapsRemaining += fish2FlapIncrement;
            GameOver.audioSwitchForFishEaten = true;
        }
    }
}