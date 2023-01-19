using UnityEngine;

using TMPro;
public class DamageUI3DPop : MonoBehaviour
{
    [SerializeField]
    private GameObject m_DamageTextObj;
    [SerializeField]
    private Vector3 m_AdjPos;

    public void ViewDamage( int damage )
    {
        GameObject damageObj = Instantiate(m_DamageTextObj);
        damageObj.GetComponent<TextMeshPro>().text = damage.ToString();
        damageObj.transform.position = transform.position + m_AdjPos;
    }
}
