using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XEP_CommonLibrary.Infrastructure;
using System.ComponentModel.DataAnnotations;
using XEP_SectionCheckCommon.Interfaces;
using XEP_CommonLibrary.Utility;

namespace XEP_SectionCheckCommon.Infrastructure
{
    [Serializable]
    public class XEP_InternalForceItem : ObservableObject
    {
        public XEP_InternalForceItem Self
        {
            get { return this; }
        }
        #region Methods
        public string GetString(XEP_IQuantityManager manager)
        {
            Exceptions.CheckNull(manager);
            StringBuilder builder = new StringBuilder();
            builder.Append(XEP_QuantityNames.GetName(Type));
            builder.Append(";");
            builder.Append(" ");
            //
            builder.Append(manager.GetValue(N));
            builder.Append(";");
            builder.Append(" ");
            builder.Append(manager.GetValue(Vy));
            builder.Append(";");
            builder.Append(" ");
            builder.Append(manager.GetValue(Vz));
            builder.Append(";");
            builder.Append(" ");
            builder.Append(manager.GetValue(Mx));
            builder.Append(";");
            builder.Append(" ");
            builder.Append(manager.GetValue(My));
            builder.Append(";");
            builder.Append(" ");
            builder.Append(manager.GetValue(Mz));
            return builder.ToString();
        }
        #endregion
        /// <summary>
        /// The <see cref="UsedInCheck" /> property's name.
        /// </summary>
        public const string UsedInCheckPropertyName = "UsedInCheck";

        private bool _usedInCheck = false;
        /// <summary>
        /// Sets and gets the UsedInCheck property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool UsedInCheck
        {
            get
            {
                return _usedInCheck;
            }

            set
            {
                if (_usedInCheck == value)
                {
                    return;
                }
                _usedInCheck = value;
                RaisePropertyChanged(UsedInCheckPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _name = "";

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        [Required]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Type" /> property's name.
        /// </summary>
        public const string TypePropertyName = "Type";

        private eEP_ForceItemType _type = eEP_ForceItemType.eULS;

        /// <summary>
        /// Sets and gets the Type property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        /// 
        [Required]
        public eEP_ForceItemType Type
        {
            get
            {
                return _type;
            }

            set
            {
                if (_type == value)
                {
                    return;
                }
                _type = value;
                RaisePropertyChanged(TypePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="N" /> property's name.
        /// </summary>
        public const string NPropertyName = "N";

        private XEP_Quantity _N = new XEP_Quantity(0.0, eEP_QuantityType.eForce);

        /// <summary>
        /// Sets and gets the N property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        /// 
        [Required]
        public XEP_Quantity N
        {
            get
            {
                return _N;
            }

            set
            {
                if (_N == value)
                {
                    return;
                }
                _N = value;
                RaisePropertyChanged(NPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Vy" /> property's name.
        /// </summary>
        public const string VyPropertyName = "Vy";

        private XEP_Quantity _Vy = new XEP_Quantity(0.0, eEP_QuantityType.eForce);

        /// <summary>
        /// Sets and gets the Vy property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_Quantity Vy
        {
            get
            {
                return _Vy;
            }

            set
            {
                if (_Vy == value)
                {
                    return;
                }
                _Vy = value;
                RaisePropertyChanged(VyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Vz" /> property's name.
        /// </summary>
        public const string VzPropertyName = "Vz";

        private XEP_Quantity _Vz = new XEP_Quantity(0.0, eEP_QuantityType.eForce);

        /// <summary>
        /// Sets and gets the Vz property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_Quantity Vz
        {
            get
            {
                return _Vz;
            }

            set
            {
                if (_Vz == value)
                {
                    return;
                }
                _Vz = value;
                RaisePropertyChanged(VzPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Mx" /> property's name.
        /// </summary>
        public const string MxPropertyName = "Mx";

        private XEP_Quantity _Mx = new XEP_Quantity(0.0, eEP_QuantityType.eMoment);

        /// <summary>
        /// Sets and gets the Mx property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_Quantity Mx
        {
            get
            {
                return _Mx;
            }

            set
            {
                if (_Mx == value)
                {
                    return;
                }
                _Mx = value;
                RaisePropertyChanged(MxPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="My" /> property's name.
        /// </summary>
        public const string MyPropertyName = "My";

        private XEP_Quantity _My = new XEP_Quantity(0.0, eEP_QuantityType.eMoment);

        /// <summary>
        /// Sets and gets the My property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_Quantity My
        {
            get
            {
                return _My;
            }

            set
            {
                if (_My == value)
                {
                    return;
                }
                _My = value;
                RaisePropertyChanged(MyPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Mz" /> property's name.
        /// </summary>
        public const string MzPropertyName = "Mz";

        private XEP_Quantity _Mz = new XEP_Quantity(0.0, eEP_QuantityType.eMoment);

        /// <summary>
        /// Sets and gets the Mz property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public XEP_Quantity Mz
        {
            get
            {
                return _Mz;
            }

            set
            {
                if (_Mz == value)
                {
                    return;
                }
                _Mz = value;
                RaisePropertyChanged(MzPropertyName);
            }
        }
    }
}
