using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;

//Enumerators for all the game phases
public enum GameStatus { PlaceCards, Choose1stCard, Reveal1stCard, Choose2ndCard, Reveal2ndCard, Check, Stare, End };

public class GameManager : MonoBehaviour
{
    //initialize the enumerator
    public static GameStatus gameStatus;

    //Array of Cards to be randmized and move arround
    [SerializeField] GameObject[] Cards;
    [SerializeField] Transform[] Targets;
    [SerializeField] Transform InitialTarget;
    
    //The Card you first select
    GameObject FirstCard;
    string FirstCardName = "";

    //The Card you compare it to
    GameObject SecondCard;
    string SecondCardName = "";

    //Check if the words match
    string PlaceHolderName = "";
    string SwitchHolderName = "";

    //Check if the words match
    bool Matched = false;

    //Before the Start Functiom
    private void Awake()
    {
        //Set the Status to when you place the cards
        gameStatus = GameStatus.PlaceCards;

        //Move all cards to outside the screen
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].transform.position = InitialTarget.transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //randomize the cards in the array
        for (var i = Cards.Length-1; i > 0; i--)
        {
            var r = Random.Range(0, i);
            var tmp = Cards[i];
            Cards[i] = Cards[r];
            Cards[r] = tmp;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //When its time to place the cards
        if(gameStatus == GameStatus.PlaceCards)
        {
            //Move all cards to their respective posiitons
            for(int i = 0; i < Cards.Length; i++)
            {
                Cards[i].transform.position = Vector3.MoveTowards(Cards[i].transform.position, Targets[i].transform.position, 30f * Time.deltaTime);
            }

            //After they move, change the pase to choosing a cars
            if (Cards[0].transform.position == Targets[0].transform.position)
            {
                gameStatus = GameStatus.Choose1stCard;
            }
        }

        //When it is time to choose the first card
        if(gameStatus == GameStatus.Choose1stCard)
        {
            //Create an array when clicking the mouse
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 30f);

                //If the array hits an object, store the name of the object in a variable and move to the next phase
                if (Physics.Raycast(ray, out hit, 100))
                {
                    FirstCardName = hit.collider.gameObject.name;
                    print(FirstCardName);
                    gameStatus = GameStatus.Reveal1stCard;
                }
            }
        }

        //Reveal what the card is.
        if(gameStatus == GameStatus.Reveal1stCard)
        {
            FirstCard = GameObject.Find(FirstCardName);
            FirstCard.transform.rotation = Quaternion.Slerp(FirstCard.transform.rotation, Quaternion.Euler(0, 180, 0), 0.2f);
            if (FirstCard.transform.rotation == Quaternion.Euler(0, -180, 0))
            {
                gameStatus = GameStatus.Choose2ndCard;
            }
        }

        //When it is time to choose the second card
        if (gameStatus == GameStatus.Choose2ndCard)
        {
            //Create an array when clicking the mouse
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 30f);

                //If the array hits an object, store the name of the object in a variable and move to the next phase
                if (Physics.Raycast(ray, out hit, 100))
                {
                    //if the second object is the same as the first object, do not do anything, if it is not, move on to the next pase
                    PlaceHolderName = hit.collider.gameObject.name;
                    if (PlaceHolderName != FirstCardName + " (1)" || PlaceHolderName != FirstCardName + " (2)")
                    {
                        SecondCardName = PlaceHolderName;
                        print(SecondCardName);
                        gameStatus = GameStatus.Reveal2ndCard;
                    }
                }
            }
        }

        //Reveal what the card is.
        if (gameStatus == GameStatus.Reveal2ndCard)
        {
            SecondCard = GameObject.Find(SecondCardName);
            SecondCard.transform.rotation = Quaternion.Slerp(SecondCard.transform.rotation, Quaternion.Euler(0, 180, 0), 0.2f);
            if (SecondCard.transform.rotation == Quaternion.Euler(0, -180, 0))
            {
                gameStatus = GameStatus.Check;
            }
        }

        //Check whether the cards match
        if (gameStatus == GameStatus.Check)
        {
            //Create a string that removes the numbers from the card names
            SwitchHolderName = FirstCardName.Substring(0,6);

            //check if the second card equals the first card, if it does, change the boolean to true, else, change it to false
            switch(SecondCardName)
            {
                case "Circle1":
                    var circle1 = SwitchHolderName == "Circle" ? Matched = true : Matched = false;
                    break;
                case "Circle2":
                    var circle2 = SwitchHolderName == "Circle" ? Matched = true : Matched = false;
                    break;
                case "Square1":
                    var sqaure1 = SwitchHolderName == "Square" ? Matched = true : Matched = false;
                    break;
                case "Square2":
                    var sqaure2 = SwitchHolderName == "Square" ? Matched = true : Matched = false;
                    break;
                case "Rectan1":
                    var rectan1 = SwitchHolderName == "Rectan" ? Matched = true : Matched = false;
                    break;
                case "Rectan2":
                    var rectan2 = SwitchHolderName == "Rectan" ? Matched = true : Matched = false;
                    break;
                case "Triang1":
                    var triang1 = SwitchHolderName == "Triang" ? Matched = true : Matched = false;
                    break;
                case "Triang2":
                    var triang2 = SwitchHolderName == "Triang" ? Matched = true : Matched = false;
                    break;
                case "Diamon1":
                    var diamon1 = SwitchHolderName == "Diamon" ? Matched = true : Matched = false;
                    break;
                case "Diamon2":
                    var diamon2 = SwitchHolderName == "Diamon" ? Matched = true : Matched = false;
                    break;
                case "Starss1":
                    var starss1 = SwitchHolderName == "Starss" ? Matched = true : Matched = false;
                    break;
                case "Starss2":
                    var starss2 = SwitchHolderName == "Starss" ? Matched = true : Matched = false;
                    break;
            }

            //change the phase to stare
            gameStatus = GameStatus.Stare;
        }

        if (gameStatus == GameStatus.Stare)
        {
            //call a couroutine
            StartCoroutine(Wait());
        }

        //when it is time to activate the results
        if(gameStatus == GameStatus.End)
        {
            //if the cards match, delete the cards
            if(Matched)
            {
                Destroy(FirstCard);
                Destroy(SecondCard);
            }

            //if they dont match, flip the cards to the original position
            if(!Matched)
            {
                SecondCard.transform.rotation = Quaternion.Slerp(SecondCard.transform.rotation, Quaternion.Euler(0, 0, 0), 0.2f);
                FirstCard.transform.rotation = Quaternion.Slerp(FirstCard.transform.rotation, Quaternion.Euler(0, 0, 0), 0.2f);
            }

            //After the cards is fliped, repeat the round
            if(FirstCard != null && SecondCard != null)
            {
                if (SecondCard.transform.rotation == Quaternion.Euler(0, 0, 0) && FirstCard.transform.rotation == Quaternion.Euler(0, 0, 0))
                {
                    gameStatus = GameStatus.Choose1stCard;
                    print(Cards.Length.ToString());
                }
            }
            //After the cards is deleted, repeat the round
            else
            {
                gameStatus = GameStatus.Choose1stCard;
                print(Cards.Length.ToString());
            }
        }
    }

    //Reload the scene
    public void ReloadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    //After one second, change the phase to "End" 
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        gameStatus = GameStatus.End;
    }
}