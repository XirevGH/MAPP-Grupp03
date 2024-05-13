using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;

    public Animator transition;

    public float transitionTime = 1f;

    public void ChangeScene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneToLoad);
    }
}
