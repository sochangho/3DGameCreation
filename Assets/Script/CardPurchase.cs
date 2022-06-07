using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPurchase : MonoBehaviour
{

    private bool is_enter = false;
    private bool is_pop = false;

    public SkinnedMeshRenderer skinned;
    public Animator animator;
    public Canvas canvas;
    public ShopPopup shopPopup;

    private void OnMouseEnter()
    {

        if (!is_enter && !is_pop)
        {
            is_enter = true;
            CharacterColor(Color.red);
            EnterAim();
        }


    }


    private void OnMouseExit()
    {
        if (is_enter)
        {
            is_enter = false;
            CharacterColor(Color.white);

        }


    }


    private void OnMouseDown()
    {
        if (is_enter && !is_pop)
        {
            is_enter = false;
            is_pop = true;
           
            CharacterColor(Color.white);
            Invoke("ShopPopupOpen", 2f);
            ClickAim();
        }

    }

    private void CharacterColor(Color color)
    {

        Material[] materials = skinned.materials;
        materials[0].color = color;

    }


    private void ShopPopupOpen()
    {
       var go =  Instantiate(shopPopup);
        go.transform.parent = canvas.transform;
        go.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
    }

    public void ShopPopupClose()
    {
        is_pop = false;

    }

    private void ClickAim()
    {

        animator.SetTrigger("Click");

    }
    private void EnterAim()
    {

        animator.SetTrigger("Enter");
    }

}
