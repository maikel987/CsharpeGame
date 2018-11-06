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
    class Avatar
    {
        public int x; //protected
        public int y;
        public double size;
        static int xMax = 1366;
        static int yMax = 768;
        

        public Avatar(int x,int y,double size)
        {
            this.x=x;
            this.y=y;
            this.size = size;

        }

        public int XMax
        {
            get { return xMax; }
        }

        public int YMax
        {
            get { return yMax; }
        }

        public virtual Ellipse CreateEllipseFromAvatar()
        {
            Ellipse ell = new Ellipse();
            ell.Height = ell.Width = size;

            Canvas.SetLeft(ell,x);
            Canvas.SetTop(ell,y);

            

            return ell;

        }

        public virtual Ellipse CreateEllipseFromEnemies(Player play)
        {
            Ellipse ell = new Ellipse();
            ell.Height = ell.Width = size;

            Canvas.SetLeft(ell, x);
            Canvas.SetTop(ell, y);
            
            ell.Fill = AffectImage(play);
            
            return ell;

        }

        public bool IsTouch( Avatar two)
        {
            return TrigoTouch(two, 3) || TrigoTouch(two, 4) || TrigoTouch(two, 6);  
        }

        public bool TrigoTouch( Avatar two, int trig)//3.4.6
        {
            double facteur = 1.01;
            double devX = (1 - Math.Cos(Math.PI / trig)) / 2*facteur;   
            double oneDevX = (1 + Math.Cos(Math.PI / trig)) / 2 * facteur;
            double devY = (1 - Math.Sin(Math.PI / trig)) / 2 * facteur;
            double oneDevY = (1 + Math.Sin(Math.PI / trig)) / 2 * facteur;
            bool contact = ((x + size * devX < (two.x) && (two.x) < (x + size * oneDevX)) && (y + size * devY < two.y && two.y < (y + size * oneDevY))
           || (x + size * devX < (two.x + two.size * oneDevX) && (two.x + two.size * oneDevX) < (x + size * oneDevX)) && (y + size * devY < two.y && two.y < (y + size * oneDevY))
           || (x + size * devX < (two.x) && (two.x) < (x + size * oneDevX)) && (y + size * devY < two.y + two.size * oneDevY && two.y + two.size * oneDevY < (y + size * oneDevY))
           || (x + size * devX < (two.x + two.size * oneDevX) && (two.x + two.size * oneDevX) < (x + size * oneDevX)) && (y + size * devY < two.y + two.size * oneDevY && two.y + two.size * oneDevY < (y + size * oneDevY)));

            return contact;
            
        }

        public virtual void Move()
        {

        }

        public virtual void MovePill(Avatar play)
        {

        }
        
        public virtual ImageBrush AffectImage(Avatar player)
        {
            ImageBrush playerImg = new ImageBrush();
            if (size < player.size)
            {
                playerImg.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Goodbacterium.jpg"));

            }
            else if (size > player.size)
            {
                playerImg.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/badBacterium.jpg"));
            }
            else
            {
                playerImg.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/EqualBacterium.jpg"));
            }
            return playerImg;

        }


    }
}
