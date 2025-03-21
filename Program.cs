

//using System;
//using System.Diagnostics;
//using System.IO;
//using PdfSharp;
//using PdfSharp.Drawing;
//using PdfSharp.Pdf;
//using PdfSharp.Pdf.IO;


using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

using System.Collections.Generic;
using System.Drawing;
using System.Text;

using System.IO;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using datagen;
using ScottPlot.DataStructures;
using ScottPlot.Diagnostic;
using ScottPlot.Drawing;
using ScottPlot.MinMaxSearchStrategies;
using ScottPlot.plottables;
using ScottPlot.Renderable;
using ScottPlot.Statistics;



namespace ConsoleApp5
{

    class Program
    {

        private static void MakeScatterPlot(XGraphics gfx, double[] water_supply_temp, double[] water_return_temp, double[] water_supply_setpoint, double[] xx,  int x, int y, double scale, string name)
            {

            var plt = new ScottPlot.Plot(700, 400); 

            plt.PlotScatter(xx, water_supply_temp, markerSize: 0, lineWidth: 1, color: Color.DarkBlue);
            plt.PlotScatter(xx, water_return_temp, markerSize: 0, lineWidth: 1, color: Color.Yellow);
            plt.PlotScatter(xx, water_supply_setpoint, markerSize: 0, lineWidth: 1, color: Color.Blue);

            //plt.Frame(left: true, bottom: true, top: false, right: false);
            plt.Legend(location: ScottPlot.legendLocation.lowerLeft);
            plt.YLabel("Temperature(°C)");
            plt.XLabel("Dec 2020");

            plt.Grid(false);


            plt.SaveFig(String.Format("{0:C}.png", name));

            XImage image = XImage.FromFile(String.Format("C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/{0:C}.png", name));

            gfx.DrawImage(image, x, y, image.PixelWidth / scale, image.PixelHeight / scale);

        }

        private static void MakeDoubleBarPlot(XGraphics gfx, double[] ys1, double[] ys2, double[] err1, double[] err2, int x, int y, double scale, string name)
        {
            var plt = new ScottPlot.Plot(700, 400);


            string[] groupNames = { "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            string[] seriesNames = { "electricity ", "Gas",};



            plt.PlotBarGroups(
                groupLabels: groupNames,
                seriesLabels: seriesNames,
                ys: new double[][] { ys1, ys2 },
                yErr: new double[][] { err1, err2 });

            plt.YLabel("Electrical energy consumed (kWh)");
            plt.Grid(enableVertical: false, lineStyle: ScottPlot.LineStyle.Solid);
            plt.Ticks(dateTimeX: false, xTickRotation: 10);

            plt.Legend(location: ScottPlot.legendLocation.upperLeft);

            plt.SaveFig(String.Format("{0:C}.png", name));

            XImage image = XImage.FromFile(String.Format("C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/{0:C}.png", name));

            gfx.DrawImage(image, x, y, image.PixelWidth / scale, image.PixelHeight / scale);




        }


        private static void MakeBarPlot(XGraphics gfx, int x, int y, double scale, string name) {
            var plt = new ScottPlot.Plot(700, 400);

            string[] labels = { "March", "April", "May", "June", "July","August","September","October","November","December" };
            double[] yy = { 217, 103, 94, 53, 12, 48, 81, 180, 210, 199 };
            int barCount = labels.Length;
            Random rand = new Random(100);
            double[] xs = ScottPlot.DataGen.Consecutive(barCount);
            double[] ys = yy;//ScottPlot.DataGen.RandomNormal(rand, barCount, 200, 200);
            double[] yError = ScottPlot.DataGen.RandomNormal(rand, barCount, 0, 0);

            plt.YLabel("Gas consumed (m3)");

            plt.PlotBar(xs, ys, yError);
            plt.Grid(false);

            plt.XTicks(xs, labels);
            plt.Ticks(dateTimeX: false, xTickRotation: 10);

            plt.SaveFig(String.Format("{0:C}.png", name));

            XImage image = XImage.FromFile(String.Format("C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/{0:C}.png", name));

            gfx.DrawImage(image, x, y, image.PixelWidth / scale, image.PixelHeight / scale);




        }

        private static void MakePiePlot(XGraphics gfx, double[] values, string[] labels, int x, int y, double scale, string name)
        {

            var plt = new ScottPlot.Plot(700, 400);


            labels = Enumerable
                .Range(0, values.Length)
                .Select(i => $"{labels[i]}\n({values[i]})")
                .ToArray();

            plt.PlotPie(values, labels);

            plt.Grid(false);
            plt.Frame(false);
            plt.Ticks(false, false);
            plt.Legend(location: ScottPlot.legendLocation.upperLeft);

            plt.SaveFig(String.Format("{0:C}.png", name));

            XImage image = XImage.FromFile(String.Format("C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/{0:C}.png", name));

            gfx.DrawImage(image, x, y, image.PixelWidth / scale, image.PixelHeight / scale);




        }


        private static void DrawPageNum(XGraphics gfx, int number)
        {

            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);
            const string facename = "Arial";
            XFont fontBlackColontitul = new XFont(facename, 11, XFontStyle.Regular, options);
            gfx.DrawString("i.Leco © 29/01/2021", fontBlackColontitul, XBrushes.Black, 45, 800);
            gfx.DrawString(String.Format("{0:C}/7", number), fontBlackColontitul, XBrushes.Black, 535, 800);


        }



        private static void DrawTitleText(XGraphics gfx,string text, int x,int y)
        {
            const string facename = "Arial";
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);
            XFont fontLightBlue = new XFont(facename, 18, XFontStyle.Regular, options);
            gfx.DrawString(text, fontLightBlue, XBrushes.Aqua, x, y);//("Cordium: Real-time heat control ", fontLightBlue, XBrushes.Aqua,260, 95);
        }



