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

            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });

            CreateParagraph(new PdfParagraph { Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }", Style = "Normal" });

            CreateTable(new List<string> { "3cm", "3cm", "3cm", "3cm", "4cm", "2cm" });

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата", "Обед", "Блюдо", "Напиток", "Семинар", "Номер" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var lunch in info.Lunches)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { lunch.Date.ToShortDateString(), lunch.Name, lunch.Dish, lunch.Drink, lunch.Seminar, lunch.Room },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }

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
