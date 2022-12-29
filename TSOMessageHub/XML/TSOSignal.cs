namespace TSOMessageHub.XML;

// NOTE: Auto-Generated code using visual studio from XML format
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class TSOSignal
{

    private int _signalIdField;

    private string _bidIdField = String.Empty;

    private string _receivedUTCField = String.Empty;

    private decimal _quantityMwField;

    /// <remarks/>
    public int SignalId
    {
        get
        {
            return this._signalIdField;
        }
        set
        {
            this._signalIdField = value;
        }
    }

    /// <remarks/>
    public string BidId
    {
        get
        {
            return this._bidIdField;
        }
        set
        {
            this._bidIdField = value;
        }
    }

    /// <remarks/>
    public string ReceivedUTC
    {
        get
        {
            return this._receivedUTCField;
        }
        set
        {
            this._receivedUTCField = value;
        }
    }

    /// <remarks/>
    public decimal QuantityMw
    {
        get
        {
            return this._quantityMwField;
        }
        set
        {
            this._quantityMwField = value;
        }
    }
}

