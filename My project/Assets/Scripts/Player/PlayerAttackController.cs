using System.Collections;
using UnityEngine;

using UniRx;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] GameObject m_AttackCollider;
    [SerializeField] Animator m_SlimeAnimatort;

    const float m_AttackColTime = 0.5f;

    void Update()
    {
        if( Input.GetMouseButtonDown( 0 ) )
        {
            Observable.FromCoroutine( AttackCoroutine, publishEveryYield: false )
            .Subscribe(
                _ => Debug.Log( "OnNext" ),
                () => Debug.Log( "OnCompleted" )
            ).AddTo( gameObject );
        }
    }

    private void OnTriggerEnter( Collider other )
    {
        if( other.tag == "Enemy" )
        {
            //other.gameObject.SetActive(false);
            if( AttackDamage( other, 1 ) )
            {
                m_SlimeAnimatort.SetTrigger("die");
            }
        }
    }

    /// <summary>
    /// 攻撃ヒット時の処理
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="damage"></param>
    /// <remarks></remarks>
    private bool AttackDamage( Collider enemy, int damage )
    {
        // 死亡後は処理させない
        if( enemy.GetComponent<MonsterStatus>().NowHP <= 0) return false;

        enemy.GetComponent<DamageUI3DPop>().ViewDamage( damage );
        enemy.GetComponent<MonsterStatus>().NowHP -= damage;
        return ( enemy.GetComponent<MonsterStatus>().NowHP ) > 0 ? false : true;
    }

    /// <summary>
    /// 攻撃のコリダーアクティブ化
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="damage"></param>
    /// <remarks></remarks>
    private IEnumerator AttackCoroutine()
    {
        m_AttackCollider.SetActive( true );
        yield return new WaitForSeconds( m_AttackColTime );
        m_AttackCollider.SetActive( false );
    }
}
