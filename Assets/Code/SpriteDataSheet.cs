using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a data sheet for storing 6x6 pixel size sprite information for the game.
/// This symbol system strives to be minimalistic.
/// The amount of information used for letters is asymmetric since the minimum information
/// required to represent a symbol is per se asymetric.
/// I: punctuation and letters 
/// II: numbers 
/// III: sprites for particle effects
/// </summary>


public class SpriteLetter
{
    //ASCII
    public Color[] Array /*= new Color[25]*/;
}

public class Button
{
    public GameObject TriggerRect;
    public SpriteLetter[] Text;
}

public class SpriteDataSheet
{
    /// <summary>
    /// We could have a Color[] of Color[] for storing all letter arrays.
    /// Then a string "hello" could be translated to its INT values, which int 
    /// vaues would correspond to Color[INT value]
    /// </summary>

    /*******************************************************************************Lower Case LETTERS*/
    //Each letter should have a lower case, an upper case, and a right margin space.
    public Color[] LetterSpace(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        /*****************A******************B******************C******************D******************E*/
        /*1**/Array25[0] = C0;/***/Array25[1] = C0;/***/Array25[2] = C0;/***/Array25[3] = C0;/***/Array25[4] = C0;
        /*****************A******************B******************C******************D******************E*/
        /*2**/Array25[5] = C0;/***/Array25[6] = C0;/***/Array25[7] = C0;/***/Array25[8] = C0;/***/Array25[9] = C0;
        /*****************A******************B******************C******************D******************E*/
        /*3*/Array25[10] = C0;/**/Array25[11] = C0;/**/Array25[12] = C0;/**/Array25[13] = C0;/**/Array25[14] = C0;
        /*****************A******************B******************C******************D******************E*/
        /*4*/Array25[15] = C0;/**/Array25[16] = C0;/**/Array25[17] = C0;/**/Array25[18] = C0;/**/Array25[19] = C0;
        /*****************A******************B******************C******************D******************E*/
        /*5*/Array25[20] = C0;/**/Array25[21] = C0;/**/Array25[22] = C0;/**/Array25[23] = C0;/**/Array25[24] = C0;
        /*****************A******************B******************C******************D******************E*/

        return Array25;
    }
    public Color[] LetterPeriod(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0;  Array25[1] = C0;  Array25[2] = C0;  Array25[3] = C0;  Array25[4] = C0;
        Array25[5] = C0;  Array25[6] = C0;  Array25[7] = C0;  Array25[8] = C0;  Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C0; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterA(Color C1, Color C0)//elég ha Array25ek
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C1; Array25[17] = C1; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterB(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C1; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C1; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterC(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C0; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C1; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterD(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C1; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C1; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterE(Color C1, Color C0)
    {
        Color[] Array35 = new Color[35];
        Array35[0] = C0; Array35[1] = C0; Array35[2] = C0; Array35[3] = C0; Array35[4] = C0;
        Array35[5] = C1; Array35[6] = C1; Array35[7] = C1; Array35[8] = C0; Array35[9] = C0;
        Array35[10] = C1; Array35[11] = C0; Array35[12] = C1; Array35[13] = C0; Array35[14] = C0;
        Array35[15] = C1; Array35[16] = C1; Array35[17] = C1; Array35[18] = C0; Array35[19] = C0;
        Array35[20] = C1; Array35[21] = C0; Array35[22] = C0; Array35[23] = C0; Array35[24] = C0;
        Array35[25] = C1; Array35[26] = C1; Array35[27] = C1; Array35[28] = C0; Array35[29] = C0;
        Array35[30] = C0; Array35[31] = C0; Array35[32] = C0; Array35[33] = C0; Array35[34] = C0;

        return Array35;
    }
    public Color[] LetterF(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C1; Array25[21] = C1; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterG(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C1; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C1; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterH(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C1; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterI(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C1; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterJ(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C1; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterK(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C1; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C0; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C1; Array25[12] = C0; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterL(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C1; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C0; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C0; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C1; Array25[17] = C0; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterM(Color C1, Color C0)
    {
        Color[] Array35 = new Color[35];
        Array35[0] = C0; Array35[1] = C0; Array35[2] = C0; Array35[3] = C0; Array35[4] = C0; Array35[5] = C0; Array35[6] = C0;
        Array35[7] = C0; Array35[8] = C1; Array35[9] = C1; Array35[10] = C0; Array35[11] = C1; Array35[12] = C1; Array35[13] = C0;
        Array35[14] = C0; Array35[15] = C1; Array35[16] = C0; Array35[17] = C1; Array35[18] = C0; Array35[19] = C1; Array35[20] = C0;
        Array35[21] = C0; Array35[22] = C1; Array35[23] = C0; Array35[24] = C1; Array35[25] = C0; Array35[26] = C1; Array35[27] = C0;
        Array35[28] = C0; Array35[29] = C0; Array35[30] = C0; Array35[31] = C0; Array35[32] = C0; Array35[33] = C0; Array35[34] = C0;

        return Array35;
    }
    public Color[] LetterN(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C0; Array25[17] = C1; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterO(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C1; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterP(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C1; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C1; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterQ(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C1; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterR(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }  
    public Color[] LetterS(Color C1, Color C0)
    {
        Color[] Array35 = new Color[35];
        Array35[0] = C0; Array35[1] = C0; Array35[2] = C0; Array35[3] = C0; Array35[4] = C0;
        Array35[5] = C0; Array35[6] = C1; Array35[7] = C1; Array35[8] = C0; Array35[9] = C0;
        Array35[10] = C0; Array35[11] = C1; Array35[12] = C0; Array35[13] = C0; Array35[14] = C0;
        Array35[15] = C0; Array35[16] = C0; Array35[17] = C1; Array35[18] = C0; Array35[19] = C0;
        Array35[20] = C0; Array35[21] = C0; Array35[22] = C1; Array35[23] = C0; Array35[24] = C0;
        Array35[25] = C0; Array35[26] = C1; Array35[27] = C1; Array35[28] = C0; Array35[29] = C0;
        Array35[30] = C0; Array35[31] = C0; Array35[32] = C0; Array35[33] = C0; Array35[34] = C0;

        return Array35;
    }
    public Color[] LetterT(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C1; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterU(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C0; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C1; Array25[16] = C1; Array25[17] = C1; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterV(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }  
    public Color[] LetterW(Color C1, Color C0)
    {
        Color[] Array35 = new Color[35];
        Array35[0] = C0; Array35[1] = C0; Array35[2] = C0; Array35[3] = C0; Array35[4] = C0; Array35[5] = C0; Array35[6] = C0;
        Array35[7] = C1; Array35[8] = C0; Array35[9] = C1; Array35[10] = C0; Array35[11] = C1; Array35[12] = C1; Array35[13] = C0;
        Array35[14] = C1; Array35[15] = C0; Array35[16] = C1; Array35[17] = C0; Array35[18] = C1; Array35[19] = C1; Array35[20] = C0;
        Array35[21] = C1; Array35[22] = C1; Array35[23] = C0; Array35[24] = C1; Array35[25] = C1; Array35[26] = C1; Array35[27] = C0;
        Array35[28] = C0; Array35[29] = C0; Array35[30] = C0; Array35[31] = C0; Array35[32] = C0; Array35[33] = C0; Array35[34] = C0;

        return Array35;
    }
    public Color[] LetterX(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterY(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterZ(Color C1, Color C0)
    {
        Color[] Array35 = new Color[35];
        Array35[0] = C0; Array35[1] = C0; Array35[2] = C0; Array35[3] = C0; Array35[4] = C0;
        Array35[5] = C1; Array35[6] = C1; Array35[7] = C1; Array35[8] = C1; Array35[9] = C0;
        Array35[10] = C0; Array35[11] = C0; Array35[12] = C1; Array35[13] = C0; Array35[14] = C0;
        Array35[15] = C0; Array35[16] = C1; Array35[17] = C0; Array35[18] = C0; Array35[19] = C0;
        Array35[20] = C1; Array35[21] = C0; Array35[22] = C0; Array35[23] = C0; Array35[24] = C0;
        Array35[25] = C1; Array35[26] = C1; Array35[27] = C1; Array35[28] = C1; Array35[29] = C0;
        Array35[30] = C0; Array35[31] = C0; Array35[32] = C0; Array35[33] = C0; Array35[34] = C0;

        return Array35;
    }
    /*************************************************************************************CAPITAL LETTERS*/
    public Color[] LetterCapA(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C0; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapB(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapC(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapD(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25];
        Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapE(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; 
        Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapF(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapG(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapH(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C0; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C0; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapI(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapJ(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C0; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C0; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapK(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C0; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C0; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapL(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapM(Color C1, Color C0)
    {
        Color[] Array35 = new Color[35];
        Array35[0] = C0; Array35[1] = C1; Array35[2] = C0; Array35[3] = C0; Array35[4] = C0; Array35[5] = C1; Array35[6] = C0;
        Array35[7] = C0; Array35[8] = C1; Array35[9] = C1; Array35[10] = C0; Array35[11] = C1; Array35[12] = C1; Array35[13] = C0;
        Array35[14] = C0; Array35[15] = C1; Array35[16] = C0; Array35[17] = C1; Array35[18] = C0; Array35[19] = C1; Array35[20] = C0;
        Array35[21] = C0; Array35[22] = C1; Array35[23] = C0; Array35[24] = C0; Array35[25] = C0; Array35[26] = C1; Array35[27] = C0;
        Array35[28] = C0; Array35[29] = C1; Array35[30] = C0; Array35[31] = C0; Array35[32] = C0; Array35[33] = C1; Array35[34] = C0;

        return Array35;
    }
    public Color[] LetterCapN(Color C1, Color C0)
    {
        Color[] Array35 = new Color[35];
        Array35[0] = C0; Array35[1] = C1; Array35[2] = C0; Array35[3] = C0; Array35[4] = C0; Array35[5] = C1; Array35[6] = C0;
        Array35[7] = C0; Array35[8] = C1; Array35[9] = C1; Array35[10] = C0; Array35[11] = C0; Array35[12] = C1; Array35[13] = C0;
        Array35[14] = C0; Array35[15] = C1; Array35[16] = C0; Array35[17] = C1; Array35[18] = C0; Array35[19] = C1; Array35[20] = C0;
        Array35[21] = C0; Array35[22] = C1; Array35[23] = C0; Array35[24] = C0; Array35[25] = C1; Array35[26] = C1; Array35[27] = C0;
        Array35[28] = C0; Array35[29] = C1; Array35[30] = C0; Array35[31] = C0; Array35[32] = C0; Array35[33] = C1; Array35[34] = C0;

        return Array35;
    }
    public Color[] LetterCapO(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapP(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapQ(Color C1, Color C0)
    {
        Color[] Array35 = new Color[35];
        Array35[0] = C0; Array35[1] = C1; Array35[2] = C1; Array35[3] = C1; Array35[4] = C1; Array35[5] = C0; Array35[6] = C0;
        Array35[7] = C0; Array35[8] = C1; Array35[9] = C0; Array35[10] = C0; Array35[11] = C1; Array35[12] = C0; Array35[13] = C0;
        Array35[14] = C0; Array35[15] = C1; Array35[16] = C0; Array35[17] = C0; Array35[18] = C1; Array35[19] = C0; Array35[20] = C0;
        Array35[21] = C0; Array35[22] = C1; Array35[23] = C0; Array35[24] = C1; Array35[25] = C1; Array35[26] = C1; Array35[27] = C0;
        Array35[28] = C0; Array35[29] = C1; Array35[30] = C1; Array35[31] = C1; Array35[32] = C1; Array35[33] = C1; Array35[34] = C0;

        return Array35;
    }
    public Color[] LetterCapR(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C0; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapS(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapT(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C1; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapU(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C0; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapV(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C0; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C1; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapW(Color C1, Color C0)
    {
        Color[] Array35 = new Color[35];
        Array35[0] = C0; Array35[1] = C1; Array35[2] = C0; Array35[3] = C0; Array35[4] = C0; Array35[5] = C1; Array35[6] = C0;
        Array35[7] = C0; Array35[8] = C1; Array35[9] = C0; Array35[10] = C0; Array35[11] = C0; Array35[12] = C1; Array35[13] = C0;
        Array35[14] = C0; Array35[15] = C1; Array35[16] = C0; Array35[17] = C1; Array35[18] = C0; Array35[19] = C1; Array35[20] = C0;
        Array35[21] = C0; Array35[22] = C1; Array35[23] = C1; Array35[24] = C0; Array35[25] = C1; Array35[26] = C1; Array35[27] = C0;
        Array35[28] = C0; Array35[29] = C1; Array35[30] = C0; Array35[31] = C0; Array35[32] = C0; Array35[33] = C1; Array35[34] = C0;

        return Array35;
    }
    public Color[] LetterCapX(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C0; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C0; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapY(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C0; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C1; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] LetterCapZ(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    /*********************************************************************************************NUMBERS*/
    public Color[] Letter0(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C0; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] Letter1(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] Letter2(Color C1, Color C0)
    {

        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] Letter3(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] Letter4(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C1; Array25[1] = C0; Array25[2] = C0; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C1; Array25[6] = C0; Array25[7] = C1; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C1; Array25[11] = C1; Array25[12] = C1; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C1; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C0; Array25[22] = C1; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] Letter5(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] Letter6(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C0; Array25[2] = C1; Array25[3] = C0; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C0; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] Letter7(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C1; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C1;
        Array25[5] = C0; Array25[6] = C0; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C0; Array25[12] = C1; Array25[13] = C0; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C0; Array25[19] = C0;
        Array25[20] = C1; Array25[21] = C0; Array25[22] = C0; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
    public Color[] Letter8(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C1; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C1; Array25[24] = C0;

        return Array25;
    }
    public Color[] Letter9(Color C1, Color C0)
    {
        Color[] Array25 = new Color[25]; Array25[0] = C0; Array25[1] = C1; Array25[2] = C1; Array25[3] = C1; Array25[4] = C0;
        Array25[5] = C0; Array25[6] = C1; Array25[7] = C0; Array25[8] = C1; Array25[9] = C0;
        Array25[10] = C0; Array25[11] = C1; Array25[12] = C1; Array25[13] = C1; Array25[14] = C0;
        Array25[15] = C0; Array25[16] = C0; Array25[17] = C0; Array25[18] = C1; Array25[19] = C0;
        Array25[20] = C0; Array25[21] = C1; Array25[22] = C1; Array25[23] = C0; Array25[24] = C0;

        return Array25;
    }
}
