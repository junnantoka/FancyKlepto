﻿using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
public class Collision
{
    public static Vector2 CalculateIntersectionDepth(Rectangle rectA, Rectangle rectB)
    {
        Vector2 minDistance = new Vector2(rectA.Width + rectB.Width,
            rectA.Height + rectB.Height) / 2;
        Vector2 centerA = new Vector2(rectA.Center.X, rectA.Center.Y);
        Vector2 centerB = new Vector2(rectB.Center.X, rectB.Center.Y);
        Vector2 distance = centerA - centerB;
        Vector2 depth = Vector2.Zero;
        if (distance.X > 0)
        {
            depth.X = minDistance.X - distance.X;
        }
        else
        {
            depth.X = -minDistance.X - distance.X;
        }
        if (distance.Y > 0)
        {
            depth.Y = minDistance.Y - distance.Y;
        }
        else
        {
            depth.Y = -minDistance.Y - distance.Y;
        }
        return depth;
    }

    public static Rectangle Intersection(Rectangle rect1, Rectangle rect2)
    {
        int xmin = (int)MathHelper.Max(rect1.Left, rect2.Left);
        int xmax = (int)MathHelper.Min(rect1.Right, rect2.Right);
        int ymin = (int)MathHelper.Max(rect1.Top, rect2.Top);
        int ymax = (int)MathHelper.Min(rect1.Bottom, rect2.Bottom);
        return new Rectangle(xmin, ymin, xmax - xmin, ymax - ymin);
    }
    public static bool lineLine(Vector2 Line1pos1, Vector2 Line1pos2, Vector2 Line2pos1, Vector2 Line2pos2)
    {
        // calculate the direction of the lines
        float uA = ((Line2pos2.X - Line2pos1.X) * (Line1pos1.Y - Line2pos1.Y) - (Line2pos2.Y - Line2pos1.Y) * (Line1pos1.X - Line2pos1.X)) / ((Line2pos2.Y - Line2pos1.Y) * (Line1pos2.X - Line1pos1.X) - (Line2pos2.X - Line2pos1.X) * (Line1pos2.Y - Line1pos1.Y));
        float uB = ((Line1pos2.X - Line1pos1.X) * (Line1pos1.Y - Line2pos1.Y) - (Line1pos2.Y - Line1pos1.Y) * (Line1pos1.X - Line2pos1.X)) / ((Line2pos2.Y - Line2pos1.Y) * (Line1pos2.X - Line1pos1.X) - (Line2pos2.X - Line2pos1.X) * (Line1pos2.Y - Line1pos1.Y));

        // if uA and uB are between 0-1, lines are colliding
        if (uA >= 0 && uA <= 1 && uB >= 0 && uB <= 1)
        {

            // optionally, draw a circle where the lines meet
            float intersectionX = Line1pos1.X + (uA * (Line1pos2.X - Line1pos1.X));
            float intersectionY = Line1pos1.Y + (uA * (Line1pos2.Y - Line1pos1.Y));

            return true;
        }
        return false;
    }

    public static bool LineRect(Vector2 Linepos1, Vector2 Linepos2,Rectangle obj)
    {
        Vector2 topLeft, botLeft, topRight, botRight;
        topLeft.X = obj.Left;
        topLeft.Y = obj.Top;
        botLeft.X = obj.Left;
        botLeft.Y = obj.Bottom;
        topRight.X = obj.Right;
        topRight.Y = obj.Top;
        botRight.X = obj.Right;
        botRight.Y = obj.Bottom;


        bool Left = lineLine(Linepos1, Linepos2, topLeft , botLeft);
        bool Right = lineLine(Linepos1, Linepos2, topRight , botRight);
        bool Top = lineLine(Linepos1, Linepos2, topLeft , topRight);
        bool Bottom = lineLine(Linepos1, Linepos2, botLeft , botRight);
        if( Left || Right || Top || Bottom)
        {
            return true;
        }
        return false;
    }
    public static bool PixelCollision(Texture2D sprite1, Texture2D sprite2, Rectangle player, Rectangle enemy)
    {
        Color[] colorData1 = new Color[sprite1.Width * sprite1.Height];
        Color[] colorData2 = new Color[sprite2.Width * sprite1.Height];
        sprite1.GetData<Color>(colorData1);
        sprite2.GetData<Color>(colorData2);

        int top, bottom, left, right;

        top = Math.Max(player.Top, enemy.Top);
        bottom = Math.Min(player.Bottom, enemy.Bottom);
        left = Math.Max(player.Left, enemy.Left);
        right = Math.Min(player.Right, enemy.Right);

        for (int y = top; y < bottom; y++)
        {
            for (int x = left; x < right; x++)
            {
                Color A = colorData1[(y - player.Top) * (player.Width) + (x - player.Left)];
                Color B = colorData2[(y - enemy.Top) * (enemy.Width) + (x - enemy.Left)];

                if (A.A != 0 && B.A != 0) return true;
            }
        }
        return false;
    }
    public static bool LineCircle(Vector2 position1,Vector2 position2,Vector2 positionCircle, float circleRadius)
    {
        return false;
    }
}

