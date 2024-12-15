using UnityEngine;
using System.Collections;

public class BuildingTween : MonoBehaviour {		//briefly scales up and down the spites parent object when the building is selected

	private GameObject sprites;
    public tk2dClippedSprite[] mysprite;
    public ParticleSystem[] myparticles;
   
	public float 
		tweenSpeed = 0.02f,
		size = 1,			
		initSize = 1,  
		maxSize =  1.1f;

	private bool 
		tweenb, 
		scaleUpb;

	void Start () {

	//	sprites = transform.FindChild ("Sprites").gameObject; 	//find the sprites parent
     //   mysprite = sprites.gameObject.transform.FindChild("Barrelt").GetComponent<tk2dClippedSprite>();
    }

	public void Tween()
	{
		tweenb = true;
		scaleUpb = true;
       
        for (int i = 0; i < transform.GetComponent<BuildingSelector>().valueOfbuilding-1; i++)
        {
            myparticles[i].Play();    //pass the scale values to the sprites parent
        }
    }
    

	void FixedUpdate()
	{
		if(tweenb)
		{
			if(scaleUpb)
			{
				size+= tweenSpeed;					//scale up
			}
			else
			{
				size-= tweenSpeed; 					//scale back down
			}

			if(size>maxSize) scaleUpb = false; 		//maximum size reached, time to scale down

			else if(size<initSize) 					//reached a size smaller than the initial size
			{
				tweenb = false;						//end the scale sequence 
				size = initSize; 					//reset the size to 1
			}
            for (int i = 0; i < transform.GetComponent<BuildingSelector>().valueOfbuilding-1; i++)
            {
                mysprite[i].scale = new Vector3(1, size, 1);    //pass the scale values to the sprites parent
            }
		}
	}
}