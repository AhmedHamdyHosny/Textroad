using System;
using System.Collections.Generic;
using System.Linq;
//using System.Linq.Expressions;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Web;
using System.Data.Entity.Validation;
using static GenericApiController.Utilities.GenericDataFormat;
using System.Text.RegularExpressions;
using System.Data.Entity;
using DocumentFormat.OpenXml;

namespace GenericApiController.Utilities
{
    public class HeaderEntityReference
    {
        public List<HeaderInfo> Headers { get; set; }
        public Type ReferenceType { get; set; }
        public string ForeignKeyProperty { get; set; }

    }

    public class HeaderInfo
    {
        public int HeaderIndex { get; set; }
        public string HeaderName { get; set; }
        public string EntityPropertyName { get; set; }
    }
    public class Import<TEntity> where TEntity : class
    {
        public HttpPostedFile postedFile { get; set; }
        private SharedStringTable sharedStringTable { get; set; }
        private Dictionary<string, Type> EntityDependences { get; set; }
        private List<HeaderEntityReference> HeaderEntityReferences { get; set; }
        private DbContext Context { get; set; }

        public Import(HttpPostedFile file, DbContext context)
        {
            postedFile = file;
            Context = context;
        }

        public virtual IEnumerable<TEntity> ImportData()
        {
            //define list of container
            List<TEntity> entities = null;
            using (SpreadsheetDocument document = SpreadsheetDocument.Open(postedFile.InputStream, false))
            {
                WorkbookPart workbookPart = document.WorkbookPart;
                SharedStringTablePart sstpart = workbookPart.GetPartsOfType<SharedStringTablePart>().First();
                sharedStringTable = sstpart.SharedStringTable;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                Worksheet workSheet = worksheetPart.Worksheet;
                //get worksheet row include header
                var rows = workSheet.Descendants<Row>();
                //check if sheet contain data
                if (rows.Count() > 0)
                {
                    //get first row as columns header
                    var headerRow = rows.ElementAt(0);
                    //get reference types with foreign key properties
                    EntityDependences = Repository<TEntity>.GetReferenceTypes(Context);
                    //validate columns header
                    if (!ValidateColumnsHeader(headerRow.Elements<Cell>()))
                    {
                        //throw validation exception
                        throw new DbEntityValidationException(string.Format("Columns Header didn't match with Entity of type {0}", typeof(TEntity).Name));
                    }
                    //get entitiies
                    entities = MapData(rows, headerRow);
                }
            }
            return entities;
        }
        
        public virtual List<TEntity> MapData(IEnumerable<Row> rows, Row headerRow)
        {
            //initialize list of container to add data
            var entities = new List<TEntity>();
            //loop on rows and skip first row that contain only column header
            for (int i = 1; i < rows.Count(); i++)
            {
                // create new instance of T type
                TEntity entity = (TEntity)Activator.CreateInstance(typeof(TEntity));
                //get data row
                Row row = rows.ElementAt(i);
                //initialize list of container of setting reference properties
                List<string> settingReferenceProperties = new List<string>();
                //get row cells include empty values
                var rowCellValues = GetRowValues(row, headerRow.Elements<Cell>().Count());
                //loop on cells of row 
                for (int y = 0; y < rowCellValues.Count(); y++)
                {
                    //get reference of cell value
                    var cellValue = rowCellValues[y];
                    //to set the value to entity property 
                    //first : check about HeaderEntityReferences if exist and header index is saved in HeaderEntityReferences
                    if (HeaderEntityReferences != null && HeaderEntityReferences.Any(h => h.Headers.Any(info => info.HeaderIndex == y)))
                    {
                        SetReferenceValue(ref entity, rowCellValues,y,settingReferenceProperties);
                        
                    }
                    else
                    {
                        //get property name from columns header that will be set with value
                        string propertyName = (string)GetCellValue(headerRow.Elements<Cell>().ElementAt(y));
                        //set value to property of object
                        Repository<TEntity>.SetPropertyValue(ref entity, propertyName, cellValue);
                    }
                }
                //insert object to list of container
                entities.Add(entity);
            }

            return entities;
        }

