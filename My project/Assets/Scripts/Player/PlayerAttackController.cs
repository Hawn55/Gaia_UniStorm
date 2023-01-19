using System.Collections;
using UnityEngine;

using UniRx;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] GameObject m_AttackCollider;
    [SerializeField] Animator m_SlimeAnimatort;
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
                Debug.Log("die Enemy");
                m_SlimeAnimatort.SetTrigger("die");
            }
        }
    }

    private bool AttackDamage( Collider enemy, int damage )
    {
        return ( enemy.GetComponent<MonsterStatus>().NowHP -= damage ) > 0 ? false : true;
    }

    private IEnumerator AttackCoroutine()
    {
        m_AttackCollider.SetActive( true );
        yield return new WaitForSeconds( 0.5f );
        m_AttackCollider.SetActive( false );
    }
}
