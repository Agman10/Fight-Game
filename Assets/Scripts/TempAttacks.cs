using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempAttacks : MonoBehaviour
{

    private Rigidbody rb;
    private TestPlayer player;
    public PlayerInput playerInput;
    public TempPlayerAnimations animations;

    [Space]
    public Collider punchHitbox;
    public Collider kickHitbox;

    [Space]
    public Attack punch;
    public Attack kick;

    /*public Bomb bomb;
    public GameObject bombModel;
    public float bombXForce;
    public float bombYForce;*/
    [Space]
    public Attack neutralSpecial;
    public Attack forwardSpecial;
    public Attack backwardSpecial;
    public Attack downSpecial;

    /*public LandMine landmine;
    public GameObject landmineModel;*/

    /*public FireBall fireBall;
    public float fireBallXForce;
    public float fireBallYForce;*/

    //public SuperSpinGrab superSpinGrab;
    [Space]
    public Attack neutralSpecial2;
    public Attack forwardSpecial2;
    public Attack backwardSpecial2;
    public Attack downSpecial2;

    /*public SpinKick spinKick;
    public UppercutAttack uppercut;*/

    /*public RagingBeast ragingBeast;
    public FlamingGrab flamingGrab;*/
    [Space]
    public Attack neutralSuper;
    public Attack forwardSuper;
    public Attack backwardSuper;
    public Attack downSuper;

    [Space]
    public Attack startAnimation;
    public Attack Taunts;
    //public ReflectorAttack reflector;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.player = GetComponent<TestPlayer>();
        this.animations = GetComponent<TempPlayerAnimations>();
        if (this.playerInput != null)
        {
            this.playerInput.PunchInput += this.Punch;
            this.playerInput.KickInput += this.Kick;
            this.playerInput.SpecialInput += this.Special;
            this.playerInput.Special2Input += this.Special2;
            this.playerInput.SuicideInput += this.Suicide;
            this.playerInput.SuperInput += this.Super;
            this.playerInput.TauntInput += this.Taunt;


            this.playerInput.StartInput += this.PauseGame;
            /*if (GameManager.Instance != null)
            {
                //this.playerInput.StartInput += GameManager.Instance.PauseGame;
                this.playerInput.StartInput += this.PauseGame;
            }*/
        }

        if (this.startAnimation != null)
            this.startAnimation.Initiate();
    }
    /*private void OnEnable()
    {
        if (this.startAnimation != null)
            this.startAnimation.Initiate();
    }*/
    /*private void OnEnable()
    {
        if (this.playerInput != null)
        {
            this.playerInput.PunchInput += this.Punch;
            this.playerInput.KickInput += this.Kick;
            this.playerInput.SpecialInput += this.Special;
            this.playerInput.Special2Input += this.Special2;
            this.playerInput.SuicideInput += this.Suicide;
            this.playerInput.SuperInput += this.Super;
        }
    }
    private void OnDisable()
    {
        if (this.playerInput != null)
        {
            this.playerInput.PunchInput -= this.Punch;
            this.playerInput.KickInput -= this.Kick;
            this.playerInput.SpecialInput -= this.Special;
            this.playerInput.Special2Input -= this.Special2;
            this.playerInput.SuicideInput -= this.Suicide;
            this.playerInput.SuperInput -= this.Super;
        }
    }*/

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            if (GameManager.Instance != null)
            {
                //GameManager.Instance.TogglePauseGame(true);
                if (!GameManager.Instance.gameIsPaused)
                    GameManager.Instance.PauseGame();
                /*else
                    GameManager.Instance.UnPauseGame();*/
                //Debug.Log("pauseeplay");
                //this.playerInput.StartInput += this.PauseGame;
            }
        }
        
    }

    public void Punch(bool punching)
    {
        if (punching && this.player != null && !this.player.dead)
            this.player.OnAttack?.Invoke();

        if (punching && this.player != null && !this.player.dead && this.player.stuns.Count <= 0 && this.player.attackStuns.Count <= 0)
        {
            if (this.punch != null)
                this.punch.Initiate();
            else
                Debug.Log("Punch not assigned...");


            /*if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                this.player.AddStun(0.2f, true);
            else
                this.player.AddStun(0.2f, false);

            if (this.punchHitbox != null)
            {
                this.StartCoroutine(this.PunchHitbox());
                //this.StartCoroutine(this.TestPunchBarage());
            }*/
        }
    }

    public void Kick(bool kicking)
    {
        if (kicking && this.player != null && !this.player.dead)
            this.player.OnAttack?.Invoke();

        if (kicking && this.player != null && !this.player.dead && this.player.stuns.Count <= 0 && this.player.attackStuns.Count <= 0)
        {
            if (this.kick != null)
                this.kick.Initiate();
            else
                Debug.Log("Kick not assigned...");


            /*if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                this.player.AddStun(0.4f, true);
            else
                this.player.AddStun(0.4f, false);

            if (this.kickHitbox != null)
            {
                this.StartCoroutine(this.KickHitbox());
            }*/
        }
    }

    public void Special(bool special)
    {
        if (special && this.player != null && !this.player.dead)
            this.player.OnAttack?.Invoke();

        if (special && this.player != null && !this.player.dead && this.player.stuns.Count <= 0 && this.player.attackStuns.Count <= 0)
        {

            //Debug.Log(this.playerInput.moveInput.x * this.transform.forward.z);
            if (this.playerInput.moveInput.y < 0f/*this.playerInput.moveInput.x * this.transform.forward.z < 0f*/)
            {
                /*if (Mathf.Abs(this.rb.velocity.y) <= 0f) //on ground
                {
                    this.player.AddStun(1.1f, true);
                    this.StartCoroutine(this.PlaceLandMineCorutine(0.8f));
                }*/

                if (this.downSpecial != null)
                    this.downSpecial.Initiate();
                else
                    Debug.Log("Down Special not assigned...");
            }
            else if (this.playerInput.moveInput.x * this.transform.forward.z > 0f)
            {
                if (this.forwardSpecial != null)
                    this.forwardSpecial.Initiate();
                else
                    Debug.Log("Forward Special not assigned...");

                /*if (Mathf.Abs(this.rb.velocity.y) <= 0f) //on ground
                {
                    this.player.AddStun(1.1f, true);
                    this.StartCoroutine(this.ThrowFireBallCorutine(0.5f));
                }
                else
                {
                    this.player.AddStun(0.6f, false);
                    this.StartCoroutine(this.ThrowFireBallCorutine(0.5f));
                }*/
            }
            else if (this.playerInput.moveInput.x * this.transform.forward.z < 0f)
            {
                //Debug.Log("Back Special not assigned...");
                /*if (this.superSpinGrab != null)
                    this.superSpinGrab.InitiateGrab(true);*/

                /*if (this.spinKick != null)
                    this.spinKick.InitiateSpinKick();*/

                if (this.backwardSpecial != null)
                    this.backwardSpecial.Initiate();
                else
                    Debug.Log("Backward Special not assigned...");

                /*if (this.superSpinGrab != null)
                    this.superSpinGrab.InitiateGrab(*//*true*//*);*/

                /*if (Mathf.Abs(this.rb.velocity.y) <= 0f) //on ground
                {
                    if (this.superSpinGrab != null)
                        this.superSpinGrab.InitiateGrab(*//*true*//*);
                }
                else
                {
                    if (this.superSpinGrab != null)
                    {
                        this.player.rb.AddForce(0, 300, 0);
                        this.superSpinGrab.InitiateGrab(*//*true*//*);
                    }

                }*/
            }
            else
            {
                if (this.neutralSpecial != null)
                    this.neutralSpecial.Initiate();
                else
                    Debug.Log("Neutral Special not assigned...");

                /*if (Mathf.Abs(this.rb.velocity.y) <= 0f) //on ground
                {
                    this.player.AddStun(1f, true);
                    this.StartCoroutine(this.ThrowBombCorutine(0.5f));
                }
                else
                {
                    this.player.AddStun(0.6f, false);
                    this.StartCoroutine(this.ThrowBombCorutine(0.2f));
                }*/
            }
        }
    }
    public void Special2(bool special)
    {
        if (special && this.player != null && !this.player.dead)
            this.player.OnAttack?.Invoke();

        if (special && this.player != null && !this.player.dead && this.player.stuns.Count <= 0 && this.player.attackStuns.Count <= 0)
        {
            if (this.playerInput.moveInput.y < 0f/*this.playerInput.moveInput.x * this.transform.forward.z < 0f*/)
            {
                if (this.downSpecial2 != null)
                    this.downSpecial2.Initiate();
                else
                    Debug.Log("Down Special 2 not assigned...");
                /*if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                {
                    
                }*/
                //Debug.Log("Down Special not assigned...");
                /*if (this.reflector != null)
                    this.reflector.InitiateReflector();*/
            }
            else if (this.playerInput.moveInput.x * this.transform.forward.z > 0f)
            {
                //Debug.Log("Forward Special not assigned...");
                /*if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                {
                    if (this.uppercut != null)
                        this.uppercut.InitiateUppercut();
                }*/

                if (this.forwardSpecial2 != null)
                    this.forwardSpecial2.Initiate();
                else
                    Debug.Log("Forward Special 2 not assigned...");

            }
            else if (this.playerInput.moveInput.x * this.transform.forward.z < 0f)
            {
                //Debug.Log("Back Special not assigned...");
                /*if (this.superSpinGrab != null)
                    this.superSpinGrab.InitiateGrab(true);*/

                /*if (this.spinKick != null)
                    this.spinKick.InitiateSpinKick();*/

                if (this.backwardSpecial2 != null)
                    this.backwardSpecial2.Initiate();
                else
                    Debug.Log("Backward Special 2 not assigned...");
            }
            else
            {
                if (this.neutralSpecial2 != null)
                    this.neutralSpecial2.Initiate();
                else
                    Debug.Log("Neutral Special 2 not assigned...");
                /*if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                {
                    
                }*/
                //Debug.Log("Neutral Special not assigned...");

                /*if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                {
                    if (this.ragingBeast != null)
                        this.ragingBeast.InitiateRagingBeast();
                }*/


                /*if (this.reflector != null)
                    this.reflector.InitiateReflector();*/
            }
        }
    }
    public void Super(bool super)
    {
        if (super && this.player != null && !this.player.dead)
            this.player.OnAttack?.Invoke();

        if (super && this.player != null && !this.player.dead && this.player.stuns.Count <= 0 && this.player.attackStuns.Count <= 0)
        {
            if (this.playerInput.moveInput.y < 0f)
            {
                /*if (this.player.superCharge >= this.player.maxSuperCharge)
                {
                    if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                    {
                        
                    }
                }*/
                if (this.downSuper != null)
                {
                    //this.player.GiveSuperCharge(-this.player.maxSuperCharge);
                    this.downSuper.Initiate();
                }
                else
                {
                    Debug.Log("Down Super not assigned...");
                }
            }
            else if (this.playerInput.moveInput.x * this.transform.forward.z > 0f)
            {
                //Debug.Log("Forward Special not assigned...");
                /*if (this.player.superCharge >= this.player.maxSuperCharge / 2)
                {
                    if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                    {

                        
                    }
                }*/

                if (this.forwardSuper != null)
                {
                    //this.player.GiveSuperCharge(-(this.player.maxSuperCharge / 2));
                    this.forwardSuper.Initiate();

                }
                else
                {
                    Debug.Log("Forward Super not assigned...");
                }
            }
            else if (this.playerInput.moveInput.x * this.transform.forward.z < 0f)
            {
                /*if (this.player.superCharge >= this.player.maxSuperCharge / 2)
                {
                    if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                    {
                        
                    }
                }*/
                if (this.backwardSuper != null)
                {
                    //this.player.GiveSuperCharge(-(this.player.maxSuperCharge / 2));
                    this.backwardSuper.Initiate();

                }
                else
                {
                    Debug.Log("Backward Super not assigned...");
                }
            }
            else
            {
                if (this.neutralSuper != null)
                {
                    //this.player.GiveSuperCharge(-this.player.maxSuperCharge);
                    this.neutralSuper.Initiate();
                }
                else
                {
                    Debug.Log("Neutral Super not assigned...");
                }
                /*if (this.player.superCharge >= this.player.maxSuperCharge)
                {
                    if (Mathf.Abs(this.rb.velocity.y) <= 0f)
                    {
                        if (this.neutralSuper != null)
                        {
                            this.player.GiveSuperCharge(-this.player.maxSuperCharge);
                            this.neutralSuper.Initiate();
                        }
                        else
                        {
                            Debug.Log("Neutral Super not assigned...");
                        }
                    }
                }*/
                
            }
            /*if (Mathf.Abs(this.rb.velocity.y) <= 0f)
            {
                this.player.superCharge = 0f;
                if (this.ragingBeast != null)
                    this.ragingBeast.InitiateRagingBeast();

                if (this.flamingGrab != null)
                    this.flamingGrab.Initiate();
            }*/
        }
    }

    public void Taunt(bool taunt)
    {
        if (taunt && this.player != null && !this.player.dead)
            this.player.OnAttack?.Invoke();

        if (taunt && this.player != null && !this.player.dead && this.player.stuns.Count <= 0 && this.player.attackStuns.Count <= 0)
        {
            if (this.Taunts != null)
            {
                this.Taunts.Initiate();
            }
            else
            {
                Debug.Log("Taunt Not Assigned...");
            }
        }
    }

    /*public void ThrowBomb()
    {
        if (this.bomb != null)
        {
            Bomb bombPrefab = this.bomb;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            bombPrefab = Instantiate(bombPrefab, new Vector3(forward + this.transform.position.x, this.transform.position.y + 2, 0), Quaternion.Euler(0, 0, 0));

            bombPrefab.KnockBack(new Vector3(forward * this.bombXForce, this.bombYForce, 0));

            if (this.player != null)
                bombPrefab.SetOwner(this.player);
        }
    }

    public void ThrowFireBall()
    {
        if (this.fireBall != null)
        {
            FireBall fireBallPrefab = this.fireBall;
            float forward = this.transform.forward.z;
            //ghostPrefab = Instantiate(ghostPrefab, this.transform.position, this.transform.rotation);
            fireBallPrefab = Instantiate(fireBallPrefab, new Vector3(forward + this.transform.position.x, this.transform.position.y + 2, 0), this.transform.rotation);
            if (this.player != null)
                fireBallPrefab.belongsTo = this.player;

            fireBallPrefab.KnockBack(new Vector3(forward * this.fireBallXForce, this.fireBallYForce, 0));
        }
    }*/

    /*public void PlaceLandmine()
    {
        if(this.landmine != null)
        {
            LandMine landminePrefab = this.landmine;
            landminePrefab = Instantiate(landminePrefab, new Vector3(this.transform.position.x, 0, 0), Quaternion.Euler(0, 0, 0));

            if (this.player != null)
                landminePrefab.SetOwner(this.player);
        }
    }*/

    /*IEnumerator ThrowBombCorutine(float time)
    {
        if (this.animations != null)
            this.animations.SetStartThrowBombPose();

        yield return new WaitForSeconds(time / 2);

        if (this.bombModel != null)
            this.bombModel.SetActive(true);

        yield return new WaitForSeconds(time / 2);

        if (this.bombModel != null)
            this.bombModel.SetActive(false);


        *//*if (this.animations != null)
            this.animations.SetStartThrowBombPose();
        yield return new WaitForSeconds(time);*//*

        if (this.animations != null)
            this.animations.SetPunchPose();
        this.ThrowBomb();

        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetDefaultPose();
        //this.ThrowFireBall();
    }
    IEnumerator ThrowFireBallCorutine(float time)
    {
        if (this.animations != null)
            this.animations.SetStartThrowFirePose();
        yield return new WaitForSeconds(time);
        if (this.animations != null)
            this.animations.SetPunchPose();
        this.ThrowFireBall();
        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetDefaultPose();
    }*/
    /*IEnumerator PlaceLandMineCorutine(float time)
    {
        if (this.animations != null)
            this.animations.SetStartThrowBombPose();

        yield return new WaitForSeconds(time / 2);

        if (this.landmineModel != null)
            this.landmineModel.SetActive(true);

        yield return new WaitForSeconds(time / 2);

        if (this.landmineModel != null)
            this.landmineModel.SetActive(false);

        //yield return new WaitForSeconds(time);

        if (this.animations != null)
            this.animations.SetDefaultPose();

        this.PlaceLandmine();
    }*/

    IEnumerator PunchHitbox()
    {
        if (this.animations != null)
            this.animations.SetStartPunchPose0();
        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.SetStartPunchPose();
        yield return new WaitForSeconds(0.04f);

        /*if (this.animations != null)
            this.animations.SetStartPunchPose2();
        yield return new WaitForSeconds(0.01f);*/


        /*if (this.animations != null)
            this.animations.SetStartPunchPose();
        yield return new WaitForSeconds(0.05f);*/

        if (this.animations != null)
            this.animations.SetPunchPose();
        this.punchHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetDefaultPose();
        this.punchHitbox.gameObject.SetActive(false);

        /*if (this.animations != null)
            this.animations.SetPunchPose();
        this.punchHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetPunchEndPose();
        this.punchHitbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        if (this.animations != null)
            this.animations.SetDefaultPose();*/
    }

    IEnumerator KickHitbox()
    {
        if (this.animations != null)
            this.animations.SetStartKickPose0();
        yield return new WaitForSeconds(0.05f);
        if (this.animations != null)
            this.animations.SetDefaultPose();
        yield return new WaitForSeconds(0.025f);
        if (this.animations != null)
            this.animations.SetStartKickPose();
        yield return new WaitForSeconds(0.025f);

        /*yield return new WaitForSeconds(0.025f);
        if (this.animations != null)
            this.animations.SetStartKickPose2();
        yield return new WaitForSeconds(0.025f);*/



        /*if (this.animations != null)
            this.animations.SetStartKickPose();
        yield return new WaitForSeconds(0.1f);*/

        if (this.animations != null)
            this.animations.SetKickPose();
        this.kickHitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        if (this.animations != null)
            this.animations.SetDefaultPose();
        this.kickHitbox.gameObject.SetActive(false);
    }

    public void Suicide(bool suicide)
    {
        this.player.Suicide();
    }




    IEnumerator TestPunchBarage(float time = 0.05f)
    {
        if (this.animations != null)
        {
            //first Cycle

            this.animations.SetStartPunchPose();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage2();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage1();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage2();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage1();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage2();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage1();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage2();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage1();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage2();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage1();
            yield return new WaitForSeconds(time);
            this.animations.PunchBarage2();
            yield return new WaitForSeconds(time);
            this.animations.SetPunchLeftPose();
            yield return new WaitForSeconds(time);
            this.animations.SetDefaultPose();
        }
    }
        
}
