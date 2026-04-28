using UnityEngine;
using System.Collections;

public class GroundTile : MonoBehaviour
{
    bool isTrap;
    bool triggered = false;
    public int posX;

    public void Init(int x, bool trap)
    {
        posX = x;
        isTrap = trap;
    }

    public void TryTrigger()
    {
        if (!isTrap) return;
        if (triggered) return;

        triggered = true;

        GameManager.Instance.OnTrapTriggered(posX);
    }

    public void Fall()
    {
        StartCoroutine(FallRoutine());
    }

    IEnumerator FallRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        GetComponent<Collider>().enabled = false;

        while (true)
        {
            transform.position += Vector3.down * 10f * Time.deltaTime;
            yield return null;
        }
    }
}