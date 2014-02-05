using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Input;
using System.Windows.Controls;

//     #Command_Action#: A description of what the behavior handles. Example: TextChanged.
//     #Behavior_NameCommandBehavior#: Replace with the name of the command behavior. Example: TextBoxTextChangedCommandBehavior.
//     #Control_Type#: The type of the control that has the events your command will attach to. Example: TextBox.
//     #Event_Name#: The event of your control you will attach to.

namespace XEP_PrismCommandsTelerik
{
   public static class XEP_RadComboBox_SelectionChanged
   {
       private static readonly DependencyProperty BehaviorNameCommandBehaviorProperty
           = DependencyProperty.RegisterAttached(
           "RadComboBox_SelectionIndexChangedCommandBehavior",
           typeof(RadComboBox_SelectionIndexChangedCommandBehavior),
           typeof(XEP_RadComboBox_SelectionChanged),
           null);

       public static readonly DependencyProperty CommandProperty
           = DependencyProperty.RegisterAttached(
           "Command",
           typeof(ICommand),
           typeof(XEP_RadComboBox_SelectionChanged),
           new PropertyMetadata(OnSetCommandCallback));

       public static readonly DependencyProperty CommandParameterProperty
           = DependencyProperty.RegisterAttached(
          "CommandParameter",
          typeof(object),
          typeof(XEP_RadComboBox_SelectionChanged),
          new PropertyMetadata(OnSetCommandParameterCallback));

       public static ICommand GetCommand(Telerik.Windows.Controls.RadComboBox control)
       {
           return control.GetValue(CommandProperty) as ICommand;
       }
       
        public static void SetCommand(Telerik.Windows.Controls.RadComboBox control, ICommand command)
        {
            control.SetValue(CommandProperty, command);
        }

       public static void SetCommandParameter(Telerik.Windows.Controls.RadComboBox control, object parameter)
       {
           control.SetValue(CommandParameterProperty, parameter);
       }

       public static object GetCommandParameter(Telerik.Windows.Controls.RadComboBox control)
       {
           return control.GetValue(CommandParameterProperty);
       }

       private static void OnSetCommandCallback
           (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
       {
           Telerik.Windows.Controls.RadComboBox control = dependencyObject as Telerik.Windows.Controls.RadComboBox;
           if (control != null)
           {
               RadComboBox_SelectionIndexChangedCommandBehavior behavior = GetOrCreateBehavior(control);
               behavior.Command = e.NewValue as ICommand;
           }
       }

       private static void OnSetCommandParameterCallback
           (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
       {
           Telerik.Windows.Controls.RadComboBox control = dependencyObject as Telerik.Windows.Controls.RadComboBox;
           if (control != null)
           {
               RadComboBox_SelectionIndexChangedCommandBehavior behavior = GetOrCreateBehavior(control);
               behavior.CommandParameter = e.NewValue;
           }
       }

       private static RadComboBox_SelectionIndexChangedCommandBehavior GetOrCreateBehavior(Telerik.Windows.Controls.RadComboBox control)
       {
           RadComboBox_SelectionIndexChangedCommandBehavior behavior =
               control.GetValue(BehaviorNameCommandBehaviorProperty) as RadComboBox_SelectionIndexChangedCommandBehavior;
           if (behavior == null)
           {
               behavior = new RadComboBox_SelectionIndexChangedCommandBehavior(control);
               control.SetValue(BehaviorNameCommandBehaviorProperty, behavior);
           }
           return behavior;
       }
   }

   public class RadComboBox_SelectionIndexChangedCommandBehavior : CommandBehaviorBase<Telerik.Windows.Controls.RadComboBox>
   {
       public RadComboBox_SelectionIndexChangedCommandBehavior(Telerik.Windows.Controls.RadComboBox control)
           : base(control)
       {
           control.SelectionChanged += new SelectionChangedEventHandler((o, e) => radComboBox_SelectionChanged(o,e));
       }

       private void radComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
           CommandParameter = e;

           ExecuteCommand();
       }
   }
}
