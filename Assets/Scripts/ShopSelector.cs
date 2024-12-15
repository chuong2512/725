using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ShopSelector : MonoBehaviour {
    public bool
        isSelected = true,
        inConstruction = true,//only for load/save
        goldBased,
        battleMap = false;
    public UILabel mylable;
    public long coinvalue;
    public int
        buildingIndex = 0,
        resourceValue;

    public string buildingType;
    public string buildingCity;
    private Component buildingCreator, relay, battleProc, soundFX, tween;
    public float baseCoefficient;
    public long initialCost ;
    public long ValueGenerate;
    private long buildingvalue;
    GameManagerOfBusiness GameMngr;
    public GameObject BuyButton;
    public GameObject BusinessIndexNumberHolder;
    public GameObject MeneyGenerateInfoHolder;
    public GameObject TimerScreen;
    public GameObject BuyWithGemObject;
    public UILabel GemRequiredTOBuy;
    public UISprite InsideBuyButton;
    public UISlider BusinessIndexNumberProgress;
    public UILabel TotalBusinessIndexNumberLable;
    public UISlider MoneyGenerateBar;
    public UILabel MoneyGenerateInfoBarLable;
    public UILabel MoneyNeededForBusinessUpgradeLable;
    public UILabel TimerCounterLable;
    public SkeletonAnimation CoinAnimation;
    public GameObject SpriteHolder;
    public float timeforgeneratemoney= 2f;
    private float minutes;
    private float seconds;
    private float timeLeft;
    private bool shrinkornot = false;
    private bool animationrunornot = false;
    public long valueOfPurchase, valueOfReturn;
    public int Eightx = 1;
    public float Minus25Cost = 1f;
    public int Global4x = 1;
    public float Speed5X = 1;
    private bool addedadvisorshow = false;
    public GameObject AdvisorInfoTab;
    public GameObject AdvisorInfoBox;
    public float tapvalueCount;
    public long requiredGem;
    public ParticleSystem MyParticle;
    private AudioSource moneyIncome;
    private AudioSource upgradeBuilding;
    // Use this for initialization
    void Start () {
        mylable.gameObject.SetActive(false);
        //   baseCoefficient = 1.04f;
        //  initialCost = 5;
        if (!GameMngr)
        {
            GameMngr = GameObject.Find("GameManagerOfBusiness").GetComponent<GameManagerOfBusiness>();
        }
        moneyIncome = GameMngr.moneyIncome;
        // coinvalue = 4;
        mylable.text = "+ $" + MyString(coinvalue);
        timeLeft = timeforgeneratemoney;
/*
        if (buildingType.Equals("Ice-Creame Parlour"))
        {
            initialCost = 5;
            ValueGenerate = 5;
        }
        else if (buildingType.Equals("Shopping Mall"))
        {

           
        }
        else if (buildingType.Equals("Pizza Restaurant"))
        {

          
        }
        else if (buildingType.Equals("Coffee Cafe"))
        {

        }
        */






        // buildingIndex = 0;

        //  coinvalue = valueOfReturn;
        // valueOfReturn = (buildingIndex * ValueGenerate);
        // valueOfPurchase = initialCost;
        if (buildingIndex == 0)
        {
            BuyButton.SetActive(true);
            BuyButton.transform.GetChild(0).GetComponent<UILabel>().text = MyString(valueOfPurchase);
            BusinessIndexNumberHolder.SetActive(false);
            MeneyGenerateInfoHolder.SetActive(false);
            TimerScreen.SetActive(false);
        }
        BuildingValueDecider(buildingIndex);
    }
    public void BuildingValueDecider(int indexForBuilding)
    {
        if(!GameMngr)
        GameMngr = GameObject.Find("GameManagerOfBusiness").GetComponent<GameManagerOfBusiness>();
        if (buildingIndex >= 1)
        {
            BuyButton.SetActive(false);
           
            BusinessIndexNumberHolder.SetActive(true);
            MeneyGenerateInfoHolder.SetActive(true);
           
            valueOfReturn =(buildingIndex * ValueGenerate * Eightx * Global4x * GameMngr.TwoXProfit);
            valueOfReturn = (valueOfReturn + valueOfReturn* (long)(GameMngr.bizBotProfit * GameMngr.TotalBizbot));
            timeforgeneratemoney = timeforgeneratemoney *(1/ Speed5X);
           
            if (buildingIndex % 25 == 0)
            {
                timeforgeneratemoney = timeforgeneratemoney / (4f);
            }
            if (timeforgeneratemoney < 1)
            {
                valueOfReturn = (valueOfReturn *(long) (1f / timeforgeneratemoney));
                coinvalue = valueOfReturn;
                MoneyGenerateInfoBarLable.text = MyString(coinvalue) + "/SEC";
                GameMngr.CountForCoin(buildingType,coinvalue);
                TimerScreen.SetActive(false);
            }
            else
            {
                coinvalue = valueOfReturn;
                GameMngr.CountForCoin(buildingType, (long)(coinvalue/timeforgeneratemoney));
                MoneyGenerateInfoBarLable.text = MyString(coinvalue);
                TimerScreen.SetActive(true);
            }
            valueOfPurchase = (long)(initialCost *(Mathf.Pow(baseCoefficient,buildingIndex+1)) * Minus25Cost);
            timeLeft = timeforgeneratemoney;
            if ((buildingIndex + 1) % 25 == 0)
            {
                valueOfPurchase = valueOfPurchase * 15;

                InsideBuyButton.spriteName = "orange solid button";
            }
            else
            {
                InsideBuyButton.spriteName = "green button";
            }
            mylable.text = "+ $" + coinvalue.ToString();
            MoneyNeededForBusinessUpgradeLable.text = "BUY \n" + MyString(valueOfPurchase);
            TotalBusinessIndexNumberLable.text = buildingIndex.ToString();
            int dk = (buildingIndex / 25);

            BusinessIndexNumberProgress.value = (buildingIndex - (25 * dk)) / (25f);
        }
        else
        {

            valueOfReturn = (buildingIndex * ValueGenerate * Eightx * Global4x * GameMngr.TwoXProfit);
            valueOfReturn = (valueOfReturn + valueOfReturn * (long)(GameMngr.bizBotProfit * GameMngr.TotalBizbot));
            coinvalue = valueOfReturn;
            valueOfPurchase = (long)(initialCost * Minus25Cost);
            BuyButton.SetActive(true);
            BuyButton.transform.GetChild(0).GetComponent<UILabel>().text = MyString(valueOfPurchase);
            BusinessIndexNumberHolder.SetActive(false);
            MeneyGenerateInfoHolder.SetActive(false);
            TimerScreen.SetActive(false);
        }

       

    }




    public void PurchaseFirstLevel()
    {
        if (GameMngr.totalcoin >= valueOfPurchase)
        {
            if(buildingType.Equals("Ice-Creame Parlour"))
            {
                GameMngr.IntroductoryButton.transform.GetComponent<TweenScale>().PlayReverse();
            }
            BuyButton.SetActive(false);
            GameMngr.shopupgradeindex++;
            if(GameMngr.shopupgradeindex == 150)
            {
                GameMngr.dailyQuestPopup.GetComponent<TweenPosition>().PlayForward();
                GameMngr.dailyQuestPopup.GetComponent<TweenScale>().PlayForward();
            }
            BusinessIndexNumberHolder.SetActive(true);
            MeneyGenerateInfoHolder.SetActive(true);
            MyParticle.Play();
            TimerScreen.SetActive(true);
            GameMngr.CoinSubtract(valueOfPurchase);
            GameMngr.TapValueCounter(buildingCity,tapvalueCount);
            buildingIndex++;
            BuildingValueDecider(buildingIndex);
            GameMngr.UpdateHome(buildingCity);
            GameMngr.buildingUpgrade.Play();

        }
    }
    public void UpdateNewSprite()
    {
       
        if(buildingType.Equals("Ice-Creame Parlour"))
        {
           
            SpriteHolder.GetComponent<TweenScale>().PlayForward();
            string nameOfSprite = "IceCreame" +( (int)(buildingIndex/25)+1).ToString();
            if(buildingIndex/25 >= 6)
            {
                nameOfSprite = "IceCreame7";
            }
            StartCoroutine(WaitToDoPerform(nameOfSprite));
        }
        else if (buildingType.Equals("Shopping Mall"))
        {

            SpriteHolder.GetComponent<TweenScale>().PlayForward();
            string nameOfSprite = "ShoppingMall" + ((int)(buildingIndex / 25) + 1).ToString();
            if (buildingIndex / 25 >= 6)
            {
                nameOfSprite = "ShoppingMall7";
            }
            StartCoroutine(WaitToDoPerform(nameOfSprite));
        }
        else if (buildingType.Equals("Pizza Restaurant"))
        {

            SpriteHolder.GetComponent<TweenScale>().PlayForward();
            string nameOfSprite = "PizzaRestaurant" + ((int)(buildingIndex / 25) + 1).ToString();
            if (buildingIndex / 25 >= 6)
            {
                nameOfSprite = "PizzaRestaurant7";
            }
            StartCoroutine(WaitToDoPerform(nameOfSprite));
        }
        else if (buildingType.Equals("Coffee Cafe"))
        {

            SpriteHolder.GetComponent<TweenScale>().PlayForward();
            string nameOfSprite = "cafe" + ((int)(buildingIndex / 25) + 1).ToString();
            if (buildingIndex / 25 >= 6)
            {
                nameOfSprite = "cafe7";
            }
            StartCoroutine(WaitToDoPerform(nameOfSprite));
        }
    }
    IEnumerator WaitToDoPerform(string nameOfSprite)
    {
        yield return new WaitForSeconds(0.6f);
        SpriteHolder.transform.Find("ShopLower").GetComponent<tk2dClippedSprite>().SetSprite(nameOfSprite);
        SpriteHolder.transform.Find("ShopUpper").GetComponent<tk2dClippedSprite>().SetSprite(nameOfSprite);
        SpriteHolder.transform.Find("Shadow").GetComponent<tk2dClippedSprite>().SetSprite(nameOfSprite);
        SpriteHolder.GetComponent<TweenScale>().PlayReverse();
    }

    public void PurchaseNewLevel()
    {
        if (GameMngr.totalcoin >= valueOfPurchase)
        {
           
            GameMngr.CoinSubtract(valueOfPurchase);
            buildingIndex++;
            BuildingValueDecider(buildingIndex);
            GameMngr.TapValueCounter(buildingCity, tapvalueCount);
            GameMngr.buildingUpgrade.Play();
            MyParticle.Play();
            GameMngr.shopupgradeindex++;
            if (GameMngr.shopupgradeindex == 150)
            {
                GameMngr.dailyQuestPopup.GetComponent<TweenPosition>().PlayForward();
                GameMngr.dailyQuestPopup.GetComponent<TweenScale>().PlayForward();
            }
            if ((buildingIndex) % 25 == 0)
            {
                UpdateNewSprite();

            }
        }
        else
        {
            if ((buildingIndex + 1) % 25 == 0)
            {
                float dif = ((float)valueOfPurchase - (float)GameMngr.totalcoin) /(float) GameMngr.coinGeneratePerSec;
                print("my dif is " + dif);
                requiredGem = (long)(1 + (dif / 60));

                BuyWithGemObject.GetComponent<TweenScale>().PlayForward();
                GemRequiredTOBuy.text = requiredGem.ToString();
                StartCoroutine("WaitToFinisheGemOject");
            }
        }
    }
    public void PurchaseWithGem()
    {
       
        if(GameMngr.totalgem >= requiredGem)
        {
            
            GameMngr.totalgem = GameMngr.totalgem - requiredGem;
            buildingIndex++;
            GameMngr.shopupgradeindex++;
            if (GameMngr.shopupgradeindex == 150)
            {
                GameMngr.dailyQuestPopup.GetComponent<TweenPosition>().PlayForward();
                GameMngr.dailyQuestPopup.GetComponent<TweenScale>().PlayForward();
            }
            BuildingValueDecider(buildingIndex);
            GameMngr.TapValueCounter(buildingCity, tapvalueCount);
            GameMngr.buildingUpgrade.Play();
            BuyWithGemObject.GetComponent<TweenScale>().PlayReverse();
            MyParticle.Play();
            UpdateNewSprite();

        }
    }
    IEnumerator WaitToFinisheGemOject()
    {
        yield return new WaitForSeconds(2f);
        BuyWithGemObject.GetComponent<TweenScale>().PlayReverse();
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
    public void AnimationOfLableIsFinished()
    {
        mylable.gameObject.SetActive(false);
    }
    IEnumerator WaitForOnOFfAnimation()
    {
        yield return new WaitForSeconds(1f);
        animationrunornot = false;
    }
    public void infoAdvisorPressed()
    {
        if (addedadvisorshow == false)
        {
            addedadvisorshow = true;
            AdvisorInfoTab.GetComponent<TweenPosition>().PlayForward();
            AdvisorInfoBox.GetComponent<TweenScale>().PlayForward();
            AdvisorInfoBox.GetComponent<TweenPosition>().PlayForward();
        }
        else
        {
            addedadvisorshow = false;
            AdvisorInfoTab.GetComponent<TweenPosition>().PlayReverse();
            AdvisorInfoBox.GetComponent<TweenScale>().PlayReverse();
            AdvisorInfoBox.GetComponent<TweenPosition>().PlayReverse();
        }
    }
    public void MessageOnOff()
    {
        if (animationrunornot == false)
        {
            animationrunornot = true;
            if (shrinkornot == false)
            {
                BusinessIndexNumberHolder.GetComponent<TweenPosition>().PlayForward();
                TimerScreen.GetComponent<TweenPosition>().PlayForward();
                MeneyGenerateInfoHolder.GetComponent<TweenScale>().PlayForward();
                StartCoroutine("WaitForOnOFfAnimation");
                shrinkornot = true;

            }
            else
            {
                BusinessIndexNumberHolder.GetComponent<TweenPosition>().PlayReverse();
                TimerScreen.GetComponent<TweenPosition>().PlayReverse();
                MeneyGenerateInfoHolder.GetComponent<TweenScale>().PlayReverse();
                StartCoroutine("WaitForOnOFfAnimation");
                shrinkornot = false;
            }
        }
       
    }
    // Update is called once per frame
    void Update () {
        if(buildingIndex < 1)
        {
            return;
        }
        timeLeft -= Time.deltaTime;
        if (timeforgeneratemoney >= 0.5f)
        {
            MoneyGenerateBar.value = ((timeforgeneratemoney - timeLeft) / timeforgeneratemoney);
        }
        else
        {
            MoneyGenerateBar.value = 1f;
        }
        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;
        TimerCounterLable.text = minutes.ToString("#00") + ":" + seconds.ToString("#00");
        if (seconds > 59) seconds = 59;
        if (minutes < 0)
        {
           
                minutes = 0;
                seconds = 0;
            if (timeforgeneratemoney < 1)
            {
                timeLeft = 1;
            }
            else
            {
                timeLeft = timeforgeneratemoney;
            }
                 mylable.transform.localScale = Vector3.zero;
                 mylable.gameObject.SetActive(true);
                 mylable.GetComponent<TweenScale>().ResetToBeginning();
                 mylable.GetComponent<TweenScale>().PlayForward();
                 GameMngr.CoinAdded(coinvalue);
            moneyIncome.Play();
            CoinAnimation.enabled = true;
            CoinAnimation.Reset();
        }
    }
}
