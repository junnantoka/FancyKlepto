﻿
using System;
using System.Collections.Generic;
using FancyKlepto.GameObjects;
using FancyKlepto.GameObjects.MapObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace FancyKlepto.GameObjects
{
    class Guard : RotatingSpriteGameObject
    {
        List<SoundEffect> walkEffects;

        public Vector2 destination;
        public Vector2 posA, posB;
        public bool Moves;

        public Guard(Vector2 positionA, Vector2 positionB) : base("agent")
        {
            walkEffects = new List<SoundEffect>();

            walkEffects.Add(GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/npc_step1"));
            walkEffects.Add(GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/npc_step2"));
            walkEffects.Add(GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/npc_step3"));
            walkEffects.Add(GameEnvironment.AssetManager.Content.Load<SoundEffect>("Sound/npc_step4"));

            position = new Vector2(18 + positionA.X * (unitSize + unitSpacing), 10 + positionA.Y * (unitSize + unitSpacing));
            positionA = position;
            positionB = new Vector2(18 + positionB.X * (unitSize + unitSpacing), 10 + positionB.Y * (unitSize + unitSpacing));

            posA = positionA;
            posB = positionB;

            Reset();
        }
        public override void Reset()
        {
            position = posA;
            this.destination = posB;
            velocity = new Vector2(3, 3);
            Moves = true;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Rotate();
            if (Moves)
            {
                Move();
            }
        }
        public void Rotate()
        {
            if (Math.Abs(destination.X - position.X) < sprite.Width && Math.Abs(destination.Y - position.Y) < sprite.Height)
            {
                DestChange();
            }
        }

        public void DestChange()
        {
            //changes the destination variable to the destination which have been earlier determined when the guard have been created
            if (destination == posB)
            {
                destination = posA;
            }
            else if (destination == posA)
            {
                destination = posB;
            }
        }
        public void Move()
        {
            ///////////////////////////// Changes the moving destination
            if (Math.Abs(destination.X - position.X) < velocity.X)
            {
                position.X = destination.X;
            }
            if (Math.Abs(destination.Y - position.Y) < velocity.Y)
            {
                position.Y = destination.Y;
            }
            ////////////////////////////// to the destination X
            if (position.X < destination.X)
            {
                position.X += velocity.X;
            }
            else if (position.X > destination.X)
            {
                position.X -= velocity.X;
            }
            ////////////////////////////// to the destination Y
            if (position.Y < destination.Y)
            {
                position.Y += velocity.Y;
            }
            else if (position.X > destination.Y)
            {
                position.Y -= velocity.Y;
            }
        }
    }
}