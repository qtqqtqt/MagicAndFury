using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SkillCooldowns : MonoBehaviour
{
   
    public bool startCD;
    private bool startGlobalCd;
    private Image skillIcon;
    private float currentCooldown;

    public Button[] skillButtons;

    public float cooldown;


    private void Start()
    {
        startCD = false;
        startGlobalCd = false;
        skillIcon = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        
        if (startCD == true)
        {

            if (currentCooldown < cooldown)
            {
                GetComponentInParent<Button>().interactable = false;
                currentCooldown += Time.deltaTime;
                skillIcon.fillAmount = currentCooldown / cooldown;
            } 
            
        }
        if (currentCooldown >= cooldown)
        {
            currentCooldown = 0;
            startCD = false;
            GetComponentInParent<Button>().interactable = true;

        }

        if(startGlobalCd == true)
        {
            for (int i =0; i < skillButtons.Length; i++)
            {
                skillButtons[i].interactable = false;
                StartCoroutine(CanTap(skillButtons[i]));
            }
 
        }
    }

    IEnumerator CanTap(Button button)
    {
        yield return new WaitForSeconds(0.3f);
        startGlobalCd = false;
        Image icon = button.transform.Find("Image").GetComponent<Image>();
        if(!icon.GetComponent<SkillCooldowns>().startCD)
           button.interactable = true;
    }

    public void StartCD()
    {
        startCD = true;
        startGlobalCd = true;
    }
}
