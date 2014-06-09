using System;
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
        /// <summary>
        /// Вспомогательная переенная
        /// </summary>
        private string[] FLine;
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
        /// Заполнение карты
        /// </summary>
        /// <param name="FileName">Путь к файлу со стартовой матрицей</param>
        /// <param name="Mtx">Матрица карты</param>
        /// <returns>Возвращает заполненную матрицу карты</returns>
        public int[][] FillMap(string FileName, int[][] Mtx)
        {
            FLine = System.IO.File.ReadAllLines(FileName);
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
                for (int i = 0; i < FLine.Length; i++)
                {
                    for (int j = 0; j < FLine[0].Length; j++)
                    {
                        int a = rnd.Next(0, FLine.Length);
                        int b = rnd.Next(0, FLine[0].Length);
                        if ((Mtx[a][b] != (int)TypeOfImg.Land) && 
                            (Mtx[a][b] != (int)TypeOfImg.Water) && 
                            (Mtx[a][b] != (int)TypeOfImg.Rock) && 
                            (Mtx[a][b] != (int)TypeOfImg.Gates) && 
                            (Mtx[a][b] != (int)TypeOfImg.Lair))
                        {
                            Mtx[a][b] = (int)TypeOfImg.Tree;
                            Rcnt++;
                            break;
                        }
                        a = rnd.Next(0, FLine.Length);
                        b = rnd.Next(0, FLine[0].Length);
                        if ((Mtx[a][b] != (int)TypeOfImg.Land) &&
                            (Mtx[a][b] != (int)TypeOfImg.Water) && 
                            (Mtx[a][b] != (int)TypeOfImg.Tree) &&
                            (Mtx[a][b] != (int)TypeOfImg.Lair) &&
                            (Mtx[a][b] != (int)TypeOfImg.Gates))
                        {
                            Mtx[a][b] = (int)TypeOfImg.Rock; //"код" камня
                            Rcnt++;
                            break;
                        }
                        a = rnd.Next(1, FLine.Length - 1);
                        b = rnd.Next(1, FLine[0].Length - 1);
                        if ((ArtCnt < Math.Truncate(Math.Sqrt(Length))) && (Mtx[a][b] != 4) && (Mtx[a][b] != 3) && (Mtx[a][b] != 8) && (Mtx[a][b] != 6))
                        {
                            Mtx[a][b] = (int)TypeOfImg.Artefact; // код артефакта
                            ArtCnt++;
                            break;
                        }
                    }
                }
            }
            return Mtx;
        }
        /// <summary>
        /// Перечисление элементов ландшафта 
        /// </summary>
        public enum TypeOfImg : int //ImgTypes
        {
            Rock = 0, Grass = 1, Water = 2, Land = 3, Tree = 4, Gates = 5, Artefact = 6, Lair = 7
        }
        private enum ListOfParams 
        {
            Height = 12, Length = 16, Size = 40
        }
        /// <summary>
        /// Заполнение карты элементами ландшафта
        /// </summary>
        /// <param name="Matrix">Матрица карты</param>
        public void FillMapObj(int[][] Matrix)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < FLine.Length; i++)
            {
                x = 0;
                for (int j = 0; j < FLine[0].Length; j++)
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
                if (x == Length*Size)
                    y += Size;
            }
        }
        public Engine(int Length, int Height, int Size)
        {
            this.Length = Length;
            this.Height = Height;
            this.Size = Size;
        }
    }
}
