using System;
using System.Drawing;

namespace dotplasma
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i =0; i < 10; i++)
                new Plasma(2048,2048,i);
            Console.WriteLine("done!");
        }
    }

    /// <summary>
	/// Description of Plasma.
	/// </summary>
	public class Plasma
	{
		Bitmap b;
		int[,] map;
		Random rand;

		public Plasma(int w, int h, int nb) {
			b = new Bitmap(w, h);
			map = new int[w + 1, h + 1];
			
			rand = new Random();
			Rectangle r = new Rectangle(0, 0, w, h);

			map[0, 0] = rand.Next(256);
			map[w, 0] = rand.Next(256);
			map[0, h] = rand.Next(256);
			map[w, h] = rand.Next(256);

			calculate(r);
			draw();

			saveAs("output/"+"malgo4_"+nb+".png");
		}

		public void saveAs(String filename) {
			if (b != null) {
				b.Save(filename, System.Drawing.Imaging.ImageFormat.Png);
			};
		}
		private void draw() {
			Bitmap pixel = new Bitmap(1, 1);
			int c;
			Graphics g = Graphics.FromImage(b);

			for (int i = 0; i < map.GetLength(0); i++)
				for (int j = 0; j < map.GetLength(1); j++) {
				c = map[i, j];
				if (c > 255) c = 255;
				pixel.SetPixel(0, 0, Color.FromArgb(c, c, c));
				g.DrawImageUnscaled(pixel, i, j);
			}
			g.Flush();
		}

		private void calculate(Rectangle r) {
			//if (r.Width < 1|| r.Height <1)
			//    return;
			int w2 = r.Width / 2;
			int h2 = r.Height / 2;
			if (h2 == 0 || w2 == 0) return;

			int z = rand.Next(w2);
			//Console.WriteLine("{0} {1}", r.Right, r.Width);
//Console.WriteLine("{0}", map[r.Right, r.Bottom]);
			map[r.Left + w2, r.Top] = (map[r.Left, r.Top] + map[r.Right, r.Top]) / 2 ;
			//z = rand.Next(w2 / 4);
			map[r.Left + w2, r.Bottom] = (map[r.Left, r.Bottom] + map[r.Right, r.Bottom]) / 2 ;
			//z = rand.Next(w2 / 4);
			map[r.Left, r.Top + h2] = (map[r.Left, r.Top] + map[r.Left, r.Bottom]) / 2;
			//z = rand.Next(w2 / 4);
			map[r.Right, r.Top + h2] = (map[r.Right, r.Top] + map[r.Right, r.Bottom]) / 2;
			z = rand.Next(w2 / 2);
			
			//Algorythm4 
			map[r.Left + w2, r.Top + h2] = (map[r.Left, r.Top] + map[r.Right, r.Top] + map[r.Left, r.Bottom] + map[r.Right, r.Bottom]) /4 +z/3 ;
		
			//keep5
			//map[r.Left + w2, r.Top + h2] = rand.Next(256);
		
		//Algorythm1
			//if (rand.Next(10) >=9)
			//map[r.Left + w2, r.Top + h2] = rand.Next(256);
		
			calculate(new Rectangle(r.Left, r.Top, w2, h2));
			calculate(new Rectangle(r.Left, r.Top + h2, w2, h2));
			calculate(new Rectangle(r.Left + w2, r.Top, w2, h2));
			calculate(new Rectangle(r.Left + w2, r.Top + h2, w2, h2));

		}
	}
}
