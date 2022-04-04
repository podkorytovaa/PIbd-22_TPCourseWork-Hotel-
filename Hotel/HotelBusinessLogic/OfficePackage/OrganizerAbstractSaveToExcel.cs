using HotelBusinessLogic.OfficePackage.HelperEnums;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.OfficePackage
{
    public abstract class OrganizerAbstractSaveToExcel
    {
		// Создание отчета номер: семинар
		public void CreateReport(OrganizerExcelInfo info)
		{
			CreateExcel(info);

			InsertCellInWorksheet(new ExcelCellParameters
			{
				ColumnName = "A",
				RowIndex = 1,
				Text = info.Title,
				StyleInfo = ExcelStyleInfoType.Title
			});

			MergeCells(new ExcelMergeParameters
			{
				CellFromName = "A1",
				CellToName = "D1"
			});

			uint rowIndex = 2;
			foreach (var cl in info.ConferenceLunches)
			{
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = cl.ConferenceName,
					StyleInfo = ExcelStyleInfoType.Text
				});
				MergeCells(new ExcelMergeParameters
				{
					CellFromName = "A" + rowIndex,
					CellToName = "B" + rowIndex
				});

				foreach (var lunch in cl.Lunches)
				{
					InsertCellInWorksheet(new ExcelCellParameters
					{
						ColumnName = "C",
						RowIndex = rowIndex,
						Text = lunch.Item1,
						StyleInfo = ExcelStyleInfoType.TextWithBroder
					});
					rowIndex++;
				}
				rowIndex++;
			}

			SaveExcel(info);
		}

		// Создание excel-файла
		protected abstract void CreateExcel(OrganizerExcelInfo info);

		// Добавляем новую ячейку в лист
		protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

		// Объединение ячеек
		protected abstract void MergeCells(ExcelMergeParameters excelParams);

		// Сохранение файла
		protected abstract void SaveExcel(OrganizerExcelInfo info);
	}
}
