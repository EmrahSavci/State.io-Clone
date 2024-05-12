using UnityEngine;

public class SelectArea : MonoBehaviour
{
    public static SelectArea Instante;
    public LineRenderer lineRenderer;
    Camera camera;
    Vector3  EndPos,StartPos;
    public Vector3 MousePos,Direction;
    public Transform Arrow;
    public bool IsClick=false;


    [SerializeField] GameObject RedCircle;
    [SerializeField] Transform SpawnPoints;
    public Vector3 PointDirection;

    void Awake()
    {
        Instante = this;
    }

    void Start()
    {
        lineRenderer = transform.GetChild(0).GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        Arrow = transform.GetChild(1);
        SpawnPoints = transform.GetChild(3);
        RedCircle = transform.GetChild(5).gameObject;
        RedCircle.SetActive(false);
        camera = Camera.main;
        if(PlayerPrefs.GetInt("IsGameStart")==0)
        {
            SetSoldierCount();
            
            PlayerPrefs.SetInt("IsGameStart", 1);
        }
        
        Invoke("SetColor", 0.2f);
    }
    void SetColor()
    {
        GetComponent<SpawnSoldier>().SetMyAreaMapColor();
    }
    public void SetSoldierCount()
    {
        GetComponent<SpawnSoldier>().GetMyPlayerSoldierCount();
    }
   public bool Touching = false;
    
    private void OnMouseExit()
    {
        if(IsClick)
        Touching = true;

        RedCircle.SetActive(false);
    }
  
    private void OnMouseEnter()
    {
        //ResetLine();
    }
    void Update()
    {
        
       
        

       
        if (Input.GetMouseButton(0) && Touching && UIManager.Instante.IsGameStart && !UIManager.Instante.IsGameFinish)
        {
            
            lineRenderer.enabled = true;
            Arrow.gameObject.SetActive(true);
            //RAY
            
            MousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            MousePos.z = 0;
            //RaycastHit2D hit = Physics2D.Raycast(MousePos, Vector2.zero, Mathf.Infinity, layer);

            




            //Arrow Position And Rotation
            

            
            Arrow.transform.position = new Vector3(MousePos.x,MousePos.y,Arrow.transform.position.z);
            Direction = Arrow.transform.position - transform.position;
            Quaternion look = Quaternion.LookRotation(Direction, Vector3.up);
            Arrow.transform.rotation = look;
           //hedef obje konumu yazýlacak
             PointDirection = Arrow.transform.position - SpawnPoints.transform.position;
             Quaternion look2 = Quaternion.LookRotation(PointDirection, Vector3.up);
             SpawnPoints.transform.localRotation = look2;   
            
            

            //Line Renderer Points
            EndPos = MousePos;
            EndPos.z = 0;
            lineRenderer.SetPosition(1, Arrow.transform.localPosition);
           
           
        }
        if (Input.GetMouseButtonUp(0))
        {
            RedCircle.SetActive(false);
            ResetLine();
        }
        if (GetComponent<SpawnSoldier>().SoldierCount <= 0)
        {
            Target = null;
            IsCountZero = true;
        }
            
        else
            IsCountZero = false;
    }
    public bool IsCountZero = false;
    public Transform Target;
    public void ResetLine()
    {
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(1, Vector3.zero);
        Arrow.transform.localPosition = Vector3.zero;
        Arrow.gameObject.SetActive(false);
        IsClick = false;
        Touching = false;
    }
}
