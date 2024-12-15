using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManagerOfBusiness : MonoBehaviour {
    public long totalcoin;
    public long totalcoinTillStart;
    public long totalgem;
    long visiblescroll;
    public UILabel mytotalcoin, mytotalgenerationcoin, mytotalgem;
    public UIRoot mainroot;
    public ShopSelector[] ShopSelectorList;
    public BuildingSelector[] buildingSelectorList;
    public long tapvaluecounter = 0;
    public GameObject MainMenuScreen;
    public GameObject MenuScreen;
    public GameObject GetNewAdvisorScreen;
    public GameObject ActivateBizBotsScreen;

    public GameObject DailyQuestScreen;

    public GameObject AdvisorScreen;
    public GameObject BusinessBonusScreen;

    public GameObject ProfileBoostScreen;
    public GameObject SettingScreen;
    public GameObject ShoppingScreen;
   
    public GameObject CollectBonusScreen;
    public GameObject CollectionScreen;
    public GameObject AdvisorSpinScreen;
    private List<AdvisorScript> Myadvisors = new List<AdvisorScript> { };
    private List<AdvisorScript> CurrentInUseAdvisor = new List<AdvisorScript> { };
    public UITable MyAdvisorTable;
    public GameObject AdvisorListScale;
    private bool showingAdvisorOrNot = false;
    public float springStrength = 0.002f;
    private int elementsPerPage;
    private UIScrollView scrollView;
    private int currentScrolledElements;
    private Vector3 startingScrollPosition;
    private UIGrid grid;
    private int indexforthattravel;
    public GameObject AdviosrTablePrefab;
    private int currentSelectedAdvisor;

    public int buisnessbonusmultiplier = 1;
    public int buisnessbonusmultiplier3x = 1;
    public float tenpercentbizbotmultiplier = 1;
    public float twnetyonepercentbizbotmultiplier = 1;
    public float bizbotbonuspoint3 = 1;
    public float bizbotbonuspoint8 = 1;
    public float bizbotbonus1point3 = 1;
    public float bizbotbonus1 = 1;
    public float twnetyfivepercentpassiveadvisorbonus = 0;
    public float Foutrypercentpassiveadvisorbonus = 0;
    public float FoutrypercentMoreBizbot = 1;
    public float Eightypercentpassiveadvisorbonus = 0;
    public float Hundredpercentpassiveadvisorbonus = 0;
    public float Minustenpercentbizbotcost = 1;
    public float bizBotProfit = 2;
    public int TotalBizbot = 0;
    public int FreeSpin = 0;
    public float timeleft = 150, seconds, minutes, second, minute, hour;
    public long coinAddedForBonus;
    public int GemAddedForBonus;
    public int TwoXProfit = 1;
    public bool TwoXPrfoitRun = false;
    public float TwoXProfitTime = 0;
    public GameObject BusinessBonusGameObject;
    public GameObject TwoXPofitGameObject;
    public GameObject IntroductoryButton;
    public GameObject WelcomeScreen;
    public GameObject ActivateBizbotActionScreen;
    public long moneyPlus = 0;
    public GameObject getAdvisorPopUp, dailyQuestPopup;
    public DateTime LastLogintime;
    public UISlider progressbar1, progressbar2, progressbar3;
    public UIButton progressbutton1, progressbutton2, progressbutton3, boxholderbutton;
    public UISprite progress1, progress2, progress3;
    public UILabel progressbarlable1, progressbarlable2, progressbarlable3;
    public int shopupgradeindex = 0, taphouseindex = 0, criticaltapindex = 0;
    public GameObject AnimatedBox;
    public int chanceOfBonus = 0;

    public UIButton freespinbutton, spinwith20gembutton, spinwith50gembutton, spinagainwith50gembutton;

    public AudioSource buildingUpgrade;
    public AudioSource moneyIncome;
    public AudioSource advisorSound;
    public AudioSource spinrotatesound;

    [SerializeField]
    public class Save
    {
        public long TotalCoin = 0;
        public long TotalCoinTillStart = 0;
        public long averageEarningPerSecond = 0;
        public long TotalGem = 0;
        public int TotalBizbot = 0;
        public int FreeSpin = 0;
        public string LastLogintime;
        public List<string> Myadvisors = new List<string> { };
        public List<string> CurrentInUseAdvisor = new List<string> { };
        public List<int> buildingIndex = new List<int> { };
        public List<double> tapvalueCounter = new List<double> { };
        public List<int> valueofcounter = new List<int> { };
    }

    void SaveData()
    {
        Save saveData = new Save();
        saveData.TotalCoin = totalcoin;
        saveData.TotalGem = totalgem;
        saveData.TotalCoinTillStart = totalcoinTillStart;
        saveData.TotalBizbot = TotalBizbot;
        saveData.FreeSpin = FreeSpin;
        saveData.averageEarningPerSecond = coinGeneratePerSec;
        saveData.LastLogintime = DateTime.Now.ToBinary().ToString();
        for (int i = 0; i < Myadvisors.Count; i++)
        {
            saveData.Myadvisors.Add(Myadvisors[i].AdvisorName);
        }
        for (int i = 0; i < CurrentInUseAdvisor.Count; i++)
        {
            saveData.CurrentInUseAdvisor.Add(CurrentInUseAdvisor[i].AdvisorName);
        }
        for (int i = 0; i < ShopSelectorList.Length; i++)
        {
            saveData.buildingIndex.Add(ShopSelectorList[i].buildingIndex);
        }
        for (int i = 0; i < buildingSelectorList.Length; i++)
        {
            saveData.tapvalueCounter.Add(buildingSelectorList[i].tapvaluecounter);
        }
        for (int i = 0; i < buildingSelectorList.Length; i++)
        {
            saveData.valueofcounter.Add(buildingSelectorList[i].valueOfbuilding);
        }
        //Convert to Json
        string jsonData = JsonUtility.ToJson(saveData);
        //Save Json string
        PlayerPrefs.SetString("MySettings", jsonData);
        PlayerPrefs.Save();
    }
    public void CollectMoneyButtonPressed()
    {
        CoinAdded(moneyPlus);
        WelcomeScreen.SetActive(false);
    }
    public bool shouldPreload = false;
    public bool contentIsReadyForPlacement = false;
   
    void OnEnable()
    {
        Debug.Log("C# PlacementExample Enable -- Adding Tapjoy Placement delegates");
    
    }
	public void HandleConnectSuccess()
{
	Debug.Log ("Connect Success");

	//Now that we are connected we can start preloading our placements
  
}
    void OnDisable()
    {
        Debug.Log("C#: Disabling and removing Tapjoy Delegates");

     
    }
   
    public void FreeGemsPressed()
    {
		Debug.Log("Free Gems Pressed");
      
    }
    void Load()
    {
       // PlayerPrefs.DeleteAll();
        //Load saved Json
        string jsonData = PlayerPrefs.GetString("MySettings");
        //Convert to Class
        if (jsonData.Length > 0)
        {
            Save loadedData = JsonUtility.FromJson<Save>(jsonData);
            print("loadedData" + jsonData);
            totalcoin = loadedData.TotalCoin;
            totalgem = loadedData.TotalGem;
            TotalBizbot = loadedData.TotalBizbot;
            totalcoinTillStart = loadedData.TotalCoinTillStart;

            FreeSpin = loadedData.FreeSpin;
            long temp = Convert.ToInt64(loadedData.LastLogintime);
            DateTime currentDate = DateTime.Now;
            DateTime oldDate = DateTime.FromBinary(temp);

            TimeSpan difference = currentDate.Subtract(oldDate);
            WelcomeScreen.SetActive(true);
            moneyPlus = loadedData.averageEarningPerSecond * difference.Seconds;
            if (difference.Hours / 24 >= 1)
            {
                FreeSpin += 1;
            }
            WelcomeScreen.transform.Find("MainLable").GetComponent<UILabel>().text = "You' have earned \n" + MyString(moneyPlus) + "\nWhile you've been Away! \n\n You were gone for \n" + string.Format("{0:D2}:{1:D2}:{2:D2}", difference.Hours, difference.Minutes, difference.Seconds);

            for (int i = 0; i < loadedData.Myadvisors.Count; i++)
            {
                AdvisorScript newadvisor = new AdvisorScript();
                newadvisor.AdvisorName = loadedData.Myadvisors[i];
                newadvisor.GetInfo(newadvisor.AdvisorName);

                newadvisor.InCurrentUse = false;
                Myadvisors.Add(newadvisor);

            }
            for (int i = 0; i < loadedData.CurrentInUseAdvisor.Count; i++)
            {
                AdvisorScript newadvisor = new AdvisorScript();
                newadvisor.AdvisorName = loadedData.CurrentInUseAdvisor[i];
                newadvisor.GetInfo(newadvisor.AdvisorName);

                newadvisor.InCurrentUse = true;
                for (int jk = 0; jk < Myadvisors.Count; jk++)
                {
                    if (Myadvisors[jk].AdvisorName.Equals(loadedData.CurrentInUseAdvisor[i]))
                    {
                        Myadvisors[jk].InCurrentUse = true;
                        break;
                    }
                }


                CurrentInUseAdvisor.Add(newadvisor);
            }
            for (int i = 0; i < loadedData.buildingIndex.Count; i++)
            {
                ShopSelectorList[i].buildingIndex = loadedData.buildingIndex[i];

            }
            for (int i = 0; i < loadedData.tapvalueCounter.Count; i++)
            {
                buildingSelectorList[i].tapvaluecounter = loadedData.tapvalueCounter[i];

            }
            for (int i = 0; i < loadedData.valueofcounter.Count; i++)
            {
                buildingSelectorList[i].valueOfbuilding = loadedData.valueofcounter[i];

            }

        }
        else
        {
            totalcoin = 11;
            totalgem = 50;
            totalcoinTillStart = 0;
            FreeSpin = 1;
            IntroductoryButton.SetActive(true);
            IntroductoryButton.GetComponent<TweenScale>().PlayForward();
            TwoXPofitGameObject.GetComponent<TweenPosition>().PlayReverse();

        }
        /* //Display saved data
         Debug.Log("Extra: " + loadedData.TotalCoin);
         Debug.Log("High Score: " + loadedData.TotalGem);
         */


    }
    IEnumerator WaitToAddSpin()
    {
        yield return new WaitForSeconds(5f);
        if (ShopSelectorList[0].buildingIndex > 0)
        {
            FreeSpin = 1;
            getAdvisorPopUp.transform.GetComponent<TweenPosition>().PlayForward();
            getAdvisorPopUp.transform.GetComponent<TweenScale>().PlayForward();
        }
    }
    public void DailyQuestPopUpPressed()
    {
        dailyQuestPopup.GetComponent<TweenPosition>().PlayReverse();
        DailyQuestButtonPressed();
    }
    // Use this for initialization
    void Start() {
        //   totalcoin = 11;
        // totalgem = 50;
       Load();
        mainroot.gameObject.SetActive(false);
        mainroot.gameObject.SetActive(true);
        CollectionScreen.SetActive(false);
        tapvaluecounter = 4;

        if (Myadvisors.Count > 0)
        {
            AdvisorScreen.transform.Find("CurrentAdvisorInfo").gameObject.SetActive(true);

            for (int i = 0; i < Myadvisors.Count; i++)
            {
                if (Myadvisors[i].InCurrentUse == true)
                {

                }
                else
                {
                    GameObject tempprefab = Instantiate(AdviosrTablePrefab, AdviosrTablePrefab.transform.position, AdviosrTablePrefab.transform.rotation);
                    tempprefab.transform.Find("AdvisorBack").GetComponent<UISprite>().spriteName = Myadvisors[i].RatedStar.ToString();
                    tempprefab.transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = Myadvisors[1].GetInfo(Myadvisors[i].AdvisorName);
                    int tempStar = Myadvisors[i].RatedStar;
                    tempprefab.transform.Find("Star1").gameObject.SetActive(false);
                    tempprefab.transform.Find("Star2").gameObject.SetActive(false);
                    tempprefab.transform.Find("Star3").gameObject.SetActive(false);
                    tempprefab.transform.Find("Star4").gameObject.SetActive(false);
                    tempprefab.transform.Find("Star5").gameObject.SetActive(false);
                    for (int ids = 1; ids <= tempStar; ids++)
                    {
                        tempprefab.transform.Find("Star" + ids.ToString()).gameObject.SetActive(true);
                    }
                    AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorName").GetComponent<UILabel>().text = Myadvisors[i].AdvisorName;
                    AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorWork").GetComponent<UILabel>().text = Myadvisors[i].GetInfo(Myadvisors[i].AdvisorName);

                    tempprefab.transform.parent = AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform;
                }
            }

            AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.GetComponent<UIGrid>().Reposition();

            if (CurrentInUseAdvisor.Count > 0)
            {
                switch (CurrentInUseAdvisor.Count)
                {
                    case 1:
                        AdvisorScreen.transform.Find("ActiveAdvisor1").gameObject.SetActive(true);
                        break;
                    case 2:
                        AdvisorScreen.transform.Find("ActiveAdvisor1").gameObject.SetActive(true);
                        AdvisorScreen.transform.Find("ActiveAdvisor2").gameObject.SetActive(true);
                        break;
                    case 3:
                        AdvisorScreen.transform.Find("ActiveAdvisor1").gameObject.SetActive(true);
                        AdvisorScreen.transform.Find("ActiveAdvisor2").gameObject.SetActive(true);
                        AdvisorScreen.transform.Find("ActiveAdvisor3").gameObject.SetActive(true);
                        break;

                }
            }
            ActiveAdvisor1Pressed();
        }
        else
        {
            AdvisorScreen.transform.Find("ActiveAdvisor1").gameObject.SetActive(false);
            AdvisorScreen.transform.Find("ActiveAdvisor2").gameObject.SetActive(false);
            AdvisorScreen.transform.Find("ActiveAdvisor3").gameObject.SetActive(false);
            AdvisorScreen.transform.Find("CurrentAdvisorInfo").gameObject.SetActive(false);
        }
        ConsiderAdvisorParticipation();
    }

    void ConsiderAdvisorParticipation()
    {
        bizbotbonuspoint3 = 0; bizbotbonuspoint8 = 0; bizbotbonus1point3 = 0;
        twnetyfivepercentpassiveadvisorbonus = 0f;

        Foutrypercentpassiveadvisorbonus = 0f;


        Eightypercentpassiveadvisorbonus = 0f;

        Hundredpercentpassiveadvisorbonus = 0f;


        buisnessbonusmultiplier = 1;

        tenpercentbizbotmultiplier = 0;

        twnetyonepercentbizbotmultiplier = 0;

        Minustenpercentbizbotcost = 0;

        FoutrypercentMoreBizbot = 0;

        buisnessbonusmultiplier3x = 1;

        bizbotbonus1 = 0;

        for (int jd = 0; jd < ShopSelectorList.Length; jd++)
        {


            ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox1").GetChild(0).gameObject.SetActive(false);
            ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox2").GetChild(0).gameObject.SetActive(false);
            ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox3").GetChild(0).gameObject.SetActive(false);
            ShopSelectorList[jd].AdvisorInfoBox.transform.Find("Label").GetComponent<UILabel>().text = "";
            ShopSelectorList[jd].Eightx = 1;
            ShopSelectorList[jd].Global4x = 1;
            ShopSelectorList[jd].Speed5X = 1;
            ShopSelectorList[jd].Minus25Cost = 1;

        }
        for (int jd = 0; jd < buildingSelectorList.Length; jd++)
        {
            buildingSelectorList[jd].Tap9X = 1;
            buildingSelectorList[jd].Tap14X = 1;
            buildingSelectorList[jd].FivexCriticalTapChance = 1;
            buildingSelectorList[jd].CriticalTap18x = 1;
            buildingSelectorList[jd].Gem2x = 1;
            buildingSelectorList[jd].Gem3x = 1;
            buildingSelectorList[jd].Gem2Point7x = 1;
        }
        if (Myadvisors.Count > 0) {
            MainMenuScreen.transform.Find("AdvisorHolderButton").gameObject.SetActive(true);
            switch (CurrentInUseAdvisor.Count)
            {
                case 1:
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor1").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[0].RatedStar.ToString();
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor1").GetChild(0).GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[0].AdvisorName;
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor1").gameObject.SetActive(true);
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor2").gameObject.SetActive(false);
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor3").gameObject.SetActive(false);
                    break;
                case 2:
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor1").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[0].RatedStar.ToString();
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor1").GetChild(0).GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[0].AdvisorName;
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor2").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[1].RatedStar.ToString();
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor2").GetChild(0).GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[1].AdvisorName;
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor1").gameObject.SetActive(true);
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor2").gameObject.SetActive(true);
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor3").gameObject.SetActive(false);
                    break;
                case 3:
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor1").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[0].RatedStar.ToString();
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor1").GetChild(0).GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[0].AdvisorName;
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor2").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[1].RatedStar.ToString();
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor2").GetChild(0).GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[1].AdvisorName;
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor3").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[2].RatedStar.ToString();
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor3").GetChild(0).GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[2].AdvisorName;
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor1").gameObject.SetActive(true);
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor2").gameObject.SetActive(true);
                    MainMenuScreen.transform.Find("AdvisorHolderButton").Find("Advisor3").gameObject.SetActive(true);
                    break;
            }
        }
        else
        {
            MainMenuScreen.transform.Find("AdvisorHolderButton").gameObject.SetActive(false);
        }
        for (int i = 0; i < Myadvisors.Count; i++)
        {
            if (Myadvisors[i].isPassive == true)
            {
                switch (Myadvisors[i].UseInArea)
                {
                    case "TwentyFivePercentPassiveAdvisor":
                        twnetyfivepercentpassiveadvisorbonus = .25f;
                        break;
                    case "FourtyPercentPassiveAdvisor":
                        Foutrypercentpassiveadvisorbonus = .40f;
                        break;
                    case "EightyPercentPassiveAdvisor":
                        Eightypercentpassiveadvisorbonus = .80f;
                        break;
                    case "AdvisorPlusBizbot":
                        Hundredpercentpassiveadvisorbonus = 1.00f;
                        break;
                }
            }
        }
        for (int i = 0; i < CurrentInUseAdvisor.Count; i++)
        {


            switch (CurrentInUseAdvisor[i].UseInArea)
            {
                case "Eightx":
                    for (int jd = 0; jd < ShopSelectorList.Length; jd++)
                    {
                        if (ShopSelectorList[jd].buildingType == CurrentInUseAdvisor[i].OwnerName)
                        {

                            ShopSelectorList[jd].Eightx = (int)(8 + 8 * (twnetyfivepercentpassiveadvisorbonus) + 8 * (Foutrypercentpassiveadvisorbonus) + 8 * (Eightypercentpassiveadvisorbonus) + 8 * (Hundredpercentpassiveadvisorbonus));
                            ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
                            string nameOfBox = "AdvisorBox1";
                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox1").GetChild(0).gameObject.activeSelf == true)
                            {
                                nameOfBox = "AdvisorBox2";
                            }
                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox2").GetChild(0).gameObject.activeSelf == true)
                            {
                                nameOfBox = "AdvisorBox3";
                            }

                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox))
                            {
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).gameObject.SetActive(true);
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[i].RatedStar.ToString();
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).GetChild(0).GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[i].AdvisorName;
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find("Label").GetComponent<UILabel>().text = ShopSelectorList[jd].AdvisorInfoBox.transform.Find("Label").GetComponent<UILabel>().text + "\n8x Profit";
                            }
                        }
                        else
                        {
                            ShopSelectorList[jd].Eightx = 1;
                            ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
                        }

                    }
                    break;


                case "Minus25Cost":
                    for (int jd = 0; jd < ShopSelectorList.Length; jd++)
                    {
                        if (ShopSelectorList[jd].buildingCity == CurrentInUseAdvisor[i].OwnerName)
                        {

                            ShopSelectorList[jd].Minus25Cost = .75f - .75f * (twnetyfivepercentpassiveadvisorbonus) - .75f * (Foutrypercentpassiveadvisorbonus) - .75f * (Eightypercentpassiveadvisorbonus) - .75f * (Hundredpercentpassiveadvisorbonus);
                            ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
                            string nameOfBox = "AdvisorBox1";
                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox1").GetChild(0).gameObject.activeSelf == true)
                            {
                                nameOfBox = "AdvisorBox2";
                            }
                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox2").GetChild(0).gameObject.activeSelf == true)
                            {
                                nameOfBox = "AdvisorBox3";
                            }

                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox))
                            {
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).gameObject.SetActive(true);
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[i].RatedStar.ToString();
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).GetChild(0).GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[i].AdvisorName;
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find("Label").GetComponent<UILabel>().text = ShopSelectorList[jd].AdvisorInfoBox.transform.Find("Label").GetComponent<UILabel>().text + "\n-25% Profit";
                            }
                        }
                        else
                        {
                            ShopSelectorList[jd].Minus25Cost = 1f;
                            ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
                        }

                    }
                    break;
                case "NinexTap":
                    for (int jd = 0; jd < buildingSelectorList.Length; jd++)
                    {
                        if (buildingSelectorList[jd].buildingType == CurrentInUseAdvisor[i].OwnerName)
                        {
                            buildingSelectorList[jd].Tap9X = (int)(9 + 9 * (twnetyfivepercentpassiveadvisorbonus) + 9 * (Foutrypercentpassiveadvisorbonus) + 9 * (Eightypercentpassiveadvisorbonus) + 9 * (Hundredpercentpassiveadvisorbonus));


                        }
                        else
                        {
                            buildingSelectorList[jd].Tap9X = 1;

                        }

                    }
                    break;
                case "FourteenxTap":
                    for (int jd = 0; jd < buildingSelectorList.Length; jd++)
                    {
                        if (buildingSelectorList[jd].buildingType == CurrentInUseAdvisor[i].OwnerName)
                        {
                            buildingSelectorList[jd].Tap14X = (int)(14 + 14 * (twnetyfivepercentpassiveadvisorbonus) + 14 * (Foutrypercentpassiveadvisorbonus) + 14 * (Eightypercentpassiveadvisorbonus) + 14 * (Hundredpercentpassiveadvisorbonus));


                        }
                        else
                        {
                            buildingSelectorList[jd].Tap14X = 1;

                        }

                    }
                    break;
                case "EighteenxCriticalTap":
                    for (int jd = 0; jd < buildingSelectorList.Length; jd++)
                    {
                        if (buildingSelectorList[jd].buildingType == CurrentInUseAdvisor[i].OwnerName)
                        {
                            buildingSelectorList[jd].CriticalTap18x = (int)(18 + 18 * (twnetyfivepercentpassiveadvisorbonus) + 18 * (Foutrypercentpassiveadvisorbonus) + 18 * (Eightypercentpassiveadvisorbonus) + 18 * (Hundredpercentpassiveadvisorbonus));


                        }
                        else
                        {
                            buildingSelectorList[jd].CriticalTap18x = 1;

                        }

                    }
                    break;
                case "FivexCriticalTapChance":
                    for (int jd = 0; jd < buildingSelectorList.Length; jd++)
                    {
                        if (buildingSelectorList[jd].buildingType == CurrentInUseAdvisor[i].OwnerName)
                        {
                            buildingSelectorList[jd].FivexCriticalTapChance = (int)(5 + 5 * (twnetyfivepercentpassiveadvisorbonus) + 5 * (Foutrypercentpassiveadvisorbonus) + 5 * (Eightypercentpassiveadvisorbonus) + 5 * (Hundredpercentpassiveadvisorbonus));


                        }
                        else
                        {
                            buildingSelectorList[jd].FivexCriticalTapChance = 1;

                        }

                    }
                    break;
                case "TwoxBusinessBonusReward":
                    buisnessbonusmultiplier = (int)(2 + 2 * (twnetyfivepercentpassiveadvisorbonus) + 2 * (Foutrypercentpassiveadvisorbonus) + 2 * (Eightypercentpassiveadvisorbonus) + 2 * (Hundredpercentpassiveadvisorbonus));
                    break;
                case "TenPercentMoreBizbot":
                    tenpercentbizbotmultiplier = .1f + .1f * (twnetyfivepercentpassiveadvisorbonus) + .1f * (Foutrypercentpassiveadvisorbonus) + .1f * (Eightypercentpassiveadvisorbonus) + .1f * (Hundredpercentpassiveadvisorbonus);
                    break;
                case "PointThreeBonusBizbot":
                    bizbotbonuspoint3 = .003f + .003f * (twnetyfivepercentpassiveadvisorbonus) + .003f * (Foutrypercentpassiveadvisorbonus) + .003f * (Eightypercentpassiveadvisorbonus) + .003f * (Hundredpercentpassiveadvisorbonus);
                    break;
                case "TwentyOnePercentMoreBizbot":
                    twnetyonepercentbizbotmultiplier = .21f + .21f * (twnetyfivepercentpassiveadvisorbonus) + .21f * (Foutrypercentpassiveadvisorbonus) + .21f * (Eightypercentpassiveadvisorbonus) + .21f * (Hundredpercentpassiveadvisorbonus);
                    break;
                case "PointEightBonusBizbot":
                    bizbotbonuspoint8 = .008f + .008f * (twnetyfivepercentpassiveadvisorbonus) + .008f * (Foutrypercentpassiveadvisorbonus) + .008f * (Eightypercentpassiveadvisorbonus) + .008f * (Hundredpercentpassiveadvisorbonus);
                    break;
                case "Gem2X":
                    for (int jd = 0; jd < buildingSelectorList.Length; jd++)
                    {
                        if (buildingSelectorList[jd].buildingType == CurrentInUseAdvisor[i].OwnerName)
                        {
                            buildingSelectorList[jd].Gem2x = (int)(2 + 2 * (twnetyfivepercentpassiveadvisorbonus) + 2 * (Foutrypercentpassiveadvisorbonus) + 2 * (Eightypercentpassiveadvisorbonus) + 2 * (Hundredpercentpassiveadvisorbonus));


                        }
                        else
                        {
                            buildingSelectorList[jd].Gem2x = 1;

                        }

                    }
                    break;
                case "Gem3X":
                    for (int jd = 0; jd < buildingSelectorList.Length; jd++)
                    {
                        if (buildingSelectorList[jd].buildingType == CurrentInUseAdvisor[i].OwnerName)
                        {
                            buildingSelectorList[jd].Gem3x = (int)(3 + 3 * (twnetyfivepercentpassiveadvisorbonus) + 3 * (Foutrypercentpassiveadvisorbonus) + 3 * (Eightypercentpassiveadvisorbonus) + 3 * (Hundredpercentpassiveadvisorbonus));


                        }
                        else
                        {
                            buildingSelectorList[jd].Gem3x = 1;

                        }

                    }
                    break;
                case "MinusTenBizbotCost":
                    Minustenpercentbizbotcost = .1f + .1f * (twnetyfivepercentpassiveadvisorbonus) + .1f * (Foutrypercentpassiveadvisorbonus) + .1f * (Eightypercentpassiveadvisorbonus) + .1f * (Hundredpercentpassiveadvisorbonus);
                    break;
                case "FourtyPercentMoreBizbot":
                    FoutrypercentMoreBizbot = .4f + .4f * (twnetyfivepercentpassiveadvisorbonus) + .4f * (Foutrypercentpassiveadvisorbonus) + .4f * (Eightypercentpassiveadvisorbonus) + .4f * (Hundredpercentpassiveadvisorbonus);
                    break;
                case "ThreexBusinessBonusReward":
                    buisnessbonusmultiplier3x = (int)(3 + 3 * (twnetyfivepercentpassiveadvisorbonus) + 3 * (Foutrypercentpassiveadvisorbonus) + 3 * (Eightypercentpassiveadvisorbonus) + 3 * (Hundredpercentpassiveadvisorbonus));
                    break;
                case "OnePointThreeBonusBizbot":
                    bizbotbonus1point3 = .013f + .013f * (twnetyfivepercentpassiveadvisorbonus) + .013f * (Foutrypercentpassiveadvisorbonus) + .013f * (Eightypercentpassiveadvisorbonus) + .013f * (Hundredpercentpassiveadvisorbonus);
                    break;
                case "AdvisorPlusBizbot":
                    bizbotbonus1 = (int)(.01f + .01f * (twnetyfivepercentpassiveadvisorbonus) + .01f * (Foutrypercentpassiveadvisorbonus) + .01f * (Eightypercentpassiveadvisorbonus) + .01f * (Hundredpercentpassiveadvisorbonus));

                    break;
                case "GemPlusCriticalTapChance":
                    for (int jd = 0; jd < buildingSelectorList.Length; jd++)
                    {

                        buildingSelectorList[jd].Gem2Point7x = 2.7f + 2.7f * (twnetyfivepercentpassiveadvisorbonus) + 2.7f * (Foutrypercentpassiveadvisorbonus) + 2.7f * (Eightypercentpassiveadvisorbonus) + 2.7f * (Hundredpercentpassiveadvisorbonus);

                        buildingSelectorList[jd].FivexCriticalTapChance = (int)(5 + 5 * (twnetyfivepercentpassiveadvisorbonus) + 5 * (Foutrypercentpassiveadvisorbonus) + 5 * (Eightypercentpassiveadvisorbonus) + 5 * (Hundredpercentpassiveadvisorbonus));

                    }
                    break;
                case "CriticalTapProfitPlusChance":
                    for (int jd = 0; jd < buildingSelectorList.Length; jd++)
                    {

                        buildingSelectorList[jd].CriticalTap18x = (int)(18 + 18 * (twnetyfivepercentpassiveadvisorbonus) + 18 * (Foutrypercentpassiveadvisorbonus) + 18 * (Eightypercentpassiveadvisorbonus) + 18 * (Hundredpercentpassiveadvisorbonus));

                        buildingSelectorList[jd].FivexCriticalTapChance = (int)(5 + 5 * (twnetyfivepercentpassiveadvisorbonus) + 5 * (Foutrypercentpassiveadvisorbonus) + 5 * (Eightypercentpassiveadvisorbonus) + 5 * (Hundredpercentpassiveadvisorbonus));

                    }
                    break;


                case "Global4x":
                    for (int jd = 0; jd < ShopSelectorList.Length; jd++)
                    {
                        if (ShopSelectorList[jd].buildingCity == CurrentInUseAdvisor[i].OwnerName)
                        {

                            ShopSelectorList[jd].Global4x = (int)(4 + 4 * (twnetyfivepercentpassiveadvisorbonus) + 4 * (Foutrypercentpassiveadvisorbonus) + 4 * (Eightypercentpassiveadvisorbonus) + 4 * (Hundredpercentpassiveadvisorbonus));
                            ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
                            string nameOfBox = "AdvisorBox1";
                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox1").GetChild(0).gameObject.activeSelf == true)
                            {
                                nameOfBox = "AdvisorBox2";
                            }
                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox2").GetChild(0).gameObject.activeSelf == true)
                            {
                                nameOfBox = "AdvisorBox3";
                            }

                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox))
                            {
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).gameObject.SetActive(true);
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[i].RatedStar.ToString();
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).GetChild(0).GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[i].AdvisorName;
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find("Label").GetComponent<UILabel>().text = ShopSelectorList[jd].AdvisorInfoBox.transform.Find("Label").GetComponent<UILabel>().text + "\n4x Profit";
                            }
                        }
                        else
                        {
                            ShopSelectorList[jd].Global4x = 1;
                            ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
                        }
                    }
                    break;
                case "Speed5X":
                    for (int jd = 0; jd < ShopSelectorList.Length; jd++)
                    {
                        if (ShopSelectorList[jd].buildingCity == CurrentInUseAdvisor[i].OwnerName)
                        {

                            ShopSelectorList[jd].Speed5X = 5 + 5 * (twnetyfivepercentpassiveadvisorbonus) + 5 * (Foutrypercentpassiveadvisorbonus) + 5 * (Eightypercentpassiveadvisorbonus) + 5 * (Hundredpercentpassiveadvisorbonus);
                            ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
                            string nameOfBox = "AdvisorBox1";
                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox1").GetChild(0).gameObject.activeSelf == true)
                            {
                                nameOfBox = "AdvisorBox2";
                            }
                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find("AdvisorBox2").GetChild(0).gameObject.activeSelf == true)
                            {
                                nameOfBox = "AdvisorBox3";
                            }

                            if (ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox))
                            {
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).gameObject.SetActive(true);
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[i].RatedStar.ToString();
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find(nameOfBox).GetChild(0).GetChild(0).GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[i].AdvisorName;
                                ShopSelectorList[jd].AdvisorInfoBox.transform.Find("Label").GetComponent<UILabel>().text = ShopSelectorList[jd].AdvisorInfoBox.transform.Find("Label").GetComponent<UILabel>().text + "\n5x Speed";
                            }
                        }
                        else
                        {
                            ShopSelectorList[jd].Speed5X = 1;
                            ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
                        }
                    }
                    break;
            }

        }
        bizBotProfit = 0.02f + bizbotbonuspoint3 + bizbotbonuspoint8 + bizbotbonus1point3 + bizbotbonus1;
        for (int jd = 0; jd < ShopSelectorList.Length; jd++)
        {
            ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
        }
    }





    string MyString(long numberg)
    {
        string jkd;
        if (numberg > 1000000000000000000)
            jkd = "$ " + (numberg / 1000000000000000000f).ToString("F") + "QUINTILLION";
        else if (numberg > 1000000000000000)
            jkd = "$ " + (numberg / 1000000000000000f).ToString("F") + "QUADRILLION";
        else if (numberg > 1000000000000)
            jkd = "$ " + (numberg / 1000000000000f).ToString("F") + " TRILLION";
        else if (numberg > 1000000000)
            jkd = "$ " + (numberg / 1000000000f).ToString("F") + " BILLION";
        else if (numberg > 1000000)
            jkd = "$ " + (numberg / 1000000f).ToString("F") + " MILLION";
        else
            jkd = "$ " + numberg.ToString();
        return jkd;
    }
    IEnumerator CountTo(long target)
    {
        long start = visiblescroll;
        for (float timer = 0; timer < .1f; timer += Time.deltaTime)
        {
            float progress = timer / .1f;
            totalcoin = (long)Mathf.Lerp((float)start, (float)target, progress);
            yield return null;
        }
        totalcoin = target;
        ConsiderUIButton();
        SaveData();
    }
    void ChangeVisibleScroll(long i)
    {
        totalcoin = i;
    }
    public void CoinAdded(long coinad)
    {
        visiblescroll = totalcoin;
        totalcoin += coinad;
        totalcoinTillStart = totalcoinTillStart + coinad;
        ConsiderUIButton();
        // StartCoroutine(CountTo(totalcoin));
        //  ConsiderUIButton();

    }
    public void CoinSubtract(long coinad)
    {
        visiblescroll = totalcoin;
        totalcoin -= coinad;
        if(totalcoin < 0)
        {
            totalcoin = 0;
        }
        ConsiderUIButton();
        //        StartCoroutine(CountTo(totalcoin));
        //   ConsiderUIButton();

    }
    public void ConsiderUIButton()
    {
        for (int i = 0; i < ShopSelectorList.Length; i++)
        {
            if (totalcoin >= ShopSelectorList[i].valueOfPurchase)
            {
                ShopSelectorList[i].InsideBuyButton.alpha = 1f;
            }
            else
            {
                ShopSelectorList[i].InsideBuyButton.alpha = .75f;
            }
        }
    }

    public void TapValueCounter(string homename, float value)
    {
        for (int i = 0; i < buildingSelectorList.Length; i++)
        {
            if (buildingSelectorList[i].buildingType.Equals(homename))
            {
                buildingSelectorList[i].tapvaluecounter = buildingSelectorList[i].tapvaluecounter + value;
                break;
            }
        }
        SaveData();
    }
    public void UpdateHome(string homename)
    {

        for (int i = 0; i < buildingSelectorList.Length; i++)
        {
            if (buildingSelectorList[i].buildingType.Equals(homename))
            {
                buildingSelectorList[i].UpdateHome();
            }
        }
        SaveData();

    }
    public long GetValueOtTap()
    {
        return tapvaluecounter;
    }

    public long coinGeneratePerSec = 0;
    private long S1, S2, S3, S4, S5, S6, S7, S8;

    public void CountForCoin(string nameOfBuilding, long newValue)
    {
        if (nameOfBuilding.Equals(ShopSelectorList[0].buildingType))
        {
            S1 = newValue;
        }
        else if (nameOfBuilding.Equals(ShopSelectorList[1].buildingType))
        {
            S2 = newValue;
        }
        else if (nameOfBuilding.Equals(ShopSelectorList[2].buildingType))
        {
            S3 = newValue;
        }
        else if (nameOfBuilding.Equals(ShopSelectorList[3].buildingType))
        {
            S4 = newValue;
        }
        coinGeneratePerSec = S1 + S2 + S3 + S4;
    }

    public void GetAdvisorPopUpPressed()
    {
        getAdvisorPopUp.GetComponent<TweenPosition>().PlayReverse();
        MainMenuScreen.SetActive(true);
        MenuScreen.SetActive(false);
        if(FreeSpin > 0)
        {
            freespinbutton.isEnabled = true;
        }
        else
        {
            freespinbutton.isEnabled = false;
        }

        if(totalgem >= 20)
        {
            spinwith20gembutton.isEnabled = true;
        }
        else
        {
            spinwith20gembutton.isEnabled = false;
        }

        if(totalgem >= 50)
        {
            spinwith50gembutton.isEnabled = true;
        }
        else
        {
            spinwith50gembutton.isEnabled = false;
        }

        GetNewAdvisorScreen.SetActive(true);
        ActivateBizBotsScreen.SetActive(false);

        DailyQuestScreen.SetActive(false);

        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);

        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
       
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
        showingAdvisorOrNot = false;
        ShowingRandomAdvisor();
    }


    // Update is called once per frame

    public void BuisnessBonusPressed()
    {
        MainMenuScreen.SetActive(true);
        MenuScreen.SetActive(false);

        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);

        DailyQuestScreen.SetActive(false);

        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(true);

        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
      
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
        // chanceOfBonus = UnityEngine.Random.Range(0, 3);

        chanceOfBonus = 2;
        switch (chanceOfBonus)
        {
            case 0:
                coinAddedForBonus = coinGeneratePerSec * 150 * buisnessbonusmultiplier * buisnessbonusmultiplier3x;
                BusinessBonusScreen.transform.Find("MainDialoge").Find("BonusLable").GetComponent<UILabel>().text = "Bonus :" + MyString(coinAddedForBonus);
                BusinessBonusGameObject.GetComponent<TweenPosition>().PlayReverse();
                break;
            case 1:
                GemAddedForBonus = 3 * buisnessbonusmultiplier * buisnessbonusmultiplier3x;
                BusinessBonusGameObject.GetComponent<TweenPosition>().PlayReverse();
                BusinessBonusScreen.transform.Find("MainDialoge").Find("BonusLable").GetComponent<UILabel>().text = "Bonus : + " + GemAddedForBonus.ToString() + " Gem";
                break;
            case 2:
                if (TwoXPrfoitRun == false)
                {
                    BusinessBonusGameObject.GetComponent<TweenPosition>().PlayReverse();
                    BusinessBonusScreen.transform.Find("MainDialoge").Find("BonusLable").GetComponent<UILabel>().text = "2x Profit bar rewarderd";
                    TwoXProfitTime = 0;
                    TwoXPrfoitRun = true;
                }
                else
                {
                    chanceOfBonus = 0;
                    coinAddedForBonus = coinGeneratePerSec * 150 * buisnessbonusmultiplier * buisnessbonusmultiplier3x;
                    BusinessBonusScreen.transform.Find("MainDialoge").Find("BonusLable").GetComponent<UILabel>().text = "Bonus :" + MyString(coinAddedForBonus);
                    BusinessBonusGameObject.GetComponent<TweenPosition>().PlayReverse();
                }
                break;
            default:
                BusinessBonusGameObject.GetComponent<TweenPosition>().PlayReverse();
                coinAddedForBonus = coinGeneratePerSec * 150 * buisnessbonusmultiplier * buisnessbonusmultiplier3x;
                break;

        }


    }
    public void PresstwoXBar()
    {
        if (TwoXPrfoitRun == true && TwoXProfitTime == 0)
        {
            MainMenuScreen.SetActive(true);
            MenuScreen.SetActive(false);

            GetNewAdvisorScreen.SetActive(false);
            ActivateBizBotsScreen.SetActive(false);
            TwoXProfit = 2;
            DailyQuestScreen.SetActive(false);

            AdvisorScreen.SetActive(false);
            BusinessBonusScreen.SetActive(false);

            ProfileBoostScreen.SetActive(false);
            SettingScreen.SetActive(false);
            ShoppingScreen.SetActive(false);
           
            CollectBonusScreen.SetActive(false);
            CollectionScreen.SetActive(false);
            AdvisorSpinScreen.SetActive(false);
            TwoXProfitTime = 3600 * 4;
            for (int jd = 0; jd < ShopSelectorList.Length; jd++)
            {
                ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
            }
        }
        else
        {
            if (totalgem >= 30)
            {
                totalgem = totalgem - 30;
                MainMenuScreen.SetActive(true);
                MenuScreen.SetActive(false);
                TwoXProfit = 2;
                GetNewAdvisorScreen.SetActive(false);
                ActivateBizBotsScreen.SetActive(false);

                DailyQuestScreen.SetActive(false);

                AdvisorScreen.SetActive(false);
                BusinessBonusScreen.SetActive(false);

                ProfileBoostScreen.SetActive(false);
                SettingScreen.SetActive(false);
                ShoppingScreen.SetActive(false);
                
                CollectBonusScreen.SetActive(false);
                CollectionScreen.SetActive(false);
                AdvisorSpinScreen.SetActive(false);
                TwoXProfitTime = 3600 * 4;
                TwoXPrfoitRun = true;
                for (int jd = 0; jd < ShopSelectorList.Length; jd++)
                {
                    ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
                }
            }
        }
    }
    public void CloseWithChance()
    {
        MainMenuScreen.SetActive(true);
        MenuScreen.SetActive(false);

        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);

        DailyQuestScreen.SetActive(false);

        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);

        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
       
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
        switch (chanceOfBonus)
        {
            case 0:
                CoinAdded(coinAddedForBonus);
                break;
            case 1:
                totalgem = totalgem + GemAddedForBonus;
                break;
            case 2:
                TwoXPofitGameObject.GetComponent<TweenPosition>().PlayForward();
                ProfileBoostScreen.SetActive(true);
                ProfileBoostScreen.transform.Find("MainDialoge").Find("Button").GetChild(0).GetComponent<UILabel>().text = "Activate Now";
                ProfileBoostScreen.transform.Find("MainDialoge").Find("TimeLeftCount").GetComponent<UILabel>().text = string.Format("{0:0}:{1:00}:{2:00}", hour, minute % 60, second) + " Time \n Left";
                ProfileBoostScreen.transform.Find("ProgressBar").GetComponent<UISlider>().value = TwoXProfitTime / (3600 * 4);
                TwoXPrfoitRun = true;
                break;
            default:
                CoinAdded(coinAddedForBonus);
                break;


        }
    }
    public void GiveUsFeedback()
    {
        // Replace your feedback page link below
        Application.OpenURL("https://play.google.com/store/apps");
    }

    public void FacebookPage()
    {
        // Replace your facebook page link below
        Application.OpenURL("https://facebook.com");
    }
    public void MoreGamesPage()
    {
        // Replace your more games page link below
        Application.OpenURL("https://play.google.com/store/apps");
    }

    public void TermsOfServicePage()
    {
        // Replace your term of service page link below
        Application.OpenURL("https://google.com/");
    }
    public void PrivacyUrl()
    {
        // Replace your privacy policy page link below
        Application.OpenURL("https://google.com/");
    }
    public UILabel SoundVolumeLable;
    public void SoundOnOff()
    {
        if (SoundVolumeLable.text.Equals("On"))
        {
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = false;
            SoundVolumeLable.text = "Off";
        }
        else
        {
            Camera.main.gameObject.GetComponent<AudioListener>().enabled = true;
            SoundVolumeLable.text = "On";
        }
    }
   

    public void DoubleReward()
    {
        MainMenuScreen.SetActive(true);
        MenuScreen.SetActive(false);

        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);

        DailyQuestScreen.SetActive(false);

        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);

        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
     
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
        switch (chanceOfBonus)
        {
            case 0:
                CoinAdded(coinAddedForBonus*2);
                break;
            case 1:
                totalgem = totalgem + GemAddedForBonus*2;
                break;
            case 2:
                TwoXPofitGameObject.GetComponent<TweenPosition>().PlayForward();
                ProfileBoostScreen.SetActive(true);
                ProfileBoostScreen.transform.Find("MainDialoge").Find("Button").GetChild(0).GetComponent<UILabel>().text = "Activate Now";
                ProfileBoostScreen.transform.Find("MainDialoge").Find("TimeLeftCount").GetComponent<UILabel>().text = string.Format("{0:0}:{1:00}:{2:00}", hour, minute % 60, second) + " Time \n Left";
                ProfileBoostScreen.transform.Find("ProgressBar").GetComponent<UISlider>().value = TwoXProfitTime / (3600 * 4);
                TwoXPrfoitRun = true;
                break;
            default:
                CoinAdded(coinAddedForBonus*2);
                break;


        }
    }
    public void Showing2XBoost()
    {
        MainMenuScreen.SetActive(true);
        MenuScreen.SetActive(false);

        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);

        DailyQuestScreen.SetActive(false);

        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);

        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
      
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
        TwoXPofitGameObject.GetComponent<TweenPosition>().PlayForward();
         ProfileBoostScreen.SetActive(true);
        if (TwoXPrfoitRun == true && TwoXProfitTime == 0)
        {
            ProfileBoostScreen.transform.Find("MainDialoge").Find("Button").GetChild(0).GetComponent<UILabel>().text = "Activate Now";
        }
        else
        {
            ProfileBoostScreen.transform.Find("MainDialoge").Find("Button").GetChild(0).GetComponent<UILabel>().text = "30 Gems";
        }

        ProfileBoostScreen.transform.Find("MainDialoge").Find("TimeLeftCount").GetComponent<UILabel>().text = string.Format("{0:0}:{1:00}:{2:00}", hour, minute % 60, second) + " Time \n Left";

        ProfileBoostScreen.transform.Find("ProgressBar").GetComponent<UISlider>().value = TwoXProfitTime / (3600 * 4);
               


    }

    void Update () {
        timeleft -= Time.deltaTime;
        minutes = Mathf.Floor(timeleft / 60);
        seconds = timeleft % 60;
        if (seconds > 59) seconds = 59;
        if (minutes < 0)
        {

            minutes = 0;
            seconds = 0;

            timeleft = 150;
            BusinessBonusGameObject.GetComponent<TweenPosition>().PlayForward();
            BusinessBonusGameObject.GetComponent<TweenScale>().PlayForward();

        }
        if(TwoXPrfoitRun == true)
        {
            if (TwoXProfitTime > 0)
            {
                TwoXProfitTime -= Time.deltaTime;
               
                hour = Mathf.Floor(TwoXProfitTime / 3600);
                minute = Mathf.Floor(TwoXProfitTime / 60) ;
                second = TwoXProfitTime % 60;
                TwoXPofitGameObject.transform.Find("TimeLable").GetComponent<UILabel>().text = string.Format("{0:0}:{1:00}:{2:00}",hour, minute%60, second);
                if (second > 59) second = 59;
                if (minute < 0)
                {
                    minute = 0;
                    second = 0;
                    TwoXProfitTime = 0;
                    TwoXPrfoitRun = false;
                    TwoXProfit = 1;
                    for (int jd = 0; jd < ShopSelectorList.Length; jd++)
                    {
                        ShopSelectorList[jd].BuildingValueDecider(ShopSelectorList[jd].buildingIndex);
                    }
                }
            }
        }



        mytotalcoin.text = MyString(totalcoin);
        mytotalgem.text = totalgem.ToString();
        mytotalgenerationcoin.text = MyString(coinGeneratePerSec) + "/sec";
    }


    // UI Section Code......

    public void MainMenuButtonPressed()
    {
        MainMenuScreen.SetActive(false);
        MenuScreen.SetActive(true);
       
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
      
        DailyQuestScreen.SetActive(false);
        
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
 
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
     
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }

    public void HomeButtonPressed()
    {
        MainMenuScreen.SetActive(true);
        MenuScreen.SetActive(false);

        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
       
        DailyQuestScreen.SetActive(false);
      
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
        
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
   
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }

    public void FreeGemsButtonPressed()
    {
        
    }
    public void ShowingRandomAdvisor()
    {
        if (showingAdvisorOrNot == false) {
            showingAdvisorOrNot = true;
            int hd = UnityEngine.Random.Range(0, MyAdvisorTable.transform.childCount);
           // Transform tempVal = AdvisorList[Random.Range(0,AdvisorList.Count)];
            if(AdvisorListScale.activeSelf == true)
            {
                showingAdvisorOrNot = false;
            }
            AdvisorListScale.transform.Find("AdvisorBack").GetComponent<UISprite>().spriteName = "Star" + MyAdvisorTable.transform.GetChild(hd).GetComponent<AdvisorScript>().RatedStar.ToString();
            AdvisorListScale.transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName =  MyAdvisorTable.transform.GetChild(hd).GetComponent<AdvisorScript>().AdvisorName;
            int tempStar = MyAdvisorTable.transform.GetChild(hd).GetComponent<AdvisorScript>().RatedStar;
            AdvisorListScale.transform.Find("Star1").gameObject.SetActive(false);
            AdvisorListScale.transform.Find("Star2").gameObject.SetActive(false);
            AdvisorListScale.transform.Find("Star3").gameObject.SetActive(false);
            AdvisorListScale.transform.Find("Star4").gameObject.SetActive(false);
            AdvisorListScale.transform.Find("Star5").gameObject.SetActive(false);
            for (int i = 1; i <= tempStar; i++)
            {
                AdvisorListScale.transform.Find("Star"+ i.ToString()).gameObject.SetActive(true);
            }
            AdvisorListScale.transform.Find("AdvisorName").GetComponent<UILabel>().text = MyAdvisorTable.transform.GetChild(hd).GetComponent<AdvisorScript>().AdvisorName;
            AdvisorListScale.transform.Find("AdvisorWork").GetComponent<UILabel>().text = MyAdvisorTable.transform.GetChild(hd).GetComponent<AdvisorScript>().GetInfo(MyAdvisorTable.transform.GetChild(hd).GetComponent<AdvisorScript>().AdvisorName);
            AdvisorListScale.GetComponent<TweenScale>().PlayReverse();
            StartCoroutine("WaitForCompleteTransition");
        }
        
    }

    IEnumerator WaitForCompleteTransition()
    {
        yield return new WaitForSeconds(3f);
        AdvisorListScale.GetComponent<TweenScale>().PlayForward();
        yield return new WaitForSeconds(.5f);
        ShowingRandomAdvisor();
    }
    public void GetNewAdvisorButtonPressed()
    {
        MainMenuScreen.SetActive(true);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(true);
        ActivateBizBotsScreen.SetActive(false);
        if (FreeSpin > 0)
        {
            
            freespinbutton.isEnabled = true;
        }
        else
        {
            freespinbutton.isEnabled = false;
        }

        if (totalgem >= 20)
        {
            spinwith20gembutton.isEnabled = true;
        }
        else
        {
            spinwith20gembutton.isEnabled = false;
        }

        if (totalgem >= 50)
        {
            spinwith50gembutton.isEnabled = true;
        }
        else
        {
            spinwith50gembutton.isEnabled = false;
        }

        DailyQuestScreen.SetActive(false);
       
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
      
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
      
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
        showingAdvisorOrNot = false;
        ShowingRandomAdvisor();
    }

    public void ExitadvisorScreenButtonPressed()
    {
        showingAdvisorOrNot = true;
        MainMenuScreen.SetActive(true);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
      
        DailyQuestScreen.SetActive(false);
       
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
     
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
    
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }

    public void ViewAllAdvisorScreenButtonPressed()
    {
        MainMenuScreen.SetActive(false);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
        
        DailyQuestScreen.SetActive(false);
      
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
      
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
    
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(true);
        AdvisorSpinScreen.SetActive(false);
        showingAdvisorOrNot = true;
    }
    private int incrementValueOfRobot;
    public void AtivateBizbotWithTrade()
    {
        if(totalgem >= 30)
        {
            totalgem -= 30;
            TotalBizbot += incrementValueOfRobot;
            ConsiderAdvisorParticipation();

            MainMenuScreen.SetActive(true);
            MenuScreen.SetActive(false);
            GetNewAdvisorScreen.SetActive(false);
            ActivateBizBotsScreen.SetActive(false);

            DailyQuestScreen.SetActive(false);

            AdvisorScreen.SetActive(false);
            BusinessBonusScreen.SetActive(false);

            ProfileBoostScreen.SetActive(false);
            SettingScreen.SetActive(false);
            ShoppingScreen.SetActive(false);
         
            CollectBonusScreen.SetActive(false);
            CollectionScreen.SetActive(true);
            AdvisorSpinScreen.SetActive(false);
           
        }

    }
    public void ActivateBizbotWithoutTrade()
    {
        TotalBizbot += incrementValueOfRobot;
        for(int i = 0; i < ShopSelectorList.Length; i++)
        {
            ShopSelectorList[i].buildingIndex = 0;
            ShopSelectorList[i].BuildingValueDecider(0);
        }
        ConsiderAdvisorParticipation();
        MainMenuScreen.SetActive(true);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);

        DailyQuestScreen.SetActive(false);

        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);

        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
    
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(true);
        AdvisorSpinScreen.SetActive(false);

    }
    public void BizBotsButtonPressed()
    {
        MainMenuScreen.SetActive(false);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(true);
        ActivateBizBotsScreen.transform.Find("CurrentlyOwnBizbotLable").GetComponent<UILabel>().text = TotalBizbot.ToString();
        ActivateBizBotsScreen.transform.Find("CurrentlyProfitBizbotLable").GetComponent<UILabel>().text = "Profit bonus : "+ (TotalBizbot*2).ToString()+"%";
        if(TotalBizbot >= 1000)
        {
            ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "No More Bizbot Available";
            ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "";
            ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "";
            ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = false;
        }
        else if (TotalBizbot >= 500)
        {
            if (totalcoinTillStart >= 1000000000000000000) {
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Activate Bizbots to get";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+500";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 1000%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = true;
                incrementValueOfRobot = 500;
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("TotalBizbotActionValue").GetComponent<UILabel>().text = "+500";
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("EarningAmountValue").GetComponent<UILabel>().text = "Profit bonus : 1000%";
            }
            else
            {
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Available When you earn 1 QUINTILLION";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+500";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 1000%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = false;
            }
        }
        else if (TotalBizbot >= 250)
        {
            if (totalcoinTillStart >= 100000000000000000)
            {
                incrementValueOfRobot = 250;
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Activate Bizbots to get";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+250";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 500%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = true;
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("TotalBizbotActionValue").GetComponent<UILabel>().text = "+250";
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("EarningAmountValue").GetComponent<UILabel>().text = "Profit bonus : 500%";
            }
            else
            {
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Available When you earn 100 QUADRILLION";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+250";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 500%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = false;
                
            }
        }
        else if (TotalBizbot == 100)
        {
            if (totalcoinTillStart >= 10000000000000000)
            {
                incrementValueOfRobot = 150;
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Activate Bizbots to get";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+150";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 300%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = true;
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("TotalBizbotActionValue").GetComponent<UILabel>().text = "+150";
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("EarningAmountValue").GetComponent<UILabel>().text = "Profit bonus : 300%";
            }
            else
            {
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Available When you earn  10 QUADRILLION";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+150";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 300%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = false;
                
            }
        }
        else if (TotalBizbot == 50)
        {
            if (totalcoinTillStart >= 100000000000000)
            {
                incrementValueOfRobot = 50;
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Activate Bizbots to get";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+50";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 100%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = true;
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("TotalBizbotActionValue").GetComponent<UILabel>().text = "+50";
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("EarningAmountValue").GetComponent<UILabel>().text = "Profit bonus : 100%";
            }
            else
            {
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Available When you earn 100 TRILLION";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+50";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 100%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = false;
             
            }
        }
        else if (TotalBizbot == 25)
        {
            if (totalcoinTillStart >= 10000000000000)
            {
                incrementValueOfRobot = 25;
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Activate Bizbots to get";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+25";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 50%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = true;
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("TotalBizbotActionValue").GetComponent<UILabel>().text = "+25";
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("EarningAmountValue").GetComponent<UILabel>().text = "Profit bonus : 50%";
            }
            else
            {
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Available When you earn 10 TRILLION";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+25";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 50%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = false;
                
            }
        }
        else if (TotalBizbot == 0)
        {
            if (totalcoinTillStart >= 100000000000)
            {
                incrementValueOfRobot = 25;
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Activate Bizbots to get";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+25";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 50%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = true;
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("TotalBizbotActionValue").GetComponent<UILabel>().text = "+25";
                ActivateBizbotActionScreen.transform.Find("EarnAmount").Find("EarningAmountValue").GetComponent<UILabel>().text = "Profit bonus : 50%";
            }
            else
            {
                ActivateBizBotsScreen.transform.Find("BizbotIndicatorLable").GetComponent<UILabel>().text = "Available When you earn 100 BILLION";
                ActivateBizBotsScreen.transform.Find("GetBizbotlable").GetComponent<UILabel>().text = "+25";
                ActivateBizBotsScreen.transform.Find("GetBizbotProfitLable").GetComponent<UILabel>().text = "Profit bonus : 50%";
                ActivateBizBotsScreen.transform.Find("ActivateBizbotButton").transform.GetComponent<UIButton>().isEnabled = false;
            }
        }

       
        DailyQuestScreen.SetActive(false);
      
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
       
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
      
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }
    public void ActivateBizbot()
    {
        MainMenuScreen.SetActive(false);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
        ActivateBizbotActionScreen.SetActive(true);
        DailyQuestScreen.SetActive(false);

        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);

        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
  
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }
    public void AchivementsButtonPressed()
    {
        MainMenuScreen.SetActive(false);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
   
        DailyQuestScreen.SetActive(false);
    
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
    
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
      
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }
    public void DailyQuestOneCompleteButtonPressed()
    {
        if(totalgem >= 1)
        {
            totalgem = totalgem - 1;
            shopupgradeindex = 150;
            progressbar1.value = 1;
            progressbarlable1.text = shopupgradeindex.ToString() + "/150";
            progressbutton1.enabled = false;
            progressbutton1.transform.gameObject.SetActive(false);
            if (progressbutton1.isEnabled == false && progressbutton2.isEnabled == false && progressbutton3.isEnabled == false)
            {
                progress1.spriteName = "green circle";
                progress2.spriteName = "green circle";
                progress3.spriteName = "green circle";
                boxholderbutton.enabled = true;
            }
            else if ((progressbutton1.isEnabled == false && progressbutton2.isEnabled == false) || (progressbutton1.isEnabled == false && progressbutton3.isEnabled == false) || (progressbutton3.isEnabled == false && progressbutton2.isEnabled == false))
            {
                progress1.spriteName = "green circle";
                progress2.spriteName = "green circle";
                progress3.spriteName = "blue circle";
                boxholderbutton.enabled = false;
            }
            else if (progressbutton1.isEnabled == false || progressbutton2.isEnabled == false || progressbutton3.isEnabled == false)
            {
                progress1.spriteName = "green circle";
                progress2.spriteName = "blue circle";
                progress3.spriteName = "blue circle";
                boxholderbutton.enabled = false;
            }
            else
            {
                progress1.spriteName = "blue circle";
                progress2.spriteName = "blue circle";
                progress3.spriteName = "blue circle";
                boxholderbutton.enabled = false;
            }
        }
    }
    private bool fisttimeboxopen = false;
    public void PressOnBox()
    {
        if (fisttimeboxopen == false)
        {
            fisttimeboxopen = true;
            AnimatedBox.GetComponent<tk2dSpriteAnimator>().Play();
            StartCoroutine("waitForBoxOpen");
        }
    }
    IEnumerator waitForBoxOpen()
    {
        yield return new WaitForSeconds(1f);
        BuisnessBonusPressed();

    }
    public void DailyQuestTwoCompleteButtonPressed()
    {
        if (totalgem >= 1)
        {
            totalgem = totalgem - 1;
            taphouseindex = 2000;
            progressbar2.value = 1;
            progressbarlable2.text = taphouseindex.ToString() + "/2000";
            progressbutton2.enabled = false;
            progressbutton2.transform.gameObject.SetActive(false);
            if (progressbutton1.isEnabled == false && progressbutton2.isEnabled == false && progressbutton3.isEnabled == false)
            {
                progress1.spriteName = "green circle";
                progress2.spriteName = "green circle";
                progress3.spriteName = "green circle";
                boxholderbutton.enabled = true;
            }
            else if ((progressbutton1.isEnabled == false && progressbutton2.isEnabled == false) || (progressbutton1.isEnabled == false && progressbutton3.isEnabled == false) || (progressbutton3.isEnabled == false && progressbutton2.isEnabled == false))
            {
                progress1.spriteName = "green circle";
                progress2.spriteName = "green circle";
                progress3.spriteName = "blue circle";
                boxholderbutton.enabled = false;
            }
            else if (progressbutton1.isEnabled == false || progressbutton2.isEnabled == false || progressbutton3.isEnabled == false)
            {
                progress1.spriteName = "green circle";
                progress2.spriteName = "blue circle";
                progress3.spriteName = "blue circle";
                boxholderbutton.enabled = false;
            }
            else
            {
                progress1.spriteName = "blue circle";
                progress2.spriteName = "blue circle";
                progress3.spriteName = "blue circle";
                boxholderbutton.enabled = false;
            }
        }
    }
    public void DailyQuestThreeCompleteButtonPressed()
    {
        if (totalgem >= 1)
        {
            totalgem = totalgem - 1;
            criticaltapindex = 10;
            progressbar3.value = 1;
            progressbarlable3.text = criticaltapindex.ToString() + "/10";
            progressbutton3.enabled = false;
            progressbutton3.transform.gameObject.SetActive(false);
            if (progressbutton1.isEnabled == false && progressbutton2.isEnabled == false && progressbutton3.isEnabled == false)
            {
                progress1.spriteName = "green circle";
                progress2.spriteName = "green circle";
                progress3.spriteName = "green circle";
                boxholderbutton.enabled = true;
            }
            else if ((progressbutton1.isEnabled == false && progressbutton2.isEnabled == false) || (progressbutton1.isEnabled == false && progressbutton3.isEnabled == false) || (progressbutton3.isEnabled == false && progressbutton2.isEnabled == false))
            {
                progress1.spriteName = "green circle";
                progress2.spriteName = "green circle";
                progress3.spriteName = "blue circle";
                boxholderbutton.enabled = false;
            }
            else if (progressbutton1.isEnabled == false || progressbutton2.isEnabled == false || progressbutton3.isEnabled == false)
            {
                progress1.spriteName = "green circle";
                progress2.spriteName = "blue circle";
                progress3.spriteName = "blue circle";
                boxholderbutton.enabled = false;
            }
            else
            {
                progress1.spriteName = "blue circle";
                progress2.spriteName = "blue circle";
                progress3.spriteName = "blue circle";
                boxholderbutton.enabled = false;
            }
        }
    }

    public void DailyQuestButtonPressed()
    {
        MainMenuScreen.SetActive(false);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
        progressbar1.value = shopupgradeindex / 150f;
        progressbarlable1.text = shopupgradeindex.ToString() + "/150";
        progressbar2.value = taphouseindex / 2000f;
        progressbarlable2.text = taphouseindex.ToString() + "/2000";
        progressbar3.value = criticaltapindex / 10f;
        progressbarlable3.text = criticaltapindex.ToString() + "/10";


        if(progressbar1.value >= 1)
        {
            progressbutton1.enabled = false;
            progressbutton1.transform.gameObject.SetActive(false);
        }
        if (progressbar2.value >= 1)
        {
            progressbutton2.enabled = false;
            progressbutton2.transform.gameObject.SetActive(false);
        }
        if (progressbar3.value >= 1)
        {
            progressbutton3.enabled = false;
            progressbutton3.transform.gameObject.SetActive(false);
        }

        if (progressbutton1.isEnabled == false && progressbutton2.isEnabled == false && progressbutton3.isEnabled == false)
        {
            progress1.spriteName = "green circle";
            progress2.spriteName = "green circle";
            progress3.spriteName = "green circle";
            boxholderbutton.enabled = true;
        }
        else if ((progressbutton1.isEnabled == false && progressbutton2.isEnabled == false) || (progressbutton1.isEnabled == false && progressbutton3.isEnabled == false) || (progressbutton3.isEnabled == false && progressbutton2.isEnabled == false))
        {
            progress1.spriteName = "green circle";
            progress2.spriteName = "green circle";
            progress3.spriteName = "blue circle";
            boxholderbutton.enabled = false;
        }
        else if (progressbutton1.isEnabled == false || progressbutton2.isEnabled == false || progressbutton3.isEnabled == false)
        {
            progress1.spriteName = "green circle";
            progress2.spriteName = "blue circle";
            progress3.spriteName = "blue circle";
            boxholderbutton.enabled = false;
        }
        else
        {
            progress1.spriteName = "blue circle";
            progress2.spriteName = "blue circle";
            progress3.spriteName = "blue circle";
            boxholderbutton.enabled = false;
        }

        DailyQuestScreen.SetActive(true);
    
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
     
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
      
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }

    public void OpenCratesButtonPressed()
    {
        MainMenuScreen.SetActive(false);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
   
        DailyQuestScreen.SetActive(false);
    
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
   
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
     
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }
    public void AdvisorOwnButtonPressed()
    {
        MainMenuScreen.SetActive(false);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
  
        DailyQuestScreen.SetActive(false);
  
        AdvisorScreen.SetActive(true);
        BusinessBonusScreen.SetActive(false);
    
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
    
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
        if (Myadvisors.Count > 0)
        {
            AdvisorScreen.transform.Find("CurrentAdvisorInfo").gameObject.SetActive(true);
            AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.DestroyChildren();
            for (int i = 0; i < Myadvisors.Count; i++)
            {



                if (Myadvisors[i].InCurrentUse == true)
                {
                  
                }
                else
                {
                    GameObject tempprefab = Instantiate(AdviosrTablePrefab, AdviosrTablePrefab.transform.position, AdviosrTablePrefab.transform.rotation);
                    tempprefab.transform.Find("AdvisorBack").GetComponent<UISprite>().spriteName = "Star"+Myadvisors[i].RatedStar.ToString();
                    tempprefab.transform.name = Myadvisors[i].AdvisorName;
                    tempprefab.transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = Myadvisors[i].AdvisorName;
                    int tempStar = Myadvisors[i].RatedStar;
                    tempprefab.transform.Find("Star1").gameObject.SetActive(false);
                    tempprefab.transform.Find("Star2").gameObject.SetActive(false);
                    tempprefab.transform.Find("Star3").gameObject.SetActive(false);
                    tempprefab.transform.Find("Star4").gameObject.SetActive(false);
                    tempprefab.transform.Find("Star5").gameObject.SetActive(false);
                    for (int ids = 1; ids <= tempStar; ids++)
                    {
                        tempprefab.transform.Find("Star" + ids.ToString()).gameObject.SetActive(true);
                    }
                    tempprefab.transform.Find("AdvisorName").GetComponent<UILabel>().text = Myadvisors[i].AdvisorName;
                    tempprefab.transform.Find("AdvisorWork").GetComponent<UILabel>().text = Myadvisors[i].GetInfo(Myadvisors[i].AdvisorName);

                    tempprefab.transform.parent = AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform;
                    tempprefab.transform.localScale = new Vector3(1f, 1f, 1f);
                    tempprefab.transform.localPosition = new Vector3(0f, 0f, 0f);
                }
            }

            AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.GetComponent<UIGrid>().Reposition();
            AdvisorScreen.transform.Find("CurrentAdvisorInfo").gameObject.SetActive(true);
            if (CurrentInUseAdvisor.Count > 0)
            {
                switch (CurrentInUseAdvisor.Count)
                {
                    case 1:
                        AdvisorScreen.transform.Find("ActiveAdvisor1").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[0].RatedStar.ToString();
                        AdvisorScreen.transform.Find("ActiveAdvisor1").Find("Sprite").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[0].AdvisorName;
                        AdvisorScreen.transform.Find("ActiveAdvisor1").gameObject.SetActive(true);
                        AdvisorScreen.transform.Find("ActiveAdvisor2").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("ActiveAdvisor3").gameObject.SetActive(false);
                        ActiveAdvisor1Pressed();
                        break;
                    case 2:
                        AdvisorScreen.transform.Find("ActiveAdvisor1").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[0].RatedStar.ToString();
                        AdvisorScreen.transform.Find("ActiveAdvisor1").Find("Sprite").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[0].AdvisorName;
                        AdvisorScreen.transform.Find("ActiveAdvisor2").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[1].RatedStar.ToString();
                        AdvisorScreen.transform.Find("ActiveAdvisor2").Find("Sprite").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[1].AdvisorName;
                        AdvisorScreen.transform.Find("ActiveAdvisor1").gameObject.SetActive(true);
                        AdvisorScreen.transform.Find("ActiveAdvisor2").gameObject.SetActive(true);
                        AdvisorScreen.transform.Find("ActiveAdvisor3").gameObject.SetActive(false);
                        ActiveAdvisor1Pressed();
                        ActiveAdvisor2Pressed();
                        break;
                    case 3:
                        AdvisorScreen.transform.Find("ActiveAdvisor1").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[0].RatedStar.ToString();
                        AdvisorScreen.transform.Find("ActiveAdvisor1").Find("Sprite").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[0].AdvisorName;
                        AdvisorScreen.transform.Find("ActiveAdvisor2").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[1].RatedStar.ToString();
                        AdvisorScreen.transform.Find("ActiveAdvisor2").Find("Sprite").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[1].AdvisorName;
                        AdvisorScreen.transform.Find("ActiveAdvisor3").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[2].RatedStar.ToString();
                        AdvisorScreen.transform.Find("ActiveAdvisor3").Find("Sprite").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[2].AdvisorName;
                        AdvisorScreen.transform.Find("ActiveAdvisor1").gameObject.SetActive(true);
                        AdvisorScreen.transform.Find("ActiveAdvisor2").gameObject.SetActive(true);
                        AdvisorScreen.transform.Find("ActiveAdvisor3").gameObject.SetActive(true);
                        ActiveAdvisor1Pressed();
                        ActiveAdvisor2Pressed();
                        ActiveAdvisor3Pressed();
                        break;

                }
            }



        }
    }
    public void PressedOnRight(string name)
    {
        GameObject jj = AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.Find(name).gameObject;
        AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").GetComponent<UIScrollView>().enabled = false;

        AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.Find(name).GetComponent<TweenPosition>().from = jj.transform.localPosition;
        AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.Find(name).GetComponent<TweenPosition>().to = jj.transform.localPosition + new Vector3(1000,0,0);
        AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.Find(name).GetComponent<TweenPosition>().ResetToBeginning();
        AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.Find(name).GetComponent<TweenPosition>().PlayForward();
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").GetComponent<TweenPosition>().from = AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.localPosition;
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").GetComponent<TweenPosition>().to = AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.localPosition + new Vector3(-1000, 0, 0);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").GetComponent<TweenPosition>().ResetToBeginning();
       AdvisorScreen.transform.Find("CurrentAdvisorInfo").GetComponent<TweenPosition>().PlayForward();

        StartCoroutine(WaitToDoItAgain(name));
    }
    IEnumerator WaitToDoItAgain(string name)
    {
        yield return new WaitForSeconds(0.6f);
        GameObject jj = AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.Find(name).gameObject;
        AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.Find(name).GetComponent<TweenPosition>().from = jj.transform.localPosition + new Vector3(-2000, 0, 0);
        AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.Find(name).GetComponent<TweenPosition>().to = jj.transform.localPosition + new Vector3(-1000, 0, 0);
        AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.Find(name).GetComponent<TweenPosition>().ResetToBeginning();
        AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").transform.Find("Grid").transform.Find(name).GetComponent<TweenPosition>().PlayForward();
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").GetComponent<TweenPosition>().from = AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.localPosition + new Vector3(2000, 0, 0);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").GetComponent<TweenPosition>().to = AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.localPosition + new Vector3(1000, 0, 0);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").GetComponent<TweenPosition>().ResetToBeginning();
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").GetComponent<TweenPosition>().PlayForward();
        switch (currentSelectedAdvisor)
        {
            case 1:
                for (int i = 0; i < Myadvisors.Count; i++)
                {
                    if (Myadvisors[i].AdvisorName.Equals(CurrentInUseAdvisor[0].AdvisorName))
                    {
                        Myadvisors[i].InCurrentUse = false;
                        
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).Find("AdvisorBack").GetComponent<UISprite>().spriteName = "Star" + Myadvisors[i].RatedStar.ToString();
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = Myadvisors[i].AdvisorName;
                        int tempStar = Myadvisors[i].RatedStar;
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star1").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star2").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star3").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star4").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star5").gameObject.SetActive(false);
                        for (int ids = 1; ids <= tempStar; ids++)
                        {
                            AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star" + ids.ToString()).gameObject.SetActive(true);
                        }
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("AdvisorName").GetComponent<UILabel>().text = Myadvisors[i].AdvisorName;
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("AdvisorWork").GetComponent<UILabel>().text = Myadvisors[i].GetInfo(Myadvisors[i].AdvisorName);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.name = Myadvisors[i].AdvisorName;

                    }
                }
                for (int j = 0; j < Myadvisors.Count; j++)
                {
                    
                    if (Myadvisors[j].AdvisorName.Equals(name))
                    {
                        Myadvisors[j].InCurrentUse = true;
                        CurrentInUseAdvisor[0] = Myadvisors[j];
                       
                        AdvisorScreen.transform.Find("ActiveAdvisor1").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[0].RatedStar.ToString();
                        AdvisorScreen.transform.Find("ActiveAdvisor1").Find("Sprite").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[0].AdvisorName;
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[0].RatedStar.ToString();
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[0].AdvisorName;
                        int tempStar = CurrentInUseAdvisor[0].RatedStar;
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star1").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star2").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star3").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star4").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star5").gameObject.SetActive(false);
                        for (int i = 1; i <= tempStar; i++)
                        {
                            AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star" + i.ToString()).gameObject.SetActive(true);
                        }
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorName").GetComponent<UILabel>().text = CurrentInUseAdvisor[0].AdvisorName;
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorWork").GetComponent<UILabel>().text = CurrentInUseAdvisor[0].GetInfo(CurrentInUseAdvisor[0].AdvisorName);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < Myadvisors.Count; i++)
                {
                    if (Myadvisors[i].AdvisorName.Equals(CurrentInUseAdvisor[1].AdvisorName))
                    {
                        Myadvisors[i].InCurrentUse = false;
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).Find("AdvisorBack").GetComponent<UISprite>().spriteName = "Star" + Myadvisors[i].RatedStar.ToString();
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = Myadvisors[i].AdvisorName;
                        int tempStar = Myadvisors[i].RatedStar;
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star1").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star2").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star3").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star4").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star5").gameObject.SetActive(false);
                        for (int ids = 1; ids <= tempStar; ids++)
                        {
                            AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star" + ids.ToString()).gameObject.SetActive(true);
                        }
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("AdvisorName").GetComponent<UILabel>().text = Myadvisors[i].AdvisorName;
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("AdvisorWork").GetComponent<UILabel>().text = Myadvisors[i].GetInfo(Myadvisors[i].AdvisorName);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.name = Myadvisors[i].AdvisorName;

                    }
                }
                for (int j = 0; j < Myadvisors.Count; j++)
                {

                    if (Myadvisors[j].AdvisorName.Equals(name))
                    {
                        Myadvisors[j].InCurrentUse = true;
                        CurrentInUseAdvisor[1] = Myadvisors[j];
                        AdvisorScreen.transform.Find("ActiveAdvisor2").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[1].RatedStar.ToString();
                        AdvisorScreen.transform.Find("ActiveAdvisor2").Find("Sprite").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[1].AdvisorName;
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[1].RatedStar.ToString();
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[1].AdvisorName;
                        int tempStar = CurrentInUseAdvisor[1].RatedStar;
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star1").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star2").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star3").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star4").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star5").gameObject.SetActive(false);
                        for (int i = 1; i <= tempStar; i++)
                        {
                            AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star" + i.ToString()).gameObject.SetActive(true);
                        }
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorName").GetComponent<UILabel>().text = CurrentInUseAdvisor[1].AdvisorName;
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorWork").GetComponent<UILabel>().text = CurrentInUseAdvisor[1].GetInfo(CurrentInUseAdvisor[1].AdvisorName);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < Myadvisors.Count; i++)
                {
                    if (Myadvisors[i].AdvisorName.Equals(CurrentInUseAdvisor[2].AdvisorName))
                    {
                        Myadvisors[i].InCurrentUse = false;
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).Find("AdvisorBack").GetComponent<UISprite>().spriteName = "Star" + Myadvisors[i].RatedStar.ToString();
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = Myadvisors[i].AdvisorName;
                        int tempStar = Myadvisors[i].RatedStar;
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star1").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star2").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star3").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star4").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star5").gameObject.SetActive(false);
                        for (int ids = 1; ids <= tempStar; ids++)
                        {
                            AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("Star" + ids.ToString()).gameObject.SetActive(true);
                        }
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("AdvisorName").GetComponent<UILabel>().text = Myadvisors[i].AdvisorName;
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.Find("AdvisorWork").GetComponent<UILabel>().text = Myadvisors[i].GetInfo(Myadvisors[i].AdvisorName);
                        AdvisorScreen.transform.Find("PassiveAdvisorHolder").Find("Scroll View").Find("Grid").Find(name).transform.name = Myadvisors[i].AdvisorName;

                    }
                }
                for (int j = 0; j < Myadvisors.Count; j++)
                {

                    if (Myadvisors[j].AdvisorName.Equals(name))
                    {
                        Myadvisors[j].InCurrentUse = true;
                        CurrentInUseAdvisor[2] = Myadvisors[j];
                        AdvisorScreen.transform.Find("ActiveAdvisor3").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[2].RatedStar.ToString();
                        AdvisorScreen.transform.Find("ActiveAdvisor3").Find("Sprite").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[2].AdvisorName;
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").GetComponent<UISprite>().spriteName = "Star" + CurrentInUseAdvisor[2].RatedStar.ToString();
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[2].AdvisorName;
                        int tempStar = CurrentInUseAdvisor[2].RatedStar;
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star1").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star2").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star3").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star4").gameObject.SetActive(false);
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star5").gameObject.SetActive(false);
                        for (int i = 1; i <= tempStar; i++)
                        {
                            AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star" + i.ToString()).gameObject.SetActive(true);
                        }
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorName").GetComponent<UILabel>().text = CurrentInUseAdvisor[2].AdvisorName;
                        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorWork").GetComponent<UILabel>().text = CurrentInUseAdvisor[2].GetInfo(CurrentInUseAdvisor[2].AdvisorName);
                    }
                }
                break;

        }
        yield return new WaitForSeconds(0.6f);
        AdvisorScreen.transform.Find("PassiveAdvisorHolder").transform.Find("Scroll View").GetComponent<UIScrollView>().enabled = true;
        ConsiderAdvisorParticipation();
    }
    public bool Star3to5 = false;
    public void SpinWithFreeSpin()
    {
        if (FreeSpin > 0)
        {
            FreeSpin--;
            MainMenuScreen.SetActive(true);
            MenuScreen.SetActive(false);
            GetNewAdvisorScreen.SetActive(false);
            ActivateBizBotsScreen.SetActive(false);

            DailyQuestScreen.SetActive(false);

            AdvisorScreen.SetActive(false);
            BusinessBonusScreen.SetActive(false);

            ProfileBoostScreen.SetActive(false);
            SettingScreen.SetActive(false);
            ShoppingScreen.SetActive(false);

            CollectBonusScreen.SetActive(false);
            CollectionScreen.SetActive(false);
            AdvisorSpinScreen.SetActive(true);
            Star3to5 = false;
            if (scrollView == null)
            {
                scrollView = AdvisorSpinScreen.transform.Find("ScrollViewHolder").Find("Scroll View").gameObject.GetComponent<UIScrollView>();
                if (scrollView == null)
                {

                    enabled = false;
                    return;
                }

                grid = scrollView.transform.GetChild(0).GetComponent<UIGrid>();

                for (int dd = 0; dd < 3; dd++)
                {
                    for (int noOfAdvisor = 0; noOfAdvisor < MyAdvisorTable.transform.childCount; noOfAdvisor++)
                    {

                        GameObject jk = GameObject.Instantiate(MyAdvisorTable.transform.GetChild(noOfAdvisor).gameObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

                        jk.transform.parent = grid.transform;
                        Destroy(jk.transform.GetComponent<UIDragScrollView>());
                        Destroy(jk.transform.GetComponent<BoxCollider>());
                        Destroy(jk.transform.GetComponent<UIButton>());
                        jk.transform.localScale = new Vector3(1, 1, 1);
                        jk.transform.localPosition = new Vector3(0, 0, 0);
                    }
                }

                grid.Reposition();


                elementsPerPage = (int)(scrollView.panel.baseClipRegion.z / grid.cellHeight);
                currentScrolledElements = 0;
                startingScrollPosition = scrollView.panel.cachedTransform.localPosition;
                StartCoroutine("WaitToScroll");
            }
            else
            {
                AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").GetComponent<TweenPosition>().PlayReverse();
                AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").GetComponent<TweenScale>().PlayReverse();
                StartCoroutine("WaitForPressAgain");
            }
        }
    }

    public void SpinWith50Diamond()
    {
        if (totalgem >= 50)
        {
            totalgem = totalgem - 50;
            MainMenuScreen.SetActive(true);
            MenuScreen.SetActive(false);
            GetNewAdvisorScreen.SetActive(false);
            ActivateBizBotsScreen.SetActive(false);

            DailyQuestScreen.SetActive(false);
            Star3to5 = true;
            AdvisorScreen.SetActive(false);
            BusinessBonusScreen.SetActive(false);

            ProfileBoostScreen.SetActive(false);
            SettingScreen.SetActive(false);
            ShoppingScreen.SetActive(false);
        
            CollectBonusScreen.SetActive(false);
            CollectionScreen.SetActive(false);
            AdvisorSpinScreen.SetActive(true);
            if (scrollView == null)
            {
                scrollView = AdvisorSpinScreen.transform.Find("ScrollViewHolder").Find("Scroll View").gameObject.GetComponent<UIScrollView>();
                if (scrollView == null)
                {

                    enabled = false;
                    return;
                }

                grid = scrollView.transform.GetChild(0).GetComponent<UIGrid>();

                for (int dd = 0; dd < 3; dd++)
                {
                    for (int noOfAdvisor = 0; noOfAdvisor < MyAdvisorTable.transform.childCount; noOfAdvisor++)
                    {

                        GameObject jk = GameObject.Instantiate(MyAdvisorTable.transform.GetChild(noOfAdvisor).gameObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

                        jk.transform.parent = grid.transform;
                        Destroy(jk.transform.GetComponent<UIDragScrollView>());
                        Destroy(jk.transform.GetComponent<BoxCollider>());
                        Destroy(jk.transform.GetComponent<UIButton>());
                        jk.transform.localScale = new Vector3(1, 1, 1);
                        jk.transform.localPosition = new Vector3(0, 0, 0);
                    }
                }

                grid.Reposition();


                elementsPerPage = (int)(scrollView.panel.baseClipRegion.z / grid.cellHeight);
                currentScrolledElements = 0;
                startingScrollPosition = scrollView.panel.cachedTransform.localPosition;
                StartCoroutine("WaitToScroll");
            }
            else
            {
                AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").GetComponent<TweenPosition>().PlayReverse();
                AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").GetComponent<TweenScale>().PlayReverse();
                StartCoroutine("WaitForPressAgain");
            }
        }
    }
    public void SpinAgainWith50Diamond()
    {
        if (totalgem >= 50)
        {
            totalgem = totalgem - 50;
            MainMenuScreen.SetActive(true);
            MenuScreen.SetActive(false);
            GetNewAdvisorScreen.SetActive(false);
            ActivateBizBotsScreen.SetActive(false);

            DailyQuestScreen.SetActive(false);
            Star3to5 = false;
            AdvisorScreen.SetActive(false);
            BusinessBonusScreen.SetActive(false);

            ProfileBoostScreen.SetActive(false);
            SettingScreen.SetActive(false);
            ShoppingScreen.SetActive(false);
         
            CollectBonusScreen.SetActive(false);
            CollectionScreen.SetActive(false);
            AdvisorSpinScreen.SetActive(true);
            if (scrollView == null)
            {
                scrollView = AdvisorSpinScreen.transform.Find("ScrollViewHolder").Find("Scroll View").gameObject.GetComponent<UIScrollView>();
                if (scrollView == null)
                {

                    enabled = false;
                    return;
                }

                grid = scrollView.transform.GetChild(0).GetComponent<UIGrid>();

                for (int dd = 0; dd < 3; dd++)
                {
                    for (int noOfAdvisor = 0; noOfAdvisor < MyAdvisorTable.transform.childCount; noOfAdvisor++)
                    {

                        GameObject jk = GameObject.Instantiate(MyAdvisorTable.transform.GetChild(noOfAdvisor).gameObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

                        jk.transform.parent = grid.transform;
                        Destroy(jk.transform.GetComponent<UIDragScrollView>());
                        Destroy(jk.transform.GetComponent<BoxCollider>());
                        Destroy(jk.transform.GetComponent<UIButton>());
                        jk.transform.localScale = new Vector3(1, 1, 1);
                        jk.transform.localPosition = new Vector3(0, 0, 0);
                    }
                }

                grid.Reposition();


                elementsPerPage = (int)(scrollView.panel.baseClipRegion.z / grid.cellHeight);
                currentScrolledElements = 0;
                startingScrollPosition = scrollView.panel.cachedTransform.localPosition;
                StartCoroutine("WaitToScroll");
            }
            else
            {
                AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").GetComponent<TweenPosition>().PlayReverse();
                AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").GetComponent<TweenScale>().PlayReverse();
                StartCoroutine("WaitForPressAgain");
            }
        }
    }
    public void SpinWith20Diamond()
    {
        if (totalgem >= 20)
        {
            MainMenuScreen.SetActive(true);
            MenuScreen.SetActive(false);
            GetNewAdvisorScreen.SetActive(false);
            ActivateBizBotsScreen.SetActive(false);
            Star3to5 = false;
            DailyQuestScreen.SetActive(false);
            totalgem = totalgem - 20;
            AdvisorScreen.SetActive(false);
            BusinessBonusScreen.SetActive(false);

            ProfileBoostScreen.SetActive(false);
            SettingScreen.SetActive(false);
            ShoppingScreen.SetActive(false);
      
            CollectBonusScreen.SetActive(false);
            CollectionScreen.SetActive(false);
            AdvisorSpinScreen.SetActive(true);
            if (scrollView == null)
            {
                scrollView = AdvisorSpinScreen.transform.Find("ScrollViewHolder").Find("Scroll View").gameObject.GetComponent<UIScrollView>();
                if (scrollView == null)
                {

                    enabled = false;
                    return;
                }

                grid = scrollView.transform.GetChild(0).GetComponent<UIGrid>();

                for (int dd = 0; dd < 3; dd++)
                {
                    for (int noOfAdvisor = 0; noOfAdvisor < MyAdvisorTable.transform.childCount; noOfAdvisor++)
                    {

                        GameObject jk = GameObject.Instantiate(MyAdvisorTable.transform.GetChild(noOfAdvisor).gameObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

                        jk.transform.parent = grid.transform;
                        Destroy(jk.transform.GetComponent<UIDragScrollView>());
                        Destroy(jk.transform.GetComponent<BoxCollider>());
                        Destroy(jk.transform.GetComponent<UIButton>());
                        jk.transform.localScale = new Vector3(1, 1, 1);
                        jk.transform.localPosition = new Vector3(0, 0, 0);
                    }
                }

                grid.Reposition();


                elementsPerPage = (int)(scrollView.panel.baseClipRegion.z / grid.cellHeight);
                currentScrolledElements = 0;
                startingScrollPosition = scrollView.panel.cachedTransform.localPosition;
                StartCoroutine("WaitToScroll");
            }
            else
            {
                AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").GetComponent<TweenPosition>().PlayReverse();
                AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").GetComponent<TweenScale>().PlayReverse();
                StartCoroutine("WaitForPressAgain");
            }
        }
    }
    public void NotNowPressed()
    {
        AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").GetComponent<TweenPosition>().PlayReverse();
        AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").GetComponent<TweenScale>().PlayReverse();
        StartCoroutine("WaitForPressForNotNow");
    }
    IEnumerator WaitForPressForNotNow()
    {

        yield return new WaitForSeconds(0.5f);
        AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").gameObject.SetActive(false);
        grid.transform.GetChild(indexforthattravel + 2).gameObject.SetActive(true);
        SpringPanel.Begin(scrollView.panel.cachedGameObject, startingScrollPosition, 16);
        MainMenuScreen.SetActive(true);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
     
        DailyQuestScreen.SetActive(false);
       
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
     
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(false);
    
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }
    IEnumerator WaitForPressAgain()
    {

        yield return new WaitForSeconds(0.5f);
        AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").gameObject.SetActive(false);
        grid.transform.GetChild(indexforthattravel + 2).gameObject.SetActive(true);
        SpringPanel.Begin(scrollView.panel.cachedGameObject, startingScrollPosition, 16).onFinished += OnFinishedAgain;
    }
    private void OnFinishedAgain()
    {
        StartCoroutine("WaitToScroll");
    }
    IEnumerator WaitToScroll()
    {
        yield return new WaitForSeconds(1f);
        indexforthattravel =UnityEngine.Random.Range(grid.transform.childCount/3, grid.transform.childCount-4);
        if (Star3to5 == true)
        {
            while (grid.transform.GetChild(indexforthattravel + 2).GetComponent<AdvisorScript>().RatedStar < 3)
            {
                indexforthattravel = UnityEngine.Random.Range(grid.transform.childCount / 3, grid.transform.childCount - 4);
            }
        }
        float nextScroll = grid.cellHeight * indexforthattravel;
        Vector3 target = new Vector3(0.0f, -nextScroll, 0.0f);
        MoveBy(target);
    }
    void MoveBy(Vector3 target)
    {
        if (scrollView != null && scrollView.panel != null)
        {
            // Spring the panel to this calculated position
            SpringPanel.Begin(scrollView.panel.cachedGameObject, startingScrollPosition - target, springStrength).onFinished += OnFinished;

        }
    }
    public void ActiveAdvisor1Pressed()
    {
        currentSelectedAdvisor = 1;
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").GetComponent<UISprite>().spriteName = "Star"+CurrentInUseAdvisor[0].RatedStar.ToString();
        AdvisorScreen.transform.Find("ActiveAdvisor1").transform.Find("Glow").gameObject.SetActive(true);
        AdvisorScreen.transform.Find("ActiveAdvisor2").transform.Find("Glow").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("ActiveAdvisor3").transform.Find("Glow").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[0].AdvisorName;
        int tempStar = CurrentInUseAdvisor[0].RatedStar;
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star1").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star2").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star3").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star4").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star5").gameObject.SetActive(false);
        for (int i = 1; i <= tempStar; i++)
        {
            AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star" + i.ToString()).gameObject.SetActive(true);
        }
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorName").GetComponent<UILabel>().text = CurrentInUseAdvisor[0].AdvisorName;
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorWork").GetComponent<UILabel>().text = CurrentInUseAdvisor[0].GetInfo(CurrentInUseAdvisor[0].AdvisorName);
        
    }
    public void ActiveAdvisor2Pressed()
    {
        currentSelectedAdvisor = 2;
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").GetComponent<UISprite>().spriteName = "Star"+CurrentInUseAdvisor[1].RatedStar.ToString();
        AdvisorScreen.transform.Find("ActiveAdvisor1").transform.Find("Glow").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("ActiveAdvisor2").transform.Find("Glow").gameObject.SetActive(true);
        AdvisorScreen.transform.Find("ActiveAdvisor3").transform.Find("Glow").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[1].AdvisorName;
        int tempStar = CurrentInUseAdvisor[1].RatedStar;
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star1").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star2").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star3").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star4").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star5").gameObject.SetActive(false);
        for (int i = 1; i <= tempStar; i++)
        {
            AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star" + i.ToString()).gameObject.SetActive(true);
        }
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorName").GetComponent<UILabel>().text = CurrentInUseAdvisor[1].AdvisorName;
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorWork").GetComponent<UILabel>().text = CurrentInUseAdvisor[1].GetInfo(CurrentInUseAdvisor[1].AdvisorName);

    }
    public void ActiveAdvisor3Pressed()
    {
        currentSelectedAdvisor = 3;
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").GetComponent<UISprite>().spriteName ="Star"+ CurrentInUseAdvisor[2].RatedStar.ToString();
        AdvisorScreen.transform.Find("ActiveAdvisor1").transform.Find("Glow").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("ActiveAdvisor2").transform.Find("Glow").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("ActiveAdvisor3").transform.Find("Glow").gameObject.SetActive(true);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorBack").Find("AdvisorPhoto").GetComponent<UISprite>().spriteName = CurrentInUseAdvisor[2].AdvisorName;
        int tempStar = CurrentInUseAdvisor[2].RatedStar;
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star1").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star2").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star3").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star4").gameObject.SetActive(false);
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star5").gameObject.SetActive(false);
        for (int i = 1; i <= tempStar; i++)
        {
            AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("Star" + i.ToString()).gameObject.SetActive(true);
        }
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorName").GetComponent<UILabel>().text = CurrentInUseAdvisor[2].AdvisorName;
        AdvisorScreen.transform.Find("CurrentAdvisorInfo").transform.Find("AdvisorWork").GetComponent<UILabel>().text = CurrentInUseAdvisor[2].GetInfo(CurrentInUseAdvisor[2].AdvisorName);

    }

    private void OnFinished()
    {
        grid.transform.GetChild(indexforthattravel + 2).gameObject.SetActive(false);
        advisorSound.Play();
        AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").transform.GetComponent<UISprite>().spriteName = "Star" + grid.transform.GetChild(indexforthattravel + 2).GetComponent<AdvisorScript>().RatedStar.ToString();
        AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").transform.GetComponent<UIButton>().normalSprite = "Star" + grid.transform.GetChild(indexforthattravel + 2).GetComponent<AdvisorScript>().RatedStar.ToString();
        AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").GetChild(0).transform.GetComponent<UISprite>().spriteName = grid.transform.GetChild(indexforthattravel + 2).GetComponent<AdvisorScript>().AdvisorName;
        AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").gameObject.SetActive(true);
        AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").Find("AdvisorName").GetComponent<UILabel>().text = grid.transform.GetChild(indexforthattravel + 2).GetComponent<AdvisorScript>().AdvisorName;
        AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").Find("AdvisorInfo").GetComponent<UILabel>().text = grid.transform.GetChild(indexforthattravel + 2).GetComponent<AdvisorScript>().GetInfo(grid.transform.GetChild(indexforthattravel + 2).GetComponent<AdvisorScript>().AdvisorName);

    }
    public void TouchToExplore()
    {
        AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").gameObject.SetActive(true);
        AdvisorSpinScreen.transform.Find("Panel").Find("Advisor").GetComponent<TweenPosition>().PlayForward();
        AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").GetComponent<TweenScale>().PlayForward();
        if (totalgem >= 50)
        {
            spinagainwith50gembutton.isEnabled = true;
        }
        else
        {
            spinagainwith50gembutton.isEnabled = false;
        }
        if (Myadvisors.Count > 0)
        {
           for(int i = 0; i < Myadvisors.Count; i++)
            {
                if (Myadvisors[i].AdvisorName.Equals(AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").Find("AdvisorName").GetComponent<UILabel>().text))
                {
                    return;
                }
            }
            AdvisorScript newadvisor = new AdvisorScript();
            newadvisor.AdvisorName = AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").Find("AdvisorName").GetComponent<UILabel>().text;
            newadvisor.GetInfo(newadvisor.AdvisorName);
            if (CurrentInUseAdvisor.Count < 3)
            {
                newadvisor.InCurrentUse = true;
                CurrentInUseAdvisor.Add(newadvisor);
            }
            else
            {
                newadvisor.InCurrentUse = false;
            }
            Myadvisors.Add(newadvisor);
            ConsiderAdvisorParticipation();

            SaveData();

        }
        else
        {
            AdvisorScript newadvisor = new AdvisorScript();
            newadvisor.AdvisorName = AdvisorSpinScreen.transform.Find("Panel").Find("Sprite").Find("AdvisorName").GetComponent<UILabel>().text;
            newadvisor.GetInfo(newadvisor.AdvisorName);
           
            newadvisor.InCurrentUse = true;
            CurrentInUseAdvisor.Add(newadvisor);
           
            Myadvisors.Add(newadvisor);
            ConsiderAdvisorParticipation();
            SaveData();

        }
    }
    public void ActiveAdvisorInfoPressed(GameObject OpenClose)
    {
        if(OpenClose.transform.localPosition.x == 0)
        {
            print("inhouse");
            OpenClose.transform.GetComponent<TweenScale>().PlayForward();
            OpenClose.transform.GetComponent<TweenPosition>().PlayForward();
        }
        else
        {
            print("outhouse");
            OpenClose.transform.GetComponent<TweenScale>().PlayReverse();
            OpenClose.transform.GetComponent<TweenPosition>().PlayReverse();
        }
    }
    public void PassiveAdvisorInfoPressed()
    {

    }
    public void GooglePlayButtonPressed()
    {

    }
    public void SettingButtonPressed()
    {
        MainMenuScreen.SetActive(false);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);

        DailyQuestScreen.SetActive(false);

        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);

        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(true);
        ShoppingScreen.SetActive(false);
    
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }
    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void ShopButtonPressed()
    {
        MainMenuScreen.SetActive(false);
        MenuScreen.SetActive(false);
        GetNewAdvisorScreen.SetActive(false);
        ActivateBizBotsScreen.SetActive(false);
       
        DailyQuestScreen.SetActive(false);
      
        AdvisorScreen.SetActive(false);
        BusinessBonusScreen.SetActive(false);
      
        ProfileBoostScreen.SetActive(false);
        SettingScreen.SetActive(false);
        ShoppingScreen.SetActive(true);
        
        CollectBonusScreen.SetActive(false);
        CollectionScreen.SetActive(false);
        AdvisorSpinScreen.SetActive(false);
    }

    public void GoldEEmpireButtonPressed()
    {

    }

    public void AdvisorPressed(string name)
    {
     
    }
    public int SortByStar(Transform a, Transform b) { return a.GetComponent<AdvisorScript>().RatedStar.CompareTo(b.GetComponent<AdvisorScript>().RatedStar); }
    public int SortByName(Transform a, Transform b) { return string.Compare(a.GetComponent<AdvisorScript>().AdvisorName, b.GetComponent<AdvisorScript>().AdvisorName); }
    public void SortTableByOwned()
    {
        MyAdvisorTable.sorting = UITable.Sorting.Custom;
        MyAdvisorTable.onCustomSort = SortByName;
        MyAdvisorTable.Reposition();
        
    }
    public void SortTableByName()
    {
        
        MyAdvisorTable.onCustomSort = SortByName;
        MyAdvisorTable.sorting = UITable.Sorting.Custom;
        MyAdvisorTable.Reposition();

    }
    public void SortTableByStar()
    {
        MyAdvisorTable.sorting = UITable.Sorting.Custom;
        MyAdvisorTable.onCustomSort = SortByStar;
        MyAdvisorTable.Reposition();

    }
    
}
