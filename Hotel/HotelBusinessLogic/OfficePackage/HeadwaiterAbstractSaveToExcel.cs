using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBusinessLogic.OfficePackage.HelperEnums;
using HotelBusinessLogic.OfficePackage.HelperModels;

namespace HotelBusinessLogic.OfficePackage
{
    public abstract class HeadwaiterAbstractSaveToExcel
    {
		// Создание отчета номер: семинар
		public void CreateReport(HeadwaiterExcelInfo info)
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
				CellToName = "E1"
			});

			uint rowIndex = 2;
			foreach (var rs in info.RoomSeminars)
			{
				InsertCellInWorksheet(new ExcelCellParameters
				{
					ColumnName = "A",
					RowIndex = rowIndex,
					Text = rs.RoomNumber,
					StyleInfo = ExcelStyleInfoType.Text
				});
				MergeCells(new ExcelMergeParameters
				{
					CellFromName = "A" + rowIndex,
					CellToName = "B" + rowIndex
				});

				foreach (var seminar in rs.Seminars)
				{
					InsertCellInWorksheet(new ExcelCellParameters
					{
						ColumnName = "C",
						RowIndex = rowIndex,
						Text = seminar.Item1,
						StyleInfo = ExcelStyleInfoType.Text
					});
					MergeCells(new ExcelMergeParameters
					{
						CellFromName = "C" + rowIndex,
						CellToName = "E" + rowIndex
					});
					rowIndex++;
				}
				rowIndex++;
			}

			SaveExcel(info);
		}

		// Создание excel-файла
		protected abstract void CreateExcel(HeadwaiterExcelInfo info);

		// Добавляем новую ячейку в лист
		protected abstract void InsertCellInWorksheet(ExcelCellParameters excelParams);

		// Объединение ячеек
		protected abstract void MergeCells(ExcelMergeParameters excelParams);

		// Сохранение файла
		protected abstract void SaveExcel(HeadwaiterExcelInfo info);
	}
}
