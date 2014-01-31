using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;

//     CommandAction: A description of what the behavior handles. Example: TextChanged.
//     BehaviorNameCommandBehavior: Replace with the name of the command behavior. Example: TextBoxTextChangedCommandBehavior.
//     ControlType: The type of the control that has the events your command will attach to. Example: TextBox.
//     EventName: The event of your control you will attach to.

namespace XEP_PrismCommandsTelerik
{
   public static class XEP_RowEditEndedCommandTelerik
   {
       private static readonly DependencyProperty XEP_RowEditEndedCommandBehaviorTelerikProperty
           = DependencyProperty.RegisterAttached(
           "XEP_RowEditEndedCommandBehaviorTelerik",
           typeof(XEP_RowEditEndedCommandBehaviorTelerik),
           typeof(XEP_RowEditEndedCommandTelerik),
           null);

       public static readonly DependencyProperty CommandProperty
           = DependencyProperty.RegisterAttached(
           "Command",
           typeof(ICommand),
           typeof(XEP_RowEditEndedCommandTelerik),
           new PropertyMetadata(OnSetCommandCallback));

       public static readonly DependencyProperty CommandParameterProperty
           = DependencyProperty.RegisterAttached(
          "CommandParameter",
          typeof(object),
          typeof(XEP_RowEditEndedCommandTelerik),
          new PropertyMetadata(OnSetCommandParameterCallback));

       public static ICommand GetCommand(RadGridView control)
       {
           return control.GetValue(CommandProperty) as ICommand;
       }
       
        public static void SetCommand(RadGridView control, ICommand command)
        {
            control.SetValue(CommandProperty, command);
        }

       public static void SetCommandParameter(RadGridView control, object parameter)
       {
           control.SetValue(CommandParameterProperty, parameter);
       }

       public static object GetCommandParameter(RadGridView control)
       {
           return control.GetValue(CommandParameterProperty);
       }

       private static void OnSetCommandCallback
           (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
       {
           RadGridView control = dependencyObject as RadGridView;
           if (control != null)
           {
               XEP_RowEditEndedCommandBehaviorTelerik behavior = GetOrCreateBehavior(control);
               behavior.Command = e.NewValue as ICommand;
           }
       }

       private static void OnSetCommandParameterCallback
           (DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
       {
           RadGridView control = dependencyObject as RadGridView;
           if (control != null)
           {
               XEP_RowEditEndedCommandBehaviorTelerik behavior = GetOrCreateBehavior(control);
               behavior.CommandParameter = e.NewValue;
           }
       }

       private static XEP_RowEditEndedCommandBehaviorTelerik GetOrCreateBehavior(RadGridView control)
       {
           XEP_RowEditEndedCommandBehaviorTelerik behavior =
               control.GetValue(XEP_RowEditEndedCommandBehaviorTelerikProperty) as XEP_RowEditEndedCommandBehaviorTelerik;
           if (behavior == null)
           {
               behavior = new XEP_RowEditEndedCommandBehaviorTelerik(control);
               control.SetValue(XEP_RowEditEndedCommandBehaviorTelerikProperty, behavior);
           }
           return behavior;
       }
   }

   public class XEP_RowEditEndedCommandBehaviorTelerik : CommandBehaviorBase<RadGridView>
   {
       public XEP_RowEditEndedCommandBehaviorTelerik(RadGridView control)
           : base(control)
       {
           control.RowEditEnded += new EventHandler<GridViewRowEditEndedEventArgs>(gridView_RowEditEnded);
       }

       private void gridView_RowEditEnded(object sender, GridViewRowEditEndedEventArgs e)
       {
           CommandParameter = e;

           ExecuteCommand();
       }
   }
}
