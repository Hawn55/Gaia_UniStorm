using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    [SerializeField] int MaxHP = 3;
    public int NowHP { get; set; }

    private void Start()
    {
        NowHP = MaxHP;
    }
}
