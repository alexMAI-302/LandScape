using System;

namespace LandScape
{
    /// <summary>
    /// Базовый класс, соответствующий природным объектам
    /// </summary>
    public class LandScapeObject
    {
        /// <summary>
        /// Координата положения объекта на карте
        /// </summary>
        public int x, y;
        /// <summary>
        /// Код изображения, соответствующий конкретному объекту
        /// </summary>
        public Engine.TypeOfImg ImgType; 
        /// <summary>
        /// Флаг, отвечающий за проходимость объекта
        /// </summary>
        public bool IsPas;
        /// <summary>
        /// Природные объекты
        /// </summary>
        /// <param name="w">Координата по горизонтали</param>
        /// <param name="z">Координата по вертикали</param>
        /// <param name="Img">Код изображения</param>
        /// <param name="flag">Флаг проходимости</param>
        public LandScapeObject(int x, int y, Engine.TypeOfImg Img, bool IsPas)
        {
            this.x = x;
            this.y = y;
            ImgType = Img;
            this.IsPas = IsPas;
        }
    }
    /// <summary>
    /// Класс, отвечающий за порталы
    /// </summary>
    class Gates : LandScapeObject
    {
        /// <summary>
        /// Порталы
        /// </summary>
        /// <param name="w">Координата по горизонтали</param>
        /// <param name="z">Координата по вертикали</param>
        /// <param name="Img">Код изображения</param>
        /// <param name="flag">Флаг проходимости</param>
        public Gates(int x, int y, Engine.TypeOfImg Img, bool IsPas)
            : base(y, y, Img, IsPas)
        { }
    }
    /// <summary>
    /// Класс, отвечающий за логова
    /// </summary>
    class Lair : LandScapeObject
    {
        /// <summary>
        /// Сила зверя
        /// </summary>
        int BeastPower;
        /// <summary>
        /// Логово зверя
        /// </summary>
        /// <param name="w">Координата по горизонтали</param>
        /// <param name="z">Координата по вертикали</param>
        /// <param name="Img">Код изображения</param>
        /// <param name="flag">Флаг проходимости</param>
        public Lair(int x, int y, Engine.TypeOfImg Img, bool flag)
            : base(x, y, Img, flag)
        {
            Random rnd = new Random();
            BeastPower = rnd.Next(0, 5); 
        }
    }
    /// <summary>
    /// Класс, отвечающий за артефакты
    /// </summary>
    class Artefact : LandScapeObject
    {
        /// <summary>
        /// Сила артефакта
        /// </summary>
        int Power;
        /// <summary>
        /// Вес артефакта
        /// </summary>
        int Weight;
        /// <summary>
        /// Артефакт
        /// </summary>
        /// <param name="w">Координата по горизонтали</param>
        /// <param name="z">Координата по вертикали</param>
        /// <param name="Img">Код изображения</param>
        /// <param name="flag">Флаг проходимости</param>
        public Artefact(int x, int y, Engine.TypeOfImg Img, bool flag)
            : base(x, y, Img, flag)
        {
            Random rnd = new Random();
            Power = rnd.Next(1, 10);
            Weight = 4 * Power;
        }
    }
}
