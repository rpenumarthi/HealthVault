using System;
using System.Data;
using System.Configuration;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Microsoft.Health;
using Microsoft.Health.Web;
using Microsoft.Health.ItemTypes;

public partial class HelloWorldPage : HealthServicePage 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            AddRandomHeightEntry();
        }

        c_UserName.Text = PersonInfo.Name;

        Basic basic = GetSingleValue<Basic>(Basic.TypeId);
        if (!ReferenceEquals(basic, null) && basic.BirthYear.HasValue)
        {
            c_BirthYear.Text = basic.BirthYear.ToString();
        }

        PopulateHeightTable();
    }

    void PopulateHeightTable()
    {
        c_HeightTable.Rows.Clear();
        TableRow headerRow = new TableRow();
        TableHeaderCell headerDateCell = new TableHeaderCell();
        headerDateCell.Text = "Date";
        headerRow.Cells.Add(headerDateCell);

        TableHeaderCell headerHeightCell = new TableHeaderCell();
        headerHeightCell.Text = "Height";
        headerRow.Cells.Add(headerHeightCell);

        c_HeightTable.Rows.Add(headerRow);

        List<Height> heightMeasurements = GetValues<Height>(Height.TypeId);

        foreach (Height height in heightMeasurements)
        {
            TableRow row = new TableRow();
            c_HeightTable.Rows.Add(row);

            TableCell dateCell = new TableCell();
            dateCell.Text = height.When.ToString();
            row.Cells.Add(dateCell);

            TableCell heightCell = new TableCell();
            heightCell.Text = String.Format("{0:F2}", height.Value.Meters);
            row.Cells.Add(heightCell);
        }
    }

    void AddRandomHeightEntry()
    {
        Random random = new Random();

        double meters = random.NextDouble() * 0.5 + 1.5;

        Length value = new Length(meters);
        Height height = new Height(new HealthServiceDateTime(DateTime.Now), value);

        PersonInfo.SelectedRecord.NewItem(height);
    }

    T GetSingleValue<T>(Guid typeID) where T:class
    {
        HealthRecordSearcher searcher = PersonInfo.SelectedRecord.CreateSearcher();

        HealthRecordFilter filter = new HealthRecordFilter(typeID);
        searcher.Filters.Add(filter);

        HealthRecordItemCollection items = searcher.GetMatchingItems()[0];

        if (!ReferenceEquals(items, null) && items.Count > 0)
        {
            return items[0] as T;
        }
        else
        {
            return null;
        }
    }

    List<T> GetValues<T>(Guid typeID) where T : HealthRecordItem
    {
        HealthRecordSearcher searcher = PersonInfo.SelectedRecord.CreateSearcher();

        HealthRecordFilter filter = new HealthRecordFilter(typeID);
        searcher.Filters.Add(filter);

        HealthRecordItemCollection items = searcher.GetMatchingItems()[0];

        List<T> typedList = new List<T>();

        foreach (HealthRecordItem item in items)
        {
            typedList.Add((T) item);
        }

        return typedList;
    }

}
