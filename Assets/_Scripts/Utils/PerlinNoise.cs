using System;
using UnityEngine;

public class PerlinNoise
{
    private static readonly int[] permutation = { 151,160,137,91,90,15,
    131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,142,8,99,37,240,21,10,23,
    190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,35,11,32,57,177,33,
    88,237,149,56,87,174,20,125,136,171,168, 68,175,74,165,71,134,139,48,27,166,
    77,146,158,231,83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,40,244,
    102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,187,208, 89,18,169,200,196,
    135,130,116,188,159,86,164,100,109,198,173,186, 3,64,52,217,226,250,124,123,
    5,202,38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,17,182,189,28,42,
    223,183,170,213,119,248,152, 2,44,154,163, 70,221,153,101,155,167, 43,172,9,
    129,22,39,253, 19,98,108,110,79,113,224,232,178,185, 112,104,218,246,97,228,
    251,34,242,193,238,210,144,12,191,179,162,241, 81,51,145,235,249,14,239,107,
    49,192,214, 31,181,199,106,157,184, 84,204,176,115,121,50,45,127, 4,150,254,
    138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,156,180
    };

    private static readonly int[] p;

    static PerlinNoise()
    {
        p = new int[512];
        for (int x = 0; x < 512; x++)
        {
            p[x] = permutation[x % 256];
        }
    }

    public static double Noise(Vector2 pos, int detail, float scale)
    {
        pos *= scale;

        int xInt = (int)pos.x;
        int yInt = (int)pos.y;
        float xFrac = pos.x - xInt;
        float yFrac = pos.y - yInt;

        int s = 0;
        float noise = 0;

        for (int i = 0; i <= detail; i++)
        {
            int xi = (xInt >> i) & 1;
            int yi = (yInt >> i) & 1;
            int aa = p[p[xi] + yi];
            int ab = p[p[xi] + yi + 1];
            int ba = p[p[xi + 1] + yi];
            int bb = p[p[xi + 1] + yi + 1];

            float x1 = Interpolate.Lerp(Grad(aa, xFrac, yFrac),
                  Grad(ba, xFrac - 1, yFrac),
                  xFrac);
            float x2 = Interpolate.Lerp(Grad(ab, xFrac, yFrac - 1),
                  Grad(bb, xFrac - 1, yFrac - 1),
                  xFrac);
            float y = Interpolate.Lerp(x1, x2, yFrac);
            noise += y * Mathf.Pow(2, -i);
            s += (int)Math.Pow(2, -i);
        }

        return noise / s;
    }

    private static float Grad(int hash, float x, float y)
    {
        switch (hash & 0x3)
        {
            case 0: return x + y;
            case 1: return -x + y;
            case 2: return x - y;
            case 3: return -x - y;
            default: return 0;
        }
    }
}

public static class Interpolate
{
    public static float Lerp(float a, float b, float t)
    {
        return a + (b - a) * t;
    }
}