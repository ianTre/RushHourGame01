using UnityEngine;

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
        player = new Player(0.0005f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") == 1)
        {
            player.CalculateVelocity();
            this.myTransform.position = new Vector3(myTransform.position.x,myTransform.position.y + player.Velocity);
        }
    }
}