        public virtual void SetReferenceValue(ref TEntity entity, object[] rowCellValues,int headerIndex, List<string> settingReferenceProperties)
        {
            // this cell is related to child reference entity 
            var her = HeaderEntityReferences.SingleOrDefault(h => h.Headers.Any(info => info.HeaderIndex == headerIndex));
            //check if foreign key is set value before
            if (!settingReferenceProperties.Any(s => s.Equals(her.ForeignKeyProperty, StringComparison.OrdinalIgnoreCase)))
            {
                // store the setting of foreign key to ignore duplication
                settingReferenceProperties.Add(her.ForeignKeyProperty);
                List<FilterItems> filters = new List<FilterItems>();
                //loop throught headers and its corresponding property name to built the filteration
                foreach (var column in her.Headers)
                {
                    FilterItems filter = new FilterItems() { Property = column.EntityPropertyName, Operation = FilterOperations.Equal, Value = rowCellValues[column.HeaderIndex], LogicalOperation = LogicalOperations.And };
                    filters.Add(filter);
                }

                //create Repository of reference type
                var repositoryType = typeof(Repository<>).MakeGenericType(her.ReferenceType);
                //create instance of Repository of reference type
                dynamic rep = Activator.CreateInstance(repositoryType, Context);
                //invoke GetFilter method in Repository class with paramters to get the filteration expression
                var method = repositoryType.GetMethod("GetFilter", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                //set the parameters of method
                object[] parameters = new object[] { filters };
                //invoke GetFilter method with parameters
                var filterExpr = method.Invoke(null, parameters);
               
                //invoke GetPKColumns method in Repository class with paramters to get primary columns name of linked entity
                method = repositoryType.GetMethod("GetPKColumns", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
                parameters = new object[] { Context };
                IEnumerable<string> PkColumnsName = (IEnumerable<string>) method.Invoke(null, parameters);
                
                //call GetReferenceForImport method in Repository class to select Linked Entity using filteration to get pk value 
                var referencesItems = rep.GetReferenceForImport(includeProperties: PkColumnsName,filter: filterExpr);
                //check referenceItems
                if (referencesItems == null || referencesItems.Count == 0)
                {
                    //see this if you thorw exception or insert new item of type linked entity
                    throw new Exception("there are one or more record don't not have linked items which has reference information. please review your data");
                }
                else if (referencesItems.Count > 1)
                {
                    // throw exception that there are more than one references founded in db
                    throw new Exception("there are one or more record have more than one linked items which have same reference information. please review your data");
                }
                else
                {
                    //get reference of linked item founded
                    var referenceItem = referencesItems[0];
                    //loop throught primary columns if contain more than one column to get value of pks
                    foreach (var column in PkColumnsName)
                    {

                        //get reference value of each primary column 
                        var referenceValue = Utility.GetPropertyValue(referenceItem, column);
                        if (referenceValue != null)
                        {
                            //set foreign key of entity with pk of linked entity
                            Repository<TEntity>.SetPropertyValue(ref entity, her.ForeignKeyProperty, referenceValue);
                        }
                    }
                }
            }
        }

        public virtual object[] GetRowValues(Row row, int cellCount)
        {
            var rowCellValues = new object[cellCount];
            int columnIndex = 0;
            foreach (var rowCell in row.Elements<Cell>())
            {
                //check for empty cells
                // Gets the column index of the cell with data
                int cellColumnIndex = (int)GetColumnIndexFromName(GetColumnName(rowCell.CellReference));
                cellColumnIndex--; //zero based index
                if (columnIndex < cellColumnIndex)
                {
                    do
                    {
                        rowCellValues[columnIndex] = null; //Insert blank data here;
                        columnIndex++;
                    }
                    while (columnIndex < cellColumnIndex);
                }
                rowCellValues[columnIndex] = GetCellValue(rowCell);
                columnIndex++;
            }

            return rowCellValues;
        }

        public virtual bool ValidateColumnsHeader(IEnumerable<Cell> cells)
        {
            try
            {
                //get properties of entity
                var properties = typeof(TEntity).GetProperties();
                //loop through each cell in header row 
                for (int i = 0; i < cells.Count(); i++)
                {
                    //get cell reference
                    Cell cell = cells.ElementAt(i);
                    //get column header text
                    string header = (string)GetCellValue(cell);
                    //check if header name is consider a property in entity
                    if (!properties.Any(x => x.Name.Equals(header, StringComparison.OrdinalIgnoreCase)))
                    {
                        //check in reference entity properties
                        var dependences = EntityDependences.Where(x => x.Value.GetProperties().Any(y => y.Name.Equals(header, StringComparison.OrdinalIgnoreCase)));
                        //if not exist then validation process is fail
                        if (!dependences.Any())
                        {
                            return false;
                        }
                        else
                        {
                            //header name is consider a property of child reference of the entity
                            //built HeaderEntityReferences object
                            if (HeaderEntityReferences == null)
                            {
                                HeaderEntityReferences = new List<HeaderEntityReference>();
                            }
                            //get dependence reference object that contain (Reference Type & Foreign Key Property name)
                            var dep = dependences.ElementAt(0);
                            //check if headerEntityReference which has same referece type is inserted before  
                            var her = HeaderEntityReferences.SingleOrDefault(x => x.ReferenceType == dep.Value);
                            
                            if(her == null)
                            {
                                //create list of headersInfo that hold information of header index and header name and corresponding property name in child reference entity 
                                List<HeaderInfo> headersInfo = new List<HeaderInfo>();
                                headersInfo.Add(new HeaderInfo() { HeaderIndex = i, HeaderName = header, EntityPropertyName = header });
                                // create new HeaderEntityReferences
                                her = new HeaderEntityReference()
                                    { Headers = headersInfo,
                                    ReferenceType = dep.Value,
                                    ForeignKeyProperty = dep.Key};
                                HeaderEntityReferences.Add(her);
                            }
                            else
                            {
                                //add only header infomartion
                                her.Headers.Add(new HeaderInfo() { HeaderIndex = i, HeaderName = header, EntityPropertyName = header });
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual object GetCellValue(Cell cell)
        {
            object cellValue = null;
            try
            {
                if ((cell.DataType != null) && (cell.DataType == CellValues.SharedString))
                {
                    int ssid = int.Parse(cell.CellValue.Text);
                    cellValue = sharedStringTable.ChildElements[ssid].InnerText;
                }
                else if (cell.CellValue != null)
                {
                    cellValue = cell.CellValue.Text;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return cellValue;

        }

        /// <summary>
        /// Given a cell name, parses the specified cell to get the column name.
        /// </summary>
        /// <param name="cellReference">Address of the cell (ie. B2)</param>
        /// <returns>Column Name (ie. B)</returns>
        public static string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name portion of the cell name.
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }
        /// <summary>
        /// Given just the column name (no row index), it will return the zero based column index.
        /// Note: This method will only handle columns with a length of up to two (ie. A to Z and AA to ZZ). 
        /// A length of three can be implemented when needed.
        /// </summary>
        /// <param name="columnName">Column Name (ie. A or AB)</param>
        /// <returns>Zero based index if the conversion was successful; otherwise null</returns>
        public static int? GetColumnIndexFromName(string columnName)
        {

            //return columnIndex;
            string name = columnName;
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return number;
        }
    }

    public class Export<TEntity> where TEntity : class
    {
        public List<object> DataItems { get; set; }
        public GenericDataFormat RequestData { get; set; }

        public Export(List<object> dataItems, GenericDataFormat requestData = null)
        {
            DataItems = dataItems;
            RequestData = requestData;
        }
        public virtual string ExportData()
        {
            var filePath = System.Configuration.ConfigurationManager.AppSettings["ExportPath"];
            if(filePath != null)
            {
                filePath = HttpContext.Current.Server.MapPath(filePath + typeof(TEntity).Name + ".xlsx");
            }
            else
            {
                filePath = HttpContext.Current.Server.MapPath("~/App_Data/" + typeof(TEntity).Name + ".xlsx");
            }
            using (SpreadsheetDocument ssdocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                CreateExcelDocument(ssdocument);
            }
            return filePath;
        }

        public virtual void CreateExcelDocument(SpreadsheetDocument document)
        {
            WorkbookPart workbookPart = document.AddWorkbookPart();
            GenerateWorkbookPartContent(workbookPart);
            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>(typeof(TEntity).Name);
            GenerateWorksheetPartContent(worksheetPart);
        }

        public virtual void GenerateWorkbookPartContent(WorkbookPart workbookPart)
        {
            Workbook workbook = new Workbook();
            //workbook.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            Sheets sheets = new Sheets();
            Sheet sheet = new Sheet() { Name = typeof(TEntity).Name, SheetId = (UInt32Value)1U, Id = typeof(TEntity).Name };
            sheets.Append(sheet);
            workbook.Append(sheets);
            workbookPart.Workbook = workbook;
        }

        public virtual void GenerateWorksheetPartContent(WorksheetPart worksheetPart)
        {
            Worksheet worksheet = new Worksheet();
            SheetData sheetdata = new SheetData();
            string[] headerColumns = GetColumnHeaders(DataItems.ElementAt(0).GetType());
            Row row = new Row();
            Cell cell = new Cell();
            int RowIndexer = 1;
            int ColumnIndexer = 1;
            row.RowIndex = (UInt32)RowIndexer;
            foreach (var header in headerColumns)
            {
                cell = new Cell();
                cell.CellReference = ColumnAddress(ColumnIndexer) + RowIndexer;
                cell.DataType = GetCellDataType(header);
                cell.InlineString = new InlineString(new Text(header));
                // consider using cell.CellValue. Then you don't need to use InlineString.
                // Because it seems you're not using any rich text so you're just bloating up
                // the XML.
                row.AppendChild(cell);
                ColumnIndexer++;
            }
            sheetdata.Append(row);
            RowIndexer = 2;
            foreach (var item in DataItems)
            {
                row = new Row();
                row.RowIndex = (UInt32)RowIndexer;
                // this follows the same starting column index as your column header.
                // I'm assuming you start with column 1. Change as you see fit.
                ColumnIndexer = 1;
                foreach (string header in headerColumns)
                {
                    cell = new Cell();
                    // I moved it here so it's consistent with the above part
                    // Also, the original code was using the row index to calculate
                    // the column name, which is weird.
                    cell.CellReference = ColumnAddress(ColumnIndexer) + RowIndexer;
                    object value = Utility.GetPropertyValue(item, header);
                    cell.DataType = GetCellDataType(value);
                    if (value != null)
                    {
                        cell.InlineString = new InlineString(new Text(value.ToString()));
                    }
                    row.AppendChild(cell);
                    ColumnIndexer++;
                }
                RowIndexer++;
                sheetdata.Append(row);
            }
            worksheet.Append(sheetdata);
            worksheetPart.Worksheet = worksheet;
        }

        private EnumValue<CellValues> GetCellDataType(object value)
        {
            return CellValues.InlineString;
            //if (value is DateTime)
            //{
            //    return CellValues.Date;
            //}
            //else if (value is sbyte || value is byte || value is short || value is ushort || value is int
            //        || value is uint || value is long || value is ulong || value is float || value is double 
            //        || value is decimal)
            //{
            //    return CellValues.Number;
            //}
            //else if (value is Boolean)
            //{
            //    return CellValues.Boolean;
            //}
            //else
            //{
            //    return CellValues.InlineString;
            //}
        }

        private string[] GetColumnHeaders(Type type)
        {
            return type.GetProperties().Where(pi => pi.PropertyType.Namespace == "System").Select(pi=>pi.Name).ToArray();
        }

        string ColumnAddress(int index)
        {
            index -= 1; //adjust so it matches 0-indexed array rather than 1-indexed column
            int quotient = index / 26;
            if (quotient > 0)
                return ColumnAddress(quotient) + chars[index % 26].ToString();
            else
                return chars[index % 26].ToString();
        }
        private char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();



    }
}
