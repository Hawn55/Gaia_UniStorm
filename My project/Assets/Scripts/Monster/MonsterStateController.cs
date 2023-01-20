using IceMilkTea.Core;
using UnityEngine;

// 状態を定義しているだけの何もしないクラス
public class MonsterStateController : MonoBehaviour
{
    ImtStateMachine<MonsterStateController> m_MonsterStateMachine;
    public ImtStateMachine<MonsterStateController> MonsterStateMachine { get; set; }

    Animator m_SlimeAnimator;

    // ステートマシンの入力（イベント）を判り易くするために列挙型で定義
    public enum StateEventId
    {
        Live,
        Dead
    }

    private void Awake()
    {
        // ステートマシンのインスタンスを生成して遷移テーブルを構築
        m_MonsterStateMachine = new ImtStateMachine<MonsterStateController>( this ); // 自身がコンテキストになるので自身のインスタンスを渡す
        m_MonsterStateMachine.AddTransition<LiveState, DeadState>( ( int ) StateEventId.Dead );
        m_MonsterStateMachine.AddTransition<DeadState, LiveState>( ( int ) StateEventId.Live );

        // 起動ステートを設定（起動ステートは IdleState）
        m_MonsterStateMachine.SetStartState<LiveState>();
    }

    private void Start()
    {
        MonsterStateMachine = m_MonsterStateMachine;
        m_SlimeAnimator = GetComponent<Animator>();
        m_MonsterStateMachine.Update();
    }
    private void Update()
    {
        m_MonsterStateMachine.Update();
    }

    // この Hoge クラスのアイドリング状態クラス
    private class LiveState : ImtStateMachine<MonsterStateController>.State
    {
        // 何もしない状態クラスなら何も書かなくても良い（むしろ無駄なoverrideは避ける）
    }

    // この Hoge クラスのなにかを処理している状態クラス
    private class DeadState : ImtStateMachine<MonsterStateController>.State
    {
        // 状態へ突入時の処理はこのEnterで行う
        protected override void Enter()
        {
            Context.m_SlimeAnimator.SetTrigger( "die" );
        }

        // 状態の更新はこのUpdateで行う
        protected override void Update()
        {
        }

        // 状態から脱出する時の処理はこのExitで行う
        protected override void Exit()
        {
        }

        /*
        // 状態で発生した未処理の例外がキャッチされた時の処理はこのErrorで行う
        protected internal override bool Error( Exception exception )
        {
            // 未処理の例外をハンドリングしたのなら true を返すことで、ステートマシンはエラーから復帰します
            return true;
        }

        // ステートマシンが状態の遷移をする前にステートマシンのイベント入力を処理するならこのGuardEventで行う
        protected internal override bool GuardEvent( int eventId )
        {
            // 特定のタイミングで遷移を拒否（ガード）するなら true を返せばステートマシンは遷移を諦めます
            if( !Context.isActiveAndEnabled )
            {
                return true;
            }

            // 遷移を許可するなら false を返せばステートマシンは状態の遷移をします
            return false;
        }

        // ステートマシンが前回のプッシュした状態に復帰する時の処理をするならこのGuardPopで行う
        protected internal override bool GuardPop()
        {
            // 復帰を拒否（ガード）するなら true を返せばステートマシンは復帰を諦めます
            if( !Context.isActiveAndEnabled )
            {
                return true;
            }

            // 復帰を許可するなら false を返せばステートマシンは復帰します
            return false;
        }
        */
    }
} 