using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UniRx;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Button m_CountUpButton;
    [SerializeField] TMP_Text m_CountText;

    // カウントを記録する
    private int count = 0;

    void Start()
    {
        m_CountText.text = count.ToString();
        Setup();
    }

    private void Setup()
    {
        m_CountUpButton.OnClickAsObservable()
            .Subscribe( _ => CountUp() )
            .AddTo( this );
    }

    private void CountUp()
    {
        count++;
        m_CountText.text = count.ToString();
    }
}
