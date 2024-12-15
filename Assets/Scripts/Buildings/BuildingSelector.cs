using UnityEngine;
using System.Collections;
//using System.Collections.Generic;

public class BuildingSelector : MonoBehaviour {//attached to each building as an invisible 2dtoolkit button
	
	public bool 
		isSelected = true,
		inConstruction = true,//only for load/save
		goldBased,
		battleMap = false;
    public UILabel mylable,criticallable,gemlable;
    public long coinvalue;
	public int 
		buildingIndex = -1,	
		resourceValue;

	public string buildingType;
	private Component buildingCreator, relay, battleProc, soundFX, tween;
    GameManagerOfBusiness GameMngr;
    public int Tap9X = 1;
    public int Tap14X = 1;
    public int CriticalTap18x = 1;
    public int FivexCriticalTapChance = 1;
    public double tapvaluecounter = 4;
    public GameObject SpriteHolder;
    public int valueOfbuilding = 1;
    public ParticleSystem MyParticle;
    public float Gem2x = 1;
    public float Gem3x = 1;
    public float Gem2Point7x = 1;
    // Use this for initialization
    void Start () {
        mylable.gameObject.SetActive(false);
        criticallable.gameObject.SetActive(false);
        gemlable.gameObject.SetActive(false);
        tween = GetComponent<BuildingTween> ();
        GameMngr = GameObject.Find("GameManagerOfBusiness").GetComponent<GameManagerOfBusiness>();
        coinvalue = 4;
        mylable.text = "+ $"+coinvalue.ToString();
    }
    public void UpdateHome()
    {
        if (buildingType.Equals("Costa Rica"))
        {

            MyParticle.Play();
             valueOfbuilding++;
            string nameOfSprite = "CostaRicaHouse" + valueOfbuilding.ToString();
            
            StartCoroutine(WaitToDoPerform(nameOfSprite));
        }
        if (buildingType.Equals("Skull Island"))
        {

            MyParticle.Play();
            valueOfbuilding++;
            string nameOfSprite = "SkullIsland" + valueOfbuilding.ToString();

            StartCoroutine(WaitToDoPerform(nameOfSprite));
        }
    }
    IEnumerator WaitToDoPerform(string nameOfSprite)
    {
        yield return new WaitForSeconds(0.6f);
        SpriteHolder.transform.Find("HomeLower").GetComponent<tk2dClippedSprite>().SetSprite(nameOfSprite);
        SpriteHolder.transform.Find("HomeUpper").GetComponent<tk2dClippedSprite>().SetSprite(nameOfSprite);
        SpriteHolder.transform.Find("Shadow").GetComponent<tk2dClippedSprite>().SetSprite(nameOfSprite);
        
    }
    public void AnimationOfLableIsFinished()
    {
        mylable.gameObject.SetActive(false);
    }
    public void AnimationOfCriticalLableIsFinished()
    {
        criticallable.gameObject.SetActive(false);
    }
    public void AnimationOfGemLableIsFinished()
    {
        gemlable.gameObject.SetActive(false);
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

    public void ReSelect()
	{

        coinvalue = (long)(tapvaluecounter* Tap9X * Tap14X* valueOfbuilding) ;
        mylable.text = MyString(coinvalue);
     //   print("number of click"+ coinvalue);
        ((BuildingTween)tween).Tween();
        mylable.transform.localScale = Vector3.zero;
        mylable.gameObject.SetActive(true);
        mylable.GetComponent<TweenScale>().ResetToBeginning();
        mylable.GetComponent<TweenScale>().PlayForward();
        GameMngr.CoinAdded(coinvalue);
        GameMngr.taphouseindex++;
        if (GameMngr.taphouseindex == 2000)
        {
            GameMngr.dailyQuestPopup.GetComponent<TweenPosition>().PlayForward();
            GameMngr.dailyQuestPopup.GetComponent<TweenScale>().PlayForward();
        }
        int criticlchance = Random.Range(0, 25/ FivexCriticalTapChance);
        if(criticlchance == 2)
        {
            long additionalcoin = coinvalue * 25*CriticalTap18x;
            criticallable.text = MyString(additionalcoin);
            criticallable.transform.localScale = Vector3.zero;
            criticallable.gameObject.SetActive(true);
            criticallable.GetComponent<TweenScale>().ResetToBeginning();
            criticallable.GetComponent<TweenScale>().PlayForward();
            GameMngr.criticaltapindex++;
            if (GameMngr.criticaltapindex == 10)
            {
                GameMngr.dailyQuestPopup.GetComponent<TweenPosition>().PlayForward();
                GameMngr.dailyQuestPopup.GetComponent<TweenScale>().PlayForward();
            }
            GameMngr.CoinAdded(additionalcoin);
        }

        int Gemtapchance = Random.Range(0, 50 / (int)(Gem2x * Gem3x * Gem2Point7x));
        if(Gemtapchance == 2)
        {
            GameMngr.totalgem = GameMngr.totalgem + 2;
        
            gemlable.transform.localScale = Vector3.zero;
            gemlable.gameObject.SetActive(true);
            gemlable.GetComponent<TweenScale>().ResetToBeginning();
            gemlable.GetComponent<TweenScale>().PlayForward();
           
        }

    }

}
