using crud_blazor.Shared.Models;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace crud_blazor.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExportController : ControllerBase
    {
        #region Custom mapping classes

        class ArticleMap : ClassMap<Article>
        {
            public ArticleMap()
            {
                Map(m => m.Id).Name("Id");
                Map(m => m.Name).Name("Name");
                Map(m => m.CreatedDate).Name("Creation date");
                References<CategoryMap>(m => m.Category);
            }
        }

        class CategoryMap : ClassMap<Category>
        {
            public CategoryMap()
            {
                Map(m => m.Name).Name("Category name");
            }
        }

        #endregion

        #region Articles

        [HttpPost("articles/csv")]
        public FileContentResult ArticlesToCSV(Article[] articles)
        {
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    sw.WriteLine("sep=,"); // FIX only for MS Excel

                    using (var csv = new CsvWriter(sw, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        // custom content mapping
                        csv.Context.RegisterClassMap<ArticleMap>();
                        csv.Context.RegisterClassMap<CategoryMap>();

                        // custom format for dates
                        var opt = new TypeConverterOptions { Formats = new[] { "dd/MM/yyyy" } };
                        csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(opt);

                        csv.WriteRecords(articles);
                    }
                }
                return File(ms.ToArray(), "text/csv", "articles.csv");
            }
        }

        [HttpPost("articles/excel")]
        public FileContentResult ArticlesToExcel(Article[] articles)
        {
            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Articles");

                ws.Cells["A1"].Value = "Id";
                ws.Cells["B1"].Value = "Name";
                ws.Cells["C1"].Value = "Creation date";
                ws.Cells["D1"].Value = "Category";

                ws.Column(3).Width = 15;
                ws.Column(4).Width = 20;

                for (int i = 0; i < articles.Length; i++)
                {
                    var item = articles[i];
                    ws.Cells[i + 2, 1].Value = item.Id;
                    ws.Cells[i + 2, 2].Value = item.Name;
                    ws.Cells[i + 2, 3].Value = item.CreatedDate.ToString("dd/MM/yyyy");
                    ws.Cells[i + 2, 4].Value = item.Category.Name;
                }

                ws.Cells["C:C"].Style.Numberformat.Format = "dd/mm/yyyy";

                return File(package.GetAsByteArray(), "text/pdf", "articles.xlsx");
            }
        }

        [HttpPost("articles/pdf")]
        public FileContentResult ArticlesToPDF(Article[] articles)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Add title
                //document.Add(new Paragraph("List of Articles"));
                Paragraph title = new Paragraph("List of Articles").
                    SetTextAlignment(TextAlignment.CENTER).
                    SetFontSize(14);
                document.Add(title);
                document.Add(new Paragraph("\n"));

                // Add table
                float[] colWidths = { 7, 15, 12, 15 }; //set 4 cols
                Table table = new Table(UnitValue.CreatePercentArray(colWidths)).
                    //UseAllAvailableWidth().
                    SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddHeaderCell("Id").SetTextAlignment(TextAlignment.CENTER);
                table.AddHeaderCell("Name").SetTextAlignment(TextAlignment.CENTER);
                table.AddHeaderCell("Creation Date").SetTextAlignment(TextAlignment.CENTER);
                table.AddHeaderCell("Category Name").SetTextAlignment(TextAlignment.CENTER);

                foreach (var article in articles)
                {
                    table.AddCell(new Cell().Add(new Paragraph(article.Id.ToString())).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(new Cell().Add(new Paragraph(article.Name)).SetMaxWidth(15).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(new Cell().Add(new Paragraph(article.CreatedDate.ToString("dd/MM/yyyy"))).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(new Cell().Add(new Paragraph(article.Category.Name)).SetMaxWidth(15).SetTextAlignment(TextAlignment.CENTER));
                }

                document.Add(table);
                document.Close();

                return File(ms.ToArray(), "text/pdf", "articles.pdf");
            }
        }

        #endregion

        #region Categories

        [HttpPost("categories/csv")]
        public FileContentResult CategoriesToCSV(Category[] categories)
        {
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(ms))
                {
                    sw.WriteLine("sep=,"); // FIX only for MS Excel
                    using (var csv = new CsvWriter(sw, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        var opt = new TypeConverterOptions { Formats = new[] { "dd/MM/yyyy" } };
                        csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(opt);

                        csv.WriteRecords(categories);
                    }
                    //var array = ms.ToArray();
                }

                return File(ms.ToArray(), "text/csv", "categories.csv");
            }
        }

        [HttpPost("categories/excel")]
        public FileContentResult CategoriesToExcel(Category[] categories)
        {
            using (var package = new ExcelPackage())
            {
                var ws = package.Workbook.Worksheets.Add("Categories");

                ws.Cells["A1"].Value = "Id";
                ws.Cells["B1"].Value = "Name";
                ws.Cells["C1"].Value = "Creation date";

                ws.Column(3).Width = 15;

                var body = ws.Cells["A2:A2"].LoadFromCollection(categories);
                body.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                ws.Cells["C:C"].Style.Numberformat.Format = "dd/mm/yyyy";

                return File(package.GetAsByteArray(), "text/excel", "categories.xlsx");
            }
        }
        
        [HttpPost("categories/pdf")]
        public FileContentResult CategoriesToPDF(Category[] categories)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(stream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Add title
                document.Add(new Paragraph("List of Categories").
                    SetTextAlignment(TextAlignment.CENTER).
                    SetFontSize(14));
                document.Add(new Paragraph("\n"));

                // Add table
                float[] colWidths = { 7, 15, 10 };
                //Table table = new Table(3);
                Table table = new Table(UnitValue.CreatePercentArray(colWidths)).
                    SetHorizontalAlignment(HorizontalAlignment.CENTER);

                table.AddHeaderCell("Id").SetTextAlignment(TextAlignment.CENTER);
                table.AddHeaderCell("Name").SetTextAlignment(TextAlignment.CENTER);
                table.AddHeaderCell("Creation Date").SetTextAlignment(TextAlignment.CENTER);

                foreach (var category in categories)
                {
                    table.AddCell(new Cell().Add(new Paragraph(category.Id.ToString())).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(new Cell().Add(new Paragraph(category.Name)).SetMaxWidth(15).SetTextAlignment(TextAlignment.CENTER));
                    table.AddCell(new Cell().Add(new Paragraph(category.CreatedDate.ToString("dd/MM/yyyy"))).SetTextAlignment(TextAlignment.CENTER));
                }

                document.Add(table);
                document.Close();

                return File(stream.ToArray(), "text/pdf", "categories.pdf");
            }
        }

        #endregion
    }
}
