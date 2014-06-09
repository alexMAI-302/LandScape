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
                        if ((Mtx[a][b] != 4) && (Mtx[a][b] != 3) && (Mtx[a][b] != 1) && (Mtx[a][b] != 6) && (Mtx[a][b] != 8))
                        {
                            Mtx[a][b] = (int)ListOfImg.Tree;
                            Rcnt++;
                            break;
                        }
                        a = rnd.Next(0, FLine.Length);
                        b = rnd.Next(0, FLine[0].Length);
                        if ((Mtx[a][b] != 4) && (Mtx[a][b] != 3) && (Mtx[a][b] != 5) && (Mtx[a][b] != 8) && (Mtx[a][b] != 6))
                        {
                            Mtx[a][b] = (int)ListOfImg.Rock; //"код" камня
                            Rcnt++;
                            break;
                        }
                        a = rnd.Next(1, FLine.Length - 1);
                        b = rnd.Next(1, FLine[0].Length - 1);
                        if ((ArtCnt < Math.Truncate(Math.Sqrt(Length))) && (Mtx[a][b] != 4) && (Mtx[a][b] != 3) && (Mtx[a][b] != 8) && (Mtx[a][b] != 6))
                        {
                            Mtx[a][b] = (int)ListOfImg.Artefact; // код артефакта
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
        public enum ListOfImg : int
        {
            Rock = 1, Grass = 2, Water = 3, Land = 4, Tree = 5, Gates = 6, Artefact = 7, Lair = 8
        }
        /// <summary>
        /// Заполнение карты элементами ландшафта
        /// </summary>
        /// <param name="Matrix">Матрица карты</param>
        public void FillMapObj(int[][] Matrix)
        {
            int w = 0;
            int z = 0;
            for (int i = 0; i < FLine.Length; i++)
            {
                w = 0;
                for (int j = 0; j < FLine[0].Length; j++)
                {
                    switch (Matrix[i][j])
                    {
                       case (int)ListOfImg.Grass:
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Grass, true));
                            break;
                        case (int)ListOfImg.Water:
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Water, false));
                            break;
                        case (int)ListOfImg.Land:
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Land, true));
                            break;
                        case (int)ListOfImg.Rock:
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Rock, false));
                            break;
                        case (int)ListOfImg.Tree:
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Tree, false));
                            break;
                        case (int)ListOfImg.Gates:
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Land, true));
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Gates, true));
                            break;
                        case (int)ListOfImg.Artefact:
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Grass, true));
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Artefact, true));
                            break;
                        case (int)ListOfImg.Lair:
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Grass, true));
                            LandScape.Add(new LandScapeObject(w, z, (int)ListOfImg.Lair, false));
                            break;
                    }
                    w += Size;
                }
                if (w == Length*Size)
                    z += Size;
            }
        }
        public Engine()
        {
            this.Length = 16;
            this.Height = 12;
            this.Size = 40;
        }
    }
}
