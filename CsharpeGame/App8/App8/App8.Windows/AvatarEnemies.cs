using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace App8
{
    class AvatarEnemies : Avatar
    {
        public int moveX;
        public int moveY;
        public static int xMax = 1366;
        public static int yMax = 768;


        public AvatarEnemies(int x, int y, double size,int moveX, int moveY) : base(x, y, size)
        {
            this.moveX = moveX;
            this.moveY = moveY;
                    
        }

        public override Ellipse CreateEllipseFromEnemies(Player play)
        {
            Ellipse ell = new Ellipse();
            ell.Height = ell.Width = size;

            Canvas.SetLeft(ell, x);
            Canvas.SetTop(ell, y);

            ell.Fill = AffectImage(play);

            return ell;

        }

        public override void Move()
        {
            x = Math.Max(Math.Min(x + IncrementationX(), XMax - (int)size), 0);
            y = Math.Max(Math.Min(y + IncrementationY(), YMax - (int)size), 0);
            //x = Math.Max(Math.Min(x + FollowX(play), XMax - 5 - (int)size), 5);
            //y = Math.Max(Math.Min(y + FollowY(play), YMax - 5 - (int)size), 5);

        }

        public virtual int IncrementationX()
        {
            if (x <= 0)
            {
                moveX = -moveX;
            }
            if ((x + size) >= xMax)
            {
                moveX = -moveX;
            }
            return moveX;
        }

        public virtual int IncrementationY()
        {           
            if (y <= 0 || (y + size) >= yMax)
            {
                moveY = -moveY;
            }
            return moveY;
        }
    }
}
