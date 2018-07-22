using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

    // ここから
    public float speed = 5.0f;
    public float slideSpeed = 2.0f;
	// ここまでの間に変数を書いてね

	Animator animator;
	UIScript uiscript;

    bool isInvincible = false;
    float timer = 0.0f;
    bool isTimer = false;
    private ParticleSystem parti;
    public static int invincibleTimes = 2;
    private int temp;
    public Text invinibleText;

    // ゲームが始まった時に一回だけ呼ばれる
    void Start () {
		animator = GetComponent <Animator> ();
		uiscript = GameObject.Find ("Canvas").GetComponent<UIScript> ();

        parti = this.GetComponent<ParticleSystem>();
        parti.Stop();
        temp = invincibleTimes;
        invinibleText.enabled = true;
	}

	// 1フレームごとに呼ばれる
	void Update () {
        //ここから
        transform.position += Vector3.forward * speed * Time.deltaTime;
        float pos_x = transform.position.x;
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (pos_x > -1.9f) {
                transform.position += Vector3.left * slideSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            if(pos_x < 1.9f) {
                transform.position += Vector3.right * slideSpeed * Time.deltaTime;
            }
        }
		//ここまでの間に書こう！

		//アニメーションについて(いじらない)
		if (Input.GetKey (KeyCode.UpArrow)) {
			animator.SetBool ("JUMP", true);
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			animator.SetBool ("JUMP", false);
		}
		if (Input.GetKey (KeyCode.DownArrow)) {
			animator.SetBool ("SLIDE", true);
		}
		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			animator.SetBool ("SLIDE", false);
		}

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (invincibleTimes > 0) {
                invincibleTimes -= 1;
                isTimer = true;
                isInvincible = true;
                parti.Play();
            }
        }

        if(isTimer == true) {
            timer += Time.deltaTime;
            if(timer >= 2.0f) {
                timer = 0.0f;
                isTimer = false;
                isInvincible = false;
                parti.Stop();
            }
        }
        

	}
		
	// Triggerである障害物にぶつかったとき
	void OnTriggerEnter (Collider colider){
		
		var stateInfo = animator.GetCurrentAnimatorStateInfo (0);
		bool isJump = stateInfo.IsName("Base Layer.JUMP00");
		bool isSlide = stateInfo.IsName("Base Layer.SLIDE00");
//		bool isRun = stateInfo.IsName("Base Layer.RUN00_F");

		bool isHigh = colider.CompareTag("High");
		bool isLow = colider.CompareTag("Low");
		bool isBarrier = colider.CompareTag ("barrier");
		bool isGoal = colider.CompareTag ("goal");


        if (isInvincible == false) {
            // 障害物に当たったとき
            if ((isHigh == true && isSlide == false) ||
                (isLow == true && isJump == false) ||
                (isBarrier == true)) {

                invinibleText.enabled = false;
                
                //この下に書こう
                speed = 0.0f;
                animator.SetBool("DEAD", true);
                // UI
                uiscript.Gameover();
                
                invincibleTimes = temp;
            }
        }
		//ゴールした時
		if(isGoal == true){
            //この下に書こう
            speed = 0.0f;
			animator.SetBool ("WIN", true);
			// UI
			uiscript.Goal();
            
            invincibleTimes = temp;
		}
	}
}
