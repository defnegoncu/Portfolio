using System.Collections;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{

    //Init finetuning in inspector
    public int gameSpeed;
    public int tileOffset;
    public int LevelLänge;
    public int startRow;
    public int floor0prob;
    public int floor1prob;
    public int floor2prob;

    //select in Inspector
    public GameObject groundTile;
    public GameObject Goldhaufen;
    public GameObject Goldkiste;
    public GameObject Pixie;
    public GameObject Flammensäule;
    public GameObject Wyvern;
    public GameObject Stacheln;
    public GameObject BossRoom;
    public GameObject dragon;

    public GameObject Coin;

    //dont touch
    int currentRow=0;
    int deleteRow = 0;
    public GameObject[,,] groundPos;
    [HideInInspector]
    public bool pixieActive=false;
    public GameObject bossbar;
    public GameObject winscreen;

    void CreateHoleLeft()
    {
        GameObject temp=null;
        for (int i = 0; i <=2; i++)
        {

        temp = groundPos[currentRow-i, 0, 0];
            Destroy(temp);
            groundPos[currentRow-i, 0, 0] = null;
        }
       
        
    }
    void CreateHoleRight()
    {
            GameObject temp=null;
        for (int i = 0; i <= 2; i++)
        {

            temp = groundPos[currentRow - i, 2, 0];
            Destroy(temp);
            groundPos[currentRow - i, 2, 0] = null;
        }
        
    }


    //Fade in Annimation
    IEnumerator MoveTiles(Transform temp,int row,int rowPos,int floor)
    {
        
        for (int i = 0; i < 50; i++)
        {
            if(temp==null) yield break;
            // interpolate position
            temp.position = Vector3.MoveTowards(temp.position, new Vector3(-row, floor, rowPos), 0.1f);

            // wait for end of frame to proceed
            yield return null;
        }
    }
    void CreateBossArena()
    {
        //muss ungerade
        int größe=11;
        int offset = Mathf.FloorToInt(größe) / 2;
        Instantiate(dragon, new Vector3(-(LevelLänge+startRow+größe+2),tileOffset-7,0),Quaternion.Euler(0,90,0));
        for (int j = 0; j <= größe-1; j++)
        {
            

            for (int i = 0; i <= größe-1; i++)
            {
                //Instantiate One Tile and store in temp GameObject
                GameObject temp = (GameObject)Instantiate(BossRoom, new Vector3(-(LevelLänge + startRow+j), tileOffset, i-offset), Quaternion.identity);

                //only pick transform for optimization
                Transform tempTransform = temp.transform;

                //Start animation for Transformation ofGameObject
                StartCoroutine(MoveTiles(tempTransform, LevelLänge + startRow+j, i - offset, 0));

            }
            
        }
    }
    //Builds Rows 
    void InstantiateRow()
    {
        //terminate after levellänge
        if (currentRow == LevelLänge - 1)
        {
            CancelInvoke("InstantiateRow");
            CreateBossArena();
        }
        //Spawn Floor 0
        for (int i = 0; i <=2 ; i++)
        {
                
            
           
                //Instantiate One Tile and store in temp GameObject
                GameObject temp = (GameObject)Instantiate(groundTile, new Vector3(-(currentRow+startRow), tileOffset, i-1), Quaternion.Euler( i * 90f,180f, 0f));

                //Add GameObject to List   
                groundPos[currentRow, i,0] = temp;
                 
                //only pick transform for optimization
                Transform tempTransform = temp.transform;

                //Start animation for Transformation ofGameObject
                StartCoroutine(MoveTiles(tempTransform, currentRow + startRow, i - 1, 0));
                
            

            
                
        }
        //Create Hole Event
        int rand0 = Random.Range(0, floor0prob);
        switch (rand0)
        {
            case 0:
                CreateHoleLeft();
                break;
            case 1:
                CreateHoleRight();
                break;
            default:
                break;
        }

        //Spawn Floor 1
        for (int i = 0; i <= 2; i++)
        {
            GameObject temp=null;
            GameObject prefab=null;
    
            int rand = Random.Range(0, floor1prob);
            //Add GOs to selection
            switch (rand)
            {
                case 0:
                    prefab = Goldhaufen;
                    break;
                case 1:
                    prefab = Flammensäule;
                    break;
                case 2:
                    prefab=Stacheln;
                    break;
                case 3:
                    if (pixieActive)
                    {
                        prefab = null; 
                    }
                    else
                    {
                    prefab = Pixie;
                        pixieActive = true;

                    }
                    break;
                case 4: 
                    prefab = Goldkiste;
                    break;
                case 5: case 6:  case 7: case 8:
                    prefab = Coin;
                    break;
                default:
                    break;
            }
            
            //if randspawn is true and ground has no hole
            if (prefab != null && groundPos[currentRow, i, 0] != null)
            {
                //Instantiate prefab
                temp=(GameObject)Instantiate(prefab, new Vector3(-(currentRow+startRow), tileOffset+1, i-1), Quaternion.identity);
                //only pick transform for optimization
                Transform tempTransform = temp.transform;

                //Start animation for Transformation ofGameObject
                StartCoroutine(MoveTiles(tempTransform, currentRow + startRow, i - 1,1));
            }
            
            //Add GameObject to List   
            groundPos[currentRow, i, 1]=temp;

            
            
        }

        //Spawn Floor 2
        for (int i = 0; i <= 2; i++)
        {
            GameObject temp = null;
            GameObject prefab = null;
            int rand = Random.Range(0, floor2prob);
            switch (rand)
            {
                case 0:prefab = Wyvern;
                    break;
                default:
                    break;
            }
            if (prefab != null && groundPos[currentRow,i,1]==null)
            {
                temp=(GameObject)Instantiate(prefab, new Vector3(-(currentRow+startRow), tileOffset+2, i-1), Quaternion.identity);

                //only pick transform for optimization
                Transform tempTransform = temp.transform;

                //Start animation for Transformation ofGameObject
                StartCoroutine(MoveTiles(tempTransform, currentRow + startRow, i - 1, 2));
            }
            //Add GO to list
            groundPos[currentRow, i, 2] = temp;
        }

        //Increase Row count
        currentRow++;
        
    }

    //Deletes Rows
    void DeleteRow()
    {
        //Terminate after LevelLänge
        if (deleteRow == LevelLänge-1) CancelInvoke("DeleteRow");
        
        //temp Gameobject to remove
        GameObject temp = null;

        //delete floors
        for (int j = 0; j <= 2; j++)
        {
            //delete tile
            for (int i = 0; i <= 2; i++)
            {
                
                    //set temp to delete onject
                    temp = groundPos[deleteRow, j,i];

               
                if(temp!=null)
                {
                    //destroy gameobject
                    Destroy(temp);

                    //remove pos from list  
                    groundPos[deleteRow, j, i] = null;
                }
                
            }
            
        }
           
        //increase pos to delete
        deleteRow++;
        
    }

    void PlaceStartTiles()
    {
       
        for (int i = 0; i < startRow; i++)
        {
            for (int q = 0; q <= 2; q++)
            {
                Instantiate(groundTile, new Vector3(-(i), tileOffset - 5, q-1), Quaternion.Euler(q*90f,180f,0f));
            }

        }
        
    }
    void Start()
    {
        LevelLänge = GameManager.menulenght;
        bossbar.SetActive(false);
        //init Game array
        groundPos = new GameObject[LevelLänge, 3,3];

        //fill start tiles
        PlaceStartTiles();

        //Initialize Builder
        InvokeRepeating("InstantiateRow", 0f,1f/gameSpeed);

        //Initialize Destroyer
        InvokeRepeating("DeleteRow",1+ startRow / gameSpeed, 1f / gameSpeed);

    }
    private void Update()
    {
        



        if (GameObject.Find("PlayerForward").transform.position.x<= -(LevelLänge + startRow))
        {
            GameObject.Find("FirstPersonPlayer").GetComponent<PlayerMovement>().enabled = false;
            GameObject.Find("PlayerForward").GetComponent<PlayerMovementinX>().enabled = false;
            GameObject.Find("PlayerForward").GetComponent<PlayerMovmentBoss>().enabled = true;
            GameObject.Find("FirstPersonPlayer").GetComponent<DTrack.DTrackReceiver6Dof> ().boss = true;
            FindObjectOfType<AudioManager>().Stop("walk");
            //GameObject.Find("FirstPersonPlayer").GetComponent<DTrack.DTrackReceiver6Dof>().DrackPos = new Vector3(-(LevelLänge + startRow), tileOffset - 5, 0);

        }
    }
}
