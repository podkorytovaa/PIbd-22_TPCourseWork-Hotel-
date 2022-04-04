using System.Collections.Generic;
using HotelBusinessLogic.OfficePackage.HelperEnums;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.OfficePackage
{
    public abstract class OrganizerAbstractSaveToPdf
    {
        public void CreateDoc(OrganizerPdfInfo info)
        {
            CreatePdf(info);

            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });

            CreateParagraph(new PdfParagraph { Text = $"с { info.DateFrom.ToShortDateString() } по { info.DateTo.ToShortDateString() }", Style = "Normal" });

            CreateTable(new List<string> { "3cm", "6cm", "3cm", "6cm", "6cm"});

            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата проведения", "Конференция", "Количество номеров", "Семинар", "Обед" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });

            foreach (var conference in info.Conferences)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { conference.DateOf.ToShortDateString(), conference.Name, conference.NumberOfRooms.ToString(), conference.Seminar, conference.Lunch },
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
            }

            SavePdf(info);
        }

        // Создание doc-файла
        protected abstract void CreatePdf(OrganizerPdfInfo info);

        // Создание параграфа с текстом
        protected abstract void CreateParagraph(PdfParagraph paragraph);

        // Создание таблицы
        protected abstract void CreateTable(List<string> columns);

        // Создание и заполнение строки
        protected abstract void CreateRow(PdfRowParameters rowParameters);

        // Сохранение файла
        protected abstract void SavePdf(OrganizerPdfInfo info);
    }
}
