using DatabaseManagement;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using WinForms = System.Windows.Forms;

namespace DataExport
{
	public class DocumentMaster
	{
		// Константы для директорий и названий файлов
		private const string DocumentApplicationSample = "Заявки";
		private const string DocumentReportSample = "Отчёты";
		private const string DocumentStockSample = "ЦентральныйСклад";
		private const string DocumentReceiptSample = "Поступления";

		private static DocumentMaster _instance;

		// Получение экземпляра класса DocumentMaster (реализация паттерна Singleton)
		public static DocumentMaster Instance()
		{
			if (_instance == null)
				_instance = new DocumentMaster();

			return _instance;
		}

		private DocumentMaster()
		{ }

		/// <summary>
		/// Выбор директории для сохранения документов
		/// </summary>
		/// <returns>Путь к выбранной директории</returns>
		private string GetFolder()
		{
			WinForms.FolderBrowserDialog folderBrowser = new WinForms.FolderBrowserDialog();
			WinForms.DialogResult result = folderBrowser.ShowDialog();

			if (result != WinForms.DialogResult.OK)
				throw new Exception("Папка не выбрана");

			if (!string.IsNullOrWhiteSpace(folderBrowser.SelectedPath))
				return folderBrowser.SelectedPath;

			throw new Exception("Папка имеет некорректный формат");
		}

		/// <summary>
		/// Создание документа заявок в формате excel
		/// </summary>
		/// <param name="applicationModel">Модель заявки</param>
		public void CreateApplicationDocument(List<ApplicationModel> applicationModels)
		{
			ExcelDocumentCreator excelDocumentCreator = new ExcelDocumentCreator();

			string[] titles = new string[] { "Id", "Дата", "Ресторан", "Продукция", "Количество" };

			string[,] data = new string[applicationModels.Count, titles.Length];

			// Заполняем данные для таблицы
			for (int i = 0; i < applicationModels.Count; i++)
			{
				ApplicationModel application = applicationModels[i];

				data[i, 0] = application.Id.ToString();
				data[i, 1] = application.Date;
				data[i, 2] = application.Restaurant.Name;
				data[i, 3] = application.Product.Name;
				data[i, 4] = application.Count.ToString();
			}

			string filePath = GetFolder();
			excelDocumentCreator.ExportDataToExcel(titles, data, filePath, DocumentApplicationSample);
		}

		public void CreateReceiptsDocument(List<ReceiptModel> receiptsList)
		{
			ExcelDocumentCreator excelDocumentCreator = new ExcelDocumentCreator();

			string[] titles = new string[] { "Id", "Дата", "Поставщик", "Продукция", "Количество" };

			string[,] data = new string[receiptsList.Count, titles.Length];

			// Заполняем данные для таблицы
			for (int i = 0; i < receiptsList.Count; i++)
			{
				ReceiptModel receipt = receiptsList[i];

				data[i, 0] = receipt.Id.ToString();
				data[i, 1] = receipt.Date;
				data[i, 2] = receipt.Provider.Name;
				data[i, 3] = receipt.Product.Name;
				data[i, 4] = receipt.Count.ToString();
			}

			string filePath = GetFolder();
			excelDocumentCreator.ExportDataToExcel(titles, data, filePath, DocumentReceiptSample);
		}

		/// <summary>
		/// Создание документа отчетов ресторанов в формате excel
		/// </summary>
		/// <param name="reportModel">Модель отчета</param>
		public void CreateReportDocument(List<ReportModel> reportModels)
		{
			ExcelDocumentCreator excelDocumentCreator = new ExcelDocumentCreator();

			string[] titles = new string[] { "Id", "Дата", "Категория Продукции" , "Объём реализации", "Ед. измерения", "Выручка" };

			string[,] data = new string[reportModels.Count, titles.Length];

			// Заполняем данные для таблицы
			for (int i = 0; i < reportModels.Count; i++)
			{
				ReportModel reportModel = reportModels[i];

				data[i, 0] = reportModel.Id.ToString();
				data[i, 1] = reportModel.Date;
				data[i, 2] = reportModel.Category.Name;
				data[i, 3] = reportModel.Count.ToString();
				data[i, 4] = reportModel.Unit.Name;
				data[i, 5] = Math.Round(reportModel.Proceeds, 2).ToString();
			}

			string filePath = GetFolder();
			excelDocumentCreator.ExportDataToExcel(titles, data, filePath, DocumentReportSample);
		}

		/// <summary>
		/// Создание документа с информацией о продукции на складе в формате Excel
		/// </summary>
		/// <param name="stockModels">Список моделей склада</param>
		public void CreateStocksDocument(List<StockModel> stockModels)
		{
			ExcelDocumentCreator excelDocumentCreator = new ExcelDocumentCreator();

			string[] titles = new string[] { "Id", "Продукт", "Ед. измерения", "Количество", "Цена за ед.", "Количество" };

			string[,] data = new string[stockModels.Count, titles.Length];

			// Заполняем данные для таблицы
			for (int i = 0; i < stockModels.Count; i++)
			{
				StockModel stockModel = stockModels[i];

				data[i, 0] = stockModel.Id.ToString();
				data[i, 1] = stockModel.Product.Name;
				data[i, 2] = stockModel.Product.Unit.Name;
				data[i, 3] = stockModel.Count.ToString();
				data[i, 4] = stockModel.Price.ToString();
				data[i, 5] = stockModel.Count.ToString();
			}

			string filePath = GetFolder();
			excelDocumentCreator.ExportDataToExcel(titles, data, filePath, DocumentStockSample);
		}
	}
}
