using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelection : MonoBehaviour
{
    private GameObject[] Cars;
    private int index;
    public Vector3 rotacja_aktualna;
    private void Start()
    {
        index = 0;
        Cars = new GameObject[transform.childCount];
        // Fill the array with models
        for (int i = 0; i < transform.childCount; i++)
        {
            Cars[i] = transform.GetChild(i).gameObject;
        }
        // Toggle off their render
        foreach (GameObject go in Cars)
        {
            go.SetActive(false);
        }
        // Toggle on the first index
        Cars[0].SetActive(true);
            //Cars[index].gameObject.SetActive(true);
    }
    public void Update()
    {
        rotacja_aktualna=Cars[index].transform.eulerAngles;
    }
    public void ToggleLeft()
    {
        Cars[index].SetActive(false);
        index--;
        if (index < 0)
            index = Cars.Length - 1;

        Cars[index].SetActive(true);
    }
    public void ToggleRight()
    {
        Cars[index].SetActive(false);
        index++;
        if (index ==Cars.Length)
            index = 0;

        Cars[index].SetActive(true);
    }
    public void ChooseButton ()
    {
        PlayerPrefs.SetInt("CharactersSelected", index);
        SceneManager.LoadScene("Game");
    }

}

