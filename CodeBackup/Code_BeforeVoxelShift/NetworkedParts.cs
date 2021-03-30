using UnityEngine;
using UnityEngine.Networking;
class NetworkedParts : NetworkBehaviour
{
    public Main Main1;
	public CreateGameObjects CreateGameObjectsNetworked;

    public Vector3[] UnitstoS = new Vector3[100];
    public int SpriteObjectsSize = 100;
    public int SpriteObjectsSize3 = 300;

    /*****************************************************************!!!*/
    public SyncListFloat UnitPositions = new SyncListFloat();
    /*********************************************************************/
    void Start()
    {
        /******************************!!!*/
        for (int n = 0; n < SpriteObjectsSize3; n += 3)
        {
            UnitPositions.Add(10);
            UnitPositions.Add(10);
            UnitPositions.Add(10);
	    //the start pos can also make bugs
	    //you could CAP the force too
            UnitPositions[n] = 1000;
            UnitPositions[n + 1] = 1000;
            UnitPositions[n + 2] = -5;
        }
        /********************************/
    }

    [Command]  
    public void CmdSendLocalVectorsToServer
    (
        Vector3[] UnitstoS
    )
    {
        /********************************************!!!*/
        for (int n = 0; n < SpriteObjectsSize3; n += 3)
        {
            UnitPositions[n] = UnitstoS[n / 3].x;
            UnitPositions[n + 1] = UnitstoS[n / 3].y;
            UnitPositions[n + 2] = UnitstoS[n / 3].z;
        }
        /**********************************************/
    }

    void ApplySync()
    {
        /*******************************************************!!!*/
        for (int n = 0; n < SpriteObjectsSize3; n += 3)
        {
            /*SpriteObject ThisSprite = Main1.SpriteObjects[n / 3];
            ThisSprite.BalanceScript.ObjPos =
                ThisSprite.SpritePos;

            ThisSprite.BalanceScript.TargetPos = 
                new Vector3
                (
                UnitPositions[n],
                UnitPositions[n + 1],
                UnitPositions[n + 2]
                );

            ThisSprite.SpriteObj.transform.position +=//velocity?
                ThisSprite.BalanceScript.BalancingForce() * 0.004f;//0.4*/
        }
        /********************************************************/
    }

    void GetVectorsToS()
    {
        for (int n = 0; n < SpriteObjectsSize; n++)
        {
            //UnitstoS[n] = Main1.SpriteObjects[n].SpritePos;//!!!!!!!!!!!!!!
        }
    }

    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            GetVectorsToS();
            CmdSendLocalVectorsToServer
            (
                UnitstoS
            );
        }
        else if (!isLocalPlayer)
        {
            ApplySync();
        }
    }
}