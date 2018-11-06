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
    class Item
    {
        private List<Avatar> units = new List<Avatar>();
        public List<Ellipse> ellList = new List<Ellipse>();

        public double GetSize(int index)
        {
            return units[index].size;
        }

        public int GetX(int index)
        {
            return units[index].x;
        }

        public int GetY(int index)
        {
            return units[index].y;
        }

        public void SetX(int _x,int index)
        {
            units[index].x = _x;
        }

        public void SetY(int _y, int index)
        {
            units[index].y = _y;
        }

        public bool IsPlayer(int index)
        {
            return units[index] is Player;
        }

        public bool IsPills(int index)
        {
            return units[index] is Pills;
        }

        public int count
        {
            get { return units.Count; }

        }

        public void AddAvatar(Avatar av)
        {
            units.Add(av);
        }

        public void RemoveDeadsAvatar()
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].size == 0) units.RemoveAt(i);
            }
        }
       
        public void SizeChangetAfterTouch()
        {
            for (int i = 1; i < units.Count; i++)
            {
                for (int j = i-1; j >= 0; j--)   

                {
                    if (units[Math.Min(i, units.Count - 1)].IsTouch(units[j])|| units[j].IsTouch(units[Math.Min(i,units.Count-1)])) // A MODIFIER
                    {
                        if (units[i].size > units[j].size)
                        {
                            SizeAndPositionTraitment(i, j);                                                   
                        }
                        else
                        {
                            SizeAndPositionTraitment(j, i);                                                  
                        }

                        //Modify the loops
                        i--; j--;

                        // Modify the Ellipse image
                        ImageChangeAfterTouch();
                    }
                }
            }
        }

        private void SizeAndPositionTraitment(int i,int j)
        {
            //Size Traitment
            units[i].size += (units[j].size / 2);

            //Position Traitment
            units[i].x = (units[i].x * (int)units[i].size + units[j].x * (int)units[j].size) / ((int)units[i].size + (int)units[j].size);
            units[i].y = (units[i].y * (int)units[i].size + units[j].y * (int)units[j].size) / ((int)units[i].size + (int)units[j].size);

            units[i].x = (int)Math.Min(units[i].x, units[i].XMax - units[i].size);
            units[i].y = (int)Math.Min(units[i].y, units[i].YMax - units[i].size);

            //Ellipse Size Traitment 
            ellList[i].Height = ellList[i].Width = units[i].size;

            // Remove Smaller Item
            units[j].size = 0;
            ellList[j].Height = ellList[j].Width = 0;
            units.RemoveAt(j);
            ellList.RemoveAt(j);

        }

        public void ImageChangeAfterTouch()
        {
            for (int i = 1; i < units.Count; i++)
            {
                ellList[i].Fill = units[i].AffectImage(units[0]);
            }
        }

        public void Move()
        {
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i] is Pills)
                {
                    units[i].MovePill(units[0]);

                }else if (units[i] is AvatarEnemies)
                {
                    units[i].Move();
                }
            }
        }

        public Ellipse ReShow(int i,Player avat)
        {
            return units[i].CreateEllipseFromEnemies(avat);           
        }

        public void AffectEllipse(Player play)
        {
            ellList.Add(play.CreateEllipseFromAvatar());
            for (int i = 1; i < units.Count; i++)
            {
                ellList.Add(units[i].CreateEllipseFromEnemies(play));
            }
        }

        public void CleanBeforeStart()
        {
            units.RemoveRange(0, units.Count);
            ellList.RemoveRange(0, ellList.Count);
        }

    }
}
