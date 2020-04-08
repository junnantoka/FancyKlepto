using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FancyKlepto.GameObjects;
using FancyKlepto.GameObjects.MapObjects;

namespace FancyKlepto.GameStates
{
    class PlayingState : GameObjectList
    {
        Player thePlayer;
        MainGoal goal;
        Door door;
        xAxis xaxis;
        yAxis yaxis;
        SwitchBoard switchBoard1;
        SwitchBoard switchBoard2;

        GameObjectList floors;
        GameObjectList vensters;
        GameObjectList walls;
        GameObjectList goals;
        GameObjectList guards;
        GameObjectList lasers;


        public PlayingState()
        {
            this.Add(new SpriteGameObject("spr_background"));
            thePlayer = new Player(3, 13);
            switchBoard1 = new SwitchBoard(14, 10, Color.Red);
            switchBoard2 = new SwitchBoard(6, 12, Color.Yellow);
            door = new Door(2, 15);

            Mouse.SetPosition(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);

            floors = new GameObjectList();
            walls = new GameObjectList();
            vensters = new GameObjectList();
            goals = new GameObjectList();

            xaxis = new xAxis(8);
            yaxis = new yAxis(10);
            goal = new MainGoal(19, 10);
            guards = new GameObjectList();
            lasers = new GameObjectList();

            this.Add(floors);
            this.Add(walls);
            this.Add(switchBoard1);
            this.Add(switchBoard2);
            this.Add(door);
            this.Add(lasers);
            this.Add(xaxis);
            this.Add(yaxis);
            this.Add(goal);
            this.Add(goals);
            this.Add(guards);
            this.Add(vensters);
            this.Add(thePlayer);

            goals.Add(new ExtraGoal(3, 3));
            guards.Add(new Guard(new Vector2(13, 2), new Vector2(25, 7)));
            lasers.Add(new Laser(new Vector2(1, 6), new Vector2(6, 5), Color.Red));
            //lasers.Add(new Laser(new Vector2(23, 7), new Vector2(28, 12), Color.Yellow));

            FloorSetup();
            WallSetup();
            VensterSetup();


        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.KeyPressed(Keys.R))
            {
                foreach (GameObject gameObject in Children)
                {
                    gameObject.Reset();
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (goal.CollidesWith(thePlayer))
            {
                goal.hold = true;
                if (inputHelper.IsKeyDown(Keys.Space))
                {
                    goal.Hold(thePlayer);
                    door.Visible = true;
                }
            }
            else
            {
                goal.hold = false;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (ExtraGoal extraGoal in goals.Children)
            {
                if (extraGoal.CollidesWith(thePlayer))
                {
                    extraGoal.hold = true;
                    if (inputHelper.IsKeyDown(Keys.Space))
                    {
                        extraGoal.Hold(thePlayer);
                    }
                }
                else
                {
                    extraGoal.hold = false;
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (thePlayer.CollidesWith(switchBoard1) || thePlayer.CollidesWith(switchBoard2))
            {
                foreach (Venster_Object venster in vensters.Children)
                {
                    if (inputHelper.KeyPressed(Keys.Space))
                    {
                        venster.open = true;
                    }
                }
            }
            else if (!thePlayer.CollidesWith(switchBoard1) && !thePlayer.CollidesWith(switchBoard2))
            {
                foreach (Venster_Object venster in vensters.Children)
                {
                    venster.open = false;
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (Wall wall in walls.Children)
            {
                if (door.Visible)
                {
                    if (wall.CollidesWith(door))
                    {
                        wall.Die = true;
                    }
                }
                if (switchBoard1.CollidesWith(wall) || switchBoard2.CollidesWith(wall))
                {
                    wall.Die = true;
                }

                if (thePlayer.xaxisCol(wall))
                {
                    if (thePlayer.Intersection(wall).Y > 0)
                        thePlayer.yCol(wall);
                }
                if (thePlayer.yaxisCol(wall))
                {
                    if (thePlayer.Intersection(wall).X > 0)
                        thePlayer.xCol(wall);
                }
                foreach (Guard guard in guards.Children)
                {
                    if (guard.xaxisCol(wall))
                    {
                        if (guard.Intersection(wall).Y > 0)
                            guard.yCol(wall);
                    }
                    if (guard.yaxisCol(wall))
                    {
                        if (guard.Intersection(wall).X > 0)
                            guard.xCol(wall);
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (Laser laser in lasers.Children)
            {
                if (Collision.LineRect(laser.Position, laser.position2, thePlayer.BoundingBox))
                {
                    thePlayer.Reset();
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (Guard guard in guards.Children)
            {
                if (guard.CollidesWith(thePlayer))
                {
                    thePlayer.Reset();
                }
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (door.Visible)
            {
                if (goal.hold && thePlayer.CollidesWith(door))
                {
                    GameEnvironment.GameStateManager.SwitchTo("EndStateWon");
                    Reset();
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        public void FloorSetup()
        {
            for (int i = 0; i < 29; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    floors.Add(new Floor(i, j));
                }
            }
        }
        public void WallSetup()
        {
            for (int j = 0; j < 16; j++)
            {
                walls.Add(new Wall(0, j));
            }
            for (int j = 0; j < 16; j++)
            {
                walls.Add(new Wall(28, j));
            }
            for (int j = 0; j < 6; j++)
            {
                walls.Add(new Wall(6, j));
                walls.Add(new Wall(7, j));
                walls.Add(new Wall(8, j));
            }
            for (int j = 11; j < 16; j++)
            {
                walls.Add(new Wall(6, j));
                walls.Add(new Wall(7, j));
                walls.Add(new Wall(8, j));
            }
            for (int j = 11; j < 16; j++)
            {
                walls.Add(new Wall(14, j));
            }
            for (int j = 5; j < 9; j++)
            {
                walls.Add(new Wall(14, j));
            }
            for (int j = 5; j < 16; j++)
            {
                walls.Add(new Wall(15, j));
                walls.Add(new Wall(16, j));
            }

            for (int i = 0; i < 29; i++)
            {
                walls.Add(new Wall(i, 0));
                walls.Add(new Wall(i, 15));
            }
            for (int i = 17; i < 23; i++)
            {
                walls.Add(new Wall(i, 5));
                walls.Add(new Wall(i, 6));
                walls.Add(new Wall(i, 7));
            }
        }

        public void VensterSetup()
        {
            /*
            vensters.Add(new Venster_Object(0, 0, "spr_point_bar"));
            vensters.Add(new Venster_Object(5, 1067, "spr_point_bar_point"));
            vensters.Add(new Venster_Object(5, 1067 - 8 - 2 * unitSpacing, "spr_point_bar_point"));
            vensters.Add(new Venster_Object(32, 0, "spr_venster_background"));
            vensters.Add(new Venster_Object(64, 34, "spr_nickname"));
            vensters.Add(new Venster_Object(64, 132, "spr_lineair_visualiseren"));
            vensters.Add(new Venster_Object(64, 420, "spr_input"));
            vensters.Add(new Venster_Object(64, 516, "spr_xyz"));
            vensters.Add(new Venster_Object(64, 730, "spr_collected_items"));
            vensters.Add(new Venster_Object(64, 917, "spr_lives"));
            vensters.Add(new Venster_Object(64, 990, "spr_score"));
            */
            vensters.Add(new Venster_Object(0, 0, "spr_venster_352"));
        }
    }
}