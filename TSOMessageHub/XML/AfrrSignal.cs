using System;
namespace TSOMessageHub.XML
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class AfrrSignal
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
}

