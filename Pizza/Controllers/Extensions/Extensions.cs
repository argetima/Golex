using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Web;

namespace Pizza.Controllers.Extensions
{
    public class Extensions
    {
        public static Pizza.Models.Order order { get; set; }

        public static void PrintOrder()
        {
            var doc = new PrintDocument();
            doc.PrinterSettings.PrinterName = System.Configuration.ConfigurationManager.AppSettings["PrinterName"];
            doc.PrintPage += new PrintPageEventHandler(PrintPage);
            doc.Print();
        }

        private static void PrintPage(Object sender, PrintPageEventArgs e)
        {
            int PozY = 10;

            PozY = PrintoHeaderinEPorosise(e, PozY, order);

            //printimi i trupit te fatures(badit)
            int NumroRreshtat = 0;

            foreach (var row in order.OrderDetails)
            {
                PozY += 12;

                e.Graphics.DrawString(row.itemName, new System.Drawing.Font("Verdana", 8, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(0, PozY, 500, 500));
                e.Graphics.DrawString(DecimalToInteger(row.quantity.ToString()), new System.Drawing.Font("Verdana", 8, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(150, PozY, 500, 500));
                e.Graphics.DrawString(row.price.ToString("0.00"), new System.Drawing.Font("Verdana", 8, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(190, PozY, 500, 500));
                e.Graphics.DrawString((row.quantity * row.price).ToString("0.00"), new System.Drawing.Font("Verdana", 8, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(240, PozY, 500, 500));

                //string str = row["Specifikat"].ToString();
                //if (row["Specifikat"].ToString().Trim() != "")
                //{
                //    PozY += 12;
                //    PozY += (8 - 7);

                //    e.Graphics.DrawString(row["Specifikat"].ToString(), new System.Drawing.Font("Verdana", 8, FontStyle.Italic), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(0, PozY, 500, 500));
                //}
                PozY += 5;
                PozY += (8 - 7);

                e.Graphics.DrawString("__ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __", new System.Drawing.Font("Verdana", 7, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(0, PozY, 500, 500));
                NumroRreshtat++;
            }

            PozY += 12;

            e.Graphics.DrawString("Sum: ", new System.Drawing.Font("Verdana", 8, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(190, PozY, 500, 500));
            e.Graphics.DrawString(order.Total.ToString("0.00"), new System.Drawing.Font("Verdana", 8, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(240, PozY, 500, 500));


            //var textToPrint = "Hello world";
            //var printFont = new Font("Verdana", 7, FontStyle.Regular);
            //var leftMargin = e.MarginBounds.Left;
            //var topMargin = e.MarginBounds.Top;
            //PrintOrderDetails(e, textToPrint, printFont, leftMargin, topMargin);
        }

        private static string DecimalToInteger(string Numri)
        {
            try
            {
                string[] str = Numri.Split('.');
                int Nr = Convert.ToInt32(str[1]);
                if (Nr > 0)
                    return Numri;
                else
                    return str[0].ToString();
            }
            catch
            {
                return Numri;
            }

        }


        private static int PrintoHeaderinEPorosise(PrintPageEventArgs e, int PozY, Models.Order order)
        {
            System.Drawing.Font font = new System.Drawing.Font("Verdana", 7, FontStyle.Regular);
            e.Graphics.DrawString(string.Format("{0} {1}", order.username, order.Phone), new System.Drawing.Font("Verdana", 7, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(0, PozY, 500, 500));
            e.Graphics.DrawString(order.PaymentMethod.name, new System.Drawing.Font("Verdana", 7, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(200, PozY, 500, 500));
            PozY += 25;
            e.Graphics.DrawString("Address:", new System.Drawing.Font("Verdana", 7, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(0, PozY, 500, 500));
            e.Graphics.DrawString(order.Address, new System.Drawing.Font("Verdana", 7, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(70, PozY, 500, 500));

            PozY += 15;
            e.Graphics.DrawString("Order Nr:", new System.Drawing.Font("Verdana", 7, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(0, PozY, 500, 500));
            e.Graphics.DrawString(order.id.ToString(), new System.Drawing.Font("Verdana", 7, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(70, PozY, 500, 500));

            PozY += 15;
            e.Graphics.DrawString("Time:", new System.Drawing.Font("Verdana", 7, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(0, PozY, 500, 500));
            e.Graphics.DrawString(order.datetime.ToShortTimeString(), new System.Drawing.Font("Verdana", 7, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(70, PozY, 500, 500));

            PozY += 15;
            e.Graphics.DrawString("__ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __", new System.Drawing.Font("Verdana", 7, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(0, PozY, 500, 500));

            PozY += 15;
            e.Graphics.DrawString("Item", new System.Drawing.Font("Verdana", 7, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(0, PozY, 500, 500));
            e.Graphics.DrawString("Quantity", new System.Drawing.Font("Verdana", 7, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(140, PozY, 500, 500));
            e.Graphics.DrawString("Price", new System.Drawing.Font("Verdana", 7, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(190, PozY, 500, 500));
            e.Graphics.DrawString("Total", new System.Drawing.Font("Verdana", 7, FontStyle.Bold), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(240, PozY, 500, 500));
            PozY += 8;
            e.Graphics.DrawString("__ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __ __", new System.Drawing.Font("Verdana", 7, FontStyle.Regular), new SolidBrush(Color.FromArgb(64, 64, 64)), new Rectangle(0, PozY, 500, 500));
            return PozY;
        }


        private static void PrintOrderDetails(PrintPageEventArgs e, string textToPrint, Font printFont, int leftMargin, int topMargin)
        {
            e.Graphics.DrawString(textToPrint, printFont, Brushes.Black, leftMargin, topMargin);
        }

        public static string datetimeToString(DateTime datetime)
        {
            return string.Format("{0} {1}", datetime.ToShortDateString(), datetime.ToShortTimeString());
        }
    }
}