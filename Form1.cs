using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ben10
{
    public class AdvImg
    {
        public Rectangle Dest = new Rectangle();
        public Rectangle Src = new Rectangle();
        public Bitmap img;
    }
    public class CActor
    {
        public int x, y, w, h, f = 0;
        public List<Bitmap> img = new List<Bitmap>();
        
        public int dir = 0;
        public int flagjump = 0;
        public int counterjump = 0;
        public int elvf = 0;
        public int punchf = 0;
        public int enemyf = 0;
        public int gravity = 0;
        public string laserstate = "";
        public int laserf = 0;
        public int laserround = 0;
        public int labelf = 0;
        public int ladderf = 0;
        public int ladderup = 0;
        public int scroll = 0;
        public int e1attack = 0;
        public int e1ct = 0;
        public int elvup = 0;
        public int e2attackf = 0;
        public int e2damage = 0;
        public int e2dead = 0;
        public int transformed = 0;
        public int watchcall = 0;
        public int e3hit = 0;
        public int e3ice = 0;
        public int icehit = 0;
        public int benlose = 0;
        public int fourpunch = 0;
        public int e3damage = 0;
        public int benwon = 0;
        public int exp_timer = 0;
    }
    public partial class Form1 : Form
    {
        Bitmap off;
        Timer t = new Timer();
        Random r = new Random();
        List<CActor> ben = new List<CActor>();
        List<CActor> e1 = new List<CActor>();
        List<CActor> platforms = new List<CActor>();
        List<CActor> elv = new List<CActor>();
        List<CActor> explosion = new List<CActor>();
        List<AdvImg> bk = new List<AdvImg>();
        List<CActor> bull = new List<CActor>();
        List<CActor> laser = new List<CActor>();
        List<CActor> ladder = new List<CActor>();
        List<CActor> heart = new List<CActor>();
        List<CActor> watch = new List<CActor>();
        List<CActor> e2 = new List<CActor>();
        List<CActor> e3 = new List<CActor>();
        List<CActor> ice = new List<CActor>();
        List<CActor> fourarms = new List<CActor>();
        List<CActor> health = new List<CActor>();
        int bullf = 0;
        int bkf = 0;
        int y = 200;
        int n = 1;
        int ct = 0;
        int x0 = 1000, x1 = 1350;

        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp1;
            t.Tick += T_Tick;
            t.Start();
            this.KeyUp += Form1_KeyUp;

        }

        private void Form1_KeyUp1(object sender, KeyEventArgs e)
        {
            ben[0].scroll = 0;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void T_Tick(object sender, EventArgs e)
        {

            if (ben[0].flagjump == 1)
            {
                herojump();
            }

            for (int i = 0; i < e1.Count; i++)
            {
                if (e1[0].enemyf == 0)
                {
                    moveenemy(i);
                }
            }
            if (e1.Count == 0)
            {
                create_enemy1();
            }

            if (ct % 150 == 0)
            {
                laser_attack();
            }
            if (laser[0].laserf == 1 && laser[0].labelf != 2)
            {
                laser_label(ct);
            }

            if (ben[0].ladderf == 1)
            {
                ladderd();
            }

            ct++;
            bullet();
            moveelv();
            gravity();
            explode();
            e3_attack();
            healthbar();
            benlost_won();
            
            if (ct % 30 == 0)
            {
                e3_ice();
                e3[0].f = 5;
            }
            if (e2[0].e2dead == 0)
            {
                enemy2_attack();
            }
            
            if (e1[0].e1ct == 0)
            {
                enemy1_attack();

            }
            if (ben[0].scroll == 0)
            {
                scroll();
            }
            if (fourarms[0].watchcall==0)
            {
                watch_transform();
            }
            if (fourarms[0].fourpunch == 1)
            {
                fourarms_punch();
            }

            DrawDubb(this.CreateGraphics());
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ben[0].scroll = 1;
            if (e.KeyCode == Keys.Right)
            {
                ben[0].dir = 0; //  Right
                ben[0].x += 20;

                ben[0].f++;
                if (ben[0].f > 2)
                {
                    ben[0].f = 0;
                }
                if (ben[0].transformed==1)
                {
                    fourarms[0].dir = 0; //  Right
                    fourarms[0].x += 20;

                    fourarms[0].f++;
                    if (fourarms[0].f > 9)
                    {
                        fourarms[0].f = 5;
                    }
                }
            }
            if (e.KeyCode == Keys.Left)
            {
                ben[0].dir = 1; //  Left
                ben[0].x -= 20;


                ben[0].f++;
                if (ben[0].f > 7)
                {
                    ben[0].f = 5;
                }

                if (ben[0].transformed == 1)
                {
                    fourarms[0].dir = 1; //  Right
                    fourarms[0].x -= 20;

                    fourarms[0].f++;
                    if (fourarms[0].f > 14)
                    {
                        fourarms[0].f = 10;
                    }
                }
            }


            if (e.KeyCode == Keys.Up)
            {
                ben[0].flagjump = 1; 
                if (ben[0].transformed == 1)
                {
                    fourarms[0].y -= 20;
                }
            }

            if (e.KeyCode == Keys.Down)
            {
                ben[0].y += 10;
                if (ben[0].transformed == 1)
                {
                    fourarms[0].y += 20;
                }
                    
            }
            if (e.KeyCode == Keys.X)
            {
                createbullet();
                bullf = 1;
                bullet();
            }
            if (e.KeyCode == Keys.P)
            {
                ben[0].punchf = 1;
                ben[0].f = 8;
            }

            if (e.KeyCode == Keys.L)
            {
                ben[0].ladderf = 1;
            }

            if(e.KeyCode==Keys.S)
            {
                fourarms[0].fourpunch = 1;
                fourarms[0].f = 14;
                fourarms_attack();
            }
        }

        void herojump()
        {
            ben[0].counterjump++;
            if (ben[0].counterjump <= 8)
            {
                ben[0].y -= 20;
                if (ben[0].dir == 0)
                {
                    ben[0].x += 10;
                }
                if (ben[0].dir == 1)
                {
                    ben[0].x -= 10;
                }
            }
            else
            {
               // ben[0].y += 20;
                if (ben[0].counterjump == 16)
                {
                    ben[0].flagjump = 0;
                    ben[0].counterjump = 0;
                }
            }
        }
        void create_explosion()
        {
            CActor pnn = new CActor();
            pnn.x = ben[0].x - 50;
            pnn.y = ben[0].y - 50;
            pnn.w = 200;
            pnn.h = 200;
            pnn.exp_timer = 10; // 10 secs

            Bitmap img = new Bitmap("explode.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);

            explosion.Add(pnn);
        }
        void explode()
        {
            for (int i = 0; i < explosion.Count; i++)
            {
                explosion[i].exp_timer--;

                explosion[i].x = ben[0].x - 50;
                explosion[i].y = ben[0].y - 50;

                if (explosion[i].exp_timer <= 0)
                {
                    explosion.RemoveAt(i);
                }
            }
        }

        void moveenemy(int i)
        {
            if (e1[0].e1attack == 0)
            {
                if (e1[i].dir == 0)
                {
                    e1[i].x -= 5;

                    if (e1[i].x == x0)
                    {
                        e1[i].dir = 1;
                    }

                    e1[i].f++;
                    if (e1[i].f >= 4)
                    {
                        e1[i].f = 0;
                    }
                }

                else if (e1[i].dir == 1)
                {
                    e1[i].x += 5;

                    if (e1[i].x == x1)
                    {
                        e1[i].dir = 0;
                    }

                    e1[i].f++;
                    if (e1[i].f >= 8)
                    {
                        e1[i].f = 5;
                    }
                }
            }
            
            
        }

        void createbk()
        {
            AdvImg pnn = new AdvImg();
            pnn.Dest.X = 0;
            pnn.Dest.Y = 0;
            pnn.Dest.Width = this.ClientSize.Width;
            pnn.Dest.Height = this.ClientSize.Height;
            pnn.Src.X = 0;
            pnn.Src.Y = 0;
            pnn.Src.Width = this.ClientSize.Width;
            pnn.Src.Height = this.ClientSize.Height;
            pnn.img = new Bitmap("bk0.jpg");
            bk.Add(pnn);

            pnn = new AdvImg();
            pnn.Dest.X = this.ClientSize.Width;
            pnn.Dest.Y = 0;
            pnn.Dest.Width = this.ClientSize.Width;
            pnn.Dest.Height = this.ClientSize.Height;
            pnn.Src.X = 0;
            pnn.Src.Y = 0;
            pnn.Src.Width = this.ClientSize.Width;
            pnn.Src.Height = this.ClientSize.Height;
            pnn.img = new Bitmap("bk2.jpg");
            bk.Add(pnn);

            pnn = new AdvImg();
            pnn.Dest.X = 0;
            pnn.Dest.Y = 0;
            pnn.Dest.Width = this.ClientSize.Width+1000;
            pnn.Dest.Height = this.ClientSize.Height;
            pnn.Src.X = 0;
            pnn.Src.Y = 0;
            pnn.Src.Width = this.ClientSize.Width;
            pnn.Src.Height = this.ClientSize.Height;
            pnn.img = new Bitmap("benlost.jpeg");
            bk.Add(pnn);

            pnn = new AdvImg();
            pnn.Dest.X = 0;
            pnn.Dest.Y = 0;
            pnn.Dest.Width = this.ClientSize.Width+1000;
            pnn.Dest.Height = this.ClientSize.Height+200;
            pnn.Src.X = 0;
            pnn.Src.Y = 0;
            pnn.Src.Width = this.ClientSize.Width;
            pnn.Src.Height = this.ClientSize.Height;
            pnn.img = new Bitmap("benwon.jpeg");
            bk.Add(pnn);

        }

        void fourarms_punch()
        {
            fourarms[0].f++;
            if (fourarms[0].f > 16)
            {
                fourarms[0].f = 5;
                fourarms[0].fourpunch = 0;

            }
        }

        void fourarms_attack()
        {
            if (fourarms[0].x + fourarms[0].w > e3[0].x - 100 && fourarms[0].x < e3[0].x)
            {
                if (fourarms[0].y > e3[0].y)
                {
                    e3[0].e3damage++;
                }
            }
        }

        void gravity()
        {
            if (ben[0].transformed == 0)
            {
                ben[0].gravity = 0;


                if (ben[0].elvf == 1 || ben[0].ladderf == 1)
                {
                    ben[0].gravity = 1;
                }
                if (ben[0].y <= platforms[1].y && ben[0].x >= platforms[1].x || ben[0].y <= platforms[0].y && ben[0].x >= platforms[0].x)
                {
                    ben[0].gravity = 1;
                }

                if (ben[0].y >= 700)
                {
                    ben[0].gravity = 1;
                    ben[0].y = 700;
                }

                if (ben[0].gravity == 0 && ben[0].flagjump == 0)
                {
                    ben[0].y += 10;
                }
            }
            else // Four Arms
            {
                fourarms[0].gravity = 0;
                if (fourarms[0].y <= platforms[1].y && fourarms[0].x >= platforms[1].x || fourarms[0].y <= platforms[0].y && fourarms[0].x >= platforms[0].x)
                {
                    fourarms[0].gravity = 0;

                }

                if (fourarms[0].y >= 600)
                {
                    fourarms[0].gravity = 1;
                    fourarms[0].y = 600;
                }


                if (fourarms[0].gravity == 0 && ben[0].flagjump == 0)
                {
                    fourarms[0].y += 10;
                }

                ben[0].x = fourarms[0].x;
                ben[0].y = fourarms[0].y;
            }
        }
        void create_laser()
        {
            CActor pnn = new CActor();
            Bitmap img;
            img = new Bitmap("laser" + 0 + ".png");
            pnn.img.Add(img);
            for (int i = 1; i < 14; i++)
            {
                img = new Bitmap("laser" + i + ".png");
                pnn.img.Add(img);
            }
            pnn.x = 0;
            pnn.y = 0;
            pnn.w = 50;
            pnn.h = 800;
            pnn.laserstate = "Attack";
            laser.Add(pnn);

        }

        void laser_attack()
        {
            laser[0].x = r.Next(0, this.Width - 200);
            laser[0].laserf = 1;
            laser[0].f = 0;
            if (ben[0].x > laser[0].x && ben[0].x < laser[0].x + 150)
            {
                laser[0].laserstate = "hit";
            }
            else
            {
                laser[0].laserstate = "attack";
            }
        }

        void laser_label(int ct)
        {
            if (ct % 5 == 0)
            {
                laser[0].laserround++;
            }
            if (laser[0].laserround % 2 == 0)
            {
                laser[0].labelf = 1;
            }
            if (laser[0].laserround % 2 != 0)
            {
                laser[0].labelf = 0;
            }
            if (laser[0].laserround == 8)
            {
                laser[0].f = 1;
                laser[0].labelf = 2;

            }
            if (ben[0].x > laser[0].x && ben[0].x < laser[0].x + 250)
            {
                laser[0].laserstate = "hit";
            }
        }

        void createben()
        {
            CActor pnn = new CActor();
           
            pnn.x = 1050;
            pnn.y = 700;
            pnn.h = 100;
            pnn.w = 100;
            pnn.f = 0;
            pnn.dir = 0;


            for (int i = 1; i <= 4; i++)
            {
                Bitmap img = new Bitmap(i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
            }

            for (int i = 5; i <= 8; i++)
            {
                Bitmap img = new Bitmap(i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
            }
            for(int i=0;i<3;i++)
            {
                Bitmap img = new Bitmap("punch" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
            }
            for (int i = 0; i < 3; i++)
            {
                Bitmap img = new Bitmap("die" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
            }
            ben.Add(pnn);
        }

        void create_fourarms()
        {
            CActor pnn = new CActor();

            pnn.x = 1300;
            pnn.y = 20;
            pnn.h = 200;
            pnn.w = 200;
            pnn.f = 0;
            pnn.dir = 0;
            //transform frames
            for (int i = 0; i <= 4; i++)
            {
                Bitmap img = new Bitmap("transform" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
            }

            for (int i = 0; i <= 9; i++)
            {
                Bitmap img = new Bitmap("f" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
            }

            for (int i = 0; i <= 1; i++)
            {
                Bitmap img = new Bitmap("fpunch" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
            }

            
            fourarms.Add(pnn);
        }

        void create_enemy1()
        {
            CActor pnn = new CActor();
            pnn.img = new List<Bitmap>();
            pnn.x = 1350;
            pnn.y = 380;
            pnn.h = 50;
            pnn.w = 200;
            pnn.f = 0;
            pnn.dir = 0;


            for (int i = 1; i <= 3; i++)
            {
                Bitmap img = new Bitmap("e" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));

                pnn.img.Add(img);
            }

            for (int i = 4; i <= 8; i++)
            {

                Bitmap img = new Bitmap("e" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
            }

            Bitmap img2 = new Bitmap("enemydead.png");
            img2.MakeTransparent(img2.GetPixel(1, 1));
            pnn.img.Add(img2);
            e1.Add(pnn);

            for (int i = 0; i < 8; i++)
            {
                Bitmap img = new Bitmap("e1attack" + i + ".png");
                //img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
            }

            
        }

        void enemy1_attack()
        {
            if (ben[0].x + ben[0].w > e1[0].x - 50 && ben[0].x + ben[0].w < e1[0].x + e1[0].w)
            {
                if(ben[0].y < e1[0].y && ben[0].y > e1[0].y - 150)
                {
                    if (e1[0].e1attack == 0)
                    {
                        e1[0].f = 9;
                    }
                    e1[0].e1attack = 1;
                    if (e1[0].f >= 15)
                    {
                        ben[0].f = 13;
                        e1[0].e1attack = 0;
                        e1[0].e1ct++;
                        heart.RemoveAt(heart.Count - 1);
                    }
                    e1[0].f++;
                }
                
            }    
        }

        void create_heart()
        {
            int x = 40;
            CActor pnn = new CActor();
            for(int i=0;i<4;i++)
            {
                pnn = new CActor();
                pnn.x = x;
                pnn.y = 40;
                pnn.h = 40;
                pnn.w = 40;
                pnn.f = 0;
                Bitmap img = new Bitmap("123.png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
                heart.Add(pnn);
                x += 50;
            }
            
        }

        void create_platform()
        {
            CActor pnn = new CActor();
            pnn.img = new List<Bitmap>();
            pnn.x = 950;
            pnn.y = 150;
            pnn.w = 700;
            pnn.h = 100;

            Bitmap img = new Bitmap("p" + 0 + ".png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);
            //first
            platforms.Add(pnn);


            pnn = new CActor();
            pnn.img = new List<Bitmap>();
            pnn.x = 950;
            pnn.y = 400;
            pnn.w = 700;
            pnn.h = 100;
            img = new Bitmap("p" + 1 + ".png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);
            
            platforms.Add(pnn);

            pnn = new CActor();
            pnn.img = new List<Bitmap>();
            pnn.x = 1000;
            pnn.y = 500;
            pnn.w = 700;
            pnn.h = 100;
            img = new Bitmap("p" + 2 + ".png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);
          
            platforms.Add(pnn);

        }

        void create_ladder()
        {
            CActor pnn = new CActor();
            Bitmap img = new Bitmap("ladder0.png");
            pnn.img.Add(img);
            pnn.x = 1000;
            pnn.y = 150;
            pnn.h = 300;
            pnn.w = 150;
            ladder.Add(pnn);

            pnn = new CActor();
             img = new Bitmap("ladder0.png");
            pnn.img.Add(img);
            pnn.x = 1650;
            pnn.y = 150;
            pnn.h = 800;
            pnn.w = 150;
            ladder.Add(pnn);


        }

        void createelv()
        {

            CActor pnn = new CActor();
            pnn.img = new List<Bitmap>();
            pnn.x = 800;
            pnn.y = 720;
            pnn.w = 200;
            pnn.h = 150;

            Bitmap img = new Bitmap("elevatorq" + ".png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);
           
            elv.Add(pnn);
        }

        void moveelv()
        {
            if (ben[0].x >= elv[0].x && ben[0].x <= elv[0].x + elv[0].w && n >= 0)
            {
                if (ben[0].y < elv[0].y && ben[0].y >= elv[0].y - 100)
                {
                    ben[0].elvf = 1;
                    elv[0].elvup = 1;
                }
            }
            else
            {
                ben[0].elvf = 0;
                elv[0].elvup = 0;
            }
           
            if (ben[0].elvf == 1)
            {
                if (elv[0].y <= platforms[1].y)
                {
                    ben[0].elvf = 0;
                    ben[0].x += 200;
                    ben[0].y -= 20;
                    //n--;
                }
                elv[0].y -= 10;
                ben[0].y -= 10;
            }
            if ((ben[0].x + 60 < elv[0].x || ben[0].x > elv[0].x + 180))
            {
                //elv[0].y += 10;
                //if (elv[0].y >= 700)
                //{
                //    ben[0].elvf = 0;
                //}
                elv[0].elvup = -1;
            }
            if (elv[0].elvup == -1)
            {
                if (elv[0].y <= 710)
                {
                    elv[0].y += 10;
                }
                else
                {
                    elv[0].elvup = 0;
                    //ben[0].elvf = 0;
                }
            }
            

        }

        void createbullet()
        {
            CActor pnn = new CActor();
            pnn.img = new List<Bitmap>();
            pnn.x = ben[0].x;
            pnn.y = ben[0].y;
            pnn.w = 30;
            pnn.h = 30;
            if (ben[0].dir==0)
            {
                pnn.dir = 0;
            }
            if (ben[0].dir == 1)
            {
                pnn.dir = 1;
            }
          //  pnn.bullf = 0;

            Bitmap img = new Bitmap("punch2.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);
            bull.Add(pnn);
        }

        void bullet()
        {
            if(bull.Count>0)
            {
                for(int i=0;i<bull.Count;i++)
                {
                    if (bull[i].dir == 0)
                    {
                        bull[i].x += 30;
                    }
                    if (bull[i].dir == 1)
                    {
                        bull[i].x -= 30;
                    }
                }
            }
        }

        void scroll()
        {
            if (ben[0].x >= this.Width - 400)
            {

               
                if (bkf==0)
                {
                    bk[0].Dest.X -= 30;
                    bk[1].Dest.X -= 30;
                    e1[0].x -= 30;
                    e2[0].x -= 30;
                    e3[0].x -= 30;
                    elv[0].x -= 30;
                    ladder[0].x -= 30;
                    //watch[0].x -= 20;
                    x0 -= 30;
                    x1 -= 30;
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        platforms[i].x -= 30;
                    }
                }
                if (bk[1].Dest.X < 20)
                {
                    bkf = 1;
                }

            }
            if (ben[0].x <= 200)
            {


                if (bkf == 0)
                {
                    bk[0].Dest.X += 30;
                    bk[1].Dest.X += 30;
                    e1[0].x += 30;
                    e2[0].x += 30;
                    e3[0].x += 10;
                    elv[0].x += 30;
                    ladder[0].x += 30;
                    //watch[0].x += 20;
                    x0 += 30;
                    x1 += 30;
                    for (int i = 0; i < platforms.Count; i++)
                    {
                        platforms[i].x += 30;
                    }
                }
                if (bk[1].Dest.X > this.Width - 20)
                {
                    bkf = 1;
                }

            }
        }

        void ladderd()
        {
            if (ben[0].ladderup==0)
            {
                if (ben[0].x >= ladder[0].x && ben[0].x <= ladder[0].x + ladder[0].w)
                {
                    ben[0].y -= 30;
                    if (ben[0].y + ben[0].h < platforms[0].y + 20)
                    {
                        ben[0].ladderf = 0;
                        ben[0].ladderup = 1;
                        ben[0].y += 10;
                    }
                }
                else
                {
                    ben[0].ladderf = 0;
                }
            }

            if (ben[0].ladderup == 1)
            {
                if (ben[0].x >= ladder[0].x && ben[0].x <= ladder[0].x + ladder[0].w)
                {
                    ben[0].y += 30;
                    if (ben[0].y + ben[0].h > 700)
                    {
                        ben[0].ladderf = 0;
                        ben[0].ladderup = 0;
                    }
                }
                else
                {
                    ben[0].ladderf = 1;
                }
            }


        }

        void create_watch()
        {
            CActor pnn = new CActor();
            pnn.x = 1300;
            pnn.y = 100;
            pnn.w = 100;
            pnn.h = 100;
            Bitmap img = new Bitmap("watch.jpg");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);
            watch.Add(pnn);
        }

        void create_enemy2()
        {//e2k
            CActor pnn = new CActor();
            pnn.x = 1200;
            pnn.y = 80;
            pnn.h = 120;
            pnn.w = 150;
            pnn.f = 0;
            pnn.dir = 0;

            for (int i = 0; i <= 2; i++)
            {
                Bitmap img = new Bitmap("e2" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));
                pnn.img.Add(img);
            }
            Bitmap img1 = new Bitmap("e2dead.jpg");
            img1.MakeTransparent(img1.GetPixel(0, 0));
            pnn.img.Add(img1);
            e2.Add(pnn);
        }

        void create_enemy3()
        {
            CActor pnn = new CActor();
            pnn.img = new List<Bitmap>();
            pnn.x = 2500;
            pnn.y = 380;
            pnn.h = 400;
            pnn.w = 400;
            pnn.f = 0;
            pnn.dir = 0;

            Bitmap img = new Bitmap("e3stand.png");
            img.MakeTransparent(img.GetPixel(0, 0));

            pnn.img.Add(img);
            

            for (int i = 0; i <= 3; i++)
            {
                img = new Bitmap("e3hit" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));

                pnn.img.Add(img);
            }

            for (int i = 0; i <= 6; i++)
            {
                img = new Bitmap("e3ice" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));

                pnn.img.Add(img);
            }
            e3.Add(pnn);
        }

        void create_ice()
        {
            CActor pnn = new CActor();
            pnn.img = new List<Bitmap>();
            pnn.x = e3[0].x;
            pnn.y = fourarms[0].y;
            pnn.h = 50;
            pnn.w = 50;
            pnn.f = 0;
            pnn.dir = 0;

            for (int i = 0; i <= 2; i++)
            {
                Bitmap img = new Bitmap("ice" + i + ".png");
                img.MakeTransparent(img.GetPixel(0, 0));

                pnn.img.Add(img);
            }
            
            ice.Add(pnn);
        }

        void e3_attack()
        {
            if (fourarms[0].x + fourarms[0].w > e3[0].x - 200 && fourarms[0].x < e3[0].x)
            {
                e3[0].e3hit = 1;
            }
            else
            {
                e3[0].e3hit = 0;
                //e3[0].f = 0;
            }
            if (e3[0].e3hit == 1)
            {
                e3[0].f++;
                if (e3[0].f >= 5)
                {
                    e3[0].f = 1;
                }
            }
            if (e3[0].e3hit == 0 && e3[0].e3ice == 1)
            {
                e3[0].f++;
                if (e3[0].f >= 12)
                {
                    e3[0].e3ice = 0;
                    create_ice();
                    e3[0].f = 0;
                }
                //create_ice();
            }
            for (int i = 0; i < ice.Count; i++)
            {
                if (ice[i].x < fourarms[0].x + fourarms[0].w + 20 && ice[i].x > fourarms[0].x)
                {
                    if (ice[i].y > fourarms[0].y && ice[i].y < fourarms[0].y + fourarms[0].h)
                    {
                        if (ice[i].icehit == 0)
                        {
                            if (health[0].img.Count > 1)
                            {
                                health[0].img.RemoveAt(health[0].img.Count - 1);
                                ice[i].icehit = 1;
                            }
                            
                        }
                        
                    }
                }
            }
        }

        void e3_ice()
        {
            if (ben[0].transformed == 1)
            {
                e3[0].e3ice = 1;
            }
        }

        void create_health()
        {
            CActor pnn = new CActor();
            pnn.img = new List<Bitmap>();
            pnn.x = fourarms[0].x;
            pnn.y = fourarms[0].y - 40;
            pnn.h = 50;
            pnn.w = fourarms[0].w;
            pnn.f = 0;
            pnn.dir = 0;

            Bitmap img = new Bitmap("health00.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);

            img = new Bitmap("health20.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);

            img = new Bitmap("health40.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);

            img = new Bitmap("health60.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);

            img = new Bitmap("health80.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);

            img = new Bitmap("health100.png");
            img.MakeTransparent(img.GetPixel(0, 0));
            pnn.img.Add(img);

            health.Add(pnn);
        }

        void enemy2_attack()
        {
            if (ben[0].x >= e2[0].x - 200 && ben[0].x <= e2[0].x + 200)
            {
                if (ben[0].y >= e2[0].y - 100 && ben[0].y <= e2[0].y + 100)
                {
                    e2[0].f++;
                    if (e2[0].f >= 3)
                    {
                        e2[0].f = 0;
                    }
                }
            }

            if (ben[0].x >= e2[0].x - 50 && ben[0].x <= e2[0].x)
            {
                if (ben[0].y >= e2[0].y - 100 && ben[0].y <= e2[0].y + 100 && ben[0].e2attackf == 0)
                {
                    ben[0].f = 13;
                    if(heart.Count>0)
                    {
                        heart.RemoveAt(heart.Count - 1);
                        create_explosion();
                    }
                    
                    ben[0].e2attackf = 1;
                }
            }
            else
            {
                ben[0].e2attackf = 0;
            }
        }

        void watch_transform()
        {
            if (ben[0].y < watch[0].y + ben[0].h)
            {
                if (ben[0].x >= watch[0].x - 50 && ben[0].x <= watch[0].x + 100)
                {
                    ben[0].transformed = 1;
                    fourarms[0].f++;
                    if (fourarms[0].f == 5)
                    {
                        fourarms[0].watchcall = 1;
                        watch.RemoveAt(watch.Count - 1);
                    }
                }
            }
        }

        void healthbar()
        {
            for (int i = 0; i < health.Count; i++)
            {
                health[i].x = fourarms[0].x;
                health[i].y = fourarms[0].y - 40;
            }
            
        }

        void benlost_won()
        {
            if (heart.Count == 0 || health[0].img.Count == 1)
            {
                ben[0].benlose = 1;
            }
            if (e3[0].e3damage >= 3)
            {
                ben[0].benwon = 1;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(this.CreateGraphics());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            
            createben();
            create_platform();
            createelv();
            create_laser();
            create_ladder();
            create_enemy1();
            create_enemy2();
            create_enemy3();
            create_heart();
            create_watch();
            create_fourarms();
            create_health();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            createbk();
        }

        public void DrawScene(Graphics g2)
        {
            g2.Clear(Color.LightBlue);
            for (int i = 0; i < 2; i++)
            {
                g2.DrawImage(bk[i].img, bk[i].Dest, bk[i].Src, GraphicsUnit.Pixel);

            }

            for (int i = 0; i < e1.Count; i++)
            {
                g2.DrawImage(e1[i].img[e1[i].f], e1[i].x, e1[i].y, e1[i].w, e1[i].h);
            }
            g2.DrawImage(e2[0].img[e2[0].f], e2[0].x, e2[0].y, e2[0].w, e2[0].h);
            g2.DrawImage(e3[0].img[e3[0].f], e3[0].x, e3[0].y, e3[0].w, e3[0].h);
            //for (int i = 0; i < platforms.Count; i++)
            //{
            //    g2.DrawImage(platforms[i].img[i], platforms[i].x, platforms[i].y, platforms[i].w, platforms[i].h);
            //}

            g2.DrawImage(platforms[0].img[0], platforms[0].x, platforms[0].y, platforms[0].w, platforms[0].h);
            g2.DrawImage(platforms[1].img[0], platforms[1].x, platforms[1].y, platforms[1].w, platforms[1].h);
            //g2.DrawImage(platforms[2].img[0], platforms[2].x, platforms[2].y, platforms[2].w, platforms[2].h);
            g2.DrawImage(elv[0].img[0], elv[0].x, elv[0].y, elv[0].w, elv[0].h);
            
            // ben punch enemy
            if (ben[0].punchf == 1)
            {
                ben[0].f++;
                if (ben[0].f == 11)
                {
                    ben[0].punchf = 0;
                    ben[0].f = 1;
                    if (ben[0].y <= e1[0].y && ben[0].y >= e1[0].y - 150)
                    {
                        e1[0].f = 8;
                        e1[0].enemyf = 1;
                    }
                }
            }
            if (bullf == 1)
            {
                for (int i = 0; i < bull.Count; i++)
                {
                    if (bull[i].y <= e1[0].y && bull[i].y >= e1[0].y - 150)
                    {
                        e1[0].f = 8;
                        e1[0].enemyf = 1;
                    }
                    if (bull[i].y <= e2[0].y + 50 && bull[i].y >= e2[0].y - 150)
                    {
                        
                        e2[0].f = 3;
                        e2[0].enemyf = 1;
                        e2[0].e2dead = 1;
                        //e2[0].e2damage++;
                        //if (e2[0].e2damage == 2)
                        //{
                        //    e2[0].f = 3;
                        //    e2[0].enemyf = 1;
                        //    e2[0].e2dead = 1;
                        //}
                        
                    }
                }

                
            }

            g2.DrawImage(ladder[0].img[0], ladder[0].x, ladder[0].y, ladder[0].w, ladder[0].h);
            g2.DrawImage(ladder[1].img[0], ladder[1].x, ladder[1].y, ladder[1].w, ladder[1].h);
            if (bull.Count > 0)
            {
                for(int i=0;i<bull.Count;i++)
                {
                    g2.DrawImage(bull[i].img[0], bull[i].x, bull[i].y, bull[i].w, bull[i].h);
                }
                
            }
            if (ben[0].transformed == 0)
            {
                for (int i = 0; i < heart.Count; i++)
                {
                    g2.DrawImage(heart[i].img[heart[i].f], heart[i].x, heart[i].y, heart[i].w, heart[i].h);
                }
            }


            if (ben[0].transformed == 0)
            {
                g2.DrawImage(ben[0].img[ben[0].f], ben[0].x, ben[0].y, ben[0].w, ben[0].h);

            }
            else if (ben[0].transformed==1)
            {
                g2.DrawImage(fourarms[0].img[fourarms[0].f], fourarms[0].x, fourarms[0].y, fourarms[0].w, fourarms[0].h);
                if (health.Count > 0)
                {
                    g2.DrawImage(health[0].img[health[0].img.Count - 1], health[0].x, health[0].y, health[0].w, health[0].h);
                }
                

            }
            if(watch.Count>0)
            {
                g2.DrawImage(watch[0].img[watch[0].f], watch[0].x, watch[0].y, watch[0].w, watch[0].h);

            }
            //laser
            if (ben[0].transformed == 0)
            {
                if (laser[0].laserf == 1)
                {
                    if (laser[0].labelf == 1)
                    {
                        laser[0].f = 0;
                        g2.DrawImage(laser[0].img[laser[0].f], laser[0].x, laser[0].y, 250, 100); //warning
                    }
                    if (laser[0].labelf == 2)
                    {
                        if (laser[0].f <= 5)
                        {
                            g2.DrawImage(laser[0].img[laser[0].f], laser[0].x, laser[0].y, laser[0].w, laser[0].h); //laser
                        }
                        else
                        {
                            g2.DrawImage(laser[0].img[laser[0].f], laser[0].x, 650, 200, 200);//explosion
                        }
                        if (laser[0].laserstate == "attack")
                        {
                            if (laser[0].f == 5)
                            {
                                laser[0].f = 0;
                                laser[0].laserf = 0;
                                laser[0].laserround = 0;
                                laser[0].labelf = 0;
                            }
                        }
                        if (laser[0].laserstate == "hit")
                        {
                            if (laser[0].f == 13)
                            {
                                laser[0].f = 0;
                                laser[0].laserf = 0;
                                laser[0].laserround = 0;
                                laser[0].labelf = 0;
                                ben[0].f = 13;
                                if (heart.Count == 0)
                                {
                                    heart.RemoveAt(heart.Count - 1);
                                }
                                
                            }
                        }
                        laser[0].f++;
                    }

                }
            }
            

            for (int i = 0; i < ice.Count; i++)
            {
                g2.DrawImage(ice[i].img[ice[i].f], ice[i].x, ice[i].y, ice[i].w, ice[i].h);
                ice[i].f++;
                if (ice[i].f == 3)
                {
                    ice[i].f = 0;
                }
                ice[i].x -= 30;
            }

            if (ben[0].benlose == 1)
            {
                g2.DrawImage(bk[2].img, bk[2].Dest, bk[2].Src, GraphicsUnit.Pixel);
            }

            if (ben[0].benwon == 1)
            {
                g2.DrawImage(bk[3].img, bk[3].Dest, bk[3].Src, GraphicsUnit.Pixel);
            }
            for (int i = 0; i < explosion.Count; i++)
            {
                g2.DrawImage(explosion[i].img[0], explosion[i].x, explosion[i].y, explosion[i].w, explosion[i].h);
            }
            //MessageBox.Show(e1[0].img.Count.ToString());

        }

        public void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}