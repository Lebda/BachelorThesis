using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using XEP_CommonLibrary.Infrastructure;
using XEP_SectionCheckInterfaces.DataCache;
using XEP_CommonLibrary.Utility;

namespace XEP_SmartComboBox
{
    /// <summary>
    /// Interaction logic for XEP_SmartComboBox.xaml
    /// </summary>
    public partial class XEP_SmartComboBox : UserControl
    {
        public XEP_SmartComboBox()
        {
            InitializeComponent();
            OnSmartComboBoxChangedInternal();
            _mySmartComboBox.SelectionChanged += new SelectionChangedEventHandler((o, e) => { OnSelectionChangedChanged(o, e);});

        }
        private void OnSelectionChangedChanged(object sender, SelectionChangedEventArgs e)
        {
            RadComboBox myComboBox = sender as RadComboBox;
            if (myComboBox == null || Quantity_XEP == null || Quantity_XEP.Enum2StringManager == null || e == null || e.AddedItems == null || e.AddedItems.Count == 0)
            {
                return;
            }
            if (myComboBox.SelectedIndex < 0 || ManagedValue_XEP == (double)myComboBox.SelectedIndex)
            {
                return;
            }
            if (myComboBox.SelectedIndex != Quantity_XEP.Enum2StringManager.GetValue(Quantity_XEP.EnumName, myComboBox.Text))
            { // for being sure
                Exceptions.CheckNull(null);
                return;
            }
            ManagedValue_XEP = myComboBox.SelectedIndex;
        }
        static XEP_SmartComboBox()
        {
            ManagedValue_XEPProperty = DependencyProperty.Register(ManagedValue_XEPPropertyName, typeof(double), typeof(XEP_SmartComboBox),
                new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(OnManagedValueChanged)));
            Quantity_XEPProperty = DependencyProperty.Register(Quantity_XEPPropertyName, typeof(XEP_IQuantity), typeof(XEP_SmartComboBox),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnQuantityChanged)));
            IsReadOnly_XEPProperty = DependencyProperty.Register(IsReadOnly_XEPPropertyName, typeof(bool), typeof(XEP_SmartComboBox),
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsReadOnlyChanged)));
            IsEnabled_XEPProperty = DependencyProperty.Register(IsEnabled_XEPPropertyName, typeof(bool), typeof(XEP_SmartComboBox),
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsEnabledChanged)));
        }
        private static string IsEnabled_XEPPropertyName = "IsEnabled_XEP";
        public static DependencyProperty IsEnabled_XEPProperty;
        public bool IsEnabled_XEP
        {
            get { return (bool)GetValue(IsEnabled_XEPProperty); }
            set { SetValue(IsEnabled_XEPProperty, value); }

        }
        private static string IsReadOnly_XEPPropertyName = "IsReadOnly_XEP";
        public static DependencyProperty IsReadOnly_XEPProperty;
        public bool IsReadOnly_XEP
        {
            get { return (bool)GetValue(IsReadOnly_XEPProperty); }
            set { SetValue(IsReadOnly_XEPProperty, value); }

        }
        private static string ManagedValue_XEPPropertyName = "ManagedValue_XEP";
        public static DependencyProperty ManagedValue_XEPProperty;
        public double ManagedValue_XEP
        {
            get { return (double)GetValue(ManagedValue_XEPProperty); }
            set { SetValue(ManagedValue_XEPProperty, value); }

        }
        private static string Quantity_XEPPropertyName = "Quantity_XEP";
        public static DependencyProperty Quantity_XEPProperty;
        public XEP_IQuantity Quantity_XEP
        {
            get { return (XEP_IQuantity)GetValue(Quantity_XEPProperty); }
            set { SetValue(Quantity_XEPProperty, value); }

        }
        private static void OnIsEnabledChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PropertySupport.OnMyPropertyChanged<XEP_SmartComboBox, bool>((XEP_SmartComboBox)sender, e, s => { ((XEP_SmartComboBox)sender).IsEnabled_XEP = s; }, ((XEP_SmartComboBox)sender).OnSmartComboBoxChangedInternal);
        }
        private static void OnIsReadOnlyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PropertySupport.OnMyPropertyChanged<XEP_SmartComboBox, bool>((XEP_SmartComboBox)sender, e, s => { ((XEP_SmartComboBox)sender).IsReadOnly_XEP = s; }, ((XEP_SmartComboBox)sender).OnSmartComboBoxChangedInternal);
        }
        private static void OnManagedValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PropertySupport.OnMyPropertyChanged<XEP_SmartComboBox, double>((XEP_SmartComboBox)sender, e, s => { ((XEP_SmartComboBox)sender).ManagedValue_XEP = s; }, ((XEP_SmartComboBox)sender).OnSmartComboBoxChangedInternal);
        }
        private static void OnQuantityChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            PropertySupport.OnMyPropertyChanged<XEP_SmartComboBox, XEP_IQuantity>((XEP_SmartComboBox)sender, e, s => { ((XEP_SmartComboBox)sender).Quantity_XEP = s; }, ((XEP_SmartComboBox)sender).OnSmartComboBoxChangedInternal);
        }
        private void OnSmartComboBoxChangedInternal()
        {
            _mySmartComboBox.Items.Clear();
            _mySmartComboBox.IsReadOnly = IsReadOnly_XEP;
            _mySmartComboBox.IsEnabled = IsEnabled_XEP;
            if (Quantity_XEP == null || Quantity_XEP.Enum2StringManager == null || Quantity_XEP.EnumName == null)
            {
                return;
            }
            string[] names = Quantity_XEP.Enum2StringManager.GetNames(Quantity_XEP.EnumName);
            if (names != null)
            {
                foreach (string item in names)
                {
                    RadComboBoxItem comboBoxItem = new RadComboBoxItem();
                    comboBoxItem.Content = item;
                    _mySmartComboBox.Items.Add( comboBoxItem );
                }
            }
            _mySmartComboBox.SelectedIndex = (Int32)Quantity_XEP.ManagedValue;
        }
    }
}
