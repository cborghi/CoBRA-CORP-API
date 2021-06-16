using CoBRA.Application.Interfaces;
using CoBRA.Application.ViewModels;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using Microsoft.Net.Http.Headers;
//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X14 = DocumentFormat.OpenXml.Office2010.Excel;

namespace CoBRA.API.Controllers
{
    [Route("api/[controller]")]
    public class RelatorioPainelController : BaseController
    {
        IAcompanhamentoMetaConsultorAppService _acompanhamentoMetaConsultorAppService;

        private List<PainelMetaAnualViewModel> painelMetasAnuais = null;

        private PainelMetaAnualGrupoViewModel painelMetasAnuaisGrupo = null;

        private SheetData sheetData = new SheetData();

        public RelatorioPainelController(IAcompanhamentoMetaConsultorAppService acompanhamentoMetaConsultorAppService, ILogAppService logAppService) : base(logAppService)
        {
            _acompanhamentoMetaConsultorAppService = acompanhamentoMetaConsultorAppService;
        }

        [HttpGet("CalculoIndividual")]
        public async Task<IActionResult> ObterCalculoIndividual(Guid UsuarioId, Guid? PeriodoId)
        {
            try
            {
                ObterDadosProcessamentoIndividual(UsuarioId, PeriodoId);
                var cabecalhoRelatorio = await _acompanhamentoMetaConsultorAppService.BuscarDadosCabecalhoRelatorio(UsuarioId);
                return Json(new {cabecalho = cabecalhoRelatorio, painel = painelMetasAnuais.OrderBy(x => x.Indicador).ToList()});
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("CalculoGrupo")]
        public IActionResult ObterCalculoGrupo(Guid? GrupoId, Guid? CargoId, Guid? PeriodoId)
        {
            try
            {
                ObterDadosProcessamentoGrupo(GrupoId, CargoId, PeriodoId);
                string a = JsonConvert.SerializeObject(painelMetasAnuaisGrupo);
                return Json(painelMetasAnuaisGrupo);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("Individual")]
        public IActionResult ObterPainelIndividual(Guid UsuarioId, string Tipo = "")
        {
            try
            {
                ObterDadosProcessamentoIndividual(UsuarioId, null);
                
                if (Tipo.Equals("PDF"))
                {
                    byte[] resultPdf = null;

                    using (MemoryStream streamPdf = ExportarPdf("Individual"))
                    {
                        resultPdf = streamPdf.ToArray();
                    };

                    return File(fileContents: resultPdf,
                                    contentType: "application/pdf",
                                    fileDownloadName: painelMetasAnuais[0].Nome.Replace(" ", "_") + "_" + DateTime.Now.ToString().Replace(" ", "_") + "_" + ".pdf");
                }
                else
                {
                    byte[] resultExcel = null;

                    using (MemoryStream streamExcel = ExportarExcel("Individual"))
                    {
                        resultExcel = streamExcel.ToArray();
                    };

                    return File(fileContents: resultExcel,
                                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                fileDownloadName: painelMetasAnuais[0].Nome.Replace(" ", "_") + "_" + DateTime.Now.ToString().Replace(" ", "_") + "_" + ".xlsx");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        private MemoryStream ExportarExcel(string tipo)
        {
            DataTable table = null;

            if (tipo.Equals("Individual"))
            {
                table = CarregarDadosPainelIndividual();
            }

            if (table != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (SpreadsheetDocument document = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
                    {
                        WorkbookPart workbookPart = document.AddWorkbookPart();
                        workbookPart.Workbook = new Workbook();
                        WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                        worksheetPart.Worksheet = new Worksheet(sheetData);

                        WorkbookStylesPart wbsp = workbookPart.AddNewPart<WorkbookStylesPart>();
                        wbsp.Stylesheet = CreateStylesheet();
                        wbsp.Stylesheet.Save();

                        Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                        Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                        sheets.Append(sheet);

                        Columns columnsSheet = new Columns();
                        columnsSheet.Append(new Column() { Min = 1, Max = 1, Width = 20, CustomWidth = true });
                        worksheetPart.Worksheet.Append(columnsSheet);

                        Row headerRow = new Row();

                        List<String> columns = new List<string>();

                        if (table != null)
                        {
                            foreach (System.Data.DataColumn column in table.Columns)
                            {
                                columns.Add(column.ColumnName);

                                Cell cell = new Cell();

                                cell.DataType = CellValues.String;


                                if (!column.Caption.Equals("_"))
                                {
                                    cell.CellValue = new CellValue(column.ColumnName);
                                }
                                else
                                {
                                    cell.CellValue = new CellValue("");
                                }

                                headerRow.AppendChild(cell);
                            }

                            sheetData.AppendChild(headerRow);

                            int RollInd = 0;
                            int RollSeg1 = 0;
                            int RollSeg2 = 0;
                            bool indicador = true;

                            foreach (DataRow dsrow in table.Rows)
                            {
                                Row newRow = new Row();

                                int CellInd = 0;
                                foreach (String col in columns)
                                {
                                    Cell cell = new Cell();
                                    if (RollInd == 0)
                                    {
                                        cell = new Cell { StyleIndex = (UInt32Value)0U };
                                    }
                                    else if (RollInd == 2)
                                    {
                                        if (CellInd == 2
                                            || CellInd == 3
                                            || CellInd == 5)
                                        {
                                            cell = new Cell { StyleIndex = (UInt32Value)1U };
                                        }
                                        if (CellInd == 4)
                                        {
                                            cell = new Cell { StyleIndex = (UInt32Value)2U };
                                        }
                                        if (CellInd == 6
                                            || CellInd == 7
                                            || CellInd == 8)
                                        {
                                            cell = new Cell { StyleIndex = (UInt32Value)3U };
                                        }
                                        if (CellInd == 9
                                            || CellInd == 10
                                            || CellInd == 11)
                                        {
                                            cell = new Cell { StyleIndex = (UInt32Value)4U };

                                        }
                                    }
                                    else if (RollInd >= 3 && indicador)
                                    {
                                        if (CellInd >= 2)
                                        {
                                            if (!string.IsNullOrEmpty(dsrow[col].ToString()))
                                            {
                                                cell = new Cell { StyleIndex = (UInt32Value)5U };
                                            }
                                            else
                                            {
                                                RollSeg1 = RollInd + 1;
                                                indicador = false;
                                            }
                                        }
                                    }
                                    else if (RollInd == RollSeg1)
                                    {
                                        if (CellInd == 4)
                                        {
                                            cell = new Cell { StyleIndex = (UInt32Value)2U };
                                        }
                                        else if (CellInd == 11)
                                        {
                                            RollSeg2 = RollInd + 2;
                                            cell = new Cell { StyleIndex = (UInt32Value)4U };
                                        }
                                    }
                                    else if (RollInd == RollSeg2)
                                    {
                                        if (CellInd == 0
                                            || CellInd == 1
                                            || CellInd == 2)
                                        {
                                            cell = new Cell { StyleIndex = (UInt32Value)3U };
                                        }
                                    }
                                    else if (RollInd == RollSeg2 + 1
                                            )
                                    {
                                        if (CellInd == 1
                                            || CellInd == 2)
                                        {
                                            cell = new Cell { StyleIndex = (UInt32Value)5U };
                                        }
                                    }
                                    else if (RollInd == RollSeg2 + 2
                                            || RollInd == RollSeg2 + 3
                                            || RollInd == RollSeg2 + 4
                                            || RollInd == RollSeg2 + 5)
                                    {
                                        if (CellInd == 0)
                                        {
                                            cell = new Cell { StyleIndex = (UInt32Value)3U };
                                        }
                                        else
                                        {
                                            cell = new Cell { StyleIndex = (UInt32Value)5U };
                                        }
                                    }

                                    if (RollInd == 0)
                                    {
                                        if (CellInd == 0)
                                        {
                                            cell.CellReference = "A2";
                                            cell.DataType = CellValues.String;
                                            cell.CellValue = new CellValue(dsrow[col].ToString());

                                            MergeCells mergeCells = new MergeCells();

                                            mergeCells.Append(new MergeCell() { Reference = new StringValue("A2:G2") });
                                            worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());
                                        }
                                    }
                                    else
                                    {
                                        cell.DataType = CellValues.String;
                                        cell.CellValue = new CellValue(dsrow[col].ToString());
                                    }

                                    newRow.AppendChild(cell);
                                    CellInd++;
                                }

                                sheetData.AppendChild(newRow);
                                RollInd++;
                            }
                        }

                        workbookPart.Workbook.ToArray();

                        memoryStream.Seek(0, SeekOrigin.Begin);

                        return memoryStream;
                    }
                }
            }

            return new MemoryStream();
        }

        private static Stylesheet CreateStylesheet()
        {
            Stylesheet stylesheet1 = new Stylesheet() { MCAttributes = new MarkupCompatibilityAttributes() { Ignorable = "x14ac" } };
            stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");

            Fonts fonts1 = new Fonts() { Count = (UInt32Value)1U, KnownFonts = true };

            Font font1 = new Font();
            FontSize fontSize1 = new FontSize() { Val = 13D };
            X14.Color color1 = new X14.Color() { Theme = (UInt32Value)1U };
            FontName fontName1 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering1 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme1 = new FontScheme() { Val = FontSchemeValues.Minor };

            Font font2 = new Font();
            FontSize fontSize2 = new FontSize() { Val = 11D };
            X14.Color color2 = new X14.Color() { Theme = (UInt32Value)2U };
            FontName fontName2 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering2 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme2 = new FontScheme() { Val = FontSchemeValues.Minor };

            Font font3 = new Font();
            FontSize fontSize3 = new FontSize() { Val = 11D };
            X14.Color color3 = new X14.Color() { Theme = (UInt32Value)2U };
            FontName fontName3 = new FontName() { Val = "Calibri" };
            FontFamilyNumbering fontFamilyNumbering3 = new FontFamilyNumbering() { Val = 2 };
            FontScheme fontScheme3 = new FontScheme() { Val = FontSchemeValues.Minor };


            font1.Append(fontSize1);
            font1.Append(color1);
            font1.Append(fontName1);
            font1.Append(fontFamilyNumbering1);
            font1.Append(fontScheme1);


            font2.Append(fontSize2);
            font2.Append(color2);
            font2.Append(fontName2);
            font2.Append(fontFamilyNumbering2);
            font2.Append(fontScheme2);

            font3.Append(fontSize3);
            font3.Append(color3);
            font3.Append(fontName3);
            font3.Append(fontFamilyNumbering3);
            font3.Append(fontScheme3);
            font3.Append(new Bold());

            fonts1.Append(font1);
            fonts1.Append(font2);
            fonts1.Append(font3);

            Fills fills1 = new Fills() { Count = (UInt32Value)5U };

            //FillId = 0
            Fill fill1 = new Fill();
            PatternFill patternFill1 = new PatternFill() { PatternType = PatternValues.None };
            fill1.Append(patternFill1);

            Fill fill2 = new Fill();
            PatternFill patternFill2 = new PatternFill() { PatternType = PatternValues.Gray125 };
            fill2.Append(patternFill2);

            Fill fill3 = new Fill();
            PatternFill patternFill3 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor1 = new ForegroundColor() { Rgb = "C3C3FF" };
            BackgroundColor backgroundColor1 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill3.Append(foregroundColor1);
            patternFill3.Append(backgroundColor1);
            fill3.Append(patternFill3);

            Fill fill4 = new Fill();
            PatternFill patternFill4 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor2 = new ForegroundColor() { Rgb = "FFEAC3" };
            BackgroundColor backgroundColor2 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill4.Append(foregroundColor2);
            patternFill4.Append(backgroundColor2);
            fill4.Append(patternFill4);

            Fill fill5 = new Fill();
            PatternFill patternFill5 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor3 = new ForegroundColor() { Rgb = "CFFFC3" };
            BackgroundColor backgroundColor3 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill5.Append(foregroundColor3);
            patternFill5.Append(backgroundColor3);
            fill5.Append(patternFill5);

            Fill fill6 = new Fill();
            PatternFill patternFill6 = new PatternFill() { PatternType = PatternValues.Solid };
            ForegroundColor foregroundColor4 = new ForegroundColor() { Rgb = "FFFFC3" };
            BackgroundColor backgroundColor4 = new BackgroundColor() { Indexed = (UInt32Value)64U };
            patternFill6.Append(foregroundColor4);
            patternFill6.Append(backgroundColor4);
            fill6.Append(patternFill6);

            fills1.Append(fill1);
            fills1.Append(fill2);
            fills1.Append(fill3);
            fills1.Append(fill4);
            fills1.Append(fill5);
            fills1.Append(fill6);

            Borders borders1 = new Borders() { Count = (UInt32Value)1U };

            Border border1 = new Border();

            LeftBorder leftBorder2 = new LeftBorder() { Style = BorderStyleValues.Double };
            X14.Color borderColor1 = new X14.Color() { Indexed = (UInt32Value)64U };
            leftBorder2.Append(borderColor1);

            RightBorder rightBorder2 = new RightBorder() { Style = BorderStyleValues.Thin };
            X14.Color borderColor2 = new X14.Color() { Indexed = (UInt32Value)64U };
            rightBorder2.Append(borderColor2);

            TopBorder topBorder2 = new TopBorder() { Style = BorderStyleValues.Thin };
            X14.Color borderColor3 = new X14.Color() { Indexed = (UInt32Value)64U };
            topBorder2.Append(borderColor3);

            BottomBorder bottomBorder2 = new BottomBorder() { Style = BorderStyleValues.Thin };
            X14.Color borderColor4 = new X14.Color() { Indexed = (UInt32Value)64U };
            bottomBorder2.Append(borderColor4);

            DiagonalBorder diagonalBorder1 = new DiagonalBorder();

            border1.Append(leftBorder2);
            border1.Append(rightBorder2);
            border1.Append(topBorder2);
            border1.Append(bottomBorder2);
            border1.Append(diagonalBorder1);

            borders1.Append(border1);

            CellStyleFormats cellStyleFormats1 = new CellStyleFormats() { Count = (UInt32Value)1U };
            CellFormat cellFormat1 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U };

            cellStyleFormats1.Append(cellFormat1);

            CellFormats cellFormats1 = new CellFormats() { Count = (UInt32Value)4U };
            CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)0U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };
            CellFormat cellFormat3 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)2U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat4 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)3U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat5 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)4U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat6 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)2U, FillId = (UInt32Value)5U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U, ApplyFill = true };
            CellFormat cellFormat7 = new CellFormat() { NumberFormatId = (UInt32Value)0U, FontId = (UInt32Value)1U, FillId = (UInt32Value)0U, BorderId = (UInt32Value)0U, FormatId = (UInt32Value)0U };


            cellFormats1.Append(cellFormat2);
            cellFormats1.Append(cellFormat3);
            cellFormats1.Append(cellFormat4);
            cellFormats1.Append(cellFormat5);
            cellFormats1.Append(cellFormat6);
            cellFormats1.Append(cellFormat7);

            CellStyles cellStyles1 = new CellStyles() { Count = (UInt32Value)1U };
            CellStyle cellStyle1 = new CellStyle() { Name = "Normal", FormatId = (UInt32Value)0U, BuiltinId = (UInt32Value)0U };

            cellStyles1.Append(cellStyle1);
            DifferentialFormats differentialFormats1 = new DifferentialFormats() { Count = (UInt32Value)0U };
            TableStyles tableStyles1 = new TableStyles() { Count = (UInt32Value)0U, DefaultTableStyle = "TableStyleMedium2", DefaultPivotStyle = "PivotStyleMedium9" };

            StylesheetExtensionList stylesheetExtensionList1 = new StylesheetExtensionList();

            StylesheetExtension stylesheetExtension1 = new StylesheetExtension() { Uri = "{EB79DEF2-80B8-43e5-95BD-54CBDDF9020C}" };
            stylesheetExtension1.AddNamespaceDeclaration("x14", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/main");
            X14.SlicerStyles slicerStyles1 = new X14.SlicerStyles() { DefaultSlicerStyle = "SlicerStyleLight1" };

            stylesheetExtension1.Append(slicerStyles1);

            stylesheetExtensionList1.Append(stylesheetExtension1);

            stylesheet1.Append(fonts1);
            stylesheet1.Append(fills1);
            stylesheet1.Append(borders1);
            stylesheet1.Append(cellStyleFormats1);
            stylesheet1.Append(cellFormats1);
            stylesheet1.Append(cellStyles1);
            stylesheet1.Append(differentialFormats1);
            stylesheet1.Append(tableStyles1);
            stylesheet1.Append(stylesheetExtensionList1);
            return stylesheet1;
        }

        private MemoryStream ExportarPdf(string tipo)
        {
            DataTable table = null;

            if (tipo.Equals("Individual"))
            {
                table = CarregarDadosPainelIndividual();
            }

            using (MemoryStream streamPdf = new MemoryStream())
            {
                PdfDocument pdfDoc = new PdfDocument(new PdfWriter(streamPdf));
                Document doc = new Document(pdfDoc);

                doc.SetMargins(0, 0, 0, 0);

                iText.Layout.Element.Table datatable = new iText.Layout.Element.Table(UnitValue.CreatePercentArray(12)).UseAllAvailableWidth();
                datatable.SetHorizontalAlignment(HorizontalAlignment.CENTER);
                datatable.SetWidth(100);

                if (table != null)
                {
                    foreach (System.Data.DataRow dsrow in table.Rows)
                    {
                        foreach (System.Data.DataColumn col in table.Columns)
                        {
                            iText.Layout.Element.Cell cell = new iText.Layout.Element.Cell().Add(new iText.Layout.Element.Paragraph(dsrow[col.ColumnName].ToString()).SetFontSize(8));
                            datatable.AddCell(cell);
                        }
                    }
                }

                doc.Add(datatable);

                doc.Close();

                return streamPdf;
            }
        }

        private DataTable CarregarDadosPainelIndividual()
        {
            if (painelMetasAnuais != null && painelMetasAnuais.Count > 0)
            {
                DataTable table = new DataTable();

                DataColumn columnNome = new DataColumn();
                columnNome.DataType = Type.GetType("System.String");
                columnNome.ColumnName = "1";
                columnNome.Caption = "_";
                table.Columns.Add(columnNome);

                DataColumn columnCargo = new DataColumn();
                columnCargo.DataType = Type.GetType("System.String");
                columnCargo.ColumnName = "2";
                columnCargo.Caption = "_";
                table.Columns.Add(columnCargo);

                DataRow row = table.NewRow();
                row["1"] = painelMetasAnuais[0].Nome + " - " + painelMetasAnuais[0].Cargo;
                table.Rows.Add(row);

                DataRow rowFilial = table.NewRow();
                rowFilial["1"] = "Filial: CEARA";
                table.Rows.Add(rowFilial);

                DataRow rowBlank1 = table.NewRow();
                table.Rows.Add(rowBlank1);

                DataColumn columnIndicador = new DataColumn();
                columnIndicador.DataType = Type.GetType("System.String");
                columnIndicador.ColumnName = "3";
                columnIndicador.Caption = "_";
                table.Columns.Add(columnIndicador);

                DataColumn columnMeta = new DataColumn();
                columnMeta.DataType = Type.GetType("System.String");
                columnMeta.ColumnName = "4";
                columnMeta.Caption = "_";
                table.Columns.Add(columnMeta);

                DataColumn columnPeso = new DataColumn();
                columnPeso.DataType = Type.GetType("System.String");
                columnPeso.ColumnName = "5";
                columnPeso.Caption = "_";
                table.Columns.Add(columnPeso);

                DataColumn columnTotal = new DataColumn();
                columnTotal.DataType = Type.GetType("System.String");
                columnTotal.ColumnName = "6";
                columnTotal.Caption = "_";
                table.Columns.Add(columnTotal);

                DataColumn columnMinimo = new DataColumn();
                columnMinimo.DataType = Type.GetType("System.String");
                columnMinimo.ColumnName = "7";
                columnMinimo.Caption = "_";
                table.Columns.Add(columnMinimo);

                DataColumn columnMaximo = new DataColumn();
                columnMaximo.DataType = Type.GetType("System.String");
                columnMaximo.ColumnName = "8";
                columnMaximo.Caption = "_";
                table.Columns.Add(columnMaximo);

                DataColumn columnUnidade = new DataColumn();
                columnUnidade.DataType = Type.GetType("System.String");
                columnUnidade.ColumnName = "9";
                columnUnidade.Caption = "_";
                table.Columns.Add(columnUnidade);

                DataColumn columnRealizado = new DataColumn();
                columnRealizado.DataType = Type.GetType("System.String");
                columnRealizado.ColumnName = "10";
                columnRealizado.Caption = "_";
                table.Columns.Add(columnRealizado);

                DataColumn columnPonderado = new DataColumn();
                columnPonderado.DataType = Type.GetType("System.String");
                columnPonderado.ColumnName = "11";
                columnPonderado.Caption = "_";
                table.Columns.Add(columnPonderado);

                DataColumn columnPercentual = new DataColumn();
                columnPercentual.DataType = Type.GetType("System.String");
                columnPercentual.ColumnName = "12";
                columnPercentual.Caption = "_";
                table.Columns.Add(columnPercentual);

                DataRow rowCaption = table.NewRow();

                rowCaption["3"] = "Indicador";
                rowCaption["4"] = "Meta";
                rowCaption["5"] = "Peso";
                rowCaption["6"] = "Total";
                rowCaption["7"] = "ValorMinimo";
                rowCaption["8"] = "ValorMaximo";
                rowCaption["9"] = "UnidadeMedida";
                rowCaption["10"] = "Realizado";
                rowCaption["11"] = "Ponderado";
                rowCaption["12"] = "Percentual";

                table.Rows.Add(rowCaption);

                foreach (PainelMetaAnualViewModel item in painelMetasAnuais)
                {
                    DataRow rowItem = table.NewRow();

                    rowItem["3"] = item.Indicador;
                    rowItem["4"] = item.Meta;
                    rowItem["5"] = item.Peso;
                    rowItem["6"] = item.Total;
                    rowItem["7"] = item.ValorMinimo;
                    rowItem["8"] = item.ValorMaximo;
                    rowItem["9"] = item.UnidadeMedida;
                    rowItem["10"] = item.realizado;
                    rowItem["11"] = item.ponderado;
                    rowItem["12"] = item.percentual;

                    table.Rows.Add(rowItem);
                }

                DataRow rowBlank2 = table.NewRow();
                table.Rows.Add(rowBlank2);

                DataRow row0 = table.NewRow();
                row0["5"] = "Peso Total: " + painelMetasAnuais[0].pesoTotal;
                row0["12"] = "Total: " + painelMetasAnuais[0].realAtingido;
                table.Rows.Add(row0);

                DataRow rowBlank3 = table.NewRow();
                table.Rows.Add(rowBlank3);

                DataRow row1 = table.NewRow();
                row1["1"] = "Valor Potencial de Ganho";
                row1["2"] = "Meta | Real)";
                row1["3"] = "Receita | Tabela";
                table.Rows.Add(row1);

                DataRow row2 = table.NewRow();
                row2["1"] = "";
                row2["2"] = painelMetasAnuais[0].valorRecebimento.ToString();
                row2["3"] = painelMetasAnuais[0].valorMetaReceitaLiquida.ToString();
                table.Rows.Add(row2);

                DataRow row3 = table.NewRow();
                row3["1"] = "% Atingido";
                row3["2"] = painelMetasAnuais[0].realAtingido.ToString();
                row3["3"] = painelMetasAnuais[0].porcentagemPagamento.ToString();
                table.Rows.Add(row3);

                DataRow row4 = table.NewRow();
                row4["1"] = "Ganho";
                row4["2"] = painelMetasAnuais[0].ganhoReal.ToString();
                row4["3"] = painelMetasAnuais[0].ganhoReceita.ToString();
                table.Rows.Add(row4);

                DataRow row5 = table.NewRow();
                row5["1"] = "Antecipado (-)";
                row5["2"] = painelMetasAnuais[0].antecipado.ToString();
                row5["3"] = painelMetasAnuais[0].antecipado.ToString();
                table.Rows.Add(row5);

                DataRow row6 = table.NewRow();
                row6["1"] = "Total a Receber";
                row6["2"] = painelMetasAnuais[0].totalReceberReal.ToString();
                row6["3"] = painelMetasAnuais[0].totalReceberReceita.ToString();
                table.Rows.Add(row6);

                //string json = JsonConvert.SerializeObject(relatorioPainelIndividual.PainelMetaAnual);

                return table; //(DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
            }
            else
            {
                return null;
            }
        }

        private void ObterDadosProcessamentoIndividual(Guid? UsuarioId, Guid? PeriodoId)
        {
            if (!UsuarioId.Equals("00000000-0000-0000-0000-000000000000"))
            {
                painelMetasAnuais = _acompanhamentoMetaConsultorAppService.CalculoPotencial(null, null, null, null, null, UsuarioId, null, PeriodoId);
            }
        }

        private void ObterDadosProcessamentoGrupo(Guid? GrupoId, Guid? CargoId, Guid? PeriodoId)
        {
            if (!GrupoId.Equals("00000000-0000-0000-0000-000000000000"))
            {
                painelMetasAnuaisGrupo = _acompanhamentoMetaConsultorAppService.CalculoPotencialGrupo(GrupoId, CargoId, PeriodoId);

                painelMetasAnuaisGrupo.LstMetasGrupo =  painelMetasAnuaisGrupo.LstMetasGrupo.OrderBy(item => item.Indicador).ToList();

            }
        }
    }
}