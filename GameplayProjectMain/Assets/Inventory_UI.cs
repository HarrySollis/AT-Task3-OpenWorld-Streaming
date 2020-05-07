
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryUI;
    
    // Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    { 
        if(Input.GetKeyDown("joystick button 3"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
	}
}
