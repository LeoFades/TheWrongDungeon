using System;
using System.IO;
using System.Collections.Generic;
using SplashKitSDK;

namespace DistinctionTask
{
    /// <summary>
    /// tile manager class that manages all the tiles of the map
    /// </summary>
    public class TileManager
    {
        private Tile[] _tileType;
        private int[,] _mapTileNum;

        public TileManager()
        {
            _tileType = new Tile[4];
            _mapTileNum = new int[200, 200];
            getTileImage();

            string CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            //test use this
            //string filepath = "D:/msys64/home/UserLeo/Uni_OOP/DistinctionTask/Design/mapDesign.txt";

            //run use this
            string filepath = Path.Combine(Environment.CurrentDirectory, @"Design\mapDesign.txt");
            loadMap(filepath);

        }

        /// <summary>
        /// returns what tiletype is at that position of 2d array
        /// </summary>
        /// <value>tile type</value>
        public int[,] MapTile
        {
            get
            {
                return _mapTileNum;
            }
        }

        /// <summary>
        /// gets the tile image and put them in array
        /// </summary>
        public void getTileImage()
        {
            _tileType[0] = new Tile("void");
            _tileType[1] = new Tile("stoneFloor");
            _tileType[2] = new Tile("woodFloor");
            _tileType[3] = new Tile("earth");

        }

        /// <summary>
        /// draw all tiles
        /// </summary>
        public void Draw()
        {
            Point2D coordinates;
            coordinates.X = 0;
            coordinates.Y = 0;

            for (int row = 0; row < 200; row++)
            {
                for (int col = 0; col < 200; col++)
                {
                    int tileNum = _mapTileNum[row, col];

                    _tileType[tileNum].Coordinates = coordinates;

                    Rectangle cam = SplashKit.ScreenRectangle();
                    cam.X = SplashKit.CameraX();
                    cam.Y = SplashKit.CameraY();
                    if (SplashKit.PointInRectangle(coordinates, cam))
                    {
                        _tileType[tileNum].Draw();
                    }


                    if (tileNum == 0)
                    {
                        _tileType[tileNum].Collision = true;
                    }

                    coordinates.X += 16;
                }

                coordinates.X = 0;
                coordinates.Y += 16;
            }

        }

        /// <summary>
        /// reads from the text file of numbers to load them
        /// </summary>
        /// <param name="filepath"></param>
        public void loadMap(string filepath)
        {
            string[] lines = File.ReadAllLines(filepath);

            int col = 0;
            int row = 0;

            foreach (string ln in lines)
            {
                string[] numbers = ln.Split(",");

                foreach (string num in numbers)
                {
                    int trueNum = Int32.Parse(num);
                    _mapTileNum[col, row] = trueNum;
                    col++;

                }

                row++;
                col = 0;

            }

        }
    }
}