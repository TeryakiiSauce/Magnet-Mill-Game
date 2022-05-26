using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D mainCursor, cursorClicked;
    public Canvas menu;

    private void Awake()
    {
        //if(Application.platform == RuntimePlatform.OSXEditor)
        //{
        //    gameObject.SetActive(false);
        //    return;
        //}
        SwitchCursor(mainCursor);

        Cursor.lockState = CursorLockMode.None; // removed: keeps the cursor within game window
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuController.IsPauseMenuOpened())
        {
            if (menu.isActiveAndEnabled)
            {
                Cursor.lockState = CursorLockMode.None; // removed: keeps the cursor within game window
                Cursor.visible = true;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.LeftControl))
                {
                    Cursor.lockState = CursorLockMode.None; // removed: keeps the cursor within game window
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked; // locks the cursor to the middle
                    Cursor.visible = false;
                    return;
                }
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // removed: keeps the cursor within game window~
            Cursor.visible = true;
        }

        // Check for mouse input to change cursor icon
        if (Input.GetKey(KeyCode.Mouse0 /* left click */))
        {
            SwitchCursor(cursorClicked);
        }
        else
        {
            SwitchCursor(mainCursor);
        }
    }

    private void SwitchCursor(Texture2D whichCursor)
    {
        Vector2 hotspot = new Vector2(whichCursor.width / 2, whichCursor.height / 2); // Stores the center point of the texture passed (i.e. makes the cursor clickable from the middle point)
        Cursor.SetCursor(whichCursor, hotspot, CursorMode.Auto);
    }
}
