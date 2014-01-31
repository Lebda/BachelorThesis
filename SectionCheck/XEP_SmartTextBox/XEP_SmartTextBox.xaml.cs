using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace XEP_SmartTextBox
{
    /// <summary>
    /// Interaction logic for XEP_SmartTextBox.xaml
    /// </summary>
    public partial class XEP_SmartTextBox : UserControl
    {
        string[] _sepratorInternal = {"&"};
        private string[] SepratorInternal
        {
            get { return _sepratorInternal; }
        }
        string[] _separatorsAll = new string[3];
        private string[] SeparatorsAll
        {
            get
            {
                return _separatorsAll;
            }
            set
            {
                _separatorsAll = value;
            }
        }

        public XEP_SmartTextBox()
        {
            InitializeComponent();
            _separatorsAll[0] = NormalScriptMark;
            _separatorsAll[1] = SubscriptMark;
            _separatorsAll[2] = SuperScriptMark;
        }

        static XEP_SmartTextBox()
        {
            OnlyTextProperty = DependencyProperty.Register(OnlyTextPropertyName, typeof(bool), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnlyTextChanged)));
            SmartTextProperty = DependencyProperty.Register(SmartTextPropertyName, typeof(string), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnSmartTextChanged)));
            SubscriptMarkProperty = DependencyProperty.Register(SubscriptMarkPropertyName, typeof(string), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata("@", new PropertyChangedCallback(OnSubscriptMarkChanged)));
            SuperScriptMarkProperty = DependencyProperty.Register(SuperScriptMarkPropertyName, typeof(string), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata("$", new PropertyChangedCallback(OnSuperScriptMarkChanged)));
            NormalScriptMarkProperty = DependencyProperty.Register(NormalScriptMarkPropertyName, typeof(string), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata("#", new PropertyChangedCallback(OnNormalScriptMarkChanged)));
            System.Windows.Media.Brush color = System.Windows.Media.Brushes.DeepSkyBlue;
            SmartColorProperty = DependencyProperty.Register(SmartColorPropertyName, typeof(System.Windows.Media.Brush), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata(color, new PropertyChangedCallback(OnSmartColorChanged)));
            SupSubSciptProperty = DependencyProperty.Register(SupSubSciptPropertyName, typeof(bool), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnSupSubSciptChanged)));
        }
  
        private static string SupSubSciptPropertyName = "SupSubScipt";
        public static DependencyProperty SupSubSciptProperty;
        public bool SupSubScipt
        {
            get { return (bool)GetValue(SupSubSciptProperty); }
            set { SetValue(SupSubSciptProperty, value); }
        }

        private static string OnlyTextPropertyName = "OnlyText";
        public static DependencyProperty OnlyTextProperty;
        public bool OnlyText
        {
            get { return (bool)GetValue(OnlyTextProperty); }
            set { SetValue(OnlyTextProperty, value); }
        }

        private static string SmartColorPropertyName = "SmartColor";
        public static DependencyProperty SmartColorProperty;

        public System.Windows.Media.Brush SmartColor
        {
            get
            {
                return (System.Windows.Media.Brush)GetValue(SmartColorProperty);
            }
            set
            {
                SetValue(SmartColorProperty, value);
            }
        }

        private static string SmartTextPropertyName = "SmartText";
        public static DependencyProperty SmartTextProperty;

        public string SmartText
        {
            get
            {
                return (string)GetValue(SmartTextProperty);
            }
            set
            {
                SetValue(SmartTextProperty, value);
            }
        }

        private static string SubscriptMarkPropertyName = "SubscriptMark";
        public static DependencyProperty SubscriptMarkProperty;

        public string SubscriptMark
        {
            get
            {
                return (string)GetValue(SubscriptMarkProperty);
            }
            set
            {
                SetValue(SubscriptMarkProperty, value);
            }
        }

        private static string SuperScriptMarkPropertyName = "SuperScriptMark";
        public static DependencyProperty SuperScriptMarkProperty;

        public string SuperScriptMark
        {
            get
            {
                return (string)GetValue(SuperScriptMarkProperty);
            }
            set
            {
                SetValue(SuperScriptMarkProperty, value);
            }
        }

        private static string NormalScriptMarkPropertyName = "NormalScriptMark";
        public static DependencyProperty NormalScriptMarkProperty;

        public string NormalScriptMark
        {
            get
            {
                return (string)GetValue(NormalScriptMarkProperty);
            }
            set
            {
                SetValue(NormalScriptMarkProperty, value);
            }
        }

        private static void OnSupSubSciptChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            XEP_SmartTextBox smartTextBox = (XEP_SmartTextBox)sender;
            if ((bool)e.OldValue != (bool)e.NewValue)
            {
                smartTextBox.SupSubScipt = (bool)e.NewValue;
                smartTextBox.OnSmartTextChangedInternal();
            }
        }

        private static void OnlyTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            XEP_SmartTextBox smartTextBox = (XEP_SmartTextBox)sender;
            if ((bool)e.OldValue != (bool)e.NewValue)
            {
                smartTextBox.OnlyText = (bool)e.NewValue;
                smartTextBox.OnSmartTextChangedInternal();
            }
        }

        private static void OnSmartColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            XEP_SmartTextBox smartTextBox = (XEP_SmartTextBox)sender;
            if ((System.Windows.Media.Brush)e.OldValue != (System.Windows.Media.Brush)e.NewValue)
            {
                smartTextBox.SmartColor = (System.Windows.Media.Brush)e.NewValue;
                smartTextBox.OnSmartTextChangedInternal();
            }
        }

        private static void OnSmartTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            XEP_SmartTextBox smartTextBox = (XEP_SmartTextBox)sender;
            if ((string)e.OldValue != (string)e.NewValue)
            {
                smartTextBox.SmartText = (string)e.NewValue;
                smartTextBox.OnSmartTextChangedInternal();
            }
        }

        private static void OnSubscriptMarkChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            XEP_SmartTextBox smartTextBox = (XEP_SmartTextBox)sender;
            if ((string)e.NewValue == smartTextBox.SepratorInternal[0])
            {
                throw new ArgumentException(String.Format("Mark con not be set on {0} !!", smartTextBox.SepratorInternal[0]));
            }
            if ((string)e.OldValue != (string)e.NewValue)
            {
                smartTextBox.SubscriptMark = (string)e.NewValue;
                smartTextBox.SeparatorsAll[1] = smartTextBox.SubscriptMark;
                smartTextBox.OnSmartTextChangedInternal();
            }
        }

        private static void OnSuperScriptMarkChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            XEP_SmartTextBox smartTextBox = (XEP_SmartTextBox)sender;
            if ((string)e.NewValue == smartTextBox.SepratorInternal[0])
            {
                throw new ArgumentException(String.Format("Mark con not be set on {0} !!", smartTextBox.SepratorInternal[0]));
            }
            if ((string)e.OldValue != (string)e.NewValue)
            {
                smartTextBox.SuperScriptMark = (string)e.NewValue;
                smartTextBox.SeparatorsAll[2] = smartTextBox.SuperScriptMark;
                smartTextBox.OnSmartTextChangedInternal();
            }
        }

        private static void OnNormalScriptMarkChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            XEP_SmartTextBox smartTextBox = (XEP_SmartTextBox)sender;
            if ((string)e.NewValue == smartTextBox.SepratorInternal[0])
            {
                throw new ArgumentException(String.Format("Mark con not be set on {0} !!", smartTextBox.SepratorInternal[0]));
            }
            if ((string)e.OldValue != (string)e.NewValue)
            {
                smartTextBox.NormalScriptMark = (string)e.NewValue;
                smartTextBox.SeparatorsAll[0] = smartTextBox.NormalScriptMark;
                smartTextBox.OnSmartTextChangedInternal();
            }
        }

        private void OnSmartTextChangedInternal()
        {
            if (OnlyText)
            {
                this.richTextBox.Visibility = Visibility.Collapsed;
                this.MyTextBox.Visibility = Visibility.Visible;
                this.MyTextBox.Foreground = SmartColor;
                this.MyTextBox.Text = SmartText;
                return;
            }
            else
            {
                this.richTextBox.Visibility = Visibility.Visible;
                this.MyTextBox.Visibility = Visibility.Collapsed;
            }
            if (SmartText == null || SmartText == String.Empty)
            {
                return;
            }
            System.Windows.Documents.Paragraph myParagraph = new System.Windows.Documents.Paragraph();
            FontFamilyConverter ffc = new FontFamilyConverter();
            myParagraph.FontFamily = (FontFamily)ffc.ConvertFromString("Palatino Linotype");
            if (SupSubScipt == false)
            {
                Bold myBold = new Bold(new Run(SmartText));
                myParagraph.Inlines.Add(myBold);
            }
            else
            {
                string value = SmartText;
                foreach (var word in _separatorsAll)
                {
                    value = value.Replace(word, _sepratorInternal[0] + word);
                }
                string[] wordsAll = value.Split(_sepratorInternal, StringSplitOptions.RemoveEmptyEntries);
                for (int counter = 0; counter < wordsAll.Count(); ++counter)
                {
                    string word = wordsAll[counter];
                    FontVariants alig = FontVariants.Normal;
                    if (word.Contains(_separatorsAll[1]))
                    {
                        alig = FontVariants.Subscript;
                        word = word.Substring(1);
                    }
                    else if (word.Contains(_separatorsAll[2]))
                    {
                        alig = FontVariants.Superscript;
                        word = word.Substring(1);
                    }
                    else if (word.Contains(_separatorsAll[0]))
                    {
                        word = word.Substring(1);
                    }
                    Bold myBold = new Bold(new Run(word));
                    myBold.Typography.Variants = alig;
                    myParagraph.Inlines.Add(myBold);
                }
            }
            FlowDocument myFlowDoc = new FlowDocument();
            myFlowDoc.Blocks.Add(myParagraph);
            this.richTextBox.Foreground = SmartColor;
            this.richTextBox.Document = myFlowDoc;
        }
    }
}