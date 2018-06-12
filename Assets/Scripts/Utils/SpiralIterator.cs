using System;
using System.Collections.Generic;
using UnityEngine;

class SpiralIterator{
    
    int X, Y;
    
    List<Vector2> list;
    int i;

    public delegate void Func(int i, int x, int y);

    public SpiralIterator(int X, int Y) {

        list = new List<Vector2>();

        for(int i = 0; i < X; i++) {
            for(int j = 0; j < Y; j++) {
                list.Add(new Vector2(i, j));
            }    
        }

        Vector2 center = new Vector2(X / 2, Y / 2);

        list.Sort((Vector2 a, Vector2 b) => {
            float f = Vector2.Distance(a, center) - Vector2.Distance(b, center); 
            return (int)f;
        });

        this.X = X;
        this.Y = Y;
        
    }

    public bool Iterate(Func f){

        f(i, (int)list[i].x, (int)list[i].y);

        i++;
        return i < X * Y;

    }

}