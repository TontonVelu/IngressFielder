using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace filder
{

    public partial class Form1 : Form
    {
        List<Portal> lstPortals = new List<Portal>();
        List<Label> lstLabels = new List<Label>();
        List<Link> lstLinks = new List<Link>();
        Pen penPortal = new Pen(Color.Green, 5);
        Pen penLink = new Pen(Color.Blue, 3);
        Dictionary<Portal, int> keysRequired = new Dictionary<Portal, int>();

        public static int portalNo = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        public void MousClick(object sender, MouseEventArgs e)
        {
            Portal p1 = new Portal(e.X, e.Y);
            lstPortals.Add(p1);
            Graphics g = this.CreateGraphics();
            g.DrawEllipse(penPortal, e.X - 5, e.Y - 5, 10, 10);
        }

        private void Bt_Proc_Click(object sender, EventArgs e)
        {
            this.Opacity = 1;
            if (lstPortals.Count < 3)
            {
                MessageBox.Show("not enough portals");
                return;
            }

            //reorder portals lst by coordX
            List<Portal> SortedList = lstPortals.OrderBy(o => o.coordX).ToList();


            foreach (Portal port in SortedList)
            {
                Label namelabel = new Label();
                namelabel.Location = new Point(port.coordX - 5, port.coordY + 15);
                namelabel.Text = "P" + portalNo.ToString();
                namelabel.BackColor = System.Drawing.Color.Transparent;
                namelabel.AutoSize = true;
                namelabel.Name = portalNo.ToString();
                this.Controls.Add(namelabel);
                lstLabels.Add(namelabel);
                port.Name = portalNo;
                portalNo++;
                keysRequired.Add(port, 0);
            }

            //link all portals to no1
            for (int i = 2; i != SortedList.Count + 1; i++)
            {
                DrawPLink(SortedList[i - 1], SortedList[0]);
                Link lk = new Link(SortedList[i - 1], SortedList[0]);
                keysRequired[SortedList[i - 1]] = keysRequired.ElementAt(i - 1).Value + 1;
                lstLinks.Add(lk);
            }



            for (int i = 1; i < lstPortals.Count; i++)
            {
                int P2 = 0;
                List<bool> doIntersectAll = new List<bool>();

                for (int j = 1; j < lstPortals.Count; j++)
                {
                    doIntersectAll.Clear();

                    if (i <= j) continue;

                    Debug.Print("\n -----------------------" + "Newtry" + "\n -----------------------");
                    P2 = j;

                    Link lk = new Link(SortedList[i], SortedList[j]);

                    if (lstLinks.Contains(lk)) continue;

                    foreach (Link link in lstLinks)
                    {


                        doIntersectAll.Add(doIntersect(lk, link));

                        Debug.Print("link from " + lk.P1.Name + " to " + lk.P2.Name + "\n"
                                  + "link from " + link.P1.Name + " to " + link.P2.Name + "\n"
                                  + "they intersect : " + doIntersect(lk, link)
                                  + "\n -----------------------");
                    }


                    if (doIntersectAll == null) continue;

                    if (doIntersectAll.Contains(true))
                    {

                    }
                    else
                    {
                        DrawPLink(SortedList[i], SortedList[j]);
                        //Link lk = new Link(SortedList[i], SortedList[P2]);
                        keysRequired[SortedList[j]] = keysRequired.ElementAt(j).Value + 1;
                        lstLinks.Add(lk);
                        doIntersectAll.Clear();
                    }
                }
            }
            labelKeys();
        }
        private void labelKeys()
        {
            foreach(Control x in this.Controls)
            {
                if(x is Label)
                {
                    
                    foreach(Portal p in lstPortals)
                    {
                        if(x.Name == p.Name.ToString())
                        {
                            
                            x.Text = x.Text + "-K:" + keysRequired[p];
                        }
                    }


                    
                }
            }
        }

        private static bool SharePoint(Link lk1, Link lk2)
        {
            if (lk1.P1.Name.Equals(lk2.P1.Name) || lk1.P1.Name.Equals(lk2.P2.Name) || lk1.P2.Name.Equals(lk2.P1.Name) || lk1.P2.Name.Equals(lk2.P2.Name))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void Bt_Clear_Click(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
            lstPortals.Clear();
            lstLinks.Clear();
            portalNo = 1;
            this.Invalidate();
            keysRequired.Clear();

            foreach (Label lb in lstLabels)
            {
                this.Controls.Remove(lb);
            }

        }

        public void DrawPLink(Portal _p1, Portal _p2)
        {
            Graphics g = this.CreateGraphics();
            Point pt1 = new Point(_p1.coordX, _p1.coordY);
            Point pt2 = new Point(_p2.coordX, _p2.coordY);
            g.DrawLine(penLink, pt1, pt2);

        }


        // Given three colinear points p, q, r, the function checks if 
        // point q lies on line segment 'pr' 
        static Boolean onSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }


        static int orientation(Point p, Point q, Point r)
        {
            int val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.X - p.X) * (r.Y - q.Y);

            if (val == 0) return 0; // colinear 

            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }
        static Boolean doIntersect(Link _lk1, Link _lk2) //Point p1, Point q1, Point p2, Point q2
        {

            Point p1 = _lk1.Pt1;
            Point q1 = _lk1.Pt2;
            Point p2 = _lk2.Pt1;
            Point q2 = _lk2.Pt2;



            // Find the four orientations needed for general and 
            // special cases 
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
                if (SharePoint(_lk1, _lk2))
                {
                    return false;
                }
                else
                {
                    return true;
                };

            // Special Cases 
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1 
            if (o1 == 0 && onSegment(p1, p2, q1)) if (SharePoint(_lk1, _lk2))
                {
                    return false;
                }
                else
                {
                    return true;
                };

            // p1, q1 and q2 are colinear and q2 lies on segment p1q1 
            if (o2 == 0 && onSegment(p1, q2, q1)) if (SharePoint(_lk1, _lk2))
                {
                    return false;
                }
                else
                {
                    return true;
                };

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2 
            if (o3 == 0 && onSegment(p2, p1, q2)) if (SharePoint(_lk1, _lk2))
                {
                    return false;
                }
                else
                {
                    return true;
                };

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2 
            if (o4 == 0 && onSegment(p2, q1, q2)) if (SharePoint(_lk1, _lk2))
                {
                    return false;
                }
                else
                {
                    return true;
                };




            return false; // Doesn't fall in any of the above cases 


        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }

    public class Link : IEquatable<Link>
    {
        private Point _Pt1;
        private Point _Pt2;
        private Portal _P1;
        private Portal _P2;

        public Portal P1
        {
            get { return _P1; }
        }

        public Portal P2
        {
            get { return _P2; }
        }
        public Point Pt1
        {
            get { return _Pt1; }
        }

        public Point Pt2
        {
            get { return _Pt2; }
        }
        public Link(Portal _Por1, Portal _Por2)
        {
            _Pt1 = _Por1.P1;
            _Pt2 = _Por2.P1;
            _P1 = _Por1;
            _P2 = _Por2;
        }

        public bool Equals(Link other)
        {
            // Would still want to check for null etc. first.

            return (this._P1 == other._P1 || this._P1 == other._P2) &&
                    (this._P2 == other._P1 || this._P2 == other._P2);

            //     return this._Pt1 == other._Pt1 &&
            //this._Pt2 == other._Pt2 &&
            //this._P1 == other._P1 &&
            //this._P2 == other._P2;
        }
    }

    public class Portal
    {
        private int _coordX;
        private int _coordY;
        private int _Name;
        private Point _P1;

        public int Name
        {
            set { _Name = value; }
            get { return _Name; }
        }
        public Point P1
        {
            get { return _P1; }
        }

        public int coordX
        {
            get { return _coordX; }
        }
        public int coordY
        {
            get { return _coordY; }
        }


        public Portal(int coordX, int coordY)
        {
            _coordX = coordX;
            _coordY = coordY;
            _P1 = new Point(coordX, coordY);

        }
    }

}


