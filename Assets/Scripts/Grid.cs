using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public enum Turn { Player, Machine, Victory } 
    public Turn CurrentTurn;
    public Sprite Square; 
    public Sprite X;
    public Slot[] Slots; 
    public Slot SlotsA1;
    public Slot SlotsA2;
    public Slot SlotsA3;
    public Slot SlotsB1;
    public Slot SlotsB2;
    public Slot SlotsB3;
    public Slot SlotsC1;
    public Slot SlotsC2;
    public Slot SlotsC3;
    public TMP_Text WinnerText;
    public int Numberofslots;
    public Sprite PlayerSprite;
    public Sprite MachineSprite;
    public Sprite O;
    public int Counter;
    // Start is called before the first frame update
    void Start()
    {
        WinnerText.text = "";
        Slots = new Slot[9];
        Slots = FindObjectsOfType<Slot>();
        float Randomsprite = Random.RandomRange(0,100);
        if (Randomsprite <= 50)
        {
            PlayerSprite = X;
            MachineSprite = O;
        }
        else
        {
            PlayerSprite = O;
            MachineSprite = X;
        }
        float Randomfirstturn = Random.RandomRange(0, 100);
        if(Randomfirstturn <= 50)
        {
            CurrentTurn = Turn.Player;

        }
        else
        {
            CurrentTurn = Turn.Machine;
            FillSlot(SlotsB2, false);
        }
    }
  public void FillSlot(Slot slot,bool clicked)
    {
        if (slot.SE == Slot.Selection.Nothing && CurrentTurn == Grid.Turn.Player && clicked)
        {
            Debug.Log("passed");
            CurrentTurn = Grid.Turn.Machine;
            slot.SE = Slot.Selection.Cross;
            slot.spriteRenderer.sprite = PlayerSprite;
            Counter = 0;
            GridChecker(slot);
            
        }
        else
        {
            if (slot.SE == Slot.Selection.Nothing && CurrentTurn == Grid.Turn.Machine && !clicked)
            {
                CurrentTurn = Grid.Turn.Player;
                slot.SE = Slot.Selection.Circle;
                slot.spriteRenderer.sprite = MachineSprite;
                Counter = 0;
                if (SlotsB2.SE == Slot.Selection.Nothing)
                {
                    FillSlot(SlotsB2, false);
                }
                GridChecker(slot);
            }
        }
        
    } 
  public  void GridChecker(Slot slot) //Checks slots for ai and checks for the victory
    {
        Counter++;
        if (SlotsA1.SE == SlotsA3.SE && SlotsA1.SE != Slot.Selection.Nothing && SlotsA3.SE != Slot.Selection.Nothing) //checks for empty slots in the middle of two extremities for the ai to stop them
        {
            FillSlot(SlotsA2, false);
        }
        if (SlotsB1.SE == SlotsB3.SE && SlotsB1.SE != Slot.Selection.Nothing && SlotsB3.SE != Slot.Selection.Nothing)//checks for empty slots in the middle of two extremities for the ai to stop them
        {
            FillSlot(SlotsB2, false);
        }
        if (SlotsC1.SE == SlotsC3.SE && SlotsC1.SE != Slot.Selection.Nothing && SlotsC3.SE != Slot.Selection.Nothing)//checks for empty slots in the middle of two extremities for the ai to stop them
        {
            FillSlot(SlotsC2, false);
        }
        if (SlotsA1.SE == SlotsC1.SE && SlotsA1.SE != Slot.Selection.Nothing && SlotsC1.SE != Slot.Selection.Nothing)//checks for empty slots in the middle of two extremities for the ai to stop them
        {
            FillSlot(SlotsB1, false);
        }
        if (SlotsA2.SE == SlotsC2.SE && SlotsA2.SE != Slot.Selection.Nothing && SlotsC2.SE != Slot.Selection.Nothing)//checks for empty slots in the middle of two extremities for the ai to stop them
        {
            FillSlot(SlotsB2, false);
        }
        if (SlotsA3.SE == SlotsC3.SE && SlotsA3.SE != Slot.Selection.Nothing && SlotsC3.SE != Slot.Selection.Nothing)//checks for empty slots in the middle of two extremities for the ai to stop them
        {
            FillSlot(SlotsB3, false);
        }
        if (SlotsB2.SE == Slot.Selection.Nothing)
        {
            FillSlot(SlotsB2, false);
        }
        if (Counter == 2 && slot.CF == Slot.Camefrom.CameFromLeft) //Reponses to player almost filling a row
        {
            if (slot.LeftSlot.LeftSlot)
            {
                FillSlot(slot.LeftSlot.LeftSlot, false);
                Counter = 0;
                slot.LeftSlot.LeftSlot.Controller.GridChecker(slot.LeftSlot.LeftSlot);
                return;
            }

        }
        if (Counter == 2 && slot.CF == Slot.Camefrom.CameFromLeft) //Reponses to player almost filling a row
        {
            if (slot.LeftSlot.LeftSlot)
            {
                FillSlot(slot.LeftSlot.LeftSlot,false);
                Counter = 0;
                slot.LeftSlot.LeftSlot.Controller.GridChecker(slot.LeftSlot.LeftSlot);
                return;
            }
            
        }
        if (Counter == 2 && slot.CF == Slot.Camefrom.CameFromRight)//Reponses to player almost filling a row
        {
            if (slot.RightSlot.RightSlot)
            {
                FillSlot(slot.RightSlot.RightSlot, false);
                Counter = 0;
                slot.RightSlot.RightSlot.Controller.GridChecker(slot.RightSlot.RightSlot);
                return;
            }

        }
        if (Counter == 2 && slot.CF == Slot.Camefrom.CameFromDown)//Reponses to player almost filling a row
        {
            if (slot.DownSlot.DownSlot)
            {
                FillSlot(slot.DownSlot.DownSlot, false);
                Counter = 0;
                slot.DownSlot.DownSlot.Controller.GridChecker(slot.DownSlot.DownSlot);
                return;
            }

        }
        if (Counter == 2 && slot.CF == Slot.Camefrom.CameFromUp)//Reponses to player almost filling a row
        {
            if (slot.UpSlot.UpSlot)
            {
                FillSlot(slot.UpSlot.UpSlot, false);
                Counter = 0;
                slot.UpSlot.UpSlot.Controller.GridChecker(slot.UpSlot.UpSlot);
                return;
            }

        }
        if (Counter == 2 && slot.CF == Slot.Camefrom.CameFromUpRight)//Reponses to player almost filling a row
        {
            if (slot.UpRightSlot.UpRightSlot)
            {
                FillSlot(slot.UpRightSlot.UpRightSlot, false);
                Counter = 0;
                slot.UpRightSlot.UpRightSlot.Controller.GridChecker(slot.UpRightSlot.UpRightSlot);
                return;
            }

        }
        if (Counter == 2 && slot.CF == Slot.Camefrom.CameFromUpLeft) //Reponses to player almost filling a row
        {
            if (slot.UpLeftSlot.UpLeftSlot)
            {
                FillSlot(slot.UpLeftSlot.UpLeftSlot, false);
                Counter = 0;
                slot.UpLeftSlot.UpLeftSlot.Controller.GridChecker(slot.UpLeftSlot.UpLeftSlot);
                return;
            }

        }
        if (Counter == 2 && slot.CF == Slot.Camefrom.CameFromDownLeft) //Reponses to player almost filling a row
        {
            if (slot.DownLeftSlot.DownLeftSlot)
            {
                FillSlot(slot.DownLeftSlot.DownLeftSlot, false);
                Counter = 0;
                slot.DownLeftSlot.DownLeftSlot.Controller.GridChecker(slot.DownLeftSlot.DownLeftSlot);
                return;
            }

        }
        if (Counter == 2 && slot.CF == Slot.Camefrom.CameFromDownRight)//Reponses to player almost filling a row
        {
            if (slot.DownRightSlot.DownRightSlot)
            {
                FillSlot(slot.DownRightSlot.DownRightSlot, false);
                Counter = 0;
                slot.DownRightSlot.DownRightSlot.Controller.GridChecker(slot.DownRightSlot.DownRightSlot);
                return;
            }

        }
        
        if (Counter == 3) //if 3 items on a row are the same Victory for who has achieved it
        {
            if(CurrentTurn == Turn.Machine)
            {
                WinnerText.text = "You won!"; //Message for when the players Wins
                WinnerText.color = Color.blue;
            }
            if (CurrentTurn == Turn.Player) //Message for when the players loses
            {
                WinnerText.text = "You Lose!";
                WinnerText.color = Color.red;
            }
            Counter = 0;
            CurrentTurn = Turn.Victory;
            return;
        }
        if (slot.CF == Slot.Camefrom.None || slot.CF == Slot.Camefrom.CameFromLeft) //PathTracing to get how many slots are left to win
        {
            if (slot.RightSlot)
            {
               
               
                if (slot.RightSlot.SE == slot.SE)
                {
                    slot.RightSlot.CF = Slot.Camefrom.CameFromLeft;
                    GridChecker(slot.RightSlot);
                    Counter = 1;
                }

            }
        }

        if (slot.CF == Slot.Camefrom.None || slot.CF == Slot.Camefrom.CameFromRight)//PathTracing to get how many slots are left to win or be blocked by the enemy
        {
            if (slot.LeftSlot)
            {
                if (slot.LeftSlot.SE == slot.SE)
                {
                    slot.LeftSlot.CF = Slot.Camefrom.CameFromRight;
                    GridChecker(slot.LeftSlot);
                    Counter = 1;
                }

            }
        }

        if (slot.CF == Slot.Camefrom.None || slot.CF == Slot.Camefrom.CameFromUp)//PathTracing to get how many slots are left to win or be blocked by the enemy
        {
            if (slot.DownSlot)
            {
                if (slot.DownSlot.SE == slot.SE)
                {
                    slot.DownSlot.CF = Slot.Camefrom.CameFromUp;
                    GridChecker(slot.DownSlot);
                    Counter = 1;
                }

            }
        }

        if (slot.CF == Slot.Camefrom.None || slot.CF == Slot.Camefrom.CameFromDown)//PathTracing to get how many slots are left to win or be blocked by the enemy
        {
            if (slot.UpSlot)
            {
                if (slot.UpSlot.SE == slot.SE)
                {
                    slot.UpSlot.CF = Slot.Camefrom.CameFromDown;
                    GridChecker(slot.UpSlot);
                    Counter = 1;
                }

            }
        }

        if (slot.CF == Slot.Camefrom.None || slot.CF == Slot.Camefrom.CameFromUpLeft)//PathTracing to get how many slots are left to win or be blocked by the enemy
        {
            if (slot.UpRightSlot)
            {
                
                if (slot.UpRightSlot.SE == slot.SE)
                {
                    slot.UpRightSlot.CF = Slot.Camefrom.CameFromUpLeft;
                    GridChecker(slot.UpRightSlot);
                    Counter = 1;

                }

            }
        }

        if (slot.CF == Slot.Camefrom.None || slot.CF == Slot.Camefrom.CameFromDownRight)//PathTracing to get how many slots are left to win or be blocked by the enemy
        {
            
            if (slot.UpLeftSlot)
            {
                
                if (slot.UpLeftSlot.SE == slot.SE)
                {
                    slot.UpLeftSlot.CF = Slot.Camefrom.CameFromDownRight;

                    GridChecker(slot.UpLeftSlot);
                    Counter = 1;

                }

            }
        }
        if (slot.CF == Slot.Camefrom.None || slot.CF == Slot.Camefrom.CameFromUpRight)//PathTracing to get how many slots are left to win or be blocked by the enemy
        {

            if (slot.DownLeftSlot)
            {

                if (slot.DownLeftSlot.SE == slot.SE)
                {
                    slot.DownLeftSlot.CF = Slot.Camefrom.CameFromUpRight;

                    GridChecker(slot.DownLeftSlot);
                    Counter = 1;

                }

            }
        }
        if (slot.CF == Slot.Camefrom.None || slot.CF == Slot.Camefrom.CameFromDownLeft)//PathTracing to get how many slots are left to win or be blocked by the enemy
        {
            if (slot.DownRightSlot)
            {

                if (slot.DownRightSlot.SE == slot.SE)
                {
                    slot.DownRightSlot.CF = Slot.Camefrom.CameFromDownLeft;
                    GridChecker(slot.DownRightSlot);
                    Counter = 1;

                }

            }
        }
        if (SlotsB2.SE == Slot.Selection.Circle)
        {
            if (SlotsA1.SE == Slot.Selection.Nothing && SlotsC3.SE == Slot.Selection.Nothing) // random AI movement
            {
                float randompick = Random.RandomRange(0, 100);
                if (randompick <= 50)
                {
                    FillSlot(SlotsA1, false);
                }
                else
                {
                    FillSlot(SlotsC3, false);
                }



            }
        }
        if (SlotsB2.SE == Slot.Selection.Circle)
        {
            if (SlotsA1.SE == Slot.Selection.Nothing || SlotsC3.SE == Slot.Selection.Nothing) //activates another slot that was not used in the random sl=election
            {
                if (SlotsA1.SE == Slot.Selection.Circle)
                {
                    FillSlot(SlotsC3, false);

                }
                else
                {
                    FillSlot(SlotsA1, false);
                }



            }
        }
        if (SlotsB2.SE == Slot.Selection.Circle)// random AI movement
        {
            if (SlotsC1.SE == Slot.Selection.Nothing && SlotsA3.SE == Slot.Selection.Nothing)
            {
                float randompick = Random.Range(0, 100);
                if (randompick <= 50)
                {
                    FillSlot(SlotsC1, false);
                }
                else
                {
                    FillSlot(SlotsA3, false);
                }



            }
        }
        if (SlotsB2.SE == Slot.Selection.Circle)//activates another slot that was not used in the random sl=election
        {
            if (SlotsA3.SE == Slot.Selection.Nothing || SlotsC1.SE == Slot.Selection.Nothing)
            {
                if (SlotsA3.SE == Slot.Selection.Circle)
                {
                    FillSlot(SlotsC1, false);

                }
                else
                {
                    FillSlot(SlotsA3, false);
                }



            }
        }
       
        if(SlotsB2.SE == Slot.Selection.Circle && SlotsA1.SE == Slot.Selection.Cross)
        {

        }
        if(SlotsB1.SE != Slot.Selection.Nothing && SlotsB2.SE != Slot.Selection.Nothing && SlotsB3.SE != Slot.Selection.Nothing)
        {
            if((SlotsA1.SE == Slot.Selection.Nothing && SlotsA1.SE == Slot.Selection.Circle) && (SlotsC1.SE == Slot.Selection.Nothing || SlotsC1.SE == Slot.Selection.Circle) && SlotsB1.SE == Slot.Selection.Circle)
            {
                float Randompick = Random.Range(0, 100);
                if(Randompick<= 50 && SlotsA1.SE == Slot.Selection.Nothing)
                {
                    FillSlot(SlotsA1, false);
                }
                else
                {
                    FillSlot(SlotsC1, false);
                }
            }
            if ((SlotsA1.SE == Slot.Selection.Nothing || SlotsC1.SE == Slot.Selection.Nothing) && SlotsB1.SE == Slot.Selection.Circle)
            {
                if (SlotsA1.SE == Slot.Selection.Nothing)
                {
                    FillSlot(SlotsA1, false);
                }
                else
                {
                    FillSlot(SlotsC1, false);
                }

            }

            if ((SlotsA2.SE == Slot.Selection.Nothing && SlotsA2.SE == Slot.Selection.Circle) && (SlotsC2.SE == Slot.Selection.Nothing || SlotsC2.SE == Slot.Selection.Circle) && SlotsB2.SE == Slot.Selection.Circle)
            {
                float Randompick = Random.Range(0, 100);
                if (Randompick <= 50 && SlotsA2.SE == Slot.Selection.Nothing)
                {
                    FillSlot(SlotsA2, false);
                }
                else
                {
                    FillSlot(SlotsC2, false);
                }
            }
            if ((SlotsA2.SE == Slot.Selection.Nothing || SlotsC2.SE == Slot.Selection.Nothing) && SlotsB2.SE == Slot.Selection.Circle)
            {
                if (SlotsA1.SE == Slot.Selection.Nothing)
                {
                    FillSlot(SlotsA2, false);
                }
                else
                {
                    FillSlot(SlotsC2, false);
                }

            }
            if ((SlotsA3.SE == Slot.Selection.Nothing && SlotsA3.SE == Slot.Selection.Circle) && (SlotsC3.SE == Slot.Selection.Nothing || SlotsC3.SE == Slot.Selection.Circle) && SlotsB3.SE == Slot.Selection.Circle)
            {
                float Randompick = Random.Range(0, 100);
                if (Randompick <= 50 && SlotsA3.SE == Slot.Selection.Nothing)
                {
                    FillSlot(SlotsA3, false);
                }
                else
                {
                    FillSlot(SlotsC3, false);
                }
            }
            if ((SlotsA3.SE == Slot.Selection.Nothing || SlotsC3.SE == Slot.Selection.Nothing) && SlotsB3.SE == Slot.Selection.Circle)
            {
                if (SlotsA1.SE == Slot.Selection.Nothing)
                {
                    FillSlot(SlotsA3, false);
                }
                else
                {
                    FillSlot(SlotsC3, false);
                }

            }
        }
        int checkallslots = 0;
        if (CurrentTurn == Turn.Machine) //uses random moves when the AI doesn't have a path
        {
            
            for (int i = 0; i <= 8; i++)
            {
                Debug.Log("Turn");
                if (Slots[i].SE == Slot.Selection.Nothing)
                {
                    Debug.Log(checkallslots);
                    FillSlot(Slots[i], false);
                    

                }
                if (Slots[i].SE == Slot.Selection.Nothing)
                {
                    checkallslots += 1;



                }



            }
            Debug.Log(checkallslots);
            if (checkallslots == 0) // Checks if there is a drawn
            {
                WinnerText.text = "Draw!";
                WinnerText.color = Color.red;
            }
        }
        
        Counter = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
