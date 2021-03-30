using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
    public MyNetManagerHud NetManager;
	void Start () 
	{
        NetManager = GameObject.Find("_WorldSpawn").GetComponent<MyNetManagerHud>();
        NetManager.IsMenu = true;
	}

#if UNITY_STANDALONE_WIN
    void MouseRay()
    {
        Ray ray;
        RaycastHit hit;
        //Itt kell a másik kamerát használni...
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //print(hit.collider.name);
            NetManager.MouseOver = hit.collider.name;
        }
        else { NetManager.MouseOver = "NULL"; }
    }
    void ClickHandling()
    {
        if (NetManager.IsTypeingIP)
        {
            NetManager.AssembleString();
            if (Input.anyKeyDown)
            {
                //DestroyButton("TypeIPAddress (type 'localhost' to connect to your own server)");
                //DestroyAll();//!!!!!!!!!!!!!!!!!!!!!!??
            }
            //DestroyButton("TypeIPAddress");
            if (Input.GetKeyDown(KeyCode.Return))
            {
                NetManager.ClientJoin();
                NetManager.IsTypeingIP = false;
                NetManager.DestroyAll();
            }
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            NetManager.IsMouseScanning = true;
        }
        else
        {
            NetManager.IsMouseScanning = false;
        }

    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!NetManager.IsMenu)
            {
                NetManager.IsMenu = true;
                NetManager.CreateRootMenu();
            }
            else
            {
                NetManager.IsMenu = false;
                NetManager.DestroyAll();
                NetManager.DestroyAll();
            }
        }
        if (NetManager.IsMenu)
        {
            MouseRay();
            ClickHandling();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!NetManager.IsTypeing)
                {
                    NetManager.IsTypeing = true;
                }
                else
                {
                    NetManager.IsTypeing = false;
                }
            }
        }
    }
#endif
    /*************************************************************************************/
#if UNITY_ANDROID
     void MouseRay()
    {
        Ray ray;
        RaycastHit hit;
        //Itt kell a másik kamerát használni...
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //print(hit.collider.name);
            NetManager.MouseOver = hit.collider.name;
        }
        else { NetManager.MouseOver = "NULL"; }
    }
    void ClickHandling()
    {
        if (NetManager.IsTypeingIP)
        {
            NetManager.AssembleString();
            if (Input.anyKeyDown)
            {
                //DestroyButton("TypeIPAddress (type 'localhost' to connect to your own server)");
                //DestroyAll();//!!!!!!!!!!!!!!!!!!!!!!??
            }
            //DestroyButton("TypeIPAddress");
            if (Input.GetKeyDown(KeyCode.Return))
            {
                NetManager.ClientJoin();
                NetManager.IsTypeingIP = false;
                NetManager.DestroyAll();
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            NetManager.IsMouseScanning = true;
        }
        else
        {
            NetManager.IsMouseScanning = false;
        }

    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!NetManager.IsMenu)
            {
                NetManager.IsMenu = true;
                NetManager.CreateRootMenu();
            }
            else
            {
                NetManager.IsMenu = false;
                NetManager.DestroyAll();
                NetManager.DestroyAll();
            }
        }
        if (NetManager.IsMenu)
        {
            MouseRay();
            ClickHandling();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!NetManager.IsTypeing)
                {
                    NetManager.IsTypeing = true;
                }
                else
                {
                    NetManager.IsTypeing = false;
                }
            }
        }
    }
#endif
}
