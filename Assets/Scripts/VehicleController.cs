using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class VehicleController : MonoBehaviour
{
    public Transform myTransform;
    public float GetX,GetY;
    Player player ;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("I am alive now");
        myTransform = this.GetComponent<Transform>();
        player = new Player(0.00001f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") == 1)
        {
            player.CalculateVelocity();
            this.myTransform.position = new Vector3(myTransform.position.x , myTransform.position.y + player.Velocity);

            /*f(myTransform.rotation.z == 0) 
            {
                this.myTransform.position = new Vector3(myTransform.position.x , myTransform.position.y + player.Velocity);
            }
            else
            {
                this.myTransform.position = new Vector3(myTransform.position.x + player.Velocity, myTransform.position.y + player.Velocity);
            }
            
            if(myTransform.rotation.z == 90 || myTransform.rotation.z != -90)
            {
                this.myTransform.position = new Vector3(myTransform.position.x + player.Velocity, myTransform.position.y);
            }
            else
            {
                this.myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y + player.Velocity);
            }*/
        }

         if(Input.GetAxis("Vertical") == -1)
        {
            player.CalculateVelocity ();
            this.myTransform.position = new Vector3(myTransform.position.x , myTransform.position.y - player.Velocity);
        }

          if(Input.GetAxis("Horizontal") == 1)
        {
            player.CalculateVelocity ();
            this.myTransform.rotation = new Quaternion(myTransform.rotation.x , myTransform.rotation.y, myTransform.rotation.z - 0.01f, myTransform.rotation.w);
       
        }

          if(Input.GetAxis("Horizontal") == -1)
        {
            player.CalculateVelocity ();
            this.myTransform.rotation = new Quaternion(myTransform.rotation.x , myTransform.rotation.y, myTransform.rotation.z + 0.01f, myTransform.rotation.w);
        }
    }
}
