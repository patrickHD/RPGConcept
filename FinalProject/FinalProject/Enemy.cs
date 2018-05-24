using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FinalProject
{
    public class Enemy
    {
        //status
        public string Name { get; set; }
        public int MaxHp { get; set; }
        public int Level { get; set; }

        //battle variables
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public string actionsStr { get; set; }

        //
        BitmapImage Img { get; set; }

        public void attack(Player player)
        {
            if (player.defending == true)
            {
                actionsStr = player.Name + " blocked " + Name + "'s attack";
            }
            else
            {
                actionsStr = Name + " attacks. " + player.Name + " takes " + Atk + " damage!";
                player.Hp -= Atk;
            }
        }
        public void takeDamage(int atk, Player player = null)
        {
            Hp -= atk - Def;
        }

        public Enemy(ref Image enemyImage, string[] enemyImages)
        {
            Name = "Enemy";
            Random rnd = new Random();
            int gen = rnd.Next(1, 5);
            MaxHp = Hp = rnd.Next(150, 200);// 15 + gen * 2;
            Atk = 2 + gen;
            Def = 2 + gen;
            Level = gen;
            actionsStr = "";
            Img = new BitmapImage();
            Img.BeginInit();
            Img.UriSource = new Uri(enemyImages[rnd.Next(enemyImages.Length)]);
            Img.EndInit();
            WpfAnimatedGif.ImageBehavior.SetAnimatedSource(enemyImage, Img);
        }

        public Enemy(int hp, int atk, int def, int lvl, string name)
        {
            Name = name;
            MaxHp = Hp = hp;
            Atk = atk;
            Def = def;
            Level = lvl;
            actionsStr = "";
        }
    }
}
