using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    private string levelToLoad;

    public void FadeToLevel (string level)
    {
        levelToLoad = level;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        FindObjectOfType<Manager>().changeScreen(levelToLoad);
    }
}
