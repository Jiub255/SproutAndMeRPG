using System;
using Godot;

namespace SproutandMeRPG.misc;

public static class Extensions
{
    public static bool XinYOdds(this Random rng, int x, int y)
    {
        if (x <= 0 || y <= 0)
        {
            GD.PushError("Numerator and Denominator must be positive.");
        }

        int random = rng.Next(y);
        return random < x;
    }

    public static float RandomFRange(this Random rng, float min, float max)
    {
        return rng.NextSingle() * (max - min) + min;
    }
}