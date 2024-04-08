using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameHandler : MonoBehaviour
{
    private float duration = 2f;
    [SerializeField]
    private GameObject newspaperGameobject;
    public void StartEndGame()
    {
        StartCoroutine(FreezeTimeToZero());
    }

    private IEnumerator FreezeTimeToZero()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.fixedDeltaTime;
            //Debug.Log(elapsedTime);

            float timeScale = Mathf.Lerp(0.5f, 0f, (elapsedTime / duration));
            Time.timeScale = timeScale;

            yield return null;
        }

        LoadnewsPaper();
        yield return new WaitForSecondsRealtime(8f);
        // Load thank you scene here
        Debug.Log("Load Thank you scene");
    }

    private void LoadnewsPaper()
    {
        newspaperGameobject.SetActive(true);
    }
}
