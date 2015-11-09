using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace MonoBlob
{
    class Blob
    {
        static int ID = 0;
        int myid = 0;
        float x;
        float y;
        float delay = BlobManager.delay;
        float timeStep = 0f;

        public Blob(float x, float y, ContentManager content)
        {
            this.myid = ID;
            ID++;
            this.x = x;
            this.y = y;
        }

        public void move()
        {
            int dir = Game1.rnd.Next(0, 4);
            switch (dir)
            {
                case 0: x += 10; break;
                case 1: x -= 10; break;
                case 2: y += 10; break;
                case 3: y -= 10; break;
            }
        }

        public float getX()
        {
            return this.x;
        }
        public float getY()
        {
            return this.y;
        }
        public int getID()
        {
            return this.myid;
        }

        public void Update(GameTime gameTime)
        {
            delay = BlobManager.delay;
            timeStep += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (timeStep > delay)
            {
                this.move();
                timeStep = 0f;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(new Vector2(this.x - 5, this.y - 5), new Vector2(10, 10), Color.Red);
        }
    }
}
