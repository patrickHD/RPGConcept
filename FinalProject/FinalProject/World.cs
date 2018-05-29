using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FinalProject
{
    //Class for world objects and variables (just music for now)
    public class World
    {
        private string AudTitlePath { get; set; }
        private string audWorldPath   {get; set;}
        private string AudBattlePath  {get; set;}
        private string AudFanfarePath { get; set; }
        public Uri AudtitleUri { get; set; }
        public Uri AudWorldUri { get; set; }
        public Uri AudBattleUri { get; set; }
        public Uri AudFanfareUri { get; set; }
        public string ImgMapPath { get; private set; }
        private string ImgEnemiesPath { get; set; }
        private string GifFilter { get; set; }
        public string[] ImgEnemies { get; set; }

        public List<Control> NumList { get; set; }
        public Grid BattleGrid { get; set; }
        public Image MapImg { get; set; }

        public int CurrentX { get; set; }
        public int CurrentY { get; set; }

        public World(ref List<Control> numList, ref Grid battleGrid, ref Image worldimg)
        {
            AudTitlePath  = @"res\audio\title.mp3";
            audWorldPath  = @"res\audio\world.mp3";
            AudBattlePath = @"res\audio\battle.mp3";
            AudFanfarePath = @"res\audio\fanfare.mp3";

            AudtitleUri = new Uri(AudTitlePath, UriKind.Relative);
            AudWorldUri = new Uri(audWorldPath, UriKind.Relative);
            AudBattleUri = new Uri(AudBattlePath, UriKind.Relative);
            AudFanfareUri = new Uri(AudFanfarePath, UriKind.Relative);

            ImgMapPath = @"res\img\map\";
            ImgEnemiesPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) 
                + @"\res\img\enemy";
            GifFilter = "*.gif";
            ImgEnemies = Directory.GetFiles(ImgEnemiesPath, GifFilter);
            this.NumList = numList; this.BattleGrid = battleGrid;
            MapImg = worldimg;
            CurrentX = 0; CurrentY = 0;
        }

        public enum Numoptions
        {
            normal, crit, miss
        }
        public enum ControlTag
        {
            player, enemy, ui, damagenum
        }
        public void ChangeMap(int x, int y, bool inc)
        {
            string imgString = null;
            if (inc)
            {
                imgString = (CurrentX + x) + "," + (CurrentY + y) + ".png";
            } else
            {
                imgString = x + "," + y + ".png";
            }
            if (x > 0) { CurrentX++; } else if(x<0) { CurrentX--; }
            if (y > 0) { CurrentY++; } else if(y<0) { CurrentY--; }
            imgString = ImgMapPath + imgString;
            MapImg.Source = new BitmapImage(new Uri(imgString, UriKind.Relative));
        }
        public void SpawnNum(Numoptions op, int dmg)
        {
            Label lbl = new Label
            {
                Content = dmg,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(583, 171, 0, 0),
                FontSize = 28,
                Tag = ControlTag.damagenum
            };
            if (op == Numoptions.normal)
            {
                lbl.Foreground = Brushes.LightGoldenrodYellow;
            }
            if (op == Numoptions.crit)
            {
                lbl.Foreground = Brushes.LightPink;
            }
            if (op == Numoptions.miss)
            {
                lbl.Content = "MISS";
                lbl.Foreground = Brushes.White;
            }
            NumList.Add(lbl);
            BattleGrid.Children.Add(lbl);
        }
        public void RemoveNum(Control control, bool removeAll)
        {
            Control c = null;
            Object x = null;
            bool f1 = false, f2 = false;
            List<int> rmvList = new List<int>();
            if (removeAll == false)
            {
                NumList.Remove(control);
                BattleGrid.Children.Remove(control);
            }
            else
            {
                //foreach(object x in battleGrid.Children)
                for(int i = 0; i < BattleGrid.Children.Count; i++)
                {
                    if(BattleGrid.Children[i] is Control)
                    {
                        f1 = true;
                        c = (Control)BattleGrid.Children[i];
                    }
                    if(c != null && c.Tag is ControlTag && (ControlTag)c.Tag == ControlTag.damagenum)
                    {
                        //rmvList.Add(i);
                        f2 = true;
                        BattleGrid.Children.Remove(c);
                    }
                }
                for(int j = 0; j < rmvList.Count; j++)
                {
                    //battleGrid.Children.RemoveAt(j);
                }
                for (int i = 0; i < NumList.Count; i++)
                {
                    NumList[i] = null;
                }
                if(f1 == f2 == true)
                {
                    BattleGrid.Children.Remove(BattleGrid.Children[BattleGrid.Children.Count - 1]);
                }
                NumList.Clear();
            }
        }
    }
}
