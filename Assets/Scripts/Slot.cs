using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Slot RightSlot;
    public Slot LeftSlot;
    public Slot DownSlot;
    public Slot UpSlot;
    public Slot UpRightSlot;
    public Slot DownRightSlot;
    public Slot UpLeftSlot;
    public Slot DownLeftSlot;
    public Grid Controller;
    public enum Selection {Nothing, Cross, Circle};
    public enum Camefrom { None, CameFromRight, CameFromLeft, CameFromDown, CameFromUp, CameFromUpRight, CameFromUpLeft, CameFromDownRight, CameFromDownLeft };
    public Selection SE;
    public Camefrom CF;
    public SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = null; // turns the white slots visible
        SE = Selection.Nothing;
        Controller.Numberofslots++;
    }

    void OnMouseOver()
    {
        if(SE == Selection.Nothing && Controller.CurrentTurn == Grid.Turn.Player)// turns the white slots visible
        {
            spriteRenderer.sprite = Controller.Square;
        }
    }
    void OnMouseDown()
    {
        Controller.FillSlot(this, true); //initializes the current unity


    }
    void OnMouseExit()
    {
        if (SE == Selection.Nothing && Controller.CurrentTurn == Grid.Turn.Player) // turns the white slots invisible
        {
            spriteRenderer.sprite = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
