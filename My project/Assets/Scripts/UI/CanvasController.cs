using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UniRx;

using UniStorm;

public class CanvasController : MonoBehaviour
{
    [SerializeField] Button m_CountUpButton;
    [SerializeField] TMP_Text m_CountText;

    [SerializeField]
    private UniStormSystem uniStormSystem;

    // カウントを記録する
    private int count = 0;

    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        m_CountUpButton.OnClickAsObservable()
            .Subscribe( _ => CloudChange() )
            .AddTo( this );
    }

    private void CloudChange()
    {
        var weather = uniStormSystem.AllWeatherTypes[Random.Range(0, uniStormSystem.AllWeatherTypes.Count)];
        uniStormSystem.ChangeWeather(weather);
        m_CountText.text = weather.ToString();
    }
}
