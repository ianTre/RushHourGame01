using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class NPCCar : MonoBehaviour
{
    public Node actualNode;
    public Node TargetNode;
    bool ctrlHasBeingPressed = false;
    [SerializeField]
    Vector3 watch;
    [SerializeField]
    Quaternion quaternion;
    public float NPCSpeed = 10;
    Rigidbody2D Npcrigidbody;
    public float waitUntil = 0.1f;
    
    private void Start()
    {
        Npcrigidbody = GetComponent<Rigidbody2D>();
        Npcrigidbody.mass = Random.Range(5,999);
    }

    private void Update()
    {
        GetNextNode();
        
            ctrlHasBeingPressed = false;
            transform.position = Vector3.MoveTowards(transform.position , TargetNode.transform.position , NPCSpeed * Time.deltaTime);
            Rotate();
    }

    private void Rotate()
    {
        Vector3 TargetLook = transform.InverseTransformPoint(TargetNode.transform.position);
        watch = TargetLook;
        float angle = Mathf.Atan2(TargetLook.y,TargetLook.x) * Mathf.Rad2Deg - 90 ;
        transform.Rotate(0,0,angle);   
    }

    private Node GetNextNode()
    {
        System.Random random  = new System.Random();
        int randomNumber = random.Next(0,2);

        if(TargetNode == null)
        {
            TargetNode = actualNode.GetNextMove();
        }
        if(transform.position == TargetNode.transform.position)
        {
            actualNode = TargetNode;
            TargetNode = actualNode.GetNextMove();
        }
        return this.TargetNode;
    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.tag != "NPC")
            return;
        if (waitUntil > 0 )
        {
            waitUntil = waitUntil- Time.deltaTime;
        }
        else
        {
            int range; 
            if(this.gameObject.name.Sum(x => (int)x) > collision.gameObject.name.Sum(x => (int)x))
            {
                range = Random.Range(7000,9999);
            }
            else
            {
                range = Random.Range(1,250);
            }
            GetComponent<Rigidbody2D>().mass = range;
            Debug.Log("Changing mass of NPC " + this.gameObject.name + range.ToString());
            waitUntil = Random.Range(0.1f,1);
        }
    }
}
