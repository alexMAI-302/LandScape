﻿using System;
using System.Collections.Generic;

namespace LandScape
{
    /// <summary>
    /// Класс, отвечающий за формирование карты
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Длина карты
        /// </summary>
        private int Length;
        /// <summary>
        /// Ширина карты
        /// </summary>
        private int Height;
        /// <summary>
        /// Размер "ячейки" карты
        /// </summary>
        private int Size;
        public List<LandScapeObject> LandScape = new List<LandScapeObject>();
        /// <summary>
        /// Очистка крты
        /// </summary>
        /// <param name="Matrix">Матрица карты</param>
        public void ResetMatrix(int[][] Matrix)
        {
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Length; j++)
                    Matrix[i][j] = -1;
        }
        /// <summary>
        /// Заполнение карты элементами ландшафта
        /// </summary>
        /// <param name="Matrix">Матрица карты</param>
        /// <param name="FLine">Массив строк</param>
        public void FillMapObj(int[][] Matrix)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < Matrix.Length; i++)
            {
                x = 0;
                for (int j = 0; j < Matrix[0].Length; j++)
                {
                    switch ((TypeOfImg)Enum.ToObject(typeof(TypeOfImg), Matrix[i][j]))
                    {
                        case TypeOfImg.Grass:
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Grass, true));
                            break;
                        case TypeOfImg.Water:
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Water, false));
                            break;
                        case TypeOfImg.Land:
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Land, true));
                            break;
                        case (int)TypeOfImg.Rock:
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Rock, false));
                            break;
                        case TypeOfImg.Tree:
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Tree, false));
                            break;
                        case TypeOfImg.Gates:
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Land, true));
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Gates, true));
                            break;
                        case TypeOfImg.Artefact:
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Grass, true));
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Artefact, true));
                            break;
                        case TypeOfImg.Lair:
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Grass, true));
                            LandScape.Add(new LandScapeObject(x, y, TypeOfImg.Lair, false));
                            break;
                    }
                    x += Size;
                }
                if (x == Length * Size)
                    y += Size;
            }
        }
        /// <summary>
        /// Заполнение карты
        /// </summary>
        /// <param name="FileName">Путь к файлу со стартовой матрицей</param>
        /// <param name="Mtx">Матрица карты</param>
        /// <returns>Возвращает заполненную матрицу карты</returns>
        public int[][] FillMap(string FileName, int[][] Mtx)
        {
            string[] FLine = System.IO.File.ReadAllLines(FileName);
            Mtx = new int[FLine.Length][];
            for (int i = 0; i < FLine.Length; i++)
            {
                Mtx[i] = new int[FLine[0].Length];
                for (int j = 0; j < FLine[0].Length; j++)
                    Mtx[i][j] = (int)char.GetNumericValue(FLine[i][j]);
            }
            int Rcnt = 0;
            int ArtCnt = 0;
            Random rnd = new Random();
            while (Rcnt < Math.Truncate(Math.Sqrt(Length*Height)))
            {
                for (int i = 0; i < Mtx.Length; i++) 
                {
                    for (int j = 0; j < Mtx[0].Length; j++) 
                    {
                        int a = rnd.Next(0, Mtx.Length); 
                        int b = rnd.Next(0, Mtx[0].Length); 
                        TypeOfImg R = (TypeOfImg)Enum.ToObject(typeof(TypeOfImg), Mtx[a][b]);
                        if ((R != TypeOfImg.Land) && 
                            (R != TypeOfImg.Water) &&
                            (R != TypeOfImg.Rock) &&
                            (R != TypeOfImg.Gates) &&
                            (R != TypeOfImg.Lair))
                        {
                            Mtx[a][b] = (int)TypeOfImg.Tree;
                            Rcnt++;
                            break;
                        }
                        a = rnd.Next(0, Mtx.Length); 
                        b = rnd.Next(0, Mtx[0].Length); 
                        R = (TypeOfImg)Enum.ToObject(typeof(TypeOfImg), Mtx[a][b]);
                        if ((R != TypeOfImg.Land) &&
                            (R != TypeOfImg.Water) &&
                            (R != TypeOfImg.Tree) &&
                            (R != TypeOfImg.Lair) &&
                            (R != TypeOfImg.Gates))
                        {
                            Mtx[a][b] = (int)TypeOfImg.Rock; 
                            Rcnt++;
                            break;
                        }
                        a = rnd.Next(1, Mtx.Length - 1); 
                        b = rnd.Next(1, Mtx[0].Length - 1); 
                        R = (TypeOfImg)Enum.ToObject(typeof(TypeOfImg), Mtx[a][b]);
                        if ((ArtCnt < Math.Truncate(Math.Sqrt(Length))) && (R != TypeOfImg.Tree) &&
                            (R != TypeOfImg.Land) && (R != TypeOfImg.Lair) && (R != TypeOfImg.Gates))
                        {
                            Mtx[a][b] = (int)TypeOfImg.Artefact; 
                            ArtCnt++;
                            break;
                        }
                    }
                }
            }
            FillMapObj(Mtx);
            return Mtx;
        }
        /// <summary>
        /// Перечисление элементов ландшафта 
        /// </summary>
        public enum TypeOfImg
        {
            Rock, Grass, Water, Land, Tree, Gates, Artefact, Lair
        }
        
        public Engine(int Length, int Height, int Size)
        {
            this.Length = Length;
            this.Height = Height;
            this.Size = Size;
        }
    }
}
