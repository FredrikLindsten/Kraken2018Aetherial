using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_tutorial_sprite : MonoBehaviour {


    private void Update()
    {

    }

    private void Start()
    {
        StartCoroutine(Script());
    }

    IEnumerator Script()
    {
        yield return new WaitForSeconds(4);
        scr_utilities.leviathan.GetComponent<scr_leviathan>().StopAllCoroutines();
        Activate(0);
        yield return new WaitForSeconds(6);
        Deactivate(0);
        Activate(1);
        yield return new WaitForSeconds(6);
        Deactivate(1);

        Activate(2);
        yield return new WaitForSeconds(2);
        Activate(13);
        while(!(Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.F)))
            yield return null;
        Deactivate(2);
        Deactivate(13);
        yield return new WaitForSeconds(4);

        Activate(3);
        Activate(9);
        scr_cloudcontroller.instance.SetIslandCount(3);
        GameObject notQuiteAWhale = Instantiate(Resources.Load("obj_skyslug"), new Vector3(scr_utilities.GetEdge(edgeId.Right, false), 0), Quaternion.identity) as GameObject;
        while (notQuiteAWhale.GetComponent<scr_hpsystem>().getHealth() > 0)
            yield return null;
        yield return new WaitForSeconds(2);
        Deactivate(3);
        Deactivate(9);
        scr_utilities.instance.aetherLeft = 0;
        yield return new WaitForSeconds(2);
        

        Activate(4);
        Activate(14);
        while(scr_cloudcontroller.instance.crystalSpawnTimers.Count < 100)
            scr_cloudcontroller.instance.crystalSpawnTimers.Add(0);
        while (scr_utilities.instance.aetherLeft < 1)
        { scr_utilities.instance.aetherLeft = 0;
            yield return null; }
        scr_cloudcontroller.instance.crystalSpawnTimers.Clear();
        yield return new WaitForSeconds(2);
        Deactivate(4);
        Deactivate(14);

        scr_utilities.leviathan.GetComponent<scr_leviathan>().Appear();
        while (scr_utilities.leviathan.transform.position.y < -8f)
            yield return null;
        Activate(5);
        while (GameObject.FindGameObjectWithTag("Harpoon").GetComponent<Harpoon>().harpState != Harpoon.STATE.STUCK)
            yield return null;
        Deactivate(5);
        yield return new WaitForSeconds(2);

        Activate(6);
        while (GameObject.FindGameObjectWithTag("Harpoon").GetComponent<Harpoon>().harpState == Harpoon.STATE.STUCK)
            yield return null;
        Deactivate(6);
        yield return new WaitForSeconds(2);

        Activate(7);
        while (scr_utilities.player.GetComponent<scr_powerup>().powerupCharges < 1)
            yield return null;
        Deactivate(7);
        yield return new WaitForSeconds(2);

        Activate(8);
        Activate(12);
        yield return new WaitForSeconds(6);
        Deactivate(8);
        Deactivate(12);

        Activate(16);
        yield return new WaitForSeconds(6);
        Deactivate(16);
    }

    void Activate(int number)
    {
        StartCoroutine(ActivateCo(number));
    }

    IEnumerator ActivateCo(int number)
    {
        Transform child = transform.GetChild(number);
        child.gameObject.SetActive(true);
        float fade = 0;
        while(fade<1)
        {
            fade += Time.deltaTime;
            child.GetComponent<CanvasRenderer>().SetAlpha(fade);
            yield return null;
        }
        child.GetComponent<CanvasRenderer>().SetAlpha(1);
    }

    void Deactivate(int number)
    {
        StartCoroutine(DeactivateCo(number));
    }

    IEnumerator DeactivateCo(int number)
    {
        Transform child = transform.GetChild(number);
        float fade = 1;
        while (fade > 0)
        {
            fade -= Time.deltaTime;
            child.GetComponent<CanvasRenderer>().SetAlpha(fade);
            yield return null;
        }
        child.GetComponent<CanvasRenderer>().SetAlpha(0);
        child.gameObject.SetActive(false);
    }
}
