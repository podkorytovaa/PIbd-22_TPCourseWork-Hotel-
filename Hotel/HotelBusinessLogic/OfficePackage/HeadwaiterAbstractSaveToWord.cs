using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBusinessLogic.OfficePackage.HelperEnums;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.OfficePackage
{
    public abstract class HeadwaiterAbstractSaveToWord
    {
        public void CreateDoc(HeadwaiterWordInfo info)
        {
            /*CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            foreach (var rs in info.RoomSeminars)
            {
                var seminars = new List<(string, WordTextProperties)>();
                seminars.Add(rs.RoomNumber + ": ", new WordTextProperties { Bold = true, Size = "24", });
    
                foreach (var s in rs.Seminars)
                {
                    seminars.Add(s.Item1 + "; ", new WordTextProperties { Size = "24", });

                }

                CreateParagraph(new WordParagraph
                {
                    Texts = seminars,
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }

            SaveWord(info);*/
        }

        // Создание doc-файла
        protected abstract void CreateWord(HeadwaiterWordInfo info);

        // Создание абзаца с текстом
        protected abstract void CreateParagraph(WordParagraph paragraph);

        // Сохранение файла
        protected abstract void SaveWord(HeadwaiterWordInfo info);
    }
}
