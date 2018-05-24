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

namespace FinalProject
{
    //Class for world objects and variables (just music for now)
    public class World
    {
        private string audTitlePath { get; set; }
        private string audWorldPath   {get; set;}
        private string audBattlePath  {get; set;}
        private string audFanfarePath { get; set; }
        public Uri audtitleUri { get; set; }
        public Uri audWorldUri { get; set; }
        public Uri audBattleUri { get; set; }
        public Uri audFanfareUri { get; set; }

        private string imgEnemiesPath { get; set; }
        private string gifFilter { get; set; }
        public string[] imgEnemies { get; set; }

        public List<Control> numList { get; set; }
        public Grid battleGrid { get; set; }

        public World(ref List<Control> numList, ref Grid battleGrid)
        {
            audTitlePath  = @"res\audio\title.mp3";
            audWorldPath  = @"res\audio\world.mp3";
            audBattlePath = @"res\audio\battle.mp3";
            audFanfarePath = @"res\audio\fanfare.mp3";

            audtitleUri = new Uri(audTitlePath, UriKind.Relative);
            audWorldUri = new Uri(audWorldPath, UriKind.Relative);
            audBattleUri = new Uri(audBattlePath, UriKind.Relative);
            audFanfareUri = new Uri(audFanfarePath, UriKind.Relative);

            imgEnemiesPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) 
                + @"\res\img\enemy";
            gifFilter = "*.gif";
            imgEnemies = Directory.GetFiles(imgEnemiesPath, gifFilter);
            this.numList = numList; this.battleGrid = battleGrid;
        }

        public enum Numoptions
        {
            normal, crit, miss
        }
        public enum ControlTag
        {
            player, enemy, ui, damagenum
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
            numList.Add(lbl);
            battleGrid.Children.Add(lbl);
        }
        public void RemoveNum(Control control, bool removeAll)
        {
            Control c = null;
            Object x = null;
            bool f1 = false, f2 = false;
            List<int> rmvList = new List<int>();
            if (removeAll == false)
            {
                numList.Remove(control);
                battleGrid.Children.Remove(control);
            }
            else
            {
                //foreach(object x in battleGrid.Children)
                for(int i = 0; i < battleGrid.Children.Count; i++)
                {
                    if(battleGrid.Children[i] is Control)
                    {
                        f1 = true;
                        c = (Control)battleGrid.Children[i];
                    }
                    if(c != null && c.Tag is ControlTag && (ControlTag)c.Tag == ControlTag.damagenum)
                    {
                        //rmvList.Add(i);
                        f2 = true;
                        battleGrid.Children.Remove(c);
                    }
                }
                for(int j = 0; j < rmvList.Count; j++)
                {
                    //battleGrid.Children.RemoveAt(j);
                }
                for (int i = 0; i < numList.Count; i++)
                {
                    numList[i] = null;
                }
                if(f1 == f2 == true)
                {
                    battleGrid.Children.Remove(battleGrid.Children[battleGrid.Children.Count - 1]);
                }
                numList.Clear();
            }
        }
    }
}
