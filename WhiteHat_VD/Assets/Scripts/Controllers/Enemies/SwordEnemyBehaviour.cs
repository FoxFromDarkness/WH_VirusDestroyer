using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordEnemyBehaviour : MonoBehaviour
{
    public enum EnemyMode { PATROL, FOLLOW_HERO, NONE}

    //public PlayerController player;
    public EnemyMode mode;
    public float enemySpeed;
    private float actualPatrolTime = 0;
    public float patrolTime;


    protected float m_MoveX;
    private Rigidbody2D m_rigidbody;
    protected CapsuleCollider2D m_CapsulleCollider;
    protected Animator m_Anim;

    // Start is called before the first frame update
    void Start()
    {
        m_CapsulleCollider = this.transform.GetComponent<CapsuleCollider2D>();
        m_Anim = this.transform.Find("model").GetComponent<Animator>();
        m_rigidbody = this.transform.GetComponent<Rigidbody2D>();

        enemySpeed *= -1;
    }

    private void FixedUpdate() {
        PatrolMode();
    }

    // Update is called once per frame
    void Update()
    {
        FollowHeroModeCheck();
    }

    private void PatrolMode() {
        if (mode == EnemyMode.PATROL) {
            this.gameObject.transform.Translate(new Vector3(1,0,0) * enemySpeed);
            actualPatrolTime += Time.deltaTime;

            if (actualPatrolTime > patrolTime) {
                enemySpeed *= -1;
                Flip(enemySpeed >= 0 ? false : true);
                actualPatrolTime = 0;
            }
        }
    }

    private void FollowHeroModeCheck() {
        //if (Vector3.Distance(this.transform.position, player.transform.position) < 10.0f) {
        //    mode = EnemyMode.FOLLOW_HERO;
        //}
        //else {
        //    mode = EnemyMode.PATROL;
        //}
    }

    private void FollowHeroMode() {
        if (mode == EnemyMode.FOLLOW_HERO) {

        }
    }

    protected void Flip(bool bLeft) {
        transform.localScale = new Vector3(bLeft ? 1 : -1, 1, 1);
    }


}
