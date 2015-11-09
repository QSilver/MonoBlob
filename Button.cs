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
    class Button
    {
        float x;
        float y;
        float size = 30;
        String name;
        Color color;

        public Button(float x, float y, String s, Color c)
        {
            this.x = x;
            this.y = y;
            this.name = s;
            this.color = c;
        }

        public void setColor(Color c)
        {
            this.color = c;
        }

        public Rectangle getRect()
        {
            return new Rectangle((int)x, (int)y, (int)size * 2, (int)size);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(new Vector2(this.x, this.y), new Vector2(this.size*2, this.size), this.color);
            //spriteBatch.DrawString();
        }
    }
}
