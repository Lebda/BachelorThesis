using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Telerik.Windows.Documents.Model;
using XEP_CommonLibrary.Infrastructure;

namespace XEP_SmartTextBox
{
    /// <summary>
    /// Interaction logic for XEP_SmartTextBox.xaml
    /// </summary>
    public partial class XEP_SmartTextBox : UserControl
    {
        string[] _sepratorInternal = { "&" };

        private string[] SepratorInternal
        {
            get
            {
                return _sepratorInternal;
            }
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

        FontFamily _fontFamily;

        public XEP_SmartTextBox()
        {
            InitializeComponent();
            _separatorsAll[0] = NormalScriptMark;
            _separatorsAll[1] = SubscriptMark;
            _separatorsAll[2] = SuperScriptMark;
            FontFamilyConverter ffc = new FontFamilyConverter();
            _fontFamily = (FontFamily)ffc.ConvertFromString("Palatino Linotype");
            OnSmartTextChangedInternal();
            myRichTextBox.TextChanged += new TextChangedEventHandler((o, e) => myRichTextBox.Width = XEP_FlowDocumentExtensions.GetFormattedText(myRichTextBox.Document).WidthIncludingTrailingWhitespace + 20);
        }

        static XEP_SmartTextBox()
        {
            IsOnlyTextBoxProperty = DependencyProperty.Register(IsOnlyTextBoxPropertyName, typeof(bool), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsOnlyTextBoxChanged)));
            SmartTextProperty = DependencyProperty.Register(SmartTextPropertyName, typeof(string), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnSmartTextChanged)));
            SubscriptMarkProperty = DependencyProperty.Register(SubscriptMarkPropertyName, typeof(string), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata("@", new PropertyChangedCallback(OnSubscriptMarkChanged)));
            SuperScriptMarkProperty = DependencyProperty.Register(SuperScriptMarkPropertyName, typeof(string), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata("$", new PropertyChangedCallback(OnSuperScriptMarkChanged)));
            NormalScriptMarkProperty = DependencyProperty.Register(NormalScriptMarkPropertyName, typeof(string), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata("#", new PropertyChangedCallback(OnNormalScriptMarkChanged)));
            SupSubSciptProperty = DependencyProperty.Register(SupSubSciptPropertyName, typeof(bool), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnSupSubSciptChanged)));
            IsTextReadOnlyProperty = DependencyProperty.Register(IsTextReadOnlyPropertyName, typeof(bool), typeof(XEP_SmartTextBox),
                new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnIsTextReadOnlyChanged)));
        }

        private static string IsTextReadOnlyPropertyName = "IsTextReadOnly";
        public static DependencyProperty IsTextReadOnlyProperty;

        public bool IsTextReadOnly
        {
            get
            {
                return (bool)GetValue(IsTextReadOnlyProperty);
            }
            set
            {
                SetValue(IsTextReadOnlyProperty, value);
            }
        }

        private static string SupSubSciptPropertyName = "SupSubScipt";
        public static DependencyProperty SupSubSciptProperty;

        public bool SupSubScipt
        {
            get
            {
                return (bool)GetValue(SupSubSciptProperty);
            }
            set
            {
                SetValue(SupSubSciptProperty, value);
            }
        }

        private static string IsOnlyTextBoxPropertyName = "IsOnlyTextBox";
        public static DependencyProperty IsOnlyTextBoxProperty;

        public bool IsOnlyTextBox
        {
            get
            {
                return (bool)GetValue(IsOnlyTextBoxProperty);
            }
            set
            {
                SetValue(IsOnlyTextBoxProperty, value);
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

        private static void OnIsTextReadOnlyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            XEP_SmartTextBox smartTextBox = (XEP_SmartTextBox)sender;
            if ((bool)e.OldValue != (bool)e.NewValue)
            {
                smartTextBox.IsTextReadOnly = (bool)e.NewValue;
                smartTextBox.OnSmartTextChangedInternal();
            }
        }

        private static void OnIsOnlyTextBoxChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            XEP_SmartTextBox smartTextBox = (XEP_SmartTextBox)sender;
            if ((bool)e.OldValue != (bool)e.NewValue)
            {
                smartTextBox.IsOnlyTextBox = (bool)e.NewValue;
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
                throw new ArgumentException(String.Format("Mark can not be set on {0} !!", smartTextBox.SepratorInternal[0]));
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
            this.MyTextBox.IsReadOnly = IsTextReadOnly;
            this.myRichTextBox.IsReadOnly = IsTextReadOnly;
            this.MyTextBox.Text = SmartText;
            if (IsOnlyTextBox)
            {
                this.myRichTextBox.Visibility = Visibility.Collapsed;
                this.MyTextBox.Visibility = Visibility.Visible;
                this.MyTextBox.FontFamily = _fontFamily;
                return;
            }
            else
            {
                this.myRichTextBox.Visibility = Visibility.Visible;
                this.MyTextBox.Visibility = Visibility.Collapsed;
            }
            if (SmartText == null || SmartText == String.Empty)
            {
                return;
            }
            System.Windows.Documents.Paragraph myParagraph = new System.Windows.Documents.Paragraph();
            myParagraph.FontFamily = _fontFamily;
            if (SupSubScipt == false)
            {
                Run myBold = new Run(SmartText);
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
                    Run myBold = new Run(word);
                    myBold.Typography.Variants = alig;
                    myParagraph.Inlines.Add(myBold);
                }
            }
            FlowDocument rtbFlowDoc = new FlowDocument();
            rtbFlowDoc.Blocks.Add(myParagraph);
            rtbFlowDoc.TextAlignment = TextAlignment.Left;
            this.myRichTextBox.Document = rtbFlowDoc;
        }
    }
}