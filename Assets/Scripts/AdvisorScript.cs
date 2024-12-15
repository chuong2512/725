using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvisorScript : MonoBehaviour {
    public string AdvisorName;
    public string OwnerName;
    public bool Owned;
    public int RatedStar;
    public bool InCurrentUse;
    public int Eightx;
    public float Minus25Cost;
    public int Global4x;
    public float Speed5X;
    public int NinexTap;
    public string UseInArea;
    public bool isPassive = false;
    // Use this for initialization
    void Start () {
	
	}
	void OnEnable()
    {
        if (AdvisorName.Length > 0)
        {
            if (transform.GetComponent<UISprite>())
            {
                GetInfo(AdvisorName);
                transform.GetComponent<UISprite>().spriteName = "Star"+RatedStar.ToString();

                

            }
        }
    }
    public string GetInfo(string nameOfAdviser)
    {
        switch (nameOfAdviser)
        {
            case "Tony Capory":
                   RatedStar = 1;
                   UseInArea = "Eightx";
                   OwnerName = "Ice-Creame Parlour";
                isPassive = false;
                return "8x Profit At Ice-Creame Parlour";
            case "Skree":
                RatedStar = 1;
                UseInArea = "Eightx";
                OwnerName = "Shopping Mall";
                isPassive = false;
                return "8x Profit At Shopping Mall";
            case "Mandy Maccoy":
                RatedStar = 1;
                UseInArea = "Minus25Cost";
                OwnerName = "Costa Rica";
                isPassive = false;
                return "-25% Milestone In Costa Rica";
            case "Rosy Hernandez":
               RatedStar = 1;
                UseInArea = "Minus25Cost";
                isPassive = false;
                OwnerName = "Skull Island";
                return "-25% Milestone In Skull Island";
            case "Peter Parker":
               RatedStar = 1;
                UseInArea = "Eightx";
                OwnerName = "Pizza Restaurant";
                isPassive = false;
                return "8x Profit At Pizza Restaurant";
            case "April Mama":
               RatedStar = 1;
                UseInArea = "Eightx";
                OwnerName = "Coffee Cafe";
                isPassive = false;
                return "8x Profit At Coffee Cafe";
            case "Angellina Jollie":
                RatedStar = 2;
                UseInArea = "NinexTap";
                OwnerName = "Costa Rica";
                isPassive = false;
                return "9x Tap Profit In Costa Rica";
            case "Derek Massa":
                RatedStar = 2;
                UseInArea = "NinexTap";
                OwnerName = "Skull Island";
                isPassive = false;
                return "9x Tap Profit In Skull Island";
            case "Rosy Gabanna":
               RatedStar = 1;
                UseInArea = "Eightx";
                isPassive = false;
                OwnerName = "Public Hospital";
                return "8x Profit At Public Hospital";
            case "Jackline Farnandise":
              RatedStar = 1;
                UseInArea = "Eightx";
                OwnerName = "Juice Parlour";
                isPassive = false;
                return "8x Profit At Juice Parlour";
            case "Robert Downey":
              RatedStar = 1;
                UseInArea = "Eightx";
                OwnerName = "Pitt's Restaurant";
                isPassive = false;
                return "8x Profit At Pitt's Restaurant";
            case "Daniel Enrique":
              RatedStar = 1;
                UseInArea = "Eightx";
                OwnerName = "Taj Hotel";
                isPassive = false;
                return "8x Profit At Taj Hotel";
            case "Yu Kai Hong":
             RatedStar = 2;
               UseInArea = "EighteenxCriticalTap";
                OwnerName = "Skull Island";
                isPassive = false;
                return "18x Critical Tap Profit In Skull Island";
            case "Sgt Reo De Genero":
              RatedStar = 2;
              UseInArea = "EighteenxCriticalTap";
                OwnerName = "Costa Rica";
                isPassive = false;
                return "18x Critical Tap Profit In Costa Rica";
            case "Stav Zilbershtein":
                RatedStar = 2;
                UseInArea = "Global4x";
                OwnerName = "Costa Rica";
                isPassive = false;
                return "4x Business Profit In Costa Rica";
            case "Marcella":
                RatedStar = 2;
                UseInArea = "Global4x";
                isPassive = false;
                OwnerName = "Skull Island";
                return "4x Business Profit In Skull Island";

            case "Betty Schiefelbein":
                RatedStar = 2;
                UseInArea = "FivexCriticalTapChance";
                OwnerName = "Skull Island";
                isPassive = false;
                return "5x Critical Tap Chance In Skull Island";
            case "Dale Vreede":
                RatedStar = 2;
                UseInArea = "FivexCriticalTapChance";
                OwnerName = "Costa Rica";
                isPassive = false;
                return "5x Critical Tap Chance In Costa Rica";
            case "David Nikki":
                RatedStar = 2;
                UseInArea = "TwoxBusinessBonusReward";
                OwnerName = "All";
                isPassive = false;
                return "2x Business Bonus Reward";
            case "Jack Conway":
                RatedStar = 2;
                UseInArea = "TenPercentMoreBizbot";
                OwnerName = "All";
                isPassive = false;
                return "10% More ProfitBots Activated";
            case "Sunny Leone":
                RatedStar = 2;
                UseInArea = "TwentyFivePercentPassiveAdvisor";
                OwnerName = "All";
                isPassive = true;
                return "25% Advisor Passive Bonus";
            case "Manuel Moreira":
                RatedStar = 2;
                UseInArea = "PointThreeBonusBizbot";
                OwnerName = "All";
                isPassive = false;
                return "+0.3% Bonus Per ProfitBots";
            case "Sam Davies":
                RatedStar = 3;
                UseInArea = "TwentyOnePercentMoreBizbot";
                OwnerName = "All";
                isPassive = false;
                return "21% More ProfitBots Activated";
            case "Terry Down":
                RatedStar = 3;
                UseInArea = "PointEightBonusBizbot";
                OwnerName = "All";
                isPassive = false;
                return "+0.8% Bonus Per ProfitBots";
            case "Sara Sultanali":
                RatedStar = 3;
                UseInArea = "FourtyPercentPassiveAdvisor";
                OwnerName = "All";
                isPassive = true;
                return "40% Advisor Passive Bonus";
            case "Sir Eisec Newton":
                RatedStar = 3;
                UseInArea = "FourteenxTap";
                OwnerName = "Costa Rica";
                isPassive = false;
                return "14x Tap Profit In Costa Rica";
            case "William Sexspear":
                RatedStar = 3;
                UseInArea = "FourteenxTap";
                OwnerName = "Skull Island";
                isPassive = false;
                return "14x Tap Profit In Skull Island";
            case "Bobby Wolmar":
                RatedStar = 3;
                UseInArea = "Speed5X";
                OwnerName = "Costa Rica";
                isPassive = false;
                return "5x Speed In Costa Rica";
            case "Harry Potter":
                RatedStar = 3;
                UseInArea = "Speed5X";
                isPassive = false;
                OwnerName = "Skull Island";
                return "5x Speed In Skull Island";
            case "Tony Stark":
                RatedStar = 3;
                UseInArea = "Gem2X";
                isPassive = false;
                OwnerName = "All";
                return "2x Gem Tap Chance";

            case "Mark Waugh":
                RatedStar = 4;
                isPassive = false;
                UseInArea = "MinusTenBizbotCost";
                OwnerName = "All";
                return "-10% ProfitBots Perk Cost";

            case "Kevin Peterson":
                RatedStar = 4;
                isPassive = true;
                UseInArea = "EightyPercentPassiveAdvisor";
                OwnerName = "All";
                return "80% Advisor Passive Bonus";
            case "Albert Stephan":
                RatedStar = 4;
                UseInArea = "FourtyPercentMoreBizbot";
                OwnerName = "All";
                isPassive = false;
                return "40% More ProfitBots Activated";
            case "Rajesh Khanna":
                RatedStar = 4;
                UseInArea = "ThreexBusinessBonusReward";
                OwnerName = "All";
                isPassive = false;
                return "3x Business Bonus Reward";

            case "Loren April":
                RatedStar = 4;
                UseInArea = "OnePointThreeBonusBizbot";
                OwnerName = "All";
                isPassive = false;
                return "+1.3% Bonus Per ProfitBots";
            case "Alice":
                RatedStar = 4;
                UseInArea = "Gem3X";
                OwnerName = "All";
                isPassive = false;
                return "3x Gem Tap Chance";

            case "Albert Einstien":
                RatedStar = 5;
                UseInArea = "GemPlusCriticalTapChance";
                OwnerName = "All";
                isPassive = false;
                return "2.7x Gem Tap Chance \n 5x Critical Tap Chance";

            case "Hillary Clinton":
                RatedStar = 5;
                isPassive = false;
                UseInArea = "CriticalTapProfitPlusChance";
                OwnerName = "All";
                return "18x Critical Tap Profit \n 5x Critical Tap Chance";
            case "Steve Jobs":
                RatedStar = 5;
                isPassive = true;
                UseInArea = "AdvisorPlusBizbot";
                OwnerName = "All";
                return "100% Advisor Passive Bonus \n +1% Bonus Per ProfitBots";
            default:
                return null;


        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
