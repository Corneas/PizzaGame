using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperoniTurret : MonoBehaviour
{
    [SerializeField]
    private GameObject pepperoniPre;

    public void Start()
    {
        StartCoroutine(CircleFire());
    }

    public IEnumerator CircleFire()
    {
        for(int j = 0; j < 4; j++)
        {
            for (int i = 0; i < 360; i += 13)
            {
                GameObject pepperroni = Instantiate(pepperoniPre, transform.position, Quaternion.Euler(0, i, 0));
                transform.SetParent(null);
                yield return new WaitForSeconds(0.05f);
            }
        }
        Destroy(gameObject);
        yield break;
    }
}
