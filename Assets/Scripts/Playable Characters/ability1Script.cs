using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ability1Script : MonoBehaviour {

    private GameObject knight;
    public Image KnightIcon;
    public GameObject[] weapons;
    public GameObject ability1Icon;
    public int isActiveToggle;
    private bool onCD;
    // Use this for initialization
    void Start ()
    {
        knight = FindObjectOfType<KnightStats>().gameObject;
        isActiveToggle = 0;
        onCD = false;
    }
    // Update is called once per frame
    void Update ()
    {
        if (ability1Icon.GetComponent<Image>().fillAmount < 1)
        {
            ability1Icon.GetComponent<Image>().fillAmount = ability1Icon.GetComponent<Image>().fillAmount + .0018f;
        }
        if (onCD == false)
        {
            if (Input.GetKeyDown("1") && GetComponentInParent<KnightStats>().energy > 0 && isActiveToggle == 0)
            {
                isActiveToggle = 1;
            }
            else if (Input.GetKeyDown("1") && GetComponentInParent<KnightStats>().energy > 0 && isActiveToggle == 1)
            {
                isActiveToggle = 2;
            }
                if (isActiveToggle ==1 && knight.GetComponent<KnightStats>().energy > 0)
            {
                knight.transform.parent.localScale= new Vector3(1+knight.GetComponent<KnightStats>().SPBonusScale, 1+ knight.GetComponent<KnightStats>().SPBonusScale, 1+ knight.GetComponent<KnightStats>().SPBonusScale);
                knight.GetComponent<Player1Controls>().Ab1 = 1.5f;
                knight.GetComponent<KnightStats>().Ab1 = 2;
                knight.GetComponent<SpriteRenderer>().color = Color.red;
                KnightIcon.GetComponent<Image>().color = Color.red;
                foreach (GameObject weapon in weapons)
                {
                    weapon.GetComponent<SpriteRenderer>().color = Color.red;
                }
                knight.GetComponent<KnightStats>().energy = knight.GetComponent<KnightStats>().energy - .1f;
            }
            if (isActiveToggle == 2 || knight.GetComponent<KnightStats>().energy < 1) //code only runs the frame ability1 is turned off
            {
                knight.transform.parent.localScale = new Vector3(1, 1, 1);
                knight.GetComponent<Player1Controls>().Ab1 = 1f;
                knight.GetComponent<KnightStats>().Ab1 = 1;
                knight.GetComponent<SpriteRenderer>().color = Color.white;
                KnightIcon.GetComponent<Image>().color = Color.white;
                foreach (GameObject weapon in weapons)
                {
                    weapon.GetComponent<SpriteRenderer>().color = Color.white;
                }
                isActiveToggle = 0;
                StartCoroutine(Ability1onCD());
            }
        }
    }

    IEnumerator Ability1onCD()
    {
        ability1Icon.GetComponent<Image>().fillAmount = 0;
        isActiveToggle = 3;
        ability1Icon.GetComponent<Image>().color = new Color32(128, 113, 113, 255);
        yield return new WaitForSeconds(5f);
        ability1Icon.GetComponent<Image>().color = Color.white;
        isActiveToggle = 0;
    }
}
