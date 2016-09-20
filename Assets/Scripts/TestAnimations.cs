using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestAnimations : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnim(string id)
    {
        if (anim.GetBool(id))
        {
            anim.SetBool(id, false);
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.white;
        }
        else
        {
            anim.SetBool(id, true);
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color = Color.green;
        }
    }
}