        private static void DrawSimpleText(XGraphics gfx, string text,  int y)
        {
            const string facename = "Arial";
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);
            XFont fontBlack = new XFont(facename, 9, XFontStyle.Regular, options);
            gfx.DrawString(text, fontBlack, XBrushes.Black, 55, y);//450



        }

        private static void DrawMarkeredText(XGraphics gfx, string text,  int y)
        {
            const string facename = "Arial";
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);

            XFont fontBlack = new XFont(facename, 9, XFontStyle.Regular, options);
            gfx.DrawString(text, fontBlack, XBrushes.Black, 75, y);//460
            XPen pen = new XPen(XColors.Black, 2.5);
            gfx.DrawEllipse(pen, XBrushes.Black, 65, y-4, 1, 1);//456



        }

        private static void DrawSignatureText(XGraphics gfx, string text, int y)
        {
            const string facename = "Arial";
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);

            XFont fontBlack = new XFont(facename, 9, XFontStyle.Regular, options);
            gfx.DrawString(text, fontBlack, XBrushes.Black, 200, y);//460



        }




        private static void DrawImage(XGraphics gfx, string imgPath, int x, int y, double scale)
        {

            XImage image = XImage.FromFile(imgPath);

            gfx.DrawImage(image, x,y, image.PixelWidth / scale, image.PixelHeight / scale);

        }
        private static void DrawLightLine(XGraphics gfx, int y)
        {
            gfx.DrawLine(XPens.LightGray, 55, y, 545, y);
        }



        private static void DrawStrongLine(XGraphics gfx, int y)
        {
            gfx.DrawLine(XPens.Gray, 55, y, 545, y);
        }
        private static void DrawRectangle(XGraphics gfx, int number)
        {
            XPen pen = new XPen(XColors.Navy, Math.PI);

            gfx.DrawRectangle(pen, 10, 0, 100, 60);
            gfx.DrawRectangle(XBrushes.DarkOrange, 130, 0, 100, 60);
            gfx.DrawRectangle(pen, XBrushes.DarkOrange, 10, 80, 100, 60);
            gfx.DrawRectangle(pen, XBrushes.DarkOrange, 150, 80, 60, 60);
        }



        private static void DrawTitle(XGraphics gfx)
        {
            const string facename = "Arial";
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);
            XFont fontDarkBlue = new XFont(facename, 23, XFontStyle.Bold, options);
            XFont fontGray = new XFont(facename, 16, XFontStyle.Regular, options);

            gfx.DrawString("Energy Service Report", fontDarkBlue, XBrushes.DarkBlue, 260, 70);
            gfx.DrawString("December, 2020 ", fontGray, XBrushes.Gray, 410, 135);

            DrawImage(gfx, "C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/house1.jpg",310, 185, 5.5);
            DrawImage(gfx, "C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/leco.jpg", 55, 50, 5.5);
            DrawImage(gfx, "C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/cordium.jpg", 55, 185, 5.5);

            DrawStrongLine(gfx, 110);
            DrawLightLine(gfx, 145);
            DrawLightLine(gfx, 180);


            DrawTitleText(gfx, "Cordium: Real-time heat control ", 260, 95);
            DrawTitleText(gfx, "Period:", 55, 137);
            DrawTitleText(gfx, "Project Details:", 55, 173);

        }


        private static void DrawDiagram(XGraphics gfx)
        {


            gfx.DrawLine(XPens.Orange, 230, 160, 320, 160);

            gfx.DrawLine(XPens.Orange, 270, 160, 270, 235);//vertical

            gfx.DrawLine(XPens.Orange, 250, 235, 300, 235);

            gfx.DrawLine(XPens.Orange, 250, 235, 250, 300);//vertical

            gfx.DrawLine(XPens.Orange, 300, 235, 300, 300);//vertical




            gfx.DrawLine(XPens.Blue, 250, 300, 300, 300);

            gfx.DrawLine(XPens.Blue, 250, 300, 250, 250);//vertical

            gfx.DrawLine(XPens.Blue, 300, 250, 300, 300);//vertical


            gfx.DrawLine(XPens.Blue, 270, 300, 270, 330);//vertical




            XPen pen2 = new XPen(XColors.Orange, 2);

            gfx.DrawEllipse(pen2, XBrushes.Orange, 270-4, 160-4, 8, 8);

            XPen pen3 = new XPen(XColors.Blue, 2);
            gfx.DrawEllipse(pen3, XBrushes.Blue, 270-4, 300-4, 8, 8);





            XPen pen = new XPen(XColors.Black, Math.PI);
            DrawImage(gfx, "C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/buffer.jpg", 250, 190, 5.0);
            DrawImage(gfx, "C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/robur.jpg", 237, 245, 4.5);
            DrawImage(gfx, "C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/robur.jpg", 277, 245, 4.5);
            DrawImage(gfx, "C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/hous.jpg", 300, 135, 5.0);

            gfx.DrawRectangle(pen, 160, 140, 70, 40);
            gfx.DrawRectangle(pen, XBrushes.LightBlue, 240, 330, 70, 40);






            const string facename = "Arial";
            XPdfFontOptions options = new XPdfFontOptions(PdfFontEncoding.WinAnsi, PdfFontEmbedding.Default);
            XFont fontBlack = new XFont(facename, 10, XFontStyle.Regular, options);

            gfx.DrawString("Thermal net", fontBlack, XBrushes.Black, 165, 165);//450
            gfx.DrawString("Gas", fontBlack, XBrushes.Black, 265, 347);//450
            gfx.DrawString("Connection", fontBlack, XBrushes.Black, 245, 363);//450


            gfx.DrawString("Robur 1 & 2", fontBlack, XBrushes.Black, 155, 250);//450
            gfx.DrawString("2   X 37 kW", fontBlack, XBrushes.Black, 155, 265);//450
            gfx.DrawString("Heat", fontBlack, XBrushes.Black, 155, 280);//450



        }



        private static void DrawFirstPage(XGraphics gfx)
        {
            int y = 350;
            const int otstup = 14;

            const int TitleOtstup = 30;
            DrawTitle(gfx);
            DrawPageNum(gfx, 1);


            DrawMarkeredText(gfx, "Project Manager: Frank Louwet", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "Location: Crutzestraat, Hasselt", y);
            y = y + TitleOtstup;

            DrawTitleText(gfx, "Executive Summary:", 55, y);
            y = y + otstup;
            DrawLightLine(gfx, y);
            y = y + TitleOtstup;

            DrawSimpleText(gfx, "This month there were a total of 345 degree days ", y);
            y = y + otstup*2;

            DrawSimpleText(gfx, "Phase 1 heating energy:", y);
            y = y + otstup;

            DrawMarkeredText(gfx, "2196 kWh of gas consumed (0.32 kWh per apartment per degree day)", y);
            y = y + otstup*2;

            DrawSimpleText(gfx, "Phase 2 heating energy:", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "14515 kWh of gas consumed (2.1 kWh per apartment per degree day)", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "3.5 kWh of electricity consumed (0.00051 kWh per apartment per degree day)", y);
            y = y + otstup*2;


            DrawSimpleText(gfx, "Phase 3 heating energy and electricity production:", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "17791 kWh of gas consumed (1.8 kWh per apartment per degree day)", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "650 kWh of electricity produced", y);
            y = y + TitleOtstup;


            DrawTitleText(gfx, "Project Overview:", 55, y );
            y = y + otstup;
            DrawLightLine(gfx, y);
            y = y + TitleOtstup;



            DrawSimpleText(gfx, "The advanced control strategy is implemented in a district heating system for social housing in Crutzestraat, Hasselt.", y);
            y = y + otstup;

            DrawSimpleText(gfx,"The social housing is operated by Cordium, the operating manager for social housing in Flemish region.The project", y);
            y = y + otstup;

            DrawSimpleText(gfx, "consists of three phases or buildings with 20, 20 and 28 apartments in each phase.Each building has its own central", y);
            y = y + otstup;

            DrawSimpleText(gfx, "heating system with various technologies installed.Furthermore, central heating systems are interconnected by an", y);
            y = y + otstup;

            DrawSimpleText(gfx, "internal heat transfer network.i.Leco developed the control strategy which sends hourly setpoints fo: maximum and", y);
            y = y + otstup;

            DrawSimpleText(gfx, "minimum temperature setpoint in each building and/or distribution circuit, operation modes of installed technologies,", y);
            y = y + otstup;

            DrawSimpleText(gfx, "and distribution state settings between building/heating systems.", y);
            y = y + otstup;

        }







        private static void DrawSecondPage(XGraphics gfx)
        {
            int y = 70;
            const int otstup = 14;

            const int TitleOtstup = 30;
            DrawPageNum(gfx, 2);


            DrawTitleText(gfx, "Phase 1", 55, y);
            //y = y + otstup;
            //DrawLightLine(gfx, y);
            y = y + TitleOtstup;
            DrawSimpleText(gfx, "Installed technologies", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "Geothermal/water gas absorption heat pumps – 2 pcs", y);
            y = y + otstup;

            //DrawImage(gfx, "C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/Figure1.jpg", 180 , y,3.0);


            DrawDiagram(gfx);

            y = y + TitleOtstup*8;
            y = y + otstup;

            DrawSignatureText(gfx, "Figure 1: Phase 1 Energy Diagram", y);
            y = y + TitleOtstup ;

            DrawSimpleText(gfx, "This month 20 MWh of heating energy was provided to the phase 1 building by heat pumps. Consumption of gas", y);
            y = y + otstup;
            DrawSimpleText(gfx, "compared with previous months is shown below.", y);
            y = y + otstup;


            MakeBarPlot(gfx, 80, y, 1.6 ,"onePlot");

            y = y + TitleOtstup * 9;
            y = y + otstup;

            DrawSignatureText(gfx, "Figure 2: Phase 1 Energy Consumption monthly comparison", y);
            y = y + TitleOtstup;


        }


        private static void DrawThirdPage(XGraphics gfx)
        {
            int y = 70;
            const int otstup = 14;

            const int TitleOtstup = 30;
            DrawPageNum(gfx, 3);

            double[] water_supply_temp = { 61, 51, 59, 55, 52, 61, 55, 49, 51, 62, 53, 62, 55, 53, 53, 52, 51, 57, 54, 54, 58, 50, 53, 57, 58, 61, 65, 69, 67, 62, 69, 72, 72, 68, 66, 67, 70, 68, 58, 69, 66, 72, 69, 67, 59, 64, 69, 67, 60, 60, 53, 58, 52, 52, 50, 57, 57, 60, 60, 58, 52, 54, 49, 57, 60, 53, 56, 58, 51, 62, 52, 51, 59, 53, 55, 72, 69, 60, 67, 67, 67, 66, 60, 61, 67, 64, 71, 61, 58, 61, 70, 59, 58, 69, 72, 62, 68, 61, 63, 62, 62, 49, 59, 53, 53, 60, 53, 50, 54, 55, 60, 55, 53, 50, 58, 50, 57, 52, 48, 51, 61, 50, 53, 50, 61, 70, 63, 58, 61, 69, 68, 69, 64, 65, 71, 64, 59, 61, 71, 68, 71, 71, 59, 64, 69, 63, 61, 68, 66, 65, 62, 57, 48, 52, 51, 61, 53, 51, 50, 48, 57, 57, 60, 49, 52, 50, 48, 50, 53, 62, 55, 60, 62, 61, 62, 70, 71, 65, 60, 69, 70, 69, 61, 69, 69, 72, 58, 68, 67, 72, 59, 66, 64, 69, 67, 68, 69, 72, 71, 72, 59, 49, 52, 51, 50, 58, 55, 52, 61, 48, 62, 53, 58, 50, 62, 58, 48, 60, 54, 58, 55, 49, 54, 59, 61, 60, 65, 61, 61, 61, 62, 59, 58, 65, 63, 71, 63, 68, 66, 68, 72, 68, 66, 59, 71, 61, 65, 69, 71, 70, 55, 57, 62, 56, 51, 61, 62, 50, 57, 48, 48, 49, 52, 57, 62, 53, 60, 51, 52, 60, 51, 54, 58, 53, 52, 72, 68, 67, 68, 65, 59, 68, 67, 69, 66, 60, 70, 65, 62, 58, 58, 66, 59, 72, 65, 63, 62, 63, 71, 70, 50, 49, 62, 53, 56, 49, 60, 51, 62, 62, 61, 54, 61, 52, 51, 56, 58, 54, 55, 59, 48, 49, 58, 55, 61, 66, 62, 69, 62, 70, 66, 61, 58, 68, 61, 62, 66, 67, 70, 70, 67, 70, 59, 61, 65, 63, 61, 69, 62, 58, 52, 62, 56, 53, 58, 49, 54, 59, 51, 62, 48, 51, 55, 50, 59, 56, 55, 54, 55, 48, 56, 52, 48, 51, 56, 72, 59, 59, 68, 72, 68, 68, 62, 58, 59, 66, 72, 59, 61, 70, 62, 72, 64, 58, 67, 66, 68, 59, 59, 66, 60, 54, 53, 58, 57, 48, 50, 59, 62, 52, 51, 52, 60, 53, 57, 62, 57, 51, 49, 48, 50, 54, 53, 48, 54, 62, 61, 66, 58, 62, 60, 68, 58, 67, 65, 72, 58, 58, 58, 66, 66, 68, 61, 65, 68, 72, 62, 59, 61, 66, 58, 54, 56, 53, 58, 48, 49, 53, 49, 49, 51, 50, 52, 61, 50, 54, 55, 60, 50, 50, 52, 56, 57, 53, 56, 61, 67, 65, 65, 72, 72, 63, 65, 63, 62, 62, 64, 62, 72, 69, 62, 70, 62, 64, 64, 60, 68, 62, 62, 64, 59, 58, 53, 57, 54, 54, 60, 55, 62, 52, 56, 48, 58, 52, 56, 60, 49, 53, 58, 51, 48, 59, 54, 59, 49, 61, 59, 66, 70, 58, 58, 63, 68, 59, 61, 58, 62, 67, 58, 63, 71, 68, 70, 67, 60, 61, 72, 70, 59, 61, 60, 53, 48, 54, 50, 54, 51, 52, 48, 51, 55, 58, 61, 57, 58, 61, 57, 58, 59, 49, 50, 60, 58, 50, 59, 62, 69, 60, 71, 68, 58, 67, 58, 66, 68, 64, 68, 61, 65, 62, 59, 72, 61, 66, 59, 66, 65, 61, 70, 67, 49, 55, 49, 61, 62, 59, 58, 53, 49, 60, 61, 58, 61, 55, 58, 55, 50, 56, 53, 59, 59, 54, 56, 50, 52 };
            double[] water_return_temp = { 41, 39, 39, 40, 43, 43, 41, 39, 37, 39, 39, 37, 37, 41, 38, 41, 39, 43, 39, 40, 40, 37, 42, 39, 43, 52, 49, 52, 50, 54, 53, 53, 49, 48, 53, 52, 51, 52, 48, 53, 52, 54, 52, 49, 54, 49, 49, 51, 49, 49, 44, 44, 40, 45, 41, 45, 44, 41, 42, 39, 39, 44, 42, 45, 44, 39, 44, 42, 43, 40, 44, 41, 42, 44, 42, 52, 55, 51, 55, 54, 55, 55, 53, 54, 56, 55, 56, 52, 55, 52, 56, 55, 52, 53, 54, 51, 51, 53, 52, 51, 44, 47, 45, 45, 41, 41, 41, 47, 45, 46, 47, 47, 46, 47, 44, 46, 44, 46, 42, 43, 44, 47, 41, 41, 47, 54, 56, 53, 54, 56, 55, 56, 53, 57, 55, 56, 52, 57, 52, 52, 56, 54, 54, 57, 55, 52, 52, 54, 56, 55, 48, 49, 48, 48, 49, 43, 43, 48, 48, 49, 45, 48, 45, 44, 47, 43, 43, 46, 48, 49, 43, 48, 47, 46, 45, 54, 58, 60, 57, 56, 57, 58, 60, 56, 60, 56, 54, 55, 58, 54, 55, 57, 60, 55, 60, 56, 54, 58, 55, 59, 46, 49, 50, 46, 51, 50, 49, 50, 50, 46, 47, 47, 49, 47, 46, 50, 48, 49, 50, 47, 50, 47, 48, 45, 49, 57, 58, 61, 58, 56, 57, 56, 61, 57, 56, 62, 58, 58, 61, 60, 61, 58, 59, 60, 57, 56, 56, 57, 60, 60, 49, 49, 49, 52, 53, 51, 50, 48, 47, 51, 48, 47, 53, 50, 52, 49, 47, 49, 49, 51, 53, 52, 51, 50, 51, 60, 60, 64, 59, 60, 58, 62, 59, 62, 64, 60, 59, 61, 61, 61, 63, 63, 60, 61, 60, 62, 59, 61, 60, 63, 50, 50, 55, 52, 52, 53, 53, 50, 53, 52, 54, 52, 55, 53, 49, 54, 55, 55, 51, 55, 50, 53, 54, 51, 51, 64, 66, 64, 65, 62, 64, 61, 61, 65, 66, 65, 63, 65, 66, 64, 66, 60, 63, 60, 65, 61, 65, 64, 62, 66, 51, 53, 51, 53, 56, 56, 56, 51, 55, 54, 53, 56, 53, 56, 52, 53, 57, 56, 51, 54, 54, 51, 55, 53, 51, 67, 67, 62, 65, 62, 63, 68, 63, 65, 67, 65, 62, 64, 67, 67, 64, 66, 65, 64, 67, 68, 66, 66, 67, 66, 56, 56, 55, 56, 55, 55, 59, 57, 56, 55, 59, 59, 55, 59, 54, 58, 59, 57, 56, 59, 59, 54, 53, 54, 55, 68, 64, 70, 67, 68, 65, 68, 69, 64, 67, 64, 69, 68, 70, 65, 65, 66, 70, 64, 70, 69, 67, 64, 66, 67, 61, 59, 57, 60, 60, 59, 57, 56, 56, 61, 58, 58, 59, 60, 55, 55, 61, 59, 55, 60, 59, 61, 60, 61, 57, 71, 67, 72, 71, 70, 67, 70, 67, 70, 72, 68, 70, 70, 66, 68, 68, 66, 70, 68, 67, 70, 69, 71, 67, 70, 59, 62, 61, 62, 58, 57, 62, 57, 61, 62, 63, 57, 57, 59, 58, 58, 62, 62, 61, 62, 62, 61, 61, 57, 59, 70, 69, 70, 70, 69, 72, 68, 71, 74, 70, 74, 72, 74, 68, 74, 70, 68, 73, 70, 70, 71, 69, 70, 71, 70, 59, 64, 65, 64, 59, 61, 59, 63, 65, 64, 62, 64, 65, 60, 64, 60, 60, 59, 61, 64, 60, 61, 62, 63, 65, 72, 70, 73, 74, 73, 75, 71, 72, 75, 75, 71, 70, 70, 71, 72, 72, 73, 71, 73, 70, 70, 70, 76, 76, 74, 66, 61, 62, 67, 66, 63, 65, 67, 61, 64, 64, 62, 62, 63, 65, 67, 61, 63, 66, 67, 61, 65, 61, 64, 67 };
            double[] water_supply_setpoint = { 60, 59, 62, 61, 60, 62, 62, 62, 61, 60, 59, 60, 59, 62, 61, 59, 61, 59, 62, 60, 62, 60, 60, 60, 61, 59, 61, 59, 60, 60, 59, 60, 60, 59, 61, 60, 60, 62, 59, 59, 62, 59, 61, 61, 61, 59, 60, 62, 62, 60, 62, 59, 60, 59, 62, 62, 62, 61, 61, 62, 61, 61, 62, 61, 61, 59, 59, 60, 60, 60, 60, 62, 61, 60, 61, 59, 60, 60, 59, 62, 59, 59, 61, 62, 60, 59, 59, 61, 62, 60, 61, 59, 61, 61, 62, 61, 62, 62, 61, 60, 61, 61, 61, 61, 61, 60, 60, 59, 59, 59, 61, 61, 62, 60, 59, 62, 61, 60, 60, 60, 62, 59, 59, 61, 61, 61, 62, 61, 61, 60, 62, 59, 60, 59, 59, 59, 59, 61, 61, 60, 60, 59, 62, 60, 59, 61, 60, 59, 62, 60, 62, 59, 59, 61, 59, 62, 60, 61, 59, 61, 62, 60, 59, 61, 62, 62, 60, 61, 59, 60, 61, 59, 61, 62, 61, 59, 62, 61, 59, 59, 62, 60, 59, 59, 61, 61, 60, 62, 62, 61, 61, 61, 61, 59, 62, 60, 62, 62, 59, 61, 62, 62, 60, 61, 61, 60, 62, 60, 61, 62, 61, 62, 62, 60, 61, 61, 61, 60, 60, 59, 62, 60, 62, 60, 61, 61, 61, 61, 62, 62, 62, 61, 59, 60, 61, 59, 60, 60, 62, 60, 61, 62, 59, 59, 60, 59, 59, 59, 62, 60, 61, 60, 60, 61, 60, 59, 61, 62, 62, 62, 60, 61, 59, 61, 61, 62, 59, 62, 60, 61, 59, 62, 59, 61, 60, 61, 62, 60, 59, 62, 60, 59, 62, 62, 62, 61, 61, 60, 59, 61, 61, 62, 61, 61, 60, 61, 59, 62, 62, 62, 60, 62, 62, 60, 62, 59, 62, 62, 61, 62, 59, 60, 62, 61, 59, 62, 60, 61, 60, 61, 61, 62, 60, 62, 60, 59, 61, 59, 60, 60, 61, 61, 62, 61, 60, 62, 59, 61, 59, 62, 62, 60, 61, 60, 59, 61, 62, 59, 62, 62, 62, 60, 62, 61, 62, 60, 60, 59, 61, 59, 62, 59, 61, 62, 59, 59, 61, 62, 61, 59, 60, 60, 61, 61, 59, 61, 62, 62, 59, 61, 60, 61, 62, 62, 61, 60, 62, 60, 62, 61, 61, 62, 59, 61, 62, 59, 60, 61, 59, 62, 59, 59, 62, 62, 59, 59, 62, 62, 60, 61, 62, 60, 60, 61, 61, 62, 61, 60, 60, 61, 62, 61, 62, 62, 62, 61, 59, 59, 60, 60, 59, 61, 60, 60, 59, 61, 61, 62, 61, 60, 59, 60, 61, 59, 62, 61, 60, 59, 60, 60, 59, 60, 60, 61, 60, 62, 61, 59, 61, 62, 60, 61, 62, 59, 61, 61, 59, 61, 62, 61, 62, 61, 59, 60, 60, 62, 62, 62, 64, 61, 64, 62, 63, 64, 62, 64, 62, 61, 64, 62, 61, 64, 61, 61, 64, 62, 62, 64, 63, 62, 64, 63, 63, 61, 61, 63, 61, 62, 64, 62, 62, 64, 63, 61, 62, 61, 63, 63, 62, 64, 62, 62, 62, 62, 64, 62, 64, 64, 62, 64, 63, 64, 63, 63, 61, 62, 64, 63, 64, 61, 63, 63, 62, 64, 62, 63, 63, 61, 64, 62, 62, 64, 63, 63, 63, 62, 63, 63, 63, 62, 62, 63, 63, 61, 63, 61, 62, 61, 62, 62, 64, 64, 63, 61, 62, 62, 63, 61, 61, 62, 61, 64, 64, 63, 61, 63, 61, 62, 63, 63, 61, 62, 62, 62, 63, 61, 63, 64, 62, 61, 64, 64, 62, 62, 63, 63, 64, 64, 61, 61, 62, 64, 61, 62, 61, 63, 62, 63, 61, 63, 62, 62, 64, 61, 64 };
            double[] xx = { 0, 0.05, 0.1, 0.15, 0.2, 0.26, 0.31, 0.36, 0.41, 0.46, 0.51, 0.56, 0.61, 0.67, 0.72, 0.77, 0.82, 0.87, 0.92, 0.97, 1.02, 1.08, 1.13, 1.18, 1.23, 1.28, 1.33, 1.38, 1.43, 1.48, 1.54, 1.59, 1.64, 1.69, 1.74, 1.79, 1.84, 1.89, 1.95, 2.0, 2.05, 2.1, 2.15, 2.2, 2.25, 2.3, 2.36, 2.41, 2.46, 2.51, 2.56, 2.61, 2.66, 2.71, 2.76, 2.82, 2.87, 2.92, 2.97, 3.02, 3.07, 3.12, 3.17, 3.23, 3.28, 3.33, 3.38, 3.43, 3.48, 3.53, 3.58, 3.64, 3.69, 3.74, 3.79, 3.84, 3.89, 3.94, 3.99, 4.04, 4.1, 4.15, 4.2, 4.25, 4.3, 4.35, 4.4, 4.45, 4.51, 4.56, 4.61, 4.66, 4.71, 4.76, 4.81, 4.86, 4.92, 4.97, 5.02, 5.07, 5.12, 5.17, 5.22, 5.27, 5.32, 5.38, 5.43, 5.48, 5.53, 5.58, 5.63, 5.68, 5.73, 5.79, 5.84, 5.89, 5.94, 5.99, 6.04, 6.09, 6.14, 6.2, 6.25, 6.3, 6.35, 6.4, 6.45, 6.5, 6.55, 6.6, 6.66, 6.71, 6.76, 6.81, 6.86, 6.91, 6.96, 7.01, 7.07, 7.12, 7.17, 7.22, 7.27, 7.32, 7.37, 7.42, 7.48, 7.53, 7.58, 7.63, 7.68, 7.73, 7.78, 7.83, 7.88, 7.94, 7.99, 8.04, 8.09, 8.14, 8.19, 8.24, 8.29, 8.35, 8.4, 8.45, 8.5, 8.55, 8.6, 8.65, 8.7, 8.76, 8.81, 8.86, 8.91, 8.96, 9.01, 9.06, 9.11, 9.16, 9.22, 9.27, 9.32, 9.37, 9.42, 9.47, 9.52, 9.57, 9.63, 9.68, 9.73, 9.78, 9.83, 9.88, 9.93, 9.98, 10.04, 10.09, 10.14, 10.19, 10.24, 10.29, 10.34, 10.39, 10.44, 10.5, 10.55, 10.6, 10.65, 10.7, 10.75, 10.8, 10.85, 10.91, 10.96, 11.01, 11.06, 11.11, 11.16, 11.21, 11.26, 11.32, 11.37, 11.42, 11.47, 11.52, 11.57, 11.62, 11.67, 11.72, 11.78, 11.83, 11.88, 11.93, 11.98, 12.03, 12.08, 12.13, 12.19, 12.24, 12.29, 12.34, 12.39, 12.44, 12.49, 12.54, 12.6, 12.65, 12.7, 12.75, 12.8, 12.85, 12.9, 12.95, 13.0, 13.06, 13.11, 13.16, 13.21, 13.26, 13.31, 13.36, 13.41, 13.47, 13.52, 13.57, 13.62, 13.67, 13.72, 13.77, 13.82, 13.88, 13.93, 13.98, 14.03, 14.08, 14.13, 14.18, 14.23, 14.28, 14.34, 14.39, 14.44, 14.49, 14.54, 14.59, 14.64, 14.69, 14.75, 14.8, 14.85, 14.9, 14.95, 15.0, 15.05, 15.1, 15.16, 15.21, 15.26, 15.31, 15.36, 15.41, 15.46, 15.51, 15.56, 15.62, 15.67, 15.72, 15.77, 15.82, 15.87, 15.92, 15.97, 16.03, 16.08, 16.13, 16.18, 16.23, 16.28, 16.33, 16.38, 16.44, 16.49, 16.54, 16.59, 16.64, 16.69, 16.74, 16.79, 16.84, 16.9, 16.95, 17.0, 17.05, 17.1, 17.15, 17.2, 17.25, 17.31, 17.36, 17.41, 17.46, 17.51, 17.56, 17.61, 17.66, 17.72, 17.77, 17.82, 17.87, 17.92, 17.97, 18.02, 18.07, 18.12, 18.18, 18.23, 18.28, 18.33, 18.38, 18.43, 18.48, 18.53, 18.59, 18.64, 18.69, 18.74, 18.79, 18.84, 18.89, 18.94, 19.0, 19.05, 19.1, 19.15, 19.2, 19.25, 19.3, 19.35, 19.4, 19.46, 19.51, 19.56, 19.61, 19.66, 19.71, 19.76, 19.81, 19.87, 19.92, 19.97, 20.02, 20.07, 20.12, 20.17, 20.22, 20.28, 20.33, 20.38, 20.43, 20.48, 20.53, 20.58, 20.63, 20.68, 20.74, 20.79, 20.84, 20.89, 20.94, 20.99, 21.04, 21.09, 21.15, 21.2, 21.25, 21.3, 21.35, 21.4, 21.45, 21.5, 21.56, 21.61, 21.66, 21.71, 21.76, 21.81, 21.86, 21.91, 21.96, 22.02, 22.07, 22.12, 22.17, 22.22, 22.27, 22.32, 22.37, 22.43, 22.48, 22.53, 22.58, 22.63, 22.68, 22.73, 22.78, 22.84, 22.89, 22.94, 22.99, 23.04, 23.09, 23.14, 23.19, 23.24, 23.3, 23.35, 23.4, 23.45, 23.5, 23.55, 23.6, 23.65, 23.71, 23.76, 23.81, 23.86, 23.91, 23.96, 24.01, 24.06, 24.12, 24.17, 24.22, 24.27, 24.32, 24.37, 24.42, 24.47, 24.52, 24.58, 24.63, 24.68, 24.73, 24.78, 24.83, 24.88, 24.93, 24.99, 25.04, 25.09, 25.14, 25.19, 25.24, 25.29, 25.34, 25.4, 25.45, 25.5, 25.55, 25.6, 25.65, 25.7, 25.75, 25.8, 25.86, 25.91, 25.96, 26.01, 26.06, 26.11, 26.16, 26.21, 26.27, 26.32, 26.37, 26.42, 26.47, 26.52, 26.57, 26.62, 26.68, 26.73, 26.78, 26.83, 26.88, 26.93, 26.98, 27.03, 27.08, 27.14, 27.19, 27.24, 27.29, 27.34, 27.39, 27.44, 27.49, 27.55, 27.6, 27.65, 27.7, 27.75, 27.8, 27.85, 27.9, 27.96, 28.01, 28.06, 28.11, 28.16, 28.21, 28.26, 28.31, 28.36, 28.42, 28.47, 28.52, 28.57, 28.62, 28.67, 28.72, 28.77, 28.83, 28.88, 28.93, 28.98, 29.03, 29.08, 29.13, 29.18, 29.24, 29.29, 29.34, 29.39, 29.44, 29.49, 29.54, 29.59, 29.64, 29.7, 29.75, 29.8, 29.85, 29.9, 29.95, 30.0, 30.05, 30.11, 30.16, 30.21, 30.26, 30.31, 30.36, 30.41, 30.46, 30.52, 30.57, 30.62, 30.67, 30.72, 30.77, 30.82, 30.87, 30.92, 30.98, 31.03, 31.08, 31.13, 31.18, 31.23, 31.28, 31.33, 31.39, 31.44, 31.49, 31.54, 31.59, 31.64, 31.69, 31.74, 31.8, 31.85, 31.9, 31.95 };

            MakeScatterPlot(gfx, water_supply_temp, water_return_temp, water_supply_setpoint, xx, 80, y, 1.6, "2plot");

            y = y + TitleOtstup * 9;
            y = y + otstup;
            
            DrawSignatureText(gfx, "Figure 3: Phase 1 Control (December, 2020)", y);
            y = y + TitleOtstup;

            DrawSimpleText(gfx, "This month 20 MWh of heating energy was provided to the phase 1 building by heat pumps. Consumption of gas", y);
            y = y + otstup;


        }







        private static void DrawForthPage(XGraphics gfx)
        {
            int y = 70;
            const int otstup = 14;

            const int TitleOtstup = 30;
            DrawPageNum(gfx, 4);


            DrawTitleText(gfx, "Phase 2", 55, y);
            //y = y + otstup;
            //DrawLightLine(gfx, y);
            y = y + TitleOtstup;
            DrawSimpleText(gfx, "Installed technologies:", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "Electrical air/water heat pump", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "Electrical geothermal/water heat pump", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "Geothermal/water gas absorption heat pump", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "Gas condensing boiler", y);
            y = y + otstup;


            DrawImage(gfx, "C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/Figure2.jpg", 100, y, 3.1);
            y = y + TitleOtstup * 8;
            y = y + otstup;
            y = y + otstup;
            DrawSignatureText(gfx, "Figure 4: Phase 2 Energy Diagram", y);
            y = y + TitleOtstup;

            DrawSimpleText(gfx, "The electrical and gas energy consumed by the installed technologies this month is shown below:", y);
            y = y + otstup;


            double[] values = { 4134, 10380,3 };
            string[] labels = { "Robur (gas)", "Boiler (gas)","Heat pumps(electricity)" };

            MakePiePlot(gfx, values, labels, 80, y, 1.6, "3plot");
            y = y + otstup;
            y = y + TitleOtstup * 8;


            DrawSignatureText(gfx, "Figure 5: Phase 2 Energy Consumption", y);
            y = y + TitleOtstup;

            DrawSimpleText(gfx, "This month 19.5 MWh of heating energy was provided to the phase 2 building by heat pumps and the gas boiler.", y);
            y = y + otstup;

            DrawSimpleText(gfx, "Consumption of electricity and gas compared with previous months is shown below.", y);
            y = y + otstup;

        }




        private static void DrawFifthPage(XGraphics gfx)
        {
            int y = 70;
            const int otstup = 14;

            const int TitleOtstup = 30;
            DrawPageNum(gfx, 5);

            int groupCount = 10;
            Random rand = new Random(0);
            double[] ys1 = ScottPlot.DataGen.RandomNormal(rand, groupCount, 600, 180);
            double[] ys2 = ScottPlot.DataGen.RandomNormal(rand, groupCount, 600, 250);
            double[] err1 = ScottPlot.DataGen.RandomNormal(rand, groupCount, 0, 0);
            double[] err2 = ScottPlot.DataGen.RandomNormal(rand, groupCount, 0, 0);

            MakeDoubleBarPlot(gfx, ys1, ys2, err1, err2, 80,  y, 1.6, "Figure6Phase2");
            y = y + TitleOtstup * 9;
            y = y + otstup;

            DrawSignatureText(gfx, "Figure 6: Phase 2 Energy Consumption monthly comparison", y);
            y = y + TitleOtstup;


            double[] water_supply_temp = { 61, 51, 59, 55, 52, 61, 55, 49, 51, 62, 53, 62, 55, 53, 53, 52, 51, 57, 54, 54, 58, 50, 53, 57, 58, 61, 65, 69, 67, 62, 69, 72, 72, 68, 66, 67, 70, 68, 58, 69, 66, 72, 69, 67, 59, 64, 69, 67, 60, 60, 53, 58, 52, 52, 50, 57, 57, 60, 60, 58, 52, 54, 49, 57, 60, 53, 56, 58, 51, 62, 52, 51, 59, 53, 55, 72, 69, 60, 67, 67, 67, 66, 60, 61, 67, 64, 71, 61, 58, 61, 70, 59, 58, 69, 72, 62, 68, 61, 63, 62, 62, 49, 59, 53, 53, 60, 53, 50, 54, 55, 60, 55, 53, 50, 58, 50, 57, 52, 48, 51, 61, 50, 53, 50, 61, 70, 63, 58, 61, 69, 68, 69, 64, 65, 71, 64, 59, 61, 71, 68, 71, 71, 59, 64, 69, 63, 61, 68, 66, 65, 62, 57, 48, 52, 51, 61, 53, 51, 50, 48, 57, 57, 60, 49, 52, 50, 48, 50, 53, 62, 55, 60, 62, 61, 62, 70, 71, 65, 60, 69, 70, 69, 61, 69, 69, 72, 58, 68, 67, 72, 59, 66, 64, 69, 67, 68, 69, 72, 71, 72, 59, 49, 52, 51, 50, 58, 55, 52, 61, 48, 62, 53, 58, 50, 62, 58, 48, 60, 54, 58, 55, 49, 54, 59, 61, 60, 65, 61, 61, 61, 62, 59, 58, 65, 63, 71, 63, 68, 66, 68, 72, 68, 66, 59, 71, 61, 65, 69, 71, 70, 55, 57, 62, 56, 51, 61, 62, 50, 57, 48, 48, 49, 52, 57, 62, 53, 60, 51, 52, 60, 51, 54, 58, 53, 52, 72, 68, 67, 68, 65, 59, 68, 67, 69, 66, 60, 70, 65, 62, 58, 58, 66, 59, 72, 65, 63, 62, 63, 71, 70, 50, 49, 62, 53, 56, 49, 60, 51, 62, 62, 61, 54, 61, 52, 51, 56, 58, 54, 55, 59, 48, 49, 58, 55, 61, 66, 62, 69, 62, 70, 66, 61, 58, 68, 61, 62, 66, 67, 70, 70, 67, 70, 59, 61, 65, 63, 61, 69, 62, 58, 52, 62, 56, 53, 58, 49, 54, 59, 51, 62, 48, 51, 55, 50, 59, 56, 55, 54, 55, 48, 56, 52, 48, 51, 56, 72, 59, 59, 68, 72, 68, 68, 62, 58, 59, 66, 72, 59, 61, 70, 62, 72, 64, 58, 67, 66, 68, 59, 59, 66, 60, 54, 53, 58, 57, 48, 50, 59, 62, 52, 51, 52, 60, 53, 57, 62, 57, 51, 49, 48, 50, 54, 53, 48, 54, 62, 61, 66, 58, 62, 60, 68, 58, 67, 65, 72, 58, 58, 58, 66, 66, 68, 61, 65, 68, 72, 62, 59, 61, 66, 58, 54, 56, 53, 58, 48, 49, 53, 49, 49, 51, 50, 52, 61, 50, 54, 55, 60, 50, 50, 52, 56, 57, 53, 56, 61, 67, 65, 65, 72, 72, 63, 65, 63, 62, 62, 64, 62, 72, 69, 62, 70, 62, 64, 64, 60, 68, 62, 62, 64, 59, 58, 53, 57, 54, 54, 60, 55, 62, 52, 56, 48, 58, 52, 56, 60, 49, 53, 58, 51, 48, 59, 54, 59, 49, 61, 59, 66, 70, 58, 58, 63, 68, 59, 61, 58, 62, 67, 58, 63, 71, 68, 70, 67, 60, 61, 72, 70, 59, 61, 60, 53, 48, 54, 50, 54, 51, 52, 48, 51, 55, 58, 61, 57, 58, 61, 57, 58, 59, 49, 50, 60, 58, 50, 59, 62, 69, 60, 71, 68, 58, 67, 58, 66, 68, 64, 68, 61, 65, 62, 59, 72, 61, 66, 59, 66, 65, 61, 70, 67, 49, 55, 49, 61, 62, 59, 58, 53, 49, 60, 61, 58, 61, 55, 58, 55, 50, 56, 53, 59, 59, 54, 56, 50, 52 };
            double[] water_return_temp = { 41, 39, 39, 40, 43, 43, 41, 39, 37, 39, 39, 37, 37, 41, 38, 41, 39, 43, 39, 40, 40, 37, 42, 39, 43, 52, 49, 52, 50, 54, 53, 53, 49, 48, 53, 52, 51, 52, 48, 53, 52, 54, 52, 49, 54, 49, 49, 51, 49, 49, 44, 44, 40, 45, 41, 45, 44, 41, 42, 39, 39, 44, 42, 45, 44, 39, 44, 42, 43, 40, 44, 41, 42, 44, 42, 52, 55, 51, 55, 54, 55, 55, 53, 54, 56, 55, 56, 52, 55, 52, 56, 55, 52, 53, 54, 51, 51, 53, 52, 51, 44, 47, 45, 45, 41, 41, 41, 47, 45, 46, 47, 47, 46, 47, 44, 46, 44, 46, 42, 43, 44, 47, 41, 41, 47, 54, 56, 53, 54, 56, 55, 56, 53, 57, 55, 56, 52, 57, 52, 52, 56, 54, 54, 57, 55, 52, 52, 54, 56, 55, 48, 49, 48, 48, 49, 43, 43, 48, 48, 49, 45, 48, 45, 44, 47, 43, 43, 46, 48, 49, 43, 48, 47, 46, 45, 54, 58, 60, 57, 56, 57, 58, 60, 56, 60, 56, 54, 55, 58, 54, 55, 57, 60, 55, 60, 56, 54, 58, 55, 59, 46, 49, 50, 46, 51, 50, 49, 50, 50, 46, 47, 47, 49, 47, 46, 50, 48, 49, 50, 47, 50, 47, 48, 45, 49, 57, 58, 61, 58, 56, 57, 56, 61, 57, 56, 62, 58, 58, 61, 60, 61, 58, 59, 60, 57, 56, 56, 57, 60, 60, 49, 49, 49, 52, 53, 51, 50, 48, 47, 51, 48, 47, 53, 50, 52, 49, 47, 49, 49, 51, 53, 52, 51, 50, 51, 60, 60, 64, 59, 60, 58, 62, 59, 62, 64, 60, 59, 61, 61, 61, 63, 63, 60, 61, 60, 62, 59, 61, 60, 63, 50, 50, 55, 52, 52, 53, 53, 50, 53, 52, 54, 52, 55, 53, 49, 54, 55, 55, 51, 55, 50, 53, 54, 51, 51, 64, 66, 64, 65, 62, 64, 61, 61, 65, 66, 65, 63, 65, 66, 64, 66, 60, 63, 60, 65, 61, 65, 64, 62, 66, 51, 53, 51, 53, 56, 56, 56, 51, 55, 54, 53, 56, 53, 56, 52, 53, 57, 56, 51, 54, 54, 51, 55, 53, 51, 67, 67, 62, 65, 62, 63, 68, 63, 65, 67, 65, 62, 64, 67, 67, 64, 66, 65, 64, 67, 68, 66, 66, 67, 66, 56, 56, 55, 56, 55, 55, 59, 57, 56, 55, 59, 59, 55, 59, 54, 58, 59, 57, 56, 59, 59, 54, 53, 54, 55, 68, 64, 70, 67, 68, 65, 68, 69, 64, 67, 64, 69, 68, 70, 65, 65, 66, 70, 64, 70, 69, 67, 64, 66, 67, 61, 59, 57, 60, 60, 59, 57, 56, 56, 61, 58, 58, 59, 60, 55, 55, 61, 59, 55, 60, 59, 61, 60, 61, 57, 71, 67, 72, 71, 70, 67, 70, 67, 70, 72, 68, 70, 70, 66, 68, 68, 66, 70, 68, 67, 70, 69, 71, 67, 70, 59, 62, 61, 62, 58, 57, 62, 57, 61, 62, 63, 57, 57, 59, 58, 58, 62, 62, 61, 62, 62, 61, 61, 57, 59, 70, 69, 70, 70, 69, 72, 68, 71, 74, 70, 74, 72, 74, 68, 74, 70, 68, 73, 70, 70, 71, 69, 70, 71, 70, 59, 64, 65, 64, 59, 61, 59, 63, 65, 64, 62, 64, 65, 60, 64, 60, 60, 59, 61, 64, 60, 61, 62, 63, 65, 72, 70, 73, 74, 73, 75, 71, 72, 75, 75, 71, 70, 70, 71, 72, 72, 73, 71, 73, 70, 70, 70, 76, 76, 74, 66, 61, 62, 67, 66, 63, 65, 67, 61, 64, 64, 62, 62, 63, 65, 67, 61, 63, 66, 67, 61, 65, 61, 64, 67 };
            double[] water_supply_setpoint = { 60, 59, 62, 61, 60, 62, 62, 62, 61, 60, 59, 60, 59, 62, 61, 59, 61, 59, 62, 60, 62, 60, 60, 60, 61, 59, 61, 59, 60, 60, 59, 60, 60, 59, 61, 60, 60, 62, 59, 59, 62, 59, 61, 61, 61, 59, 60, 62, 62, 60, 62, 59, 60, 59, 62, 62, 62, 61, 61, 62, 61, 61, 62, 61, 61, 59, 59, 60, 60, 60, 60, 62, 61, 60, 61, 59, 60, 60, 59, 62, 59, 59, 61, 62, 60, 59, 59, 61, 62, 60, 61, 59, 61, 61, 62, 61, 62, 62, 61, 60, 61, 61, 61, 61, 61, 60, 60, 59, 59, 59, 61, 61, 62, 60, 59, 62, 61, 60, 60, 60, 62, 59, 59, 61, 61, 61, 62, 61, 61, 60, 62, 59, 60, 59, 59, 59, 59, 61, 61, 60, 60, 59, 62, 60, 59, 61, 60, 59, 62, 60, 62, 59, 59, 61, 59, 62, 60, 61, 59, 61, 62, 60, 59, 61, 62, 62, 60, 61, 59, 60, 61, 59, 61, 62, 61, 59, 62, 61, 59, 59, 62, 60, 59, 59, 61, 61, 60, 62, 62, 61, 61, 61, 61, 59, 62, 60, 62, 62, 59, 61, 62, 62, 60, 61, 61, 60, 62, 60, 61, 62, 61, 62, 62, 60, 61, 61, 61, 60, 60, 59, 62, 60, 62, 60, 61, 61, 61, 61, 62, 62, 62, 61, 59, 60, 61, 59, 60, 60, 62, 60, 61, 62, 59, 59, 60, 59, 59, 59, 62, 60, 61, 60, 60, 61, 60, 59, 61, 62, 62, 62, 60, 61, 59, 61, 61, 62, 59, 62, 60, 61, 59, 62, 59, 61, 60, 61, 62, 60, 59, 62, 60, 59, 62, 62, 62, 61, 61, 60, 59, 61, 61, 62, 61, 61, 60, 61, 59, 62, 62, 62, 60, 62, 62, 60, 62, 59, 62, 62, 61, 62, 59, 60, 62, 61, 59, 62, 60, 61, 60, 61, 61, 62, 60, 62, 60, 59, 61, 59, 60, 60, 61, 61, 62, 61, 60, 62, 59, 61, 59, 62, 62, 60, 61, 60, 59, 61, 62, 59, 62, 62, 62, 60, 62, 61, 62, 60, 60, 59, 61, 59, 62, 59, 61, 62, 59, 59, 61, 62, 61, 59, 60, 60, 61, 61, 59, 61, 62, 62, 59, 61, 60, 61, 62, 62, 61, 60, 62, 60, 62, 61, 61, 62, 59, 61, 62, 59, 60, 61, 59, 62, 59, 59, 62, 62, 59, 59, 62, 62, 60, 61, 62, 60, 60, 61, 61, 62, 61, 60, 60, 61, 62, 61, 62, 62, 62, 61, 59, 59, 60, 60, 59, 61, 60, 60, 59, 61, 61, 62, 61, 60, 59, 60, 61, 59, 62, 61, 60, 59, 60, 60, 59, 60, 60, 61, 60, 62, 61, 59, 61, 62, 60, 61, 62, 59, 61, 61, 59, 61, 62, 61, 62, 61, 59, 60, 60, 62, 62, 62, 64, 61, 64, 62, 63, 64, 62, 64, 62, 61, 64, 62, 61, 64, 61, 61, 64, 62, 62, 64, 63, 62, 64, 63, 63, 61, 61, 63, 61, 62, 64, 62, 62, 64, 63, 61, 62, 61, 63, 63, 62, 64, 62, 62, 62, 62, 64, 62, 64, 64, 62, 64, 63, 64, 63, 63, 61, 62, 64, 63, 64, 61, 63, 63, 62, 64, 62, 63, 63, 61, 64, 62, 62, 64, 63, 63, 63, 62, 63, 63, 63, 62, 62, 63, 63, 61, 63, 61, 62, 61, 62, 62, 64, 64, 63, 61, 62, 62, 63, 61, 61, 62, 61, 64, 64, 63, 61, 63, 61, 62, 63, 63, 61, 62, 62, 62, 63, 61, 63, 64, 62, 61, 64, 64, 62, 62, 63, 63, 64, 64, 61, 61, 62, 64, 61, 62, 61, 63, 62, 63, 61, 63, 62, 62, 64, 61, 64 };
            double[] xx = { 0, 0.05, 0.1, 0.15, 0.2, 0.26, 0.31, 0.36, 0.41, 0.46, 0.51, 0.56, 0.61, 0.67, 0.72, 0.77, 0.82, 0.87, 0.92, 0.97, 1.02, 1.08, 1.13, 1.18, 1.23, 1.28, 1.33, 1.38, 1.43, 1.48, 1.54, 1.59, 1.64, 1.69, 1.74, 1.79, 1.84, 1.89, 1.95, 2.0, 2.05, 2.1, 2.15, 2.2, 2.25, 2.3, 2.36, 2.41, 2.46, 2.51, 2.56, 2.61, 2.66, 2.71, 2.76, 2.82, 2.87, 2.92, 2.97, 3.02, 3.07, 3.12, 3.17, 3.23, 3.28, 3.33, 3.38, 3.43, 3.48, 3.53, 3.58, 3.64, 3.69, 3.74, 3.79, 3.84, 3.89, 3.94, 3.99, 4.04, 4.1, 4.15, 4.2, 4.25, 4.3, 4.35, 4.4, 4.45, 4.51, 4.56, 4.61, 4.66, 4.71, 4.76, 4.81, 4.86, 4.92, 4.97, 5.02, 5.07, 5.12, 5.17, 5.22, 5.27, 5.32, 5.38, 5.43, 5.48, 5.53, 5.58, 5.63, 5.68, 5.73, 5.79, 5.84, 5.89, 5.94, 5.99, 6.04, 6.09, 6.14, 6.2, 6.25, 6.3, 6.35, 6.4, 6.45, 6.5, 6.55, 6.6, 6.66, 6.71, 6.76, 6.81, 6.86, 6.91, 6.96, 7.01, 7.07, 7.12, 7.17, 7.22, 7.27, 7.32, 7.37, 7.42, 7.48, 7.53, 7.58, 7.63, 7.68, 7.73, 7.78, 7.83, 7.88, 7.94, 7.99, 8.04, 8.09, 8.14, 8.19, 8.24, 8.29, 8.35, 8.4, 8.45, 8.5, 8.55, 8.6, 8.65, 8.7, 8.76, 8.81, 8.86, 8.91, 8.96, 9.01, 9.06, 9.11, 9.16, 9.22, 9.27, 9.32, 9.37, 9.42, 9.47, 9.52, 9.57, 9.63, 9.68, 9.73, 9.78, 9.83, 9.88, 9.93, 9.98, 10.04, 10.09, 10.14, 10.19, 10.24, 10.29, 10.34, 10.39, 10.44, 10.5, 10.55, 10.6, 10.65, 10.7, 10.75, 10.8, 10.85, 10.91, 10.96, 11.01, 11.06, 11.11, 11.16, 11.21, 11.26, 11.32, 11.37, 11.42, 11.47, 11.52, 11.57, 11.62, 11.67, 11.72, 11.78, 11.83, 11.88, 11.93, 11.98, 12.03, 12.08, 12.13, 12.19, 12.24, 12.29, 12.34, 12.39, 12.44, 12.49, 12.54, 12.6, 12.65, 12.7, 12.75, 12.8, 12.85, 12.9, 12.95, 13.0, 13.06, 13.11, 13.16, 13.21, 13.26, 13.31, 13.36, 13.41, 13.47, 13.52, 13.57, 13.62, 13.67, 13.72, 13.77, 13.82, 13.88, 13.93, 13.98, 14.03, 14.08, 14.13, 14.18, 14.23, 14.28, 14.34, 14.39, 14.44, 14.49, 14.54, 14.59, 14.64, 14.69, 14.75, 14.8, 14.85, 14.9, 14.95, 15.0, 15.05, 15.1, 15.16, 15.21, 15.26, 15.31, 15.36, 15.41, 15.46, 15.51, 15.56, 15.62, 15.67, 15.72, 15.77, 15.82, 15.87, 15.92, 15.97, 16.03, 16.08, 16.13, 16.18, 16.23, 16.28, 16.33, 16.38, 16.44, 16.49, 16.54, 16.59, 16.64, 16.69, 16.74, 16.79, 16.84, 16.9, 16.95, 17.0, 17.05, 17.1, 17.15, 17.2, 17.25, 17.31, 17.36, 17.41, 17.46, 17.51, 17.56, 17.61, 17.66, 17.72, 17.77, 17.82, 17.87, 17.92, 17.97, 18.02, 18.07, 18.12, 18.18, 18.23, 18.28, 18.33, 18.38, 18.43, 18.48, 18.53, 18.59, 18.64, 18.69, 18.74, 18.79, 18.84, 18.89, 18.94, 19.0, 19.05, 19.1, 19.15, 19.2, 19.25, 19.3, 19.35, 19.4, 19.46, 19.51, 19.56, 19.61, 19.66, 19.71, 19.76, 19.81, 19.87, 19.92, 19.97, 20.02, 20.07, 20.12, 20.17, 20.22, 20.28, 20.33, 20.38, 20.43, 20.48, 20.53, 20.58, 20.63, 20.68, 20.74, 20.79, 20.84, 20.89, 20.94, 20.99, 21.04, 21.09, 21.15, 21.2, 21.25, 21.3, 21.35, 21.4, 21.45, 21.5, 21.56, 21.61, 21.66, 21.71, 21.76, 21.81, 21.86, 21.91, 21.96, 22.02, 22.07, 22.12, 22.17, 22.22, 22.27, 22.32, 22.37, 22.43, 22.48, 22.53, 22.58, 22.63, 22.68, 22.73, 22.78, 22.84, 22.89, 22.94, 22.99, 23.04, 23.09, 23.14, 23.19, 23.24, 23.3, 23.35, 23.4, 23.45, 23.5, 23.55, 23.6, 23.65, 23.71, 23.76, 23.81, 23.86, 23.91, 23.96, 24.01, 24.06, 24.12, 24.17, 24.22, 24.27, 24.32, 24.37, 24.42, 24.47, 24.52, 24.58, 24.63, 24.68, 24.73, 24.78, 24.83, 24.88, 24.93, 24.99, 25.04, 25.09, 25.14, 25.19, 25.24, 25.29, 25.34, 25.4, 25.45, 25.5, 25.55, 25.6, 25.65, 25.7, 25.75, 25.8, 25.86, 25.91, 25.96, 26.01, 26.06, 26.11, 26.16, 26.21, 26.27, 26.32, 26.37, 26.42, 26.47, 26.52, 26.57, 26.62, 26.68, 26.73, 26.78, 26.83, 26.88, 26.93, 26.98, 27.03, 27.08, 27.14, 27.19, 27.24, 27.29, 27.34, 27.39, 27.44, 27.49, 27.55, 27.6, 27.65, 27.7, 27.75, 27.8, 27.85, 27.9, 27.96, 28.01, 28.06, 28.11, 28.16, 28.21, 28.26, 28.31, 28.36, 28.42, 28.47, 28.52, 28.57, 28.62, 28.67, 28.72, 28.77, 28.83, 28.88, 28.93, 28.98, 29.03, 29.08, 29.13, 29.18, 29.24, 29.29, 29.34, 29.39, 29.44, 29.49, 29.54, 29.59, 29.64, 29.7, 29.75, 29.8, 29.85, 29.9, 29.95, 30.0, 30.05, 30.11, 30.16, 30.21, 30.26, 30.31, 30.36, 30.41, 30.46, 30.52, 30.57, 30.62, 30.67, 30.72, 30.77, 30.82, 30.87, 30.92, 30.98, 31.03, 31.08, 31.13, 31.18, 31.23, 31.28, 31.33, 31.39, 31.44, 31.49, 31.54, 31.59, 31.64, 31.69, 31.74, 31.8, 31.85, 31.9, 31.95 };

            MakeScatterPlot(gfx, water_supply_temp, water_return_temp, water_supply_setpoint, xx, 80, y, 1.6, "Figure 7Phase 2 ");
            y = y + TitleOtstup * 9;
            y = y + otstup;

            DrawSignatureText(gfx, "Figure 7: Phase 2 Control (December, 2020)", y);
            y = y + TitleOtstup;

            DrawSimpleText(gfx, "The minimum return water temperature this month was 35.6 °C", y);
            y = y + otstup;

        }




        private static void DrawSixPage(XGraphics gfx)
        {
            int y = 70;
            const int otstup = 14;

            const int TitleOtstup = 30;
            DrawPageNum(gfx, 6);


            DrawTitleText(gfx, "Phase 3", 55, y);
            y = y + TitleOtstup;
            DrawSimpleText(gfx, "Installed technologies:", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "Combined heat and power", y);
            y = y + otstup;
            DrawMarkeredText(gfx, "Gas boilers – 3 pcs", y);
            y = y + otstup;


            DrawImage(gfx, "C:/Users/wital/source/repos/ConsoleApp5/bin/Debug/Figure3.jpg", 100, y, 3.1);
            y = y + TitleOtstup * 8;
            y = y + otstup;
            y = y + otstup;

            DrawSignatureText(gfx, "Figure 8: Phase 3 Energy Diagram", y);
            y = y + TitleOtstup;

            DrawSimpleText(gfx, "The gas energy consumed by the installed technologies this month is shown below:", y);
            y = y + otstup;

            double[] values = { 14460, 3331 };
            string[] labels = { "Boilers", "CHP" };

            MakePiePlot(gfx, values, labels, 80, y, 1.6, "Figure 9Phase 3 Energy Consumption");
            y = y + otstup;
            y = y + TitleOtstup * 8;


            DrawSignatureText(gfx, "Figure 9: Phase 3 Energy Consumption", y);
            y = y + TitleOtstup;

            DrawSimpleText(gfx, "This month 29 MWh of heating energy was provided to the phase 3 building by gas boilers and the combined heat &", y);
            y = y + otstup;

            DrawSimpleText(gfx, "power plant. Consumption of gas and electricity production compared with previous months is shown below.", y);
            y = y + otstup;

        }




        private static void DrawSeventhPage(XGraphics gfx)
        {
            int y = 70;
            const int otstup = 14;

            const int TitleOtstup = 30;
            DrawPageNum(gfx, 7);

            int groupCount = 10;
            Random rand = new Random(0);
            double[] ys1 = ScottPlot.DataGen.RandomNormal(rand, groupCount, 600, 180);
            double[] ys2 = ScottPlot.DataGen.RandomNormal(rand, groupCount, 600, 250);
            double[] err1 = ScottPlot.DataGen.RandomNormal(rand, groupCount, 0, 0);
            double[] err2 = ScottPlot.DataGen.RandomNormal(rand, groupCount, 0, 0);

            MakeDoubleBarPlot(gfx, ys1, ys2, err1, err2, 80, y, 1.6, "Figure 6Phase 2");
            y = y + TitleOtstup * 9;
            y = y + otstup;
            DrawSignatureText(gfx, "Figure 10: Phase 3 Energy Consumption/production monthly comparison", y);
            y = y + TitleOtstup;


            double[] water_supply_temp = { 63, 66, 65, 66, 65, 60, 63, 65, 63, 65, 61, 60, 61, 66, 61, 62, 62, 61, 60, 65, 63, 66, 64, 65, 63, 55, 58, 61, 57, 56, 61, 58, 61, 56, 59, 56, 57, 60, 58, 56, 60, 57, 60, 55, 56, 61, 56, 60, 57, 60, 64, 61, 64, 63, 64, 61, 60, 62, 62, 60, 64, 65, 63, 63, 62, 66, 62, 63, 60, 65, 63, 60, 60, 63, 61, 59, 55, 58, 61, 57, 56, 55, 57, 55, 57, 59, 58, 56, 56, 60, 58, 57, 60, 59, 58, 55, 57, 61, 60, 55, 62, 64, 62, 63, 61, 64, 63, 63, 60, 62, 65, 65, 65, 62, 60, 61, 65, 65, 62, 60, 60, 62, 62, 65, 66, 61, 56, 57, 57, 56, 59, 56, 58, 55, 61, 57, 61, 56, 59, 55, 55, 61, 61, 60, 55, 58, 56, 57, 60, 55, 60, 63, 60, 62, 61, 60, 66, 63, 63, 60, 64, 66, 62, 65, 61, 64, 60, 61, 62, 60, 66, 60, 61, 66, 66, 55, 55, 61, 59, 55, 55, 57, 58, 56, 56, 58, 61, 58, 58, 58, 55, 58, 61, 58, 55, 55, 58, 60, 61, 57, 66, 60, 63, 61, 65, 66, 65, 61, 65, 60, 65, 60, 66, 66, 63, 65, 64, 66, 63, 66, 66, 63, 63, 66, 62, 57, 56, 60, 55, 61, 58, 59, 58, 58, 56, 57, 59, 60, 55, 60, 56, 59, 56, 59, 58, 61, 56, 59, 57, 60, 60, 63, 64, 64, 64, 62, 64, 65, 64, 66, 64, 64, 66, 61, 65, 60, 64, 62, 63, 64, 65, 63, 63, 63, 61, 56, 58, 57, 58, 55, 59, 61, 59, 58, 57, 58, 58, 55, 61, 57, 60, 56, 58, 59, 61, 57, 59, 58, 56, 58, 61, 62, 63, 62, 65, 62, 62, 62, 63, 63, 62, 63, 65, 64, 66, 65, 61, 64, 64, 65, 64, 65, 63, 65, 66, 61, 55, 60, 61, 57, 55, 61, 61, 59, 61, 56, 61, 59, 60, 56, 59, 59, 60, 57, 57, 61, 55, 61, 58, 59, 62, 64, 62, 62, 61, 64, 65, 61, 65, 60, 66, 60, 62, 63, 62, 61, 60, 60, 63, 62, 66, 60, 63, 65, 66, 61, 55, 55, 56, 58, 57, 60, 60, 60, 55, 58, 61, 59, 61, 58, 55, 61, 60, 56, 55, 58, 55, 55, 57, 59, 66, 61, 66, 62, 60, 66, 62, 66, 60, 63, 64, 66, 60, 65, 64, 65, 63, 60, 65, 66, 61, 62, 65, 66, 65, 57, 57, 59, 56, 58, 60, 55, 57, 57, 55, 57, 61, 61, 60, 58, 59, 61, 59, 59, 57, 55, 57, 59, 60, 55, 62, 62, 66, 64, 64, 62, 62, 64, 64, 60, 64, 60, 61, 60, 65, 65, 66, 63, 66, 64, 65, 64, 61, 62, 60, 61, 58, 60, 56, 58, 56, 56, 60, 58, 55, 57, 55, 61, 59, 61, 61, 60, 56, 58, 60, 60, 56, 60, 60, 59, 62, 60, 64, 66, 60, 63, 60, 60, 65, 66, 62, 66, 65, 62, 60, 61, 62, 60, 65, 63, 60, 61, 64, 60, 63, 57, 57, 57, 61, 58, 58, 55, 56, 57, 58, 60, 61, 61, 58, 60, 57, 57, 59, 60, 58, 61, 57, 58, 57, 58, 61, 61, 63, 64, 66, 61, 63, 65, 60, 64, 64, 63, 62, 63, 60, 62, 60, 64, 63, 64, 63, 63, 61, 63, 63, 58, 55, 58, 58, 59, 59, 58, 59, 56, 60, 57, 58, 58, 59, 55, 55, 56, 59, 61, 56, 59, 56, 55, 60, 56, 65, 64, 63, 64, 63, 64, 61, 60, 65, 66, 60, 66, 61, 63, 62, 60, 63, 66, 66, 66, 61, 66, 65, 61, 60 };
            double[] water_return_temp = { 48, 51, 50, 50, 52, 49, 48, 51, 51, 52, 50, 52, 48, 50, 48, 51, 50, 48, 52, 48, 48, 48, 48, 52, 52, 57, 57, 53, 53, 55, 55, 54, 56, 53, 54, 56, 54, 54, 56, 56, 53, 55, 53, 57, 57, 54, 56, 57, 57, 55, 52, 50, 51, 48, 52, 51, 52, 48, 52, 48, 51, 49, 52, 50, 52, 48, 52, 48, 52, 48, 50, 52, 49, 48, 52, 54, 54, 55, 56, 56, 54, 54, 53, 53, 57, 56, 57, 53, 57, 55, 53, 57, 55, 55, 56, 53, 57, 54, 54, 54, 50, 52, 51, 52, 48, 51, 51, 50, 49, 52, 52, 52, 48, 51, 52, 49, 52, 52, 49, 48, 50, 50, 51, 49, 49, 54, 54, 53, 54, 55, 53, 53, 55, 55, 57, 55, 53, 57, 53, 56, 55, 53, 57, 56, 55, 56, 57, 57, 57, 53, 48, 51, 50, 49, 48, 48, 52, 48, 50, 51, 49, 48, 49, 50, 50, 48, 51, 49, 52, 52, 49, 52, 48, 48, 50, 54, 53, 56, 56, 57, 55, 53, 54, 55, 57, 56, 54, 53, 56, 54, 56, 53, 56, 57, 56, 53, 56, 55, 54, 55, 49, 49, 48, 50, 51, 51, 51, 52, 52, 48, 50, 49, 50, 50, 49, 49, 50, 52, 48, 51, 50, 49, 52, 52, 49, 55, 54, 56, 54, 55, 53, 54, 56, 55, 55, 53, 56, 55, 55, 54, 54, 57, 53, 53, 54, 55, 56, 56, 55, 53, 50, 51, 48, 50, 49, 51, 50, 48, 52, 48, 52, 48, 49, 49, 50, 50, 51, 52, 52, 48, 50, 51, 48, 49, 48, 57, 53, 53, 56, 57, 56, 57, 53, 53, 56, 53, 54, 56, 53, 57, 56, 55, 53, 53, 55, 53, 57, 53, 57, 56, 49, 51, 50, 48, 48, 49, 50, 50, 49, 48, 51, 49, 50, 51, 50, 51, 48, 51, 50, 52, 50, 50, 52, 50, 50, 53, 56, 57, 56, 53, 56, 56, 56, 55, 53, 54, 53, 57, 56, 56, 54, 57, 56, 53, 57, 55, 55, 56, 53, 55, 52, 48, 49, 48, 48, 52, 51, 48, 52, 52, 50, 50, 52, 50, 48, 49, 51, 51, 52, 50, 52, 51, 52, 49, 49, 57, 57, 54, 53, 54, 54, 55, 56, 56, 53, 55, 56, 57, 53, 54, 54, 53, 54, 57, 53, 53, 57, 56, 57, 54, 51, 51, 48, 49, 52, 52, 49, 48, 49, 52, 50, 50, 49, 50, 52, 50, 49, 48, 49, 49, 52, 49, 50, 48, 51, 54, 56, 56, 53, 57, 53, 54, 53, 55, 57, 56, 57, 54, 55, 53, 56, 57, 54, 57, 54, 57, 54, 54, 53, 55, 51, 50, 52, 51, 50, 51, 49, 49, 48, 48, 50, 49, 50, 49, 51, 48, 49, 50, 50, 51, 52, 52, 48, 51, 51, 54, 57, 56, 54, 56, 54, 55, 56, 55, 57, 57, 54, 54, 56, 55, 54, 56, 57, 54, 53, 53, 53, 54, 53, 55, 51, 52, 50, 50, 50, 51, 49, 52, 52, 49, 48, 52, 52, 49, 50, 50, 49, 48, 48, 51, 50, 52, 49, 50, 52, 55, 54, 54, 53, 54, 53, 54, 57, 57, 54, 57, 57, 57, 53, 53, 55, 53, 55, 54, 57, 54, 55, 53, 56, 55, 50, 51, 49, 50, 52, 51, 52, 48, 50, 49, 49, 52, 51, 50, 48, 48, 50, 49, 49, 52, 49, 52, 51, 49, 50, 55, 55, 55, 55, 55, 56, 55, 55, 57, 57, 54, 56, 55, 55, 57, 55, 56, 53, 54, 56, 54, 55, 57, 55, 57, 50, 52, 51, 52, 50, 51, 49, 48, 52, 49, 48, 51, 49, 51, 50, 50, 50, 51, 48, 50, 52, 50, 49, 48, 50};
            double[] water_supply_setpoint = { 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 58, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20 };
            double[] xx = { 0, 0.05, 0.1, 0.15, 0.2, 0.26, 0.31, 0.36, 0.41, 0.46, 0.51, 0.56, 0.61, 0.67, 0.72, 0.77, 0.82, 0.87, 0.92, 0.97, 1.02, 1.08, 1.13, 1.18, 1.23, 1.28, 1.33, 1.38, 1.43, 1.48, 1.54, 1.59, 1.64, 1.69, 1.74, 1.79, 1.84, 1.89, 1.95, 2.0, 2.05, 2.1, 2.15, 2.2, 2.25, 2.3, 2.36, 2.41, 2.46, 2.51, 2.56, 2.61, 2.66, 2.71, 2.76, 2.82, 2.87, 2.92, 2.97, 3.02, 3.07, 3.12, 3.17, 3.23, 3.28, 3.33, 3.38, 3.43, 3.48, 3.53, 3.58, 3.64, 3.69, 3.74, 3.79, 3.84, 3.89, 3.94, 3.99, 4.04, 4.1, 4.15, 4.2, 4.25, 4.3, 4.35, 4.4, 4.45, 4.51, 4.56, 4.61, 4.66, 4.71, 4.76, 4.81, 4.86, 4.92, 4.97, 5.02, 5.07, 5.12, 5.17, 5.22, 5.27, 5.32, 5.38, 5.43, 5.48, 5.53, 5.58, 5.63, 5.68, 5.73, 5.79, 5.84, 5.89, 5.94, 5.99, 6.04, 6.09, 6.14, 6.2, 6.25, 6.3, 6.35, 6.4, 6.45, 6.5, 6.55, 6.6, 6.66, 6.71, 6.76, 6.81, 6.86, 6.91, 6.96, 7.01, 7.07, 7.12, 7.17, 7.22, 7.27, 7.32, 7.37, 7.42, 7.48, 7.53, 7.58, 7.63, 7.68, 7.73, 7.78, 7.83, 7.88, 7.94, 7.99, 8.04, 8.09, 8.14, 8.19, 8.24, 8.29, 8.35, 8.4, 8.45, 8.5, 8.55, 8.6, 8.65, 8.7, 8.76, 8.81, 8.86, 8.91, 8.96, 9.01, 9.06, 9.11, 9.16, 9.22, 9.27, 9.32, 9.37, 9.42, 9.47, 9.52, 9.57, 9.63, 9.68, 9.73, 9.78, 9.83, 9.88, 9.93, 9.98, 10.04, 10.09, 10.14, 10.19, 10.24, 10.29, 10.34, 10.39, 10.44, 10.5, 10.55, 10.6, 10.65, 10.7, 10.75, 10.8, 10.85, 10.91, 10.96, 11.01, 11.06, 11.11, 11.16, 11.21, 11.26, 11.32, 11.37, 11.42, 11.47, 11.52, 11.57, 11.62, 11.67, 11.72, 11.78, 11.83, 11.88, 11.93, 11.98, 12.03, 12.08, 12.13, 12.19, 12.24, 12.29, 12.34, 12.39, 12.44, 12.49, 12.54, 12.6, 12.65, 12.7, 12.75, 12.8, 12.85, 12.9, 12.95, 13.0, 13.06, 13.11, 13.16, 13.21, 13.26, 13.31, 13.36, 13.41, 13.47, 13.52, 13.57, 13.62, 13.67, 13.72, 13.77, 13.82, 13.88, 13.93, 13.98, 14.03, 14.08, 14.13, 14.18, 14.23, 14.28, 14.34, 14.39, 14.44, 14.49, 14.54, 14.59, 14.64, 14.69, 14.75, 14.8, 14.85, 14.9, 14.95, 15.0, 15.05, 15.1, 15.16, 15.21, 15.26, 15.31, 15.36, 15.41, 15.46, 15.51, 15.56, 15.62, 15.67, 15.72, 15.77, 15.82, 15.87, 15.92, 15.97, 16.03, 16.08, 16.13, 16.18, 16.23, 16.28, 16.33, 16.38, 16.44, 16.49, 16.54, 16.59, 16.64, 16.69, 16.74, 16.79, 16.84, 16.9, 16.95, 17.0, 17.05, 17.1, 17.15, 17.2, 17.25, 17.31, 17.36, 17.41, 17.46, 17.51, 17.56, 17.61, 17.66, 17.72, 17.77, 17.82, 17.87, 17.92, 17.97, 18.02, 18.07, 18.12, 18.18, 18.23, 18.28, 18.33, 18.38, 18.43, 18.48, 18.53, 18.59, 18.64, 18.69, 18.74, 18.79, 18.84, 18.89, 18.94, 19.0, 19.05, 19.1, 19.15, 19.2, 19.25, 19.3, 19.35, 19.4, 19.46, 19.51, 19.56, 19.61, 19.66, 19.71, 19.76, 19.81, 19.87, 19.92, 19.97, 20.02, 20.07, 20.12, 20.17, 20.22, 20.28, 20.33, 20.38, 20.43, 20.48, 20.53, 20.58, 20.63, 20.68, 20.74, 20.79, 20.84, 20.89, 20.94, 20.99, 21.04, 21.09, 21.15, 21.2, 21.25, 21.3, 21.35, 21.4, 21.45, 21.5, 21.56, 21.61, 21.66, 21.71, 21.76, 21.81, 21.86, 21.91, 21.96, 22.02, 22.07, 22.12, 22.17, 22.22, 22.27, 22.32, 22.37, 22.43, 22.48, 22.53, 22.58, 22.63, 22.68, 22.73, 22.78, 22.84, 22.89, 22.94, 22.99, 23.04, 23.09, 23.14, 23.19, 23.24, 23.3, 23.35, 23.4, 23.45, 23.5, 23.55, 23.6, 23.65, 23.71, 23.76, 23.81, 23.86, 23.91, 23.96, 24.01, 24.06, 24.12, 24.17, 24.22, 24.27, 24.32, 24.37, 24.42, 24.47, 24.52, 24.58, 24.63, 24.68, 24.73, 24.78, 24.83, 24.88, 24.93, 24.99, 25.04, 25.09, 25.14, 25.19, 25.24, 25.29, 25.34, 25.4, 25.45, 25.5, 25.55, 25.6, 25.65, 25.7, 25.75, 25.8, 25.86, 25.91, 25.96, 26.01, 26.06, 26.11, 26.16, 26.21, 26.27, 26.32, 26.37, 26.42, 26.47, 26.52, 26.57, 26.62, 26.68, 26.73, 26.78, 26.83, 26.88, 26.93, 26.98, 27.03, 27.08, 27.14, 27.19, 27.24, 27.29, 27.34, 27.39, 27.44, 27.49, 27.55, 27.6, 27.65, 27.7, 27.75, 27.8, 27.85, 27.9, 27.96, 28.01, 28.06, 28.11, 28.16, 28.21, 28.26, 28.31, 28.36, 28.42, 28.47, 28.52, 28.57, 28.62, 28.67, 28.72, 28.77, 28.83, 28.88, 28.93, 28.98, 29.03, 29.08, 29.13, 29.18, 29.24, 29.29, 29.34, 29.39, 29.44, 29.49, 29.54, 29.59, 29.64, 29.7, 29.75, 29.8, 29.85, 29.9, 29.95, 30.0, 30.05, 30.11, 30.16, 30.21, 30.26, 30.31, 30.36, 30.41, 30.46, 30.52, 30.57, 30.62, 30.67, 30.72, 30.77, 30.82, 30.87, 30.92, 30.98, 31.03, 31.08, 31.13, 31.18, 31.23, 31.28, 31.33, 31.39, 31.44, 31.49, 31.54, 31.59, 31.64, 31.69, 31.74, 31.8, 31.85, 31.9, 31.95 };

            MakeScatterPlot(gfx, water_supply_temp, water_return_temp, water_supply_setpoint, xx, 80, y, 1.6, "Figure 7 Phase 2 ");
            y = y + TitleOtstup * 9;
            y = y + otstup;

            DrawSignatureText(gfx, "Figure 11: Phase 3 Control (December, 2020)", y);
            y = y + TitleOtstup;

            DrawSimpleText(gfx, "The minimum return water temperature this month was 45.9 °C", y);
            y = y + otstup;

        }


        static void Main(string[] args)
        {

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";


            // Draw the text
            //gfx.DrawString("Hello, World!", font, XBrushes.Black,
            //  new XRect(0, 0, page.Width, page.Height),
            //  XStringFormats.Center);



            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            DrawFirstPage(gfx);


            PdfPage page2 = document.AddPage();
            XGraphics gfx2 = XGraphics.FromPdfPage(page2);
            DrawSecondPage(gfx2);


            PdfPage page3 = document.AddPage();
            XGraphics gfx3 = XGraphics.FromPdfPage(page3);
            DrawThirdPage(gfx3);


            PdfPage page4 = document.AddPage();
            XGraphics gfx4 = XGraphics.FromPdfPage(page4);
            DrawForthPage(gfx4);


            PdfPage page5 = document.AddPage();
            XGraphics gfx5 = XGraphics.FromPdfPage(page5);
            DrawFifthPage(gfx5);

            PdfPage page6 = document.AddPage();
            XGraphics gfx6 = XGraphics.FromPdfPage(page6);
            DrawSixPage(gfx6);


            PdfPage page7 = document.AddPage();
            XGraphics gfx7 = XGraphics.FromPdfPage(page7);
            DrawSeventhPage(gfx7);




            const string filename = "Cordium_monthly_report_December, 2020W.pdf";
            document.Save(filename);
            Process.Start(filename);
        }
    }
}









