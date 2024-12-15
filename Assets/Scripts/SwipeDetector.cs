using UnityEngine;
using System.Collections;
using System;

public class SwipeDetector : MonoBehaviour
{
   

    public float minSwipeDistY;

    public float minSwipeDistX;

    private Vector2 startPos;

    private Vector2 mStartPosition;
    private float mSwipeStartTime;

    private const float mAngleRange = 30;

    // To recognize as swipe user should at lease swipe for this many pixels
    private float mMinSwipeDist = 5.0f;

    // To recognize as a swipe the velocity of the swipe
    // should be at least mMinVelocity
    // Reduce or increase to control the swipe speed
    private const float mMinVelocity = 0.0f;

    private readonly Vector2 mXAxis = new Vector2(1, 0);
    private readonly Vector2 mYAxis = new Vector2(0, 1);
    public bool doswipe = true;
    public float maxvaluetorightswipe;
    public float maxvalueofleftswipe;

    public AudioSource swipeSound;
    public GameObject costarica;
    public GameObject skulisland;


    IEnumerator WaitTodotrue()
    {
        yield return new WaitForSeconds(1f);
        doswipe = true;
    }
    IEnumerator WaitTodskull()
    {
        yield return new WaitForSeconds(2f);
        skulisland.SetActive(false);
    }
    IEnumerator WaitTodcosta()
    {
        yield return new WaitForSeconds(2f);
        costarica.SetActive(false);
    }
    void Update()
    {
      
        if (doswipe == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Record start time and position

                mStartPosition = new Vector2(Input.mousePosition.x,
                         Input.mousePosition.y);
                //	print("my start position "+mStartPosition.y);
                mSwipeStartTime = Time.time;
            }

            // Mouse button up, possible chance for a swipe
            if (Input.GetMouseButtonUp(0))
            {

                float deltaTime = Time.time - mSwipeStartTime;

                Vector2 endPosition = new Vector2(Input.mousePosition.x,
                               Input.mousePosition.y);
                Vector2 swipeVector = endPosition - mStartPosition;

                float velocity = swipeVector.magnitude / deltaTime;

                if (velocity > mMinVelocity &&
                        swipeVector.magnitude > mMinSwipeDist)
                {
                    // if the swipe has enough velocity and enough distance
                    doswipe = false;
                    StartCoroutine("WaitTodotrue");
                    swipeVector.Normalize();

                    float angleOfSwipe = Vector2.Dot(swipeVector, mXAxis);
                    angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;

                    // Detect left and right swipe
                    if (angleOfSwipe < mAngleRange)
                    {
                        //OnSwipeRight();
                       
                    }
                    else if ((180.0f - angleOfSwipe) < mAngleRange)
                    {
                        //OnSwipeLeft();
                     
                    }
                    else
                    {
                        // Detect top and bottom swipe
                        angleOfSwipe = Vector2.Dot(swipeVector, mYAxis);
                        angleOfSwipe = Mathf.Acos(angleOfSwipe) * Mathf.Rad2Deg;
                        if (angleOfSwipe < mAngleRange)
                        {
                            float jp = transform.localPosition.y;
                            if (jp > maxvaluetorightswipe)
                            {
                               
                                float dd = transform.localPosition.y;
                              
                                swipeSound.Play();
                                transform.GetComponent<TweenPosition>().from.Set(24.5f, 15, -100);
                                transform.GetComponent<TweenPosition>().to.Set(24.5f, -45, -100);
                                transform.GetComponent<TweenPosition>().duration = .5f;
                                transform.GetComponent<TweenPosition>().ResetToBeginning();
                               transform.GetComponent<TweenPosition>().PlayForward();
                               
                                costarica.SetActive(true);
                                StartCoroutine("WaitTodcosta");
                            }
                            //	OnSwipeTop();
                        }
                        else if ((180.0f - angleOfSwipe) < mAngleRange)
                        {
                            float jp = transform.localPosition.y;
                            if (jp < maxvalueofleftswipe)
                            {
                               
                                swipeSound.Play();
                                float dd = transform.localPosition.y;

                                transform.GetComponent<TweenPosition>().from.Set(24.5f, -45, -100);
                                transform.GetComponent<TweenPosition>().to.Set(24.5f, 15, -100);
                                transform.GetComponent<TweenPosition>().duration = .5f;
                                transform.GetComponent<TweenPosition>().ResetToBeginning();
                                transform.GetComponent<TweenPosition>().PlayForward();
                                skulisland.SetActive(true);
                                StartCoroutine("WaitTodskull");
                            }
                            //	OnSwipeBottom();
                        }
                        else
                        {
                            //	mMessageIndex = 0;
                        }
                    }
                }
            }
            //#endif
        }
    }
    void Start()
    {
        mMinSwipeDist = (Screen.width / 8f);
        maxvalueofleftswipe = 15;
        maxvaluetorightswipe = -45;
    }
    
}
