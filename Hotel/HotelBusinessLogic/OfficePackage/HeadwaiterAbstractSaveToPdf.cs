using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBusinessLogic.OfficePackage.HelperEnums;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.OfficePackage
{
    public abstract class HeadwaiterAbstractSaveToPdf
    {
        public void CreateDoc(HeadwaiterPdfInfo info)
        {
            CreatePdf(info);
            //сделать
            SavePdf(info);
        }

        // Создание doc-файла
        protected abstract void CreatePdf(HeadwaiterPdfInfo info);

        // Создание параграфа с текстом
        protected abstract void CreateParagraph(PdfParagraph paragraph);

        // Создание таблицы
        protected abstract void CreateTable(List<string> columns);

        // Создание и заполнение строки
        protected abstract void CreateRow(PdfRowParameters rowParameters);

        // Сохранение файла
        protected abstract void SavePdf(HeadwaiterPdfInfo info);
    }
}
