using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetManagerHud : NetworkManager
{
    public MenuInput MenuInput1;

    public bool IsMenu;
    public bool IsMouseScanning;
    public bool IsTypeing;
    public string IpAddress;
    public bool IsTypeingIP;

    public string MouseOver;
    /**************************************/
    float TypeCursorPos;

    GameObject[] Buttons;
    int ButtonCountEnd;

    GameObject[] Sprites;
    int SpriteCountEnd;

    SpriteLetter[] Letters = new SpriteLetter[12];
    Color TextColor = Color.cyan;
    Color BackgroundColor = new Vector4(0.5f, 0.3f, 0.1f, 0.98f);
    /*******************************************************************STRINGASSEMBLY*/
    private void Create2DTextSprite(string Name, float CursPosX, float CursPosY, int ArrayWidth, int ArrayHeight
        , Color[] Letter
        )
    {
        GameObject SpriteObj;
        SpriteObj = new GameObject();
        SpriteObj.name = Name /*+ "Sprite"*/;
        SpriteObj.transform.position = Camera.main.transform.position
           + Camera.main.transform.forward * 1.8f * 100 - Camera.main.transform.right * 1 * 100;
        SpriteObj.transform.position += Camera.main.transform.right * CursPosX * 100;
        SpriteObj.transform.position += Camera.main.transform.up * CursPosY * 100;
        //SpriteObj.transform.position += Camera.main.transform.forward * 0.1f;
        SpriteObj.transform.rotation = Camera.main.transform.rotation;
        SpriteObj.transform.localScale *= 100;

        SpriteRenderer SpriteRend = SpriteObj.AddComponent<SpriteRenderer>();
        BoxCollider Coll = SpriteObj.AddComponent<BoxCollider>();
        Coll.size = new Vector3(ArrayWidth/30, 0.2f, 0.1f);
        //Coll.size

        //SpriteDataSheet SpriteCreate = new SpriteDataSheet();       
        Texture2D Text;
        Text = new Texture2D(ArrayWidth, ArrayHeight);//ARRAYWIDTHARRAYHEIGHT

        Text.SetPixels(0, 0, ArrayWidth, ArrayHeight, Letter);
        Text.filterMode = FilterMode.Point;
        Text.Apply();
        Sprite TextSprite;
        TextSprite = Sprite.Create(Text, new Rect(0.0f, 0.0f, Text.width, Text.height), new Vector2(0.5f, 0.5f), 36.0f/*this determines spacing 100*/);
        SpriteRend.sprite = TextSprite;
        SpriteRend.flipY = true;
        Sprites[SpriteCountEnd] = SpriteObj;
        SpriteCountEnd += 1;

        //since some sprites contain more array information, they have to be shrunk
        //to match the size of the sprites containing less array information
        if (ArrayHeight > 5)
        {
            SpriteObj.transform.localScale = new Vector3(
                SpriteObj.transform.localScale.x,
                SpriteObj.transform.localScale.y * 0.6f,
                SpriteObj.transform.localScale.z);
            //SpriteObj.transform.position += Camera.main.transform.up * 0.036f;
        }
        if (ArrayWidth > 5)
        {
            SpriteObj.transform.localScale = new Vector3(
                SpriteObj.transform.localScale.x * 0.6f,
                SpriteObj.transform.localScale.y,
                SpriteObj.transform.localScale.z);
        }
    }
    private void CreateSpriteWord(string Name, float YPos, SpriteLetter[] Letters, int WordLength)
    {
        int WordLength2 = WordLength * WordLength;
        Color[] Word = new Color[WordLength2 * 25];
        //SpriteDataSheet LetterCreator = new SpriteDataSheet();
        int WordRow = 0;
        int Row = 0;

        for (int n = 0; n < WordLength2 * 5; n++)
        {
            Word[n] = BackgroundColor;
        }

        for (int Y = 0; Y < 5; Y++)
        {
            for (int X = 0; X < 5; X++)
            {
                for (int L = 0; L < WordLength; L++)
                {
                    Word[X + WordRow + WordLength * L] = Letters[L].Array[X + Row];
                }
            }
            Row += 5;
            WordRow += WordLength2;
        }
        Create2DTextSprite(Name, 0, YPos, WordLength2, 5, Word);
    }
    private void DestroySprite(GameObject SpriteToDestroy)
    {

        Destroy(SpriteToDestroy);
        SpriteCountEnd -= 1;
        
    }
    public void AssembleString()//Assemble string might belong to Sprite Creator
    {
        SpriteDataSheet LetterCreator = new SpriteDataSheet();
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DestroySprite(Sprites[SpriteCountEnd - 1]);
            IpAddress = IpAddress.Substring(0, IpAddress.Length - 1);
            TypeCursorPos -= 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Color[] Letter = LetterCreator.LetterSpace(TextColor, BackgroundColor);
            IpAddress += " ";
            Create2DTextSprite("Space", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Period))
        {
            Color[] Letter = LetterCreator.LetterPeriod(TextColor, BackgroundColor);
            IpAddress += ".";
            Create2DTextSprite("Period", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        /****************************************************************************NUMBERS*/
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Color[] Letter = LetterCreator.Letter0(TextColor, BackgroundColor);
            IpAddress += "0";
            Create2DTextSprite("0", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Color[] Letter = LetterCreator.Letter1(TextColor, BackgroundColor);
            IpAddress += "1";
            Create2DTextSprite("1", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Color[] Letter = LetterCreator.Letter2(TextColor, BackgroundColor);
            IpAddress += "2";
            Create2DTextSprite("2", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Color[] Letter = LetterCreator.Letter3(TextColor, BackgroundColor);
            IpAddress += "3";
            Create2DTextSprite("3", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Color[] Letter = LetterCreator.Letter4(TextColor, BackgroundColor);
            IpAddress += "4";
            Create2DTextSprite("4", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;

        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Color[] Letter = LetterCreator.Letter5(TextColor, BackgroundColor);
            IpAddress += "5";
            Create2DTextSprite("5", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Color[] Letter = LetterCreator.Letter6(TextColor, BackgroundColor);
            IpAddress += "6";
            Create2DTextSprite("6", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Color[] Letter = LetterCreator.Letter7(TextColor, BackgroundColor);
            IpAddress += "7";
            Create2DTextSprite("7", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Color[] Letter = LetterCreator.Letter8(TextColor, BackgroundColor);
            IpAddress += "8";
            Create2DTextSprite("8", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Color[] Letter = LetterCreator.Letter9(TextColor, BackgroundColor);
            IpAddress += "9";
            Create2DTextSprite("9", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        /***************************************************************************CAPITAL LETTERS*/
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Color[] Letter = LetterCreator.LetterCapA(TextColor, BackgroundColor);
                IpAddress += "a";
                Create2DTextSprite("a", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                Color[] Letter = LetterCreator.LetterCapB(TextColor, BackgroundColor);
                IpAddress += "b";
                Create2DTextSprite("b", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                Color[] Letter = LetterCreator.LetterCapC(TextColor, BackgroundColor);
                IpAddress += "c";
                Create2DTextSprite("c", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Color[] Letter = LetterCreator.LetterCapD(TextColor, BackgroundColor);
                IpAddress += "d";
                Create2DTextSprite("d", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Color[] Letter = LetterCreator.LetterCapE(TextColor, BackgroundColor);
                IpAddress += "e";
                Create2DTextSprite("e", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                Color[] Letter = LetterCreator.LetterCapF(TextColor, BackgroundColor);
                IpAddress += "f";
                Create2DTextSprite("f", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                Color[] Letter = LetterCreator.LetterCapG(TextColor, BackgroundColor);
                IpAddress += "g";
                Create2DTextSprite("g", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.H))
            {
                Color[] Letter = LetterCreator.LetterCapH(TextColor, BackgroundColor);
                IpAddress += "h";
                Create2DTextSprite("h", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                Color[] Letter = LetterCreator.LetterCapI(TextColor, BackgroundColor);
                IpAddress += "j";
                Create2DTextSprite("j", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                Color[] Letter = LetterCreator.LetterCapJ(TextColor, BackgroundColor);
                IpAddress += "j";
                Create2DTextSprite("j", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.K))
            {
                Color[] Letter = LetterCreator.LetterCapK(TextColor, BackgroundColor);
                IpAddress += "k";
                Create2DTextSprite("k", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                Color[] Letter = LetterCreator.LetterCapL(TextColor, BackgroundColor);
                IpAddress += "l";
                Create2DTextSprite("l", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                Color[] Letter = LetterCreator.LetterCapM(TextColor, BackgroundColor);
                IpAddress += "m";
                Create2DTextSprite("m", TypeCursorPos, 0,  7, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                Color[] Letter = LetterCreator.LetterCapN(TextColor, BackgroundColor);
                IpAddress += "n";
                Create2DTextSprite("n", TypeCursorPos, 0,  7, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.O))
            {
                Color[] Letter = LetterCreator.LetterCapO(TextColor, BackgroundColor);
                IpAddress += "o";
                Create2DTextSprite("o", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                Color[] Letter = LetterCreator.LetterCapP(TextColor, BackgroundColor);
                IpAddress += "p";
                Create2DTextSprite("p", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                Color[] Letter = LetterCreator.LetterCapQ(TextColor, BackgroundColor);
                IpAddress += "q";
                Create2DTextSprite("q", TypeCursorPos, 0,  7, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                Color[] Letter = LetterCreator.LetterCapR(TextColor, BackgroundColor);
                IpAddress += "r";
                Create2DTextSprite("r", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                Color[] Letter = LetterCreator.LetterCapS(TextColor, BackgroundColor);
                IpAddress += "s";
                Create2DTextSprite("s", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                Color[] Letter = LetterCreator.LetterCapT(TextColor, BackgroundColor);
                IpAddress += "t";
                Create2DTextSprite("t", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                Color[] Letter = LetterCreator.LetterCapU(TextColor, BackgroundColor);
                IpAddress += "u";
                Create2DTextSprite("u", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.V))
            {
                Color[] Letter = LetterCreator.LetterCapV(TextColor, BackgroundColor);
                IpAddress += "v";
                Create2DTextSprite("v", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                Color[] Letter = LetterCreator.LetterCapW(TextColor, BackgroundColor);
                IpAddress += "w";
                Create2DTextSprite("w", TypeCursorPos, 0,  7, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Color[] Letter = LetterCreator.LetterCapX(TextColor, BackgroundColor);
                IpAddress += "x";
                Create2DTextSprite("x", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                Color[] Letter = LetterCreator.LetterCapY(TextColor, BackgroundColor);
                IpAddress += "y";
                Create2DTextSprite("y", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                Color[] Letter = LetterCreator.LetterCapZ(TextColor, BackgroundColor);
                IpAddress += "z";
                Create2DTextSprite("z", TypeCursorPos, 0,  5, 5, Letter);
                TypeCursorPos += 0.16f;
            }
        }
        /*********************************************************************LOWER CASE*/
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Color[] Letter = LetterCreator.LetterA(TextColor, BackgroundColor);
            IpAddress += "a";
            Create2DTextSprite("a", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            Color[] Letter = LetterCreator.LetterB(TextColor, BackgroundColor);
            IpAddress += "b";
            Create2DTextSprite("b", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Color[] Letter = LetterCreator.LetterC(TextColor, BackgroundColor);
            IpAddress += "c";
            Create2DTextSprite("c", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Color[] Letter = LetterCreator.LetterD(TextColor, BackgroundColor);
            IpAddress += "d";
            Create2DTextSprite("d", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Color[] Letter = LetterCreator.LetterE(TextColor, BackgroundColor);
            IpAddress += "e";
            Create2DTextSprite("e", TypeCursorPos, 0,  5, 7, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Color[] Letter = LetterCreator.LetterF(TextColor, BackgroundColor);
            IpAddress += "f";
            Create2DTextSprite("f", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Color[] Letter = LetterCreator.LetterG(TextColor, BackgroundColor);
            IpAddress += "g";
            Create2DTextSprite("g", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            Color[] Letter = LetterCreator.LetterH(TextColor, BackgroundColor);
            IpAddress += "h";
            Create2DTextSprite("h", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Color[] Letter = LetterCreator.LetterI(TextColor, BackgroundColor);
            IpAddress += "j";
            Create2DTextSprite("j", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            Color[] Letter = LetterCreator.LetterJ(TextColor, BackgroundColor);
            IpAddress += "j";
            Create2DTextSprite("j", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Color[] Letter = LetterCreator.LetterK(TextColor, BackgroundColor);
            IpAddress += "k";
            Create2DTextSprite("k", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            Color[] Letter = LetterCreator.LetterL(TextColor, BackgroundColor);
            IpAddress += "l";
            Create2DTextSprite("l", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Color[] Letter = LetterCreator.LetterM(TextColor, BackgroundColor);
            IpAddress += "m";
            Create2DTextSprite("m", TypeCursorPos, 0,  7, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            Color[] Letter = LetterCreator.LetterN(TextColor, BackgroundColor);
            IpAddress += "n";
            Create2DTextSprite("n", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            Color[] Letter = LetterCreator.LetterO(TextColor, BackgroundColor);
            IpAddress += "o";
            Create2DTextSprite("o", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Color[] Letter = LetterCreator.LetterP(TextColor, BackgroundColor);
            IpAddress += "p";
            Create2DTextSprite("p", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Color[] Letter = LetterCreator.LetterQ(TextColor, BackgroundColor);
            IpAddress += "q";
            Create2DTextSprite("q", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Color[] Letter = LetterCreator.LetterR(TextColor, BackgroundColor);
            IpAddress += "r";
            Create2DTextSprite("r", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Color[] Letter = LetterCreator.LetterS(TextColor, BackgroundColor);
            IpAddress += "s";
            Create2DTextSprite("s", TypeCursorPos, 0,  5, 7, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            Color[] Letter = LetterCreator.LetterT(TextColor, BackgroundColor);
            IpAddress += "t";
            Create2DTextSprite("t", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            Color[] Letter = LetterCreator.LetterU(TextColor, BackgroundColor);
            IpAddress += "u";
            Create2DTextSprite("u", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            Color[] Letter = LetterCreator.LetterV(TextColor, BackgroundColor);
            IpAddress += "v";
            Create2DTextSprite("v", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Color[] Letter = LetterCreator.LetterW(TextColor, BackgroundColor);
            IpAddress += "w";
            Create2DTextSprite("w", TypeCursorPos, 0,  7, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Color[] Letter = LetterCreator.LetterX(TextColor, BackgroundColor);
            IpAddress += "x";
            Create2DTextSprite("x", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            Color[] Letter = LetterCreator.LetterY(TextColor, BackgroundColor);
            IpAddress += "y";
            Create2DTextSprite("y", TypeCursorPos, 0,  5, 5, Letter);
            TypeCursorPos += 0.16f;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            Color[] Letter = LetterCreator.LetterZ(TextColor, BackgroundColor);
            IpAddress += "z";
            Create2DTextSprite("z", TypeCursorPos, 0,  5, 7, Letter);
            TypeCursorPos += 0.16f;
        }
      
    }
    private void SpriteLifeCycle()
    {

    }
    /********************************************************************/
    private void CreateMenuBackGround()
    {
        GameObject Button;
        //GameObject LetterSprite;

        //CreateButton
        Button = GameObject.CreatePrimitive(PrimitiveType.Plane);
        Button.name = "BackGround";

        Button.transform.position = Camera.main.transform.position
            + Camera.main.transform.forward * 2.2f - Camera.main.transform.right * 1.1f;
        Button.transform.position += Camera.main.transform.right * Button.name.Length * 0.17f * 0.5f;
        Button.transform.position += Camera.main.transform.up * 1;

        Button.transform.rotation = Camera.main.transform.rotation;
        Button.transform.Rotate(-90, 0, 0);

        Button.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Button.GetComponent<Renderer>().material.shader = Shader.Find("Sprites/Default");
        Button.GetComponent<Renderer>().material.color = new Color32(100,160,140,100);

        Buttons[ButtonCountEnd] = Button;
        ButtonCountEnd += 1;
    }
    //private void CreateButton(string Name, float PosOffset)
    //{
    //    GameObject Button;
    //    Button = GameObject.CreatePrimitive(PrimitiveType.Plane);
    //    Button.name = Name;
    //    Button.transform.position = Camera.main.transform.position
    //        + Camera.main.transform.forward * 1.82f - Camera.main.transform.right * 1.1f;
    //    Button.transform.position += Camera.main.transform.right * Name.Length * 0.17f * 0.5f;
    //    Button.transform.position += Camera.main.transform.up * PosOffset * 100;
    //    Button.transform.rotation = Camera.main.transform.rotation;
    //    Button.transform.Rotate(-90, 0, 0);
    //    Button.transform.localScale = new Vector3(Name.Length * 0.017f, 0.02f, 0.02f) * 100;

    //    //Button.GetComponent<Renderer>().material.shader = Shader.Find("Sprites/Default");
    //    Button.GetComponent<Renderer>().material.color = new Color32(100, 160, 140, 255);

    //    Buttons[ButtonCountEnd] = Button;
    //    ButtonCountEnd += 1;    

    //}
    public void DestroyAll()
    {
        for (int a = 0; a < ButtonCountEnd; a++)
        {
            Destroy(Buttons[a]);
        }
        for (int a = 0; a < SpriteCountEnd; a++)
        {
            Destroy(Sprites[a]);
        }
    }
    public void DestroyButton(string Name)
    {

        Destroy(GameObject.Find(Name));
        for (int a = 0; a < Name.Length - 5/*!!!*/; a++)
        {
            //Destroy(GameObject.Find(Name + "L" + a));//why L?
            DestroySprite(Sprites[SpriteCountEnd - 1]);
        }
    }
    private void DestroyRootMenu()
    {
        DestroyButton("Background");
        DestroyButton("SinglePlayer");
        DestroyButton("MultiPlayer");
        DestroyButton("Options");
        DestroyButton("Exit");    
    }
    public void CreateRootMenu()
    {
        
        CreateMenuBackGround();
        SpriteDataSheet LetterCreator = new SpriteDataSheet();
        for (int n = 0; n < 12; n++)
        {
            Letters[n] = new SpriteLetter();
        }

        Letters[0].Array = LetterCreator.LetterCapS(TextColor, BackgroundColor);
        Letters[1].Array = LetterCreator.LetterCapI(TextColor, BackgroundColor);
        Letters[2].Array = LetterCreator.LetterCapN(TextColor, BackgroundColor);
        Letters[3].Array = LetterCreator.LetterCapG(TextColor, BackgroundColor);
        Letters[4].Array = LetterCreator.LetterCapL(TextColor, BackgroundColor);
        Letters[5].Array = LetterCreator.LetterCapE(TextColor, BackgroundColor);
        Letters[6].Array = LetterCreator.LetterCapP(TextColor, BackgroundColor);
        Letters[7].Array = LetterCreator.LetterCapL(TextColor, BackgroundColor);
        Letters[8].Array = LetterCreator.LetterCapA(TextColor, BackgroundColor);
        Letters[9].Array = LetterCreator.LetterCapY(TextColor, BackgroundColor);
        Letters[10].Array = LetterCreator.LetterCapE(TextColor, BackgroundColor);
        Letters[11].Array = LetterCreator.LetterCapR(TextColor, BackgroundColor);
        CreateSpriteWord("SinglePlayer", 0.8f, Letters, 12);
        /**************************************************************************************/
        Letters[0].Array = LetterCreator.LetterCapM(TextColor, BackgroundColor);
        Letters[1].Array = LetterCreator.LetterCapU(TextColor, BackgroundColor);
        Letters[2].Array = LetterCreator.LetterCapL(TextColor, BackgroundColor);
        Letters[3].Array = LetterCreator.LetterCapT(TextColor, BackgroundColor);
        Letters[4].Array = LetterCreator.LetterCapI(TextColor, BackgroundColor);
        Letters[5].Array = LetterCreator.LetterCapP(TextColor, BackgroundColor);
        Letters[6].Array = LetterCreator.LetterCapL(TextColor, BackgroundColor);
        Letters[7].Array = LetterCreator.LetterCapA(TextColor, BackgroundColor);
        Letters[8].Array = LetterCreator.LetterCapY(TextColor, BackgroundColor);
        Letters[9].Array = LetterCreator.LetterCapE(TextColor, BackgroundColor);
        Letters[10].Array = LetterCreator.LetterCapR(TextColor, BackgroundColor);
        CreateSpriteWord("MultiPlayer", 0.58f, Letters, 11);
        /************************************************************************************/
        Letters[0].Array = LetterCreator.LetterCapO(TextColor, BackgroundColor);
        Letters[1].Array = LetterCreator.LetterCapP(TextColor, BackgroundColor);
        Letters[2].Array = LetterCreator.LetterCapT(TextColor, BackgroundColor);
        Letters[3].Array = LetterCreator.LetterCapI(TextColor, BackgroundColor);
        Letters[4].Array = LetterCreator.LetterCapO(TextColor, BackgroundColor);
        Letters[5].Array = LetterCreator.LetterCapN(TextColor, BackgroundColor);
        Letters[6].Array = LetterCreator.LetterCapS(TextColor, BackgroundColor);
        CreateSpriteWord("Options", 0.36f, Letters, 7);
        /******************************************************************************/
        Letters[0].Array = LetterCreator.LetterCapE(TextColor, BackgroundColor);
        Letters[1].Array = LetterCreator.LetterCapX(TextColor, BackgroundColor);
        Letters[2].Array = LetterCreator.LetterCapI(TextColor, BackgroundColor);
        Letters[3].Array = LetterCreator.LetterCapT(TextColor, BackgroundColor);
        CreateSpriteWord("Exit", 0.14f, Letters, 4);
    }
    public void DestroyMultiplayerMenu()
    {
        DestroyButton("HostGame");
        DestroyButton("JoinGame");
    }
    public void CreateMultiPlayerMenu()
    {
        SpriteDataSheet LetterCreator = new SpriteDataSheet();
        /*************************************************************************/
        Letters[0].Array = LetterCreator.LetterCapH(TextColor, BackgroundColor);
        Letters[1].Array = LetterCreator.LetterCapO(TextColor, BackgroundColor);
        Letters[2].Array = LetterCreator.LetterCapS(TextColor, BackgroundColor);
        Letters[3].Array = LetterCreator.LetterCapT(TextColor, BackgroundColor);
        Letters[4].Array = LetterCreator.LetterCapG(TextColor, BackgroundColor);
        Letters[5].Array = LetterCreator.LetterCapA(TextColor, BackgroundColor);
        Letters[6].Array = LetterCreator.LetterCapM(TextColor, BackgroundColor);
        Letters[6].Array = LetterCreator.LetterCapE(TextColor, BackgroundColor);
        CreateSpriteWord("HostGame", 0.58f, Letters, 8);
        /***************************************************************************/
        Letters[0].Array = LetterCreator.LetterCapJ(TextColor, BackgroundColor);
        Letters[1].Array = LetterCreator.LetterCapO(TextColor, BackgroundColor);
        Letters[2].Array = LetterCreator.LetterCapI(TextColor, BackgroundColor);
        Letters[3].Array = LetterCreator.LetterCapN(TextColor, BackgroundColor);
        Letters[4].Array = LetterCreator.LetterCapG(TextColor, BackgroundColor);
        Letters[5].Array = LetterCreator.LetterCapA(TextColor, BackgroundColor);
        Letters[6].Array = LetterCreator.LetterCapM(TextColor, BackgroundColor);
        Letters[6].Array = LetterCreator.LetterCapE(TextColor, BackgroundColor);
        CreateSpriteWord("JoinGame", 0.36f, Letters, 8);
    }
    public void SetIp()
    {
        //ezt egyszer csinálja meg amikor rákattintasz
        SpriteDataSheet LetterCreator = new SpriteDataSheet();

        Letters[0].Array = LetterCreator.LetterCapT(TextColor, BackgroundColor);
        Letters[1].Array = LetterCreator.LetterCapY(TextColor, BackgroundColor);
        Letters[2].Array = LetterCreator.LetterCapP(TextColor, BackgroundColor);
        Letters[3].Array = LetterCreator.LetterCapE(TextColor, BackgroundColor);
        Letters[4].Array = LetterCreator.LetterCapI(TextColor, BackgroundColor);
        Letters[5].Array = LetterCreator.LetterCapP(TextColor, BackgroundColor);
        CreateSpriteWord("TypeIP", 0.36f, Letters, 6);

        TypeCursorPos = 0;
        IsTypeingIP = true;       
    }
    //
    public void SetPort()
    {

    }
    /*************************************************************************************/
    public void StartHosting()
    {
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.StartHost();
        MouseOver = "0";
    }
    public void ClientJoin()
    {
        //IpAddress = "localhost";
        NetworkManager.singleton.networkPort = 7777;
        NetworkManager.singleton.networkAddress = IpAddress;
        //NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.StartClient();
        MouseOver = "0";
    }
    /*************************************************************************************/
    void Start ()
    {
        MenuInput1 = this.gameObject.AddComponent<MenuInput>();

        Sprites = new GameObject[16000];
        SpriteCountEnd = 0;

        Buttons = new GameObject[64];
        ButtonCountEnd = 0;

        IpAddress = "localhost";
        IsMenu = true;

        CreateRootMenu();

        //!!!!!!!!!!!!!!!!!!
        //IsMenu = false;
        //Debug.Log("startinghost");
        //StartHosting();
        //!!!!!!!!!!!!!!!!!!       
    }
    void MouseRay()
    {
        Ray ray;
        RaycastHit hit;
        //Itt kell a másik kamerát használni...
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //print(hit.collider.name);
            MouseOver = hit.collider.name;
        }
        else { MouseOver = "NULL"; }
    }
    void ClickGraphics()
    {
        
    }
    void ClickHandling()
    {
        if (IsMouseScanning)
        {
            //Debug.Log(MouseOver);
            if (MouseOver == "SinglePlayer")
            {
                DestroyAll();
                StartHosting();
                IsMenu = false;
            }
            if (MouseOver == "HostGame")
            {
                DestroyAll();
                StartHosting();
                IsMenu = false;
            }
            if (MouseOver == "JoinGame")
            {
                //MouseWasOver = "0";
                DestroyAll();
                SetIp();
                //SetPort();
                //ClientJoin();
                //create 2 new buttons
            }
            if (MouseOver == "MultiPlayer")
            {
                DestroyAll();
                CreateMultiPlayerMenu();
            }
            if (MouseOver == "Options")
            {

            }
            if (MouseOver == "Exit")
            {
                Application.Quit();
            }
        }
        else
        {
            //Debug.Log(MouseOver);
            if (MouseOver == "SinglePlayer")
            {
                GameObject.Find("SinglePlayer").GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                //MouseWasOver = "SinglePlayer";
            }
            else if (MouseOver != "SinglePlayer" && GameObject.Find("SinglePlayer"))//Ezt a drága műveletet ki lehet váltani, ha vna Button object
            {
                GameObject.Find("SinglePlayer").GetComponent<Renderer>().material.color = new Color32(220, 220, 220, 220);
            }

            if (MouseOver == "HostGame")
            {
                GameObject.Find("HostGame").GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                //MouseWasOver = "HostGame";
            }
            else if (MouseOver != "HostGame" && GameObject.Find("HostGame"))
            {
                GameObject.Find("HostGame").GetComponent<Renderer>().material.color = new Color32(220, 220, 220, 220);
            }

            if (MouseOver == "JoinGame")
            {
                GameObject.Find("JoinGame").GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                //MouseWasOver = "JoinGame";
            }
            else if (MouseOver != "JoinGame" && GameObject.Find("JoinGame"))
            {
                GameObject.Find("JoinGame").GetComponent<Renderer>().material.color = new Color32(220, 220, 220, 220);
            }

            if (MouseOver == "MultiPlayer")
            {
                GameObject.Find("MultiPlayer").GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                //MouseWasOver = "MultiPlayer";
            }
            else if (MouseOver != "MultiPlayer" && GameObject.Find("MultiPlayer"))
            {
                GameObject.Find("MultiPlayer").GetComponent<Renderer>().material.color = new Color32(220, 220, 220, 220);
            }

            if (MouseOver == "Options")
            {
                GameObject.Find("Options").GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                //MouseWasOver = "Options";
            }
            else if (MouseOver != "Options" && GameObject.Find("Options")/*MouseWasOver == "Options"*/)
            {
                GameObject.Find("Options").GetComponent<Renderer>().material.color = new Color32(220, 220, 220, 220);
            }

            if (MouseOver == "Exit")
            {
                GameObject.Find("Exit").GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                //MouseWasOver = "Exit";
            }
            else if (MouseOver != "Exit" && GameObject.Find("Exit"))
            {
                GameObject.Find("Exit").GetComponent<Renderer>().material.color = new Color32(220, 220, 220, 220);
            }

            //if (MouseOver == "BackGround")
            //{
            //    GameObject.Find("SinglePlayer").GetComponent<Renderer>().material.color = Color.black;
            //    ButtonColor = Color.black;
            //}
        }
    }
    float deltaTime;
    private void OnGUI()
    {
        float fps = 1.0f / deltaTime;
        GUI.color = Color.green;
        GUI.Label(new Rect(50, 110, 90, 30), fps.ToString());
    }
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;       
    }
    void FixedUpdate()
    {
        ClickHandling();
    }
}
