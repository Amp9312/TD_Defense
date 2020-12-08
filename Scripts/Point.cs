using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Last Edited 12/7/2020
 * Edited by Amp
 * 
 * Note - uppercase are properties while lowercase are fields
 */

public struct Point
{
// Value based struct (boneless class)
    public int X { get; set; }

    public int Y { get; set; }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }



}
