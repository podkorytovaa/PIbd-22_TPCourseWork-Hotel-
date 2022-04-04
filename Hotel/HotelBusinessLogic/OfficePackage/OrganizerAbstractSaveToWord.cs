using System.Collections.Generic;
using HotelBusinessLogic.OfficePackage.HelperEnums;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.OfficePackage
{
    public abstract class OrganizerAbstractSaveToWord
    {
        public void CreateDoc(OrganizerWordInfo info)
        {
            CreateWord(info);

            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24", }) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });

            foreach (var cl in info.ConferenceLunches)
            {
                var lunches = new List<(string, WordTextProperties)>();
                lunches.Add((cl.ConferenceName + ": ", new WordTextProperties { Bold = true, Size = "24", }));
    
                foreach (var l in cl.Lunches)
                {
                    lunches.Add((l.Item1 + "; ", new WordTextProperties { Size = "24", }));

                }

                CreateParagraph(new WordParagraph
                {
                    Texts = lunches,
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }

            SaveWord(info);
        }

        // Создание doc-файла
        protected abstract void CreateWord(OrganizerWordInfo info);

        // Создание абзаца с текстом
        protected abstract void CreateParagraph(WordParagraph paragraph);

        // Сохранение файла
        protected abstract void SaveWord(OrganizerWordInfo info);
    }
}